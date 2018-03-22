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
    int temp;

    public GameObject Credit;
    public GameObject Dialogue;
    
    void Awake()
    {
        if(!PlayerPrefs.HasKey("MapTutorial"))
        {
            PlayerPrefs.SetInt("MapTutorial", 0);
            PlayerPrefs.Save();
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
        Credit.SetActive(true);
    }

    public void Developer_Close()
    {
        Credit.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    


}
