using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {

    public Vector3 velocity;
    public Vector3 forceDirection;
    public float forceMultiplier;
    public float forceAccel = 1.2f;
    
    public float frictionDecel = 0.6f;
    
    public float speedMaximum = 3f;

	// Use this for initialization
	void Start () {
	
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
}
