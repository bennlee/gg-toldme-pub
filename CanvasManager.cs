using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {


    public static CanvasManager instance = null;

    public Canvas screen1_canvas;
    public Canvas screen2_canvas;
    public CanvasGroup screen1;
    public CanvasGroup screen2;

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
    }

    public void screen1Active(){
        screen1.alpha = 1;
        screen1.interactable = true;
        screen1_canvas.enabled = true;
        screen2_canvas.enabled = false;
    }

    public void screen2Active(){
        screen2.alpha = 1;
        screen2.interactable = true;
        screen1_canvas.enabled = false;
        screen2_canvas.enabled = true;
    }
}
