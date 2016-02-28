using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour {


	public ActorData ad;
    public bool motorEnabled = false;
    public Motor motor;
    
    public Vector3 attachPosition = Vector3.zero;
    
    public Actor attachParent = null;
    public List<Actor> attachedList = new List<Actor>();
	
    
    public virtual void Update()
    { 
        float deltaTime = Time.deltaTime;
        motor.UpdateMotor(deltaTime);
    }
    
    public virtual void HandleEvent(ActorEvent ae)
    {
        Rule r = Rules.GetRule(ae.etype, ae.source, ae.target);
        r.Invoke(ae);
    }
    
    public void AttachChild(Actor other)
    {
        
    }
    
    public void AttachToParent(Actor other, Vector3 offset)
    {
        this.motorEnabled = false;
        this.transform.parent = other.transform;
        this.transform.localPosition = offset;
        other.attachParent = this;
        attachedList.Add(this);
    }
}
