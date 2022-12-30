using DefaultNamespace;
using UnityEngine;

public class LaserCollider : ReceiverBoxCollider
{
    public override void Received()
    {
        RobonCtrl.Instance.robonRespawn.RobonDeath();
    }
}
