using DefaultNamespace;

public class WinZoneCollider : ReceiverBoxCollider
{
    public override void Received()
    {
        GameManager.Instance.WinLevel();
    }
}
