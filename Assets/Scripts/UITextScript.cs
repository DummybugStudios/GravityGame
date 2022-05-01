using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextScript : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI yellow, red, normal;
    string text;
    Color yellow_color, red_color;
    void Start()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        yellow = texts[0];
        red = texts[1]; 
        normal = texts[2];
        text = normal.text;
        yellow_color = yellow.color;
        red_color = red.color;
    }

    // Update is called once per frame
    public float yellowBlinkSpeed = 1.0f;
    public float redBlinkSpeed = 1.0f;
    public float phase = 0.0f;
    void Update()
    {
        yellow_color.a = Mathf.Sin(Time.time/yellowBlinkSpeed)*0.5f + 0.5f;
        // red_color.a = Mathf.Sin(Time.time/redBlinkSpeed + phase)*0.5f + 0.5f;
        yellow.color = yellow_color;
        // red.color = red_color;
    }
}
