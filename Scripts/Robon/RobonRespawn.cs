using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class RobonRespawn : RepeatMonobehaviour
    {
        [SerializeField] protected Transform robonTransform;
        [SerializeField] protected GameObject robonRespawnPoint;
        public float timeRespawnDie = 1f;
        public RobonControl robonControl;
        public Animator cam;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadRobon();
            this.LoadRobonRespawnPoint();
        }
        
        protected virtual void LoadRobonRespawnPoint()
        {
            if (this.robonRespawnPoint != null) return;
            this.robonRespawnPoint = GameObject.Find("RobonRespawnPoint");
        }
        protected virtual void LoadRobon()
        {
            if (this.robonTransform != null) return;
            this.robonTransform = transform.parent;
        }
        
        public virtual void RobonDie()
        {
            if (GameManager.Instance.IsLose()) return;
            cam.SetTrigger("Shake");
            StartCoroutine(DelayTransition());
            //this.RespawnBin();
        }

        private IEnumerator DelayTransition()
        {
            AudioSource sound = GameObject.Find("DieSound").GetComponent<AudioSource>();
            robonControl.animator.SetBool("isDeath",true);
            robonControl.rb.bodyType = RigidbodyType2D.Static;
            sound.Play();
            GameManager.Instance.timeBar.SetMaxTime(GameManager.Instance.TimeMax);
            yield return new WaitForSeconds(timeRespawnDie);
            robonControl.animator.SetBool("isDeath",false);
            this.robonTransform.position = robonRespawnPoint.transform.position;
            GameManager.Instance.robonHealth.Deduct(1);
            robonControl.rb.bodyType = RigidbodyType2D.Dynamic;
            GameManager.Instance.timeBar.hadWarning = false;
            if (!GameManager.Instance.IsLose())
            {
                AudioSource buttonSound = GameObject.Find("UIButtonSound").GetComponent<AudioSource>();
                buttonSound.Play();
            }
        }


        public virtual void RobonCompleteBox()
        {
            if (GameManager.Instance.IsLose()) return;
            this.robonTransform.position = robonRespawnPoint.transform.position;
            GameManager.Instance.timeBar.SetMaxTime(GameManager.Instance.TimeMax);
        }

        // protected virtual void RespawnBin()
        // {
        //     GameManager.Instance.robonCollect.isCollected = false;
        //     
        //     if (GameManager.Instance.binController.isFbinCollected == true)
        //     {
        //         GameManager.Instance.binController.fBin.gameObject.SetActive(true);
        //         GameManager.Instance.binController.isFbinCollected = false;
        //     }
        //
        //     if (GameManager.Instance.binController.isPbinCollected == true)
        //     {
        //         GameManager.Instance.binController.pBin.gameObject.SetActive(true);
        //         GameManager.Instance.binController.isPbinCollected = false;
        //     }
        //
        //     if (GameManager.Instance.binController.isTbinCollected == true)
        //     {
        //         GameManager.Instance.binController.tBin.gameObject.SetActive(true);
        //         GameManager.Instance.binController.isTbinCollected = false;
        //     }
        // }
        
        
    }
}