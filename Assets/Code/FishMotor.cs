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
                forceMultiplier *= 0f; 
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
        base.UpdateMotor(deltaTime);
    } 
}
