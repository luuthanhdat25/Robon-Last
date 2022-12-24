using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class RobonHealth : RepeatMonobehaviour
    {
        [SerializeField] protected int hpMax = 3;
        public int hp;
        public RobonControl robonControl;
        
        private void Start()
        {
            this.hp = hpMax;
        }

        public void ReBorn()
        {
            this.hp = hpMax;
        }
        
        public void Deduct(int hpDeduct)
        {
            
            
            this.hp -= hpDeduct;

            if (this.hp <= 0)
            {
                AudioSource bgSound = GameObject.Find("BackgroundSound").GetComponent<AudioSource>();
                bgSound.Pause();

                AudioSource loseSound = GameObject.Find("LoseGameSound").GetComponent<AudioSource>();
                loseSound.Play();
            }
        }
    }
}