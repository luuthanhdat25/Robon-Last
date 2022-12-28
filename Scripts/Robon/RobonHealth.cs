using System;
using System.Collections;
using Audio;
using UnityEngine;

namespace DefaultNamespace
{
    public class RobonHealth : MonoBehaviour
    {
        [SerializeField] protected int hpMax = 3;
        [SerializeField] protected int hp;
        public int Hp { get => hp; }
        
        private void Start()
        {
            this.hp = hpMax;
        }

        public virtual void ReBorn()
        {
            this.hp = hpMax;
        }
        
        public virtual bool IsLose()
        {
            return hp <= 0;
        }
        
        public void Deduct(int hpDeduct)
        {
            this.hp -= hpDeduct;
        }
        
        
    }
}