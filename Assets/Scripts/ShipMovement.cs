/*
*   This script is responsible for a lot of game mechanics
*   It changes time scale, checks for win / lose conditions
*   And moves the spaceship according to the blackholes
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

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

    public Vector2 startSpeed = new Vector2(0,1);

    GameObject postProcessing; 
    void Start()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(startSpeed.x,0,startSpeed.y);
        totalPlanets = GameObject.FindGameObjectsWithTag("Finish").Length;
        postProcessing = FindObjectOfType<PostProcessVolume>(true).gameObject;
    }

    // changes timescale when space is pressed
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            postProcessing.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 5.0f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale; 
            postProcessing.SetActive(true);
        }
        

    }

    public Vector3 GetForceAtPointFromBlackhole(Vector3 position, Transform blackhole)
    {
        Vector3 distanceVector = blackhole.position - position;
        float distance = distanceVector.magnitude;
        if (distance < blackhole.GetComponent<SphereCollider>().radius)
        {
            return Vector3.positiveInfinity;
        }

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
        Vector3 force = GetForceAtPoint(transform.position);
        if (float.IsInfinity(force.magnitude))
        {
            force = Vector3.zero;
        }
        rb.AddForce(force);
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }


    bool waiting = false;
    IEnumerator CheckCollision(Collider other){
        if(waiting) yield break;
         if (other.CompareTag("Finish"))
        { 
            // Add force to the ship
            rb.AddForce(rb.velocity.normalized*1.2f, ForceMode.VelocityChange);

            // Play particle effects
            ParticleSystem[] planetParticles = other.GetComponentsInChildren<ParticleSystem>(true);
            ParticleSystem halo = planetParticles[0];
            ParticleSystem hit = planetParticles[1]; 

            halo.Pause();
            halo.Clear();
            GameObject planet = hit.gameObject;
            planet.SetActive(true);
            hit.Play();
            other.GetComponent<MeshRenderer>().enabled = false;

            // let the animation play before continuing
            waiting = true;
            yield return new WaitForSeconds(0.7f);
            waiting = false;
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
            GameObject explosion = Instantiate(explosion1, transform.position, Quaternion.identity);
            explosion.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
            rb.velocity = rb.velocity/4;
            waiting = true;
            yield return new WaitForSeconds(0.3f);
            waiting = false;
            if (SceneManager.GetActiveScene().name == "Start" || SceneManager.GetActiveScene().name=="Level 7")
            {
                SceneManager.LoadScene("Level 1"); 
            }
            else{
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    // This function checks for for Win / Lose conditions
    IEnumerator OnTriggerEnter(Collider other) {
        yield return CheckCollision(other); 
    }

    // Also detect collisions if inside the Object (?)
    IEnumerator OnTriggerStay(Collider other)
    {
        yield return CheckCollision(other); 
    }

    // Reset time scale and delta time when restarting.
    void OnDestroy()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = this.fixedDeltaTime;
    }
}
