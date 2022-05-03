using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UIManager : MonoBehaviour
{
    public GameObject blackholes;
    public void onResetClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void changeBHText(int num) {
        blackholes.transform.GetChild(num).gameObject.SetActive(false);
    }
}
