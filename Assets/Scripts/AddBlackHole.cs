using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBlackHole : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blackHoles;
    public GameObject blackHolePrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Debug.Log("Clicked at " + hit.point);
        Vector3 point = new Vector3(hit.point.x,0,hit.point.z);
        GameObject x = Instantiate(blackHolePrefab, point, Quaternion.identity);
        x.transform.parent = blackHoles.transform;
        
    }

    void OnMouseUp()
    {
        Debug.Log("click ended");
    }
}
