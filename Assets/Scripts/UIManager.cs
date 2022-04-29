using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI blackholesText;
    public Game game;
    
    public void onResetClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void changeBHText(int num) {
        blackholesText.text = "Blackholes: " + num; 
    }

    public void onSwapClick() {
        game.swapCameras();
    }
}
