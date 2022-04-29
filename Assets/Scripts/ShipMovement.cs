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
    private Rigidbody rb;
    public GameObject gravity;
    public float gravityConstant = 1.0f;

    public GameObject explosion1;
    public GameObject finishAnimation;

    public float fixedDeltaTime; 
    void Start()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,1);
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

    // This
    void FixedUpdate()
    {
        Vector3 force = Vector3.zero;

        for(int i = 0; i < gravity.transform.childCount; i++)
        {
            Transform obj = gravity.transform.GetChild(i);
            Vector3 distanceVector = obj.position - transform.position;

            float distance = distanceVector.magnitude;
            Vector3 direction = distanceVector.normalized; 
            force += Mathf.Pow(obj.transform.localScale.x,2) * direction * gravityConstant * 1 / (distance * distance);

            
        }
        rb.AddForce(force);
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    // This function checks for for Win / Lose conditions
    IEnumerator OnTriggerEnter(Collider other) {
        if (other.CompareTag("Finish"))
        { 
            finishAnimation.SetActive(true);
            finishAnimation.GetComponent<ParticleSystem>().Play();
            other.GetComponent<MeshRenderer>().enabled = false;

            // Change to the next scene
            string currentLevelName = SceneManager.GetActiveScene().name;
            string currentLevelNum = currentLevelName.Substring(currentLevelName.Length-1, 1);
            int number = int.Parse("1");
            Debug.Log("name "+ number.ToString());
            string newLevelName = string.Format("Level {0}", number+1);
            yield return new WaitForSeconds(0.7f);
            SceneManager.LoadScene(newLevelName);
        }

        else if (other.CompareTag("Obstacle"))
        {
            Vector3 pos = other.transform.position;
            Instantiate(explosion1, pos, Quaternion.identity);
            //other.GetComponent<Fracture>().FractureObject();
            rb.velocity = rb.velocity/4;
            yield return new WaitForSeconds(0.7f);

            Debug.Log("You Lose"); 
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
        Debug.Log("Destroyed");
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = this.fixedDeltaTime;
    }
}
