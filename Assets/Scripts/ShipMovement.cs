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

    // Update is called once per frame
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
        // Debug.Log("velx = "+rb.velocity.x + " vely = "+rb.velocity.z +" theta = "+theta);
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    IEnumerator OnTriggerEnter(Collider other) {
        if (other.CompareTag("Finish"))
        { 
            finishAnimation.SetActive(true);
            finishAnimation.GetComponent<ParticleSystem>().Play();
            other.GetComponent<MeshRenderer>().enabled = false;

            Debug.Log("You win");
        }

        else if (other.CompareTag("Obstacle"))
        {
            Vector3 pos = other.transform.position;
            Instantiate(explosion1, pos, Quaternion.identity);
            //other.GetComponent<Fracture>().FractureObject();
            rb.velocity = rb.velocity/4;
            yield return new WaitForSeconds(0.7f);

            Debug.Log("You Lose"); 
            // TODO : update the scene name here 
            SceneManager.LoadScene("SampleScene"); 
        }
    }

    void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }
}
