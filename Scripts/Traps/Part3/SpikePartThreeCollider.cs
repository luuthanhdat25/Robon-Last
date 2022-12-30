using DefaultNamespace;
using UnityEngine;

public class SpikePartThreeCollider : ReceiverBoxCollider
{
    public override void Received()
    {
        RobonCtrl.Instance.robonRespawn.RobonDeath();
    }
}
