using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    ShipMovement movement;
    void Start()
    {
        movement = FindObjectOfType<ShipMovement>();
        
    }

    // Update is called once per frame
    bool startDescent = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            startDescent = true; 
        }
    }

    void FixedUpdate()
    {
        if (startDescent)
        {
            movement.gravityConstant += movement.gravityConstant > 5.0f? 0 : 0.2f; 
        }
    }
}
