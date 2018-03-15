using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {


    public static CanvasManager instance = null;

    public Canvas screen1_canvas;
    public Canvas screen2_canvas;
    public Canvas screen3_canvas;
    public Canvas screen4_canvas;

    public CanvasGroup screen1;
    public CanvasGroup screen2;
    public CanvasGroup screen3;
    public CanvasGroup screen4;

    public Text startText;
    bool flag = true;

    void Awake(){
        if(instance == null){
            instance = this;
        }else if(instance !=this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        screen1.alpha = 1;
        screen1_canvas.enabled = true;

        screen2.alpha = 0;
        screen2_canvas.enabled = false;

        screen3.alpha = 0;
        screen3_canvas.enabled = false;

        screen4.alpha = 0;
        screen4_canvas.enabled = false;
    }
    
    void Start()
    {
        //InvokeRepeating("ChangeColor", 2, 2);
    }

    void ChangeColor()
    {
        if (flag)
        {
            startText.color = Color.black;
            flag = false;
        }
        else
        {
            startText.color = Color.white;
            flag = true;
        }
    }

    public void screen1Active(){
        screen1.alpha = 1;
        screen1.interactable = true;
        screen1_canvas.enabled = true;
    }

    public void screen2Active()
    {
        screen1_canvas.enabled = false;
        screen2.alpha = 1;
        screen2.interactable = true;
        screen2_canvas.enabled = true;
    }

    public void screen3Active()
    {
        screen2_canvas.enabled = false;
        screen3.alpha = 1;
        screen3.interactable = true;
        screen3_canvas.enabled = true;
    }

    public void screen4Active()
    {
        screen3_canvas.enabled = false;
        screen4.alpha = 1;
        screen4.interactable = true;
        screen4_canvas.enabled = true;
    }
}
