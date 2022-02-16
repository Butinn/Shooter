using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject menu;
    public TextMeshProUGUI txtscore;

    private int currentScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GetScore (int score)
    {
        currentScore++;
        txtscore.text = "Zombie killed: " + currentScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void EndGame()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }
}
