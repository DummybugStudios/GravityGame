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
    private LineRenderer aimLine;
    private bool drawAimLine = false;
    private ShipMovement mShipMovement;
    private GameObject tempObstacle;
    private float fixedDeltaTime;
    void Start()
    {
        uIManager = canvas.GetComponent<UIManager>();
        uIManager.changeBHText(lives);
        aimLine = GetComponentInChildren<LineRenderer>(true);
        mShipMovement = FindObjectOfType<ShipMovement>(true);
        fixedDeltaTime = Time.fixedDeltaTime;
    }


    void Update()
    {
        if (!drawAimLine) return;

        Vector3 position = mShipMovement.transform.position;
        Vector3 velocity = mShipMovement.rb.velocity;
        float DT = fixedDeltaTime * 20;
        
        for (int i = 0; i < aimLine.positionCount; i++)
        {
            aimLine.SetPosition(i, position);
            position += DT * velocity;

            // Get total black hole influence at the new position
            Vector3 force = mShipMovement.GetForceAtPoint(position);
            // Get influence of the extra black hole being added.
            force += mShipMovement.GetForceAtPointFromBlackhole(position, tempObstacle.transform);
            velocity += force * DT / mShipMovement.rb.mass;
        }
    }
    Vector3 FindClickLocation(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 point = new Vector3(hit.point.x,0,hit.point.z);
        return point;
    }

    void OnMouseDown()
    {
        if (lives < 1)
            return;
        startPoint = FindClickLocation();
        tempObstacle = Instantiate(blackHolePrefab, startPoint, Quaternion.identity);
        drawAimLine = true; 
        aimLine.enabled = true;
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
        drawAimLine = false;
        aimLine.enabled = false;
    }
}
