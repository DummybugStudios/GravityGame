using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextScript : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI yellow, red;
    [SerializeField]
    Color yellow_color =  new Color(1.0f, 216.0f/255.0f,0.0f,1.0f);
    [SerializeField]
    Color red_color = new Color(1.0f, 61.0f/255, 61.0f/255, 1.0f);

    [SerializeField]
    Vector3 yellow_pos = new Vector3(-3.6f, 5.0f, 0.0f);
    [SerializeField]
    Vector3 red_pos = new Vector3(2.9f, -7.5f, 0.0f);
    void Start()
    {
        GameObject child = GetComponentInChildren<TextMeshProUGUI>().gameObject;

        GameObject yellowInstance = Instantiate(child, transform.position + yellow_pos, Quaternion.identity, transform);
        yellow = yellowInstance.GetComponent<TextMeshProUGUI>();
        yellow.color = yellow_color;


        GameObject redInstance = Instantiate(child, transform.position + red_pos, Quaternion.identity, transform);
        red = redInstance.GetComponent<TextMeshProUGUI>();
        red.color = red_color;

        //Force transform to update
        child.transform.SetParent(yellowInstance.transform);
        child.transform.SetParent(transform);
    }

    // Update is called once per frame
    public float yellowBlinkSpeed = 0.25f;
    // void Update()
    // {
    //     yellow_color.a = Mathf.Sin(Time.time/yellowBlinkSpeed)*0.5f + 0.5f;
    //     yellow.color = yellow_color;
    // }
}
