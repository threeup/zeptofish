using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {

    public bool canMove = true;
    
    public Vector3 velocity;
    public Vector3 forceDirection;
    public float forceMultiplier;
    public float forceAccel;
    
    public float frictionDecel;
    
    public float speedMaximum;

	// Use this for initialization
	void Reset () {
        canMove = true;
        forceAccel = 1.2f;
        frictionDecel = 0.6f;
        speedMaximum = 3f;
	}
	
    public virtual void SetDesiredMoveVector(Vector3 desiredMoveVector)
    {
        forceMultiplier = desiredMoveVector.magnitude;
        forceDirection = desiredMoveVector.normalized;
    }
	public virtual void UpdateMotor(float deltaTime) {
        
        velocity += forceDirection*forceMultiplier*forceAccel*deltaTime;
        Vector3 velocityDir = velocity.normalized;
        velocity -= velocityDir*frictionDecel*deltaTime;
        float speed = velocity.magnitude;
        if( speed > speedMaximum )
        {
            velocity = speedMaximum*velocityDir;
        }
       
        Vector3 lastPosition = this.transform.position;
        this.transform.position = lastPosition + velocity*deltaTime; 
        
	}
    
    public virtual void SetPosition(Vector3 pos)
    {
        this.transform.position = pos;
    }
}
