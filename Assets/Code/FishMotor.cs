using UnityEngine;
using System.Collections;

public class FishMotor : Motor {

    private float energy = 1f;
    private float maxEnergy = 4f;
    
    
    public enum MoveRate
    {
        REST,
        SWIM,
        SPRINT,
    }
    
    private MoveRate moveRate;
    
    public float sprintCost = 4f;
    public float swimCost = 2f;
    public float restCost = 3f;
    public float wiggleSpeed = 1f;
    
    public float restThreshold = 3f;
    
    public override void SetDesiredMoveVector(Vector3 desiredMoveVector)
    {
        float magnitude = desiredMoveVector.magnitude;
        bool actionAlpha = false;
        MoveRate desiredMoveRate = MoveRate.SWIM;
        if( magnitude < 0.05f )
        {
            desiredMoveRate = MoveRate.REST;
        }
        else if ( actionAlpha )
        {
            desiredMoveRate = MoveRate.SPRINT;
        }
        else
        {
            desiredMoveRate = MoveRate.SWIM;
        }
        switch(desiredMoveRate)
        {
            default:
            case MoveRate.SWIM: 
            case MoveRate.SPRINT: 
                if( energy < 0.01f )
                {
                    moveRate = MoveRate.REST;
                }
                else if( energy > restThreshold && moveRate == MoveRate.REST )
                {
                    moveRate = desiredMoveRate;
                }
                break;
            
            case MoveRate.REST: 
                moveRate = desiredMoveRate; 
                break;
        }
        forceDirection = desiredMoveVector;
        forceMultiplier = Mathf.Min(1f, desiredMoveVector.magnitude);
        switch(moveRate)
        {
            case MoveRate.SPRINT: 
                forceMultiplier *= 2f; 
                break;
            default:
            case MoveRate.SWIM: 
                forceMultiplier *= 1f; 
                break;
            case MoveRate.REST: 
                forceMultiplier *= 0.2f; 
                break;
        }
    }
    
    public override void UpdateMotor(float deltaTime) 
    {
        switch(moveRate)
        {
            case MoveRate.SPRINT: energy -= sprintCost*deltaTime; break;
            default:
            case MoveRate.SWIM: energy -= swimCost*deltaTime; break;
            case MoveRate.REST: energy += restCost*deltaTime; break;
        }
        energy = Mathf.Min(Mathf.Max(energy, 0f), maxEnergy);
        
        
        float dotp = Vector3.Dot(forceDirection, motorForward);
        float? forceAngle = null;
        if (forceMultiplier > 0.1f) 
        {
            forceAngle = Mathf.Atan2(forceDirection.y,forceDirection.x);
        }  
        float motorAngle = Mathf.Atan2(motorForward.y,motorForward.x);  
        float diffAngle = forceAngle.HasValue ? (forceAngle.Value - motorAngle) : 0f;
        if( diffAngle > Mathf.PI )
        {
            diffAngle -= Mathf.PI*2f;
        }
        if( diffAngle < -Mathf.PI )
        {
            diffAngle += Mathf.PI*2f;
        }
        


        /*if( Mathf.Abs(diffAngle) > Mathf.PI/2 )
        {
            velocity = Vector3.zero;
            Debug.Log ("Flip"+diffAngle);
            this.transform.Rotate(Vector3.up, (float)Mathf.PI);
            //mySequence = DOTween.Sequence();
            //mySequence.Append(transform.DOLookAt(thisTransform.position + backwards, duration, AxisConstraint.None));
        } 
        else*/
        {
            if( Mathf.Abs(diffAngle) > 0.2f )
            {
                rotationVelocity += diffAngle*rotationAccel*deltaTime;
                rotationVelocity = Mathf.Clamp(rotationVelocity, -rotationMaximum, rotationMaximum);
				
                this.transform.Rotate(rotationVelocity*Vector3.forward, Space.World);
            }
            else
            {
                rotationVelocity = 0;
            }
            RecomputeForward();
			if (forceMultiplier > 0.1f) 
			{
                velocity += motorForward * forceMultiplier * forceAccel * deltaTime;
			}
            Vector3 velocityDir = velocity.normalized;
            float dotVelocityMotor = Vector3.Dot(velocityDir, motorForward);
            float frictionNumber = 1.2f;
            friction = (frictionNumber-Mathf.Abs(dotVelocityMotor))/frictionNumber;
            velocity -= velocityDir*friction*frictionDecel*deltaTime;
            float speed = velocity.magnitude;
            if( speed > speedMaximum )
            {
                velocity = speedMaximum*velocityDir;
            }
        }
        
        
        MotorMove(deltaTime, velocity);
        RecomputeForward();
    } 
    
    
    void AddWiggle(float deltaTime)
    {
        body.transform.Rotate(Vector3.up, wiggleSpeed*deltaTime);
        float yaw = body.transform.rotation.eulerAngles.y;
        while( yaw > 90 )
        {
            yaw -= 180f;
        }
        while( yaw < -90 )
        {
            yaw += 180f;
        }
        float maxYaw = 15f;
        if( yaw > maxYaw )
        {
            wiggleSpeed = -Mathf.Abs(wiggleSpeed);
        }
        if( yaw < -maxYaw )
        {
            wiggleSpeed = Mathf.Abs(wiggleSpeed);
        }
    }
}
