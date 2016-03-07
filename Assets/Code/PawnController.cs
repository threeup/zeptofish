using UnityEngine;
using System.Collections;

public class PawnController : MonoBehaviour {

    public enum ControllerBinding
    {
        AI_DRIFT = 0,
        AI_BRAIN = 1,
        KEYBOARD = 2,
        CONTROLLER0 = 3,   
    }
    public ControllerBinding binding;
    
    public PawnInputData GetInput()
    {
        PawnInputData curInput = new PawnInputData();
        switch(binding)
        {
            default:
            case ControllerBinding.AI_DRIFT: break;
            case ControllerBinding.AI_BRAIN: break;
            case ControllerBinding.KEYBOARD: GetKeyboardInput(ref curInput); break;
            case ControllerBinding.CONTROLLER0: GetControllerInput(0, ref curInput); break;
        }
        return curInput;
    }
    
    void GetKeyboardInput(ref PawnInputData curInput)
    {
        if( Input.GetKey(KeyCode.A) )
        {
            curInput.axisX -= 1f;
        }
        if( Input.GetKey(KeyCode.D) )
        {
            curInput.axisX += 1f;
        }
        if( Input.GetKey(KeyCode.W) )
        {
            curInput.axisY += 1f;
        }
        if( Input.GetKey(KeyCode.S) )
        {
            curInput.axisY -= 1f;
        }
        if( Input.GetKey(KeyCode.Space) )
        {
            curInput.alphaDown = true;
        }
    }
    
    void GetControllerInput(int controller, ref PawnInputData curInput)
    {
       
    }
}
