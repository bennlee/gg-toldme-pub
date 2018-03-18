using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverMenu : MonoBehaviour {

    Animator animator;
    SceneManager SceneManager;
    public Text swordText;
    public Text bowText;
    public Text wandText;
    public Text totalGoldText;

    [HideInInspector]
    public int swordCount;
    [HideInInspector]
    public int bowCount;
    [HideInInspector]
    public int wandCount;

    int loadGold;
    public int totalGold;

    private void Start()
    {
        animator = GetComponent<Animator>();
        loadGold = PlayerPrefs.GetInt("TotalGolds");
        swordText.text = (""+swordCount);
        bowText.text = ("" + bowCount);
        wandText.text = ("" + wandCount);
        totalGoldText.text = ("" + totalGold);
    }

    public void ToMain()
    {
        Save();
        SceneManager.LoadScene(1);
    }

    public void PlayAgain()
    {
        Save();
        SceneManager.LoadScene(3);
    }

    private void Save()
    {
        loadGold += totalGold;
        PlayerPrefs.SetInt("TotalGolds", loadGold);
    }

}
