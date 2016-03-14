using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BasicCommon;

public class Actor : MonoBehaviour {


	public ActorData ad = new ActorData();
	public ActorConfig acfg = null;
    public World.LiveableArea liveableArea;
    
    [ReadOnly]
    public string configName = "-";
    
    [HideInInspector]
    public Motor motor;
    [HideInInspector]
    public ActorBody body;
    
    [ReadOnly]
    public float attachOffset = 0f;
    [ReadOnly]
    public Actor attachParent = null;
    public Transform attachBone;
    public Vector3 AttachBonePosition { get { return attachBone.position; } }
    [ReadOnly]
    public List<Actor> attached = new List<Actor>();
    
	
    public virtual void Reset()
    {
        foreach(Transform t in this.transform)
        {
            attachBone = t;
            break;
        }
    }
    
    public virtual void Awake()
    {
        motor = this.GetComponent<Motor>();
        body = this.GetComponentInChildren<ActorBody>();
    }
    
    public virtual void Configure()
    {
        configName = acfg.aspecies.ToString();
        ad = new ActorData();
        ad.hp = acfg.hp;
        ad.stomach = 0;
    }
    
    public virtual void Update()
    { 
        float deltaTime = Time.deltaTime;
        motor.UpdateMotor(deltaTime);
        UpdateAttached(deltaTime);
    }
    
    protected virtual void UpdateAttached(float deltaTime)
    {
        if( attached.Count > 0 )
        {
            Vector3 forwardOffset = this.transform.forward;
            float forwardAngle = Mathf.Atan2(forwardOffset.y, forwardOffset.x);
            foreach(Actor child in attached)
            {
                float angle = forwardAngle + child.attachOffset;
                
                Vector3 offsetPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
                float attachRadius = 1.8f;
                child.SetPosition(attachBone.position + offsetPosition*attachRadius);
            }
        }
    }
    
    public void SetPosition(Vector3 vec)
    {
        motor.SafeMove(vec);
    }
    
    public virtual void HandleEvent(ref ActorEvent ae)
    {
        Rule r = Rules.GetGameRule(ae.aetype);
        r.Invoke(ref ae);
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
