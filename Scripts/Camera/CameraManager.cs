using UnityEngine;

namespace Camera
{
    public class CameraManager : RepeatMonobehaviour
    {
        public Animator cameraAnimator;
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadCameraAnimator();
        }
        
        protected virtual void LoadCameraAnimator()
        {
            if (this.cameraAnimator != null) return;
            this.cameraAnimator = GetComponent<Animator>();
        }
    }
}