using DefaultNamespace;
using DefaultNamespace.Traps;

public class HacksawCollider : ReceiverCircleCollider
{
    public override void Received()
    {
        RobonCtrl.Instance.robonRespawn.RobonDeath();
    }
}
