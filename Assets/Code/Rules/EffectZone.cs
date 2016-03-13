using UnityEngine;
using System.Collections;

public enum EffectZoneType
{
    SURFACE,
    ATTACHER,
}
public class EffectZone : MonoBehaviour {


    public Collider effectCollider;
    public Actor owner;
    public EffectZoneType eztype;
    
    public ActorType OwnerActorType { get { return owner.acfg.atype; } } 
    
    public void Reset()
    {
        effectCollider = GetComponent<Collider>();
        owner = this.transform.root.GetComponent<Actor>();
    }
    
    public void OnTriggerEnter(Collider other)
    {
        EffectZone otherZone = other.gameObject.GetComponent<EffectZone>();
        Rule r = Rules.GetIntersect(this, otherZone);
        if( r != null )
        {
            ActorEvent ae = new ActorEvent(owner, otherZone.owner, ActorEventType.INTERSECT);   
            r.Invoke(ref ae);
        }
        
    }
}
