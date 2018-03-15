using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {

    SceneManager SceneManager;
    public GameObject OptionMenu;
    public Text gemText;
    public Text goldText;

    private void Awake()
    {
        gemText.text = (""+PlayerPrefs.GetInt("Gem"));
        goldText.text = ("" + PlayerPrefs.GetInt("Gold"));
    }

    public void Palace()
    {
        Debug.Log("Loading Palace...");
        SceneManager.LoadScene(2);
    }

    public void Stage1()
    {
        Debug.Log("Loading Stage...");
        SceneManager.LoadScene(3);
    }

    public void Option()
    {
        Debug.Log("Options Panel");
        OptionMenu.active = true;
    }

    public void Close()
    {
        OptionMenu.active = false;
    }

    public void Guide()
    {

    }

    public void Developer()
    {

    }

    public void Quit()
    {
        //Application.Quit();
    }
}
