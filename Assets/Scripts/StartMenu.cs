using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Rigidbody rb =  GetComponent<Game>().playerObj.GetComponent<Rigidbody>();
            rb.velocity *= 0.6f;
        }   
    }
}
