using DefaultNamespace.Traps;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RobonCollider : RepeatMonobehaviour
{
    public BoxCollider2D boxCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }
    
    protected virtual void LoadCollider()
    {
        if (this.boxCollider != null) return;
        this.boxCollider = GetComponent<BoxCollider2D>();
        this.boxCollider.size = new Vector3(0.4f, 0.4f, 1);
        Debug.Log(transform.name + ": LoadBoxCollider", gameObject);
    }
    
    protected void OnTriggerEnter2D(Collider2D col)
    {
        Send(col.transform);
    }
    
    public virtual void Send(Transform obj)
    {
        Receiver receiver = obj.GetComponent<Receiver>();
        if (receiver == null) return;
        receiver.Received();
    }
}
