using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {

    SceneManager SceneManager;
    public GameObject OptionMenu;
    public GameObject LevelLoader;
    public Text gemText;
    public Text goldText;
    int gemCount;
    int goldCount;

    public GameObject Dialogue;
    
    void Awake()
    {
        int temp = PlayerPrefs.GetInt("WorldMapCount");
        if(temp == 0)
        {
            Dialogue.SetActive(true);
        }
    }

    void Start()
    {
        gemCount = PlayerPrefs.GetInt("Gem");
        goldCount = PlayerPrefs.GetInt("TotalGolds");
        gemText.text = (""+ gemCount);
        goldText.text = ("" + goldCount);
    }

    public void Palace()
    {
        Debug.Log("Loading Palace...");
        SceneManager.LoadScene(3);
    }

    public void Stage1()
    {
        Debug.Log("Loading Stage...");
        LevelLoader.GetComponent<LevelLoader>().LoadLevel(4);
    }

    public void Stage2()
    {
        Debug.Log("Loading Stage...");
       //SceneManager.LoadScene(3);
    }

    public void Stage3()
    {
        Debug.Log("Loading Stage...");
        //SceneManager.LoadScene(3);
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
