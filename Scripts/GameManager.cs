using DefaultNamespace.UI;
using UnityEngine;
using System.Collections;
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
        //[SerializeField] protected int coinScore = 10;
        //public int CoinScore { get => coinScore; }
        //[SerializeField] protected int boxScore = 50;
        //public int BoxScore { get => boxScore; }
        [Header("LoadComponents")]
        //public RobonCollect robonCollect;
        //public BinController binController;
        //[SerializeField] protected BoxController boxController;
        public TimeBar timeBar;
        //public RobonScore robonScore;
        public RobonRespawn robonRespawn;
        public RobonHealth robonHealth;
        public Transform loseGameUI;
        public Transform winLevelUI;
        public Transform winGameUI;
        private int isWin = 0;
        public RobonControl robonControl;
        public float timeDelayNextLevel = 1;
        public GameObject robon;
        
        protected override void Awake()
        {
            Application.targetFrameRate = 60;
            if (GameManager.instance != null) Debug.LogError("Only 1 GameManager can exist!");
            GameManager.instance = this;    
        }
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadTimeBar();
            // this.LoadRobonCollet();
            // this.LoadBinController();
            // this.LoadBoxCompleted();
            // this.LoadRobonScore();
            this.LoadRobonRespawn();
            this.LoadRobonHeath();
        }

        protected virtual void LoadTimeBar()
        {
            if (this.timeBar != null) return;
            this.timeBar = GameObject.Find("TimeBar").GetComponent<TimeBar>();
        }
        
        protected virtual void LoadRobonHeath()
        {
            if (this.robonHealth != null) return;
            this.robonHealth = GameObject.Find("RobonHealth").GetComponent<RobonHealth>();
        }
        // protected virtual void LoadRobonCollet()
        // {
        //     if (this.robonCollect != null) return;
        //     this.robonCollect = GameObject.Find("RobonCollect").GetComponent<RobonCollect>();
        // }
        
        // protected virtual void LoadBoxCompleted()
        // {
        //     if (this.boxController != null) return;
        //     this.boxController = GameObject.Find("BoxManager").GetComponent<BoxController>();
        // }
        
        // protected virtual void LoadBinController()
        // {
        //     if (this.binController != null) return;
        //     this.binController = GameObject.Find("BinManager").GetComponent<BinController>();
        // }
        
        // protected virtual void LoadRobonScore()
        // {
        //     if (this.robonScore != null) return;
        //     this.robonScore = GameObject.Find("RobonScore").GetComponent<RobonScore>();
        // }
        
        protected virtual void LoadRobonRespawn()
        {
            if (this.robonRespawn != null) return;
            this.robonRespawn = GameObject.Find("RobonRespawn").GetComponent<RobonRespawn>();
        }

        private void FixedUpdate()
        {
            this.isWinLevel();
            this.LoseGame();
        }
        
        
        protected virtual void isWinLevel()
        {
            if (this.isWin == 1)
            {
                winLevelUI.gameObject.SetActive(true);
            }
        }

        public void WinLevel()
        {
            this.isWin = 1;
            StartCoroutine(NextLevel());
        }

        private IEnumerator NextLevel()
        {
            robonControl.rb.bodyType = RigidbodyType2D.Static;
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                winGameUI.gameObject.SetActive(true);
                robonControl.animator.SetTrigger("isWinGame");
                AudioSource winGameSound = GameObject.Find("WinGameSound").GetComponent<AudioSource>();
                winGameSound.Play();
            }
            else
            {
                AudioSource sound = GameObject.Find("CompletedLevel").GetComponent<AudioSource>();
                sound.Play();
                robonControl.animator.SetTrigger("isNextLevel");
                winLevelUI.gameObject.SetActive(true);
                yield return new WaitForSeconds(timeDelayNextLevel);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        public virtual void LoseGame()
        {
            if (IsLose())
            {
                loseGameUI.gameObject.SetActive(true);
            }
        }
        
        public virtual bool IsLose()
        {
            return robonHealth.hp <= 0;
        }
    }
}