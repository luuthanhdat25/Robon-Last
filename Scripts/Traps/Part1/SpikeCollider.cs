using DefaultNamespace;
using UnityEngine;

public class SpikeCollider : ReceiverBoxCollider
{
    public override void Received()
    {
        RobonCtrl.Instance.robonRespawn.RobonDeath();
    }
}
