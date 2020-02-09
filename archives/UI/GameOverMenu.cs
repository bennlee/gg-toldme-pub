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
    public int swordCount = 0;
    [HideInInspector]
    public int bowCount = 0;
    [HideInInspector]
    public int wandCount = 0;

    int loadGold;
    public int totalGold;
    int loadHeroCount;
    public int deadHeroCount;

    private void Start()
    {
        animator = GetComponent<Animator>();
        loadGold = PlayerPrefs.GetInt("TotalGolds");
        loadHeroCount = PlayerPrefs.GetInt("TotalHero ");
        swordText.text = (""+swordCount);
        bowText.text = ("" + bowCount);
        wandText.text = ("" + wandCount);
        totalGoldText.text = ("" + totalGold);
    }

    public void ToMain()
    {
        Save();
        SceneManager.LoadScene(2);
    }

    public void PlayAgain()
    {
        Save();
        SceneManager.LoadScene(4);
    }

    private void Save()
    {
        loadGold += totalGold;
        loadHeroCount += deadHeroCount;
        PlayerPrefs.SetInt("TotalGolds", loadGold);
        PlayerPrefs.SetInt("TotalHero", loadHeroCount);
    }

}
