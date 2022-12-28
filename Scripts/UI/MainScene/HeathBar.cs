using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class HeathBar : RepeatMonobehaviour
    {
        [SerializeField] protected List<Transform> heartImages;
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadHeathImage();
        }

        protected void LoadHeathImage()
        {
            if (this.heartImages.Count > 0) return;
            foreach (Transform image in transform)
            {
                this.heartImages.Add(image);
            }
        }

        private void FixedUpdate()
        {
            UpdateHealthBar();
        }

        protected virtual void UpdateHealthBar()
        {
            for (int i = 0; i < heartImages.Count; i++)
            {
                if (i < RobonCtrl.Instance.robonHealth.Hp) heartImages[i].gameObject.SetActive(true);
                else heartImages[i].gameObject.SetActive(false);
            }
        }
    }
}