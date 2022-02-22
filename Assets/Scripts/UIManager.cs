using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UIManager : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI blackholesText;
    public GameObject gameObj;
    public Game game;
    // Start is called before the first frame update
    void Start()
    {
        game = gameObj.GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onResetClick() {
        SceneManager.LoadScene("SampleScene"); 
    }

    public void changeBHText(int num) {
        blackholesText.text = "Blackholes: " + num; 
    }

    public void onSwapClick() {
        game.swapCameras();
    }
}
