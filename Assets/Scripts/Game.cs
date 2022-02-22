using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public GameObject playerObj;
    public GameObject uiManagerObj;

    private ShipMovement player;
    private UIManager uIManager;

    public GameObject mainStaticCam;
    public GameObject mainFollowCam;
    public GameObject miniStaticCam;
    public GameObject miniFollowCam;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<ShipMovement>();
        uIManager = uiManagerObj.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void swapCameras() {
        if(mainStaticCam.activeSelf) {
            mainStaticCam.SetActive(false);
        }
        else{
            mainStaticCam.SetActive(true);
        }

        if(mainFollowCam.activeSelf) {
            mainFollowCam.SetActive(false);
        }
        else{
            mainFollowCam.SetActive(true);
        }

        if(miniStaticCam.activeSelf) {
            miniStaticCam.SetActive(false);
        }
        else{
            miniStaticCam.SetActive(true);
        }

        if(miniFollowCam.activeSelf) {
            miniFollowCam.SetActive(false);
        }
        else{
            miniFollowCam.SetActive(true);
        }
    }

    public void toggleCameraFollow() {
        if(mainStaticCam.activeSelf) {
            mainStaticCam.SetActive(false);
        }
        else{
            mainStaticCam.SetActive(true);
        }

        if(mainFollowCam.activeSelf) {
            mainFollowCam.SetActive(false);
        }
        else{
            mainFollowCam.SetActive(true);
        }
    }


}
