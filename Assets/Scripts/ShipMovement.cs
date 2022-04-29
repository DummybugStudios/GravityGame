/*
*   This script is responsible for a lot of game mechanics
*   It changes time scale, checks for win / lose conditions
*   And moves the spaceship according to the blackholes
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [System.NonSerialized]
    public Rigidbody rb;
    public GameObject gravity;
    public float gravityConstant = 1.0f;

    public GameObject explosion1;
    float fixedDeltaTime; 
    int totalPlanets;
    int planetsVisited = 0;
    void Start()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,1);
        totalPlanets = GameObject.FindGameObjectsWithTag("Finish").Length;
    }

    // changes timescale when space is pressed
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 5.0f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale; 
        }
        

    }

    public Vector3 GetForceAtPointFromBlackhole(Vector3 position, Transform blackhole)
    {
        Vector3 distanceVector = blackhole.position - position;
        float distance = distanceVector.magnitude;
        Vector3 direction = distanceVector.normalized; 
        return Mathf.Pow(blackhole.localScale.x,2) * direction * gravityConstant * 1 / (distance * distance);
    }

    // Get Force from blackholes at any point on the map
    public Vector3 GetForceAtPoint(Vector3 position){
        Vector3 force = Vector3.zero;
        for(int i = 0; i < gravity.transform.childCount; i++)
        {
            Transform obj = gravity.transform.GetChild(i);
            force += GetForceAtPointFromBlackhole(position, obj);
        }
        return force;
    }
    // This
    void FixedUpdate()
    {
        rb.AddForce(GetForceAtPoint(transform.position));
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    // This function checks for for Win / Lose conditions
    IEnumerator OnTriggerEnter(Collider other) {
        if (other.CompareTag("Finish"))
        { 
            ParticleSystem planetParticles = other.GetComponentInChildren<ParticleSystem>(true);
            GameObject planet = planetParticles.gameObject;
            planet.SetActive(true);
            planetParticles.Play();
            other.GetComponent<MeshRenderer>().enabled = false;

            // let the animation play before continuing
            yield return new WaitForSeconds(0.7f);
            // Destroy Object so that future collisions do not happen.
            // Destruction after wait so that animation can be played
            Destroy(other.gameObject);
            
            // Don't do anyhthing if all planets aren't visited;
            if (++planetsVisited == totalPlanets)
            {
                // Change to the next scene if all planets visited
                string currentLevelName = SceneManager.GetActiveScene().name;
                string currentLevelNum = currentLevelName.Substring(currentLevelName.Length-1, 1);
                int number = int.Parse(currentLevelNum);
                string newLevelName = string.Format("Level {0}", number+1);
                SceneManager.LoadScene(newLevelName);
            }
        }

        else if (other.CompareTag("Obstacle"))
        {
            Vector3 pos = other.transform.position;
            Instantiate(explosion1, pos, Quaternion.identity);
            rb.velocity = rb.velocity/4;
            yield return new WaitForSeconds(0.7f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }

    // Also detect collisions if inside the Object (?)
    void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }

    // Reset time scale and delta time when restarting.
    void OnDestroy()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = this.fixedDeltaTime;
    }
}
