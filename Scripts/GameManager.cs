using DefaultNamespace.UI;
using UnityEngine;
using System.Collections;
using Audio;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameManager : RepeatMonobehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get => instance; }
        [Header("SettingData")]
        [SerializeField] protected float timeMax = 30f;
        public float TimeMax { get => timeMax; }
        [Header("LoadComponents")]
        public RobonRespawn robonRespawn;
        private int isWin = 0;
        public float timeDelayNextLevel = 1;
        public int levelNumber;
        protected override void Awake()
        {
            Application.targetFrameRate = 60;
            if (GameManager.instance != null) Debug.LogError("Only 1 GameManager can exist!");
            GameManager.instance = this;    
        }
        
        protected virtual void isWinLevel()
        {
            if (this.isWin == 1 && levelNumber != 3)
            {
                MainUIManager.Instance.winGameUI.gameObject.SetActive(true);
            }
        }

        public void WinLevel()
        {
            this.isWin = 1;
            StartCoroutine(NextLevel());
        }

        private IEnumerator NextLevel()
        {
            RobonCtrl.Instance.rigidbody2D.bodyType = RigidbodyType2D.Static;
            if (levelNumber == 3)
            {
                MainUIManager.Instance.hudGameUI.gameObject.SetActive(false);
                MainUIManager.Instance.winGameUI.gameObject.SetActive(true);
                RobonCtrl.Instance.robonAnimator.SetTrigger("isWinGame");
                
                AudioManager.Instance.winGameSound.Play();
                AudioManager.Instance.backgroundSound.Pause();
            }
            else
            {
                AudioManager.Instance.completedLevel.Play();
                AudioManager.Instance.backgroundSound.Pause();
                
                RobonCtrl.Instance.robonAnimator.SetTrigger("isNextLevel");
                
                if (MainUIManager.Instance.winGameUI != null)
                {
                    MainUIManager.Instance.winGameUI.gameObject.SetActive(true);
                    yield return new WaitForSeconds(timeDelayNextLevel);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
}