using UnityEngine;

namespace DefaultNamespace
{
    public class RobonCtrl : RepeatMonobehaviour
    {
        private static RobonCtrl instance;
        public static RobonCtrl Instance { get => instance; }
        
        public Rigidbody2D rigidbody2D;
        public RobonCollider robonCollider;
        public Animator robonAnimator;
        public RobonHealth robonHealth;
        public RobonRespawn robonRespawn;
        
        protected override void Awake()
        {
            RobonCtrl.instance = this;    
        }
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadRigidbody();
            this.LoadRobonAnimator();
            this.LoadRobonHeath();
            this.LoadRobonRespawn();
            this.LoadRobonCollider();
        }
        
        protected virtual void LoadRigidbody()
        {
            if (this.rigidbody2D != null) return;
            this.rigidbody2D = GetComponent<Rigidbody2D>();
            this.rigidbody2D.gravityScale = 0;
            this.rigidbody2D.freezeRotation = true;
        }
        
        protected virtual void LoadRobonAnimator()
        {
            if (this.robonAnimator != null) return;
            this.robonAnimator = transform.GetComponentInChildren<Animator>();
        }
        
        protected virtual void LoadRobonHeath()
        {
            if (this.robonHealth != null) return;
            this.robonHealth = transform.GetComponentInChildren<RobonHealth>();
        }
        
        protected virtual void LoadRobonRespawn()
        {
            if (this.robonRespawn != null) return;
            this.robonRespawn = transform.GetComponentInChildren<RobonRespawn>();
        }
        
        protected virtual void LoadRobonCollider()
        {
            if (this.robonCollider != null) return;
            this.robonCollider = GetComponent<RobonCollider>();
        }
    }
}