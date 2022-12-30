using System.Collections;
using Audio;
using Camera;
using UnityEngine;

namespace DefaultNamespace
{
    public class RobonRespawn : RepeatMonobehaviour
    {
        [SerializeField] protected CameraManager cameraManager;
        [SerializeField] protected Transform robonTransform;
        public Transform robonRespawnPoint;
        
        [SerializeField] protected float timeRespawnDie = 1f;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadRobon();
            this.LoadCameraManager();
        }
        
        protected virtual void LoadRobon()
        {
            if (this.robonTransform != null) return;
            this.robonTransform = transform.parent;
        }
        
        protected virtual void LoadCameraManager()
        {
            if (this.cameraManager != null) return;
            this.cameraManager = GameObject.Find("Camera").GetComponent<CameraManager>();
        }
        
        public virtual void RobonDeath()
        {
            this.DisableMovement();
            RobonCtrl.Instance.robonAnimator.SetBool("isDeath", true);
            cameraManager.cameraAnimator.SetTrigger("Shake");
            
            RobonCtrl.Instance.robonHealth.Deduct(1);
            AudioManager.Instance.dieSound.Play();
            
            if (RobonCtrl.Instance.robonHealth.IsLose())
            {
                AudioManager.Instance.backgroundSound.Pause();
                StartCoroutine(DelayLoseUI());
            }else StartCoroutine(DelayTransition());
        }
        
        private IEnumerator DelayTransition()
        {
            MainUIManager.Instance.timeBarGameUI.SetMaxTime(GameManager.Instance.TimeMax);

            yield return new WaitForSeconds(timeRespawnDie);
            
            this.EnableMovement();
            MainUIManager.Instance.timeBarGameUI.hadWarning = false;
            AudioManager.Instance.uiButtonSound.Play();
        }
        
        public virtual void DisableMovement()
        {
            RobonCtrl.Instance.robonCollider.boxCollider.enabled = false;
            RobonCtrl.Instance.rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        protected virtual void EnableMovement()
        {
            RobonCtrl.Instance.rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            RobonCtrl.Instance.robonCollider.boxCollider.enabled = true;
            RobonCtrl.Instance.robonAnimator.SetBool("isDeath", false);
            robonTransform.position = robonRespawnPoint.transform.position;
        }
        
        public virtual void RobonStartPosition()
        {
            if (RobonCtrl.Instance.robonHealth.IsLose()) return;
            this.EnableMovement();
            MainUIManager.Instance.timeBarGameUI.SetMaxTime(GameManager.Instance.TimeMax);
        }

        private IEnumerator DelayLoseUI()
        {
            yield return new WaitForSeconds(2f);
            AudioManager.Instance.loseGameSound.Play();
            MainUIManager.Instance.loseGameUI.gameObject.SetActive(true);
        }
    }
}