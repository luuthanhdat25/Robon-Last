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
        [SerializeField] protected int hpMax = 3;
        public int HpMax { get => hpMax;}
        [SerializeField] protected float timeMax = 30f;
        public float TimeMax { get => timeMax;}
        public int levelNumber;
        public float timeDelayNextLevel = 1;
        
        
        protected override void Awake()
        {
            Application.targetFrameRate = 60;
            if (GameManager.instance != null) Debug.LogError("Only 1 GameManager can exist!");
            GameManager.instance = this;    
        }

        public void WinLevel()
        {
            RobonCtrl.Instance.robonRespawn.DisableMovement();
            MainUIManager.Instance.timeBarGameUI.StopTimeBar();
            AudioManager.Instance.backgroundSound.Pause();

            StartCoroutine(NextLevel());
        }

        private IEnumerator NextLevel()
        {
            if (levelNumber == 3)
            {
                RobonCtrl.Instance.robonAnimator.SetTrigger("isWinGame");
                
                MainUIManager.Instance.hudGameUI.gameObject.SetActive(false);
                MainUIManager.Instance.winGameUI.gameObject.SetActive(true);
                
                AudioManager.Instance.winGameSound.Play();
            }
            else
            {
                RobonCtrl.Instance.robonAnimator.SetTrigger("isNextLevel");
                
                if (MainUIManager.Instance.winLevelGameUI != null) 
                    MainUIManager.Instance.winLevelGameUI.gameObject.SetActive(true);
                
                AudioManager.Instance.completedLevel.Play();
                
                yield return new WaitForSeconds(timeDelayNextLevel);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}