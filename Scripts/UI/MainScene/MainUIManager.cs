using System;
using Audio;
using DefaultNamespace;
using DefaultNamespace.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : RepeatMonobehaviour
{
    private static MainUIManager instance;
    public static MainUIManager Instance { get => instance; }
    
    public GameObject menuGameUI;
    public GameObject pauseGameUI;
    public GameObject hudGameUI;
    public GameObject loseGameUI;
    public GameObject winLevelGameUI;
    public GameObject winGameUI;
    public TimeBar timeBarGameUI;
    
    private static bool gameIsPaused = false;
    private float previousTimeScale = 1;
    
    protected override void Awake()
    {
        MainUIManager.instance = this;    
    }

    private void Update()
    {
        if (!CanPauseGame()) return;
        GetEscapeButton();
    }

    public void PlayGame()
    {
        this.hudGameUI.SetActive(true);
        RobonCtrl.Instance.robonRespawn.RobonStartPosition();
    }

    
    public void Replay()
    {
        this.ResumeGame();
        loseGameUI.gameObject.SetActive(false);
        
        RobonCtrl.Instance.robonHealth.ReBorn();
        RobonCtrl.Instance.robonRespawn.RobonStartPosition();
    
        AudioManager.Instance.backgroundSound.Play();
    }

    private void GetEscapeButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseGameUI != null) this.PauseGameUI();
    }

    public void ReloadLevel1()
    {
        SceneManager.LoadScene("Level 1");
        AudioSource sound = GameObject.Find("BackgroundSound").GetComponent<AudioSource>();
        sound.Play();
    }

    public void PauseGameUI()
    {
        if (Time.timeScale > 0)
        {
            this.PauseGame();
        }else if (Time.timeScale == 0)
        {
            this.ResumeGame();
        }
    }

    private bool CanPauseGame()
    {
        if ((menuGameUI != null && menuGameUI.gameObject.activeSelf ) || 
            (winLevelGameUI != null && winLevelGameUI.gameObject.activeSelf) || 
            winGameUI.gameObject.activeSelf || loseGameUI.gameObject.activeSelf) return false;
        return true;
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
        this.pauseGameUI.SetActive(true);
    }
    
    
    public void ResumeGame()
    {
        Time.timeScale = previousTimeScale;
        gameIsPaused = false;
        pauseGameUI.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
