using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour {


	public ActorData ad = new ActorData();
    public Motor motor;
    
    public float attachOffset = 0f;
    
    public Actor attachParent = null;
    public List<Actor> attached = new List<Actor>();
    public Transform attachBone;
    public Vector3 AttachBonePosition { get { return attachBone.position; } }
	
    public virtual void Reset()
    {
        ad.atype = ActorType.FISH;
        ad.hp = 10;
        ad.speed = 10;
        ad.hunger = 10;  
        motor = this.GetComponent<Motor>();
        foreach(Transform t in this.transform)
        {
            attachBone = t;
            break;
        }
    }
    
    public virtual void Update()
    { 
        float deltaTime = Time.deltaTime;
        motor.UpdateMotor(deltaTime);
        UpdateAttached();
    }
    
    protected void UpdateAttached()
    {
        if( attached.Count > 0 )
        {
            Vector3 forwardOffset = this.transform.forward;
            float forwardAngle = Mathf.Atan2(forwardOffset.y, forwardOffset.z);
            foreach(Actor child in attached)
            {
                float angle = Mathf.PI/2 - forwardAngle;
                if( Mathf.Abs(forwardAngle) > Mathf.PI/2 )
                {
                    angle += child.attachOffset;
                }
                else
                {
                    angle -= child.attachOffset;
                }
                Vector3 offsetPosition = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
                float attachRadius = 0.8f;
                child.SetPosition(attachBone.position + offsetPosition*attachRadius);
            }
        }
    }
    
    public void SetPosition(Vector3 vec)
    {
        motor.SetPosition(vec);
    }
    
    public virtual void HandleEvent(ActorEvent ae)
    {
        Rule r = Rules.GetGameRule(ae.source, ae.victim, ae.aetype);
        r.Invoke(ae);
    }
    
    public void AttachChild(Actor other)
    {
        
    }
    
    public void AttachToParent(Actor other, float offset)
    {
        other.motor.canMove = false;
        other.attachOffset = offset;
        other.attachParent = this;
        this.attached.Add(other);
    }
}
