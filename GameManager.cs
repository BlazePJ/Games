using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOver;
    public static event GameDelegate OnGamePaused;
    public static event GameDelegate Spawn;
    public static GameManager Instance;

    public GameObject StartPage;
    public GameObject GamePage;
    public GameObject GameOverPage;
    public GameObject CountdownPage;
    public GameObject PausePage;
   
    Parallaxer para;
    private int i = 0;

    public Text ScoreText;

    enum PageState
    {
        None,
        Start,
        InGame,
        Pause,
        GameOver,
        Countdown
    }

    int score = 0;
  bool gameOver = true;
    public bool GameOver { get { return gameOver; } }
    public int Score {  get { return score; } }
    private void Start()
    {
        
        SetPageState(PageState.Start);
    }
    private void Awake()
    {
        Instance = this;
        
    }
    private void OnEnable()
    {
       
        CountdownText.OnCountdownFinished += OnCountdownFinished;
        Tapcontroller.OnPlayerDied += OnPlayerDied;
        Tapcontroller.OnPlayerScored += OnPlayerScored;
        Tapcontroller.OnPause += OnPause;
        
    }
        private void OnDisable()
        { 
            CountdownText.OnCountdownFinished -= OnCountdownFinished;
            Tapcontroller.OnPlayerDied -= OnPlayerDied;
            Tapcontroller.OnPlayerScored -= OnPlayerScored;
        Tapcontroller.OnPause -= OnPause;
        

    }
        void OnCountdownFinished()
    {    
        SetPageState(PageState.InGame);
        OnGameStarted();
        gameOver = false;
                 }
   
    void OnPlayerDied()
    {
        gameOver = true;
        int savedScore = PlayerPrefs.GetInt("HighScore");
        if (score> savedScore)
        {   
            PlayerPrefs.SetInt("HighScore",score);
        }
        SetPageState(PageState.GameOver);
    }
    void OnPlayerScored()
    {
        score++;
        ScoreText.text = score.ToString();
    }
    void SetPageState(PageState state)
    {
        switch(state)
        {
            case PageState.None:
                StartPage.SetActive(false);
                GamePage.SetActive(false);
                GameOverPage.SetActive(false);
                CountdownPage.SetActive(false);
                PausePage.SetActive(false);
              
                break;
            case PageState.Start:
                StartPage.SetActive(true);
                GamePage.SetActive(false);
                GameOverPage.SetActive(false);
                CountdownPage.SetActive(false);
                PausePage.SetActive(false);
              
                break;
            case PageState.InGame:
                StartPage.SetActive(false);
                GamePage.SetActive(true);
                GameOverPage.SetActive(false);
                CountdownPage.SetActive(false);
                PausePage.SetActive(false);
              
                break;

            case PageState.Pause:
                StartPage.SetActive(false);
                GamePage.SetActive(false);
                GameOverPage.SetActive(false);
                CountdownPage.SetActive(false);
                PausePage.SetActive(true);
              
                break;
            case PageState.GameOver:
                StartPage.SetActive(false);
                GamePage.SetActive(false);
                GameOverPage.SetActive(true);
                CountdownPage.SetActive(false);
                PausePage.SetActive(false);
              
                break;
            case PageState.Countdown:
                StartPage.SetActive(false);
                GamePage.SetActive(false);
                GameOverPage.SetActive(false);
                CountdownPage.SetActive(true);
                PausePage.SetActive(false);
              
                break;
            


        }
    }
    public void ConfirmGameOver()
    {
        score = 0;
        OnGameOver();
        ScoreText.text = "0";
        SetPageState(PageState.Countdown);
    }
    public void StartGame()
    {
        Spawn();
        score = 0;
        SetPageState(PageState.Countdown);
        
    }
    public void OnPause()
    {
        OnGamePaused();
        SetPageState(PageState.Pause);
        gameOver = true;
        

    }
    public void OnResume()
    {
        OnGameStarted();
        SetPageState(PageState.Countdown);
        

    }
    }


