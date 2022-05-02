using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float scrollSpeed = 1.5f;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y - Input.mouseScrollDelta.y*scrollSpeed, 30, 50),
            transform.position.z
        );
    }
}
