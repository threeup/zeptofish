using UnityEngine;
using System.Collections;

public class Pawn : Actor {

    public PawnController controller;
    public PawnInputData lastInput;
    public PawnInputData curInput;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public override void Update()
    { 
        float deltaTime = Time.deltaTime;
        lastInput = curInput;
        curInput = controller.GetInput();
        Vector3 desiredMoveVector = new Vector3(curInput.axisX, curInput.axisY);
        motor.SetDesiredMoveVector(desiredMoveVector);
        
        motor.UpdateMotor(deltaTime);
    }
    
    

}
