using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public GameObject gravity;
    public float gravityConstant = 1.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 force = Vector3.zero;

        for(int i = 0; i < gravity.transform.childCount; i++)
        {
            Transform obj = gravity.transform.GetChild(i);
            Vector3 distanceVector = obj.position - transform.position;

            float distance = distanceVector.magnitude;
            Vector3 direction = distanceVector.normalized; 
            force += direction * gravityConstant * 1 / (distance * distance);

            
        }
        rb.AddForce(force);
        // Debug.Log("velx = "+rb.velocity.x + " vely = "+rb.velocity.z +" theta = "+theta);
        transform.rotation = Quaternion.LookRotation(rb.velocity);

    }
}
