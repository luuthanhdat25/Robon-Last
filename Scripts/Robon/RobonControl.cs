using UnityEngine;

namespace DefaultNamespace
{
    public class RobonControl : Movement
    {
        [SerializeField] float moveSpeed = 3f;
        
        protected override void Update()
        {
            this.Move();
            this.SetAnimaiton();
        }

        private void SetAnimaiton()
        {
            RobonCtrl.Instance.robonAnimator.SetFloat("Horizontal", InputManager.Instance.MoveButtonVector3.x);
            RobonCtrl.Instance.robonAnimator.SetFloat("Vertical", InputManager.Instance.MoveButtonVector3.y);
            RobonCtrl.Instance.robonAnimator.SetFloat("Speed", InputManager.Instance.MoveButtonVector3.sqrMagnitude);
        }

        protected void FixedUpdate()
        {
            this.RobonMoving();
        }

        protected virtual void RobonMoving()
        {
            RobonCtrl.Instance.rigidbody2D.velocity = new Vector2(InputManager.Instance.MoveButtonVector3.x * moveSpeed,
                InputManager.Instance.MoveButtonVector3.y * moveSpeed);
        }
    }
}