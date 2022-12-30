using UnityEngine;

namespace DefaultNamespace.Traps.Enemy_Part3
{
    public class FireCollider : ReceiverBoxCollider
    {
        public override void Received()
        {
            RobonCtrl.Instance.robonRespawn.RobonDeath();
        }
    }
}