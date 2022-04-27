using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBlackHole : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blackHoles;
    public GameObject blackHolePrefab;
    public UIManager uIManager;
    public GameObject canvas;


    public int lives = 3;

    private Vector3 startPoint;
    void Start()
    {
        uIManager = canvas.GetComponent<UIManager>();
        uIManager.changeBHText(lives);
    }

    Vector3 FindClickLocation(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 point = new Vector3(hit.point.x,0,hit.point.z);
        return point;
    }

    private GameObject tempObstacle;
    void OnMouseDown()
    {
        if (lives < 1)
            return;
        startPoint = FindClickLocation();
        Debug.Log("start point: " + startPoint);
        tempObstacle = Instantiate(blackHolePrefab, startPoint, Quaternion.identity);
    }

    void OnMouseDrag()
    {
        if (lives < 1)
            return;
        Vector3 point = FindClickLocation();
        float scaleFactor = 1.0f + (point - startPoint).magnitude * 0.5f;
        scaleFactor = Mathf.Clamp(scaleFactor, 1.0f, 4.0f);
        // TODO: object won't be a sphere forever probably. Don't scale in all direction perhaps
        tempObstacle.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    void OnMouseUp()
    {
        if (lives < 1)
            return;

        Vector3 point = FindClickLocation();

        tempObstacle.transform.parent = blackHoles.transform;
        tempObstacle.tag = "Obstacle";
        lives--; 
        uIManager.changeBHText(lives);
    }
}
