using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {

    protected Actor actor;
    protected ActorBody body;
    
    public bool canMove = true;
    
    [ReadOnly] [SerializeField]
    protected Vector3 velocity;
    
    [ReadOnly] [SerializeField]
    protected float speed;
    
    [ReadOnly] [SerializeField]
    protected Vector3 forceDirection;
    
    [ReadOnly] [SerializeField]
    protected float forceMultiplier;
    
    [ReadOnly] [SerializeField]
    protected float forceAccel;
    
    [ReadOnly] [SerializeField]
    protected float frictionDecel;
    
    [ReadOnly] [SerializeField]
    protected float friction;
    
    [ReadOnly] [SerializeField]
    protected float speedMaximum;
    
    
    [ReadOnly] [SerializeField]
    protected float rotationAccel;
    
    [ReadOnly] [SerializeField]
    protected float rotationVelocity;
    
    [ReadOnly] [SerializeField]
    protected float rotationMaximum;
        
    protected Vector3 motorForward = Vector3.one;
    public Vector3 MotorForward { get { return motorForward; } }
    
	void Reset () {
        canMove = true;
	}
    
    void Awake()
    {
        actor = gameObject.GetComponent<Actor>();
        this.body = actor.body;
    }
    
    public void Configure()
    {
        ActorConfig cfg = actor.acfg;
        this.forceAccel = cfg.forceAccel;
        this.speedMaximum = cfg.speedMaximum;
        this.frictionDecel = cfg.frictionDecel;
        this.rotationAccel = cfg.rotationAccel;
        this.rotationMaximum = 0.7f;//cfg.rotationMaximum;
    }
	
    public virtual void SetDesiredMoveVector(Vector3 desiredMoveVector)
    {
        forceMultiplier = desiredMoveVector.magnitude;
        forceDirection = desiredMoveVector.normalized;
    }
	public virtual void UpdateMotor(float deltaTime) 
    {
        velocity += forceDirection*forceMultiplier*forceAccel*deltaTime;
        Vector3 velocityDir = velocity.normalized;
        velocity -= velocityDir*frictionDecel*deltaTime;
        float speed = velocity.magnitude;
        if( speed > speedMaximum )
        {
            velocity = speedMaximum*velocityDir;
        }
        
        MotorMove(deltaTime, velocity);
        RecomputeForward();
        
	}
    
    
    public void MotorMove(float deltaTime, Vector3 velocity)
    {
        
        //velocity = desiredSpeed*motorForward;
        SafeMove(this.transform.position + velocity*deltaTime);
        speed = Vector3.Dot(velocity, motorForward);
        return;
    }
    

    
    protected void RecomputeForward()
    {
        motorForward = this.transform.forward;
        //motorForward.z = 0;
    }
    
    public virtual void SafeMove(Vector3 desiredPos)
    {
        Bounds liveable = World.Instance.GetLiveableArea(actor.liveableArea);
        if( desiredPos.x < liveable.min.x )
        {
            desiredPos.x = this.transform.position.x;
            velocity.x = 0;
            OnCollideMinX();
        }
        if( desiredPos.x > liveable.max.x )
        {
            desiredPos.x = liveable.max.x;
            velocity.x = 0;
            OnCollideMaxX();
        }
        if( desiredPos.y < liveable.min.y )
        {
            desiredPos.y = this.transform.position.y;
            velocity.y = 0;
            OnCollideMinY();
        }
        if( desiredPos.y > liveable.max.y )
        {
            desiredPos.y = this.transform.position.y;
            velocity.y = 0;
            OnCollideMaxY();
        }
        if( desiredPos.z < liveable.min.z )
        {
            desiredPos.z = this.transform.position.z;
            velocity.z = 0;
            OnCollideMinZ();
        }
        if( desiredPos.z > liveable.max.z )
        {
            desiredPos.z = this.transform.position.z;
            velocity.z = 0;
            OnCollideMaxZ();
        }
        this.transform.position = desiredPos;
    }
    
    
    protected virtual void OnCollideMinX() { }
    protected virtual void OnCollideMaxX() { }
    protected virtual void OnCollideMinY() { }
    protected virtual void OnCollideMaxY() { }
    protected virtual void OnCollideMinZ() { }
    protected virtual void OnCollideMaxZ() { }
    
    
}
