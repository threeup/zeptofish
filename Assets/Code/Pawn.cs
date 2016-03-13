using UnityEngine;
using System.Collections;
using BasicCommon;

public class Pawn : Actor {

    
    [SerializeField]
    private int stomachCapacity;
    [SerializeField]
    private int stomach;
    [SerializeField]
    private float stomachPeriod;
    
    private BasicTimer stomachTimer;
    
    [HideInInspector]
    public PawnController controller;
    
    public PawnInputData lastInput;
    public PawnInputData curInput;
    
	// Use this for initialization
	public override void Reset()
    {
	   base.Reset();
	}
    
    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<PawnController>();
        
    }
    
    public override void Launch()
    {
        stomachPeriod = acfg.stomachPeriod;
        if( stomachPeriod > 0.01f)
        {
            stomachTimer = new BasicTimer(acfg.stomachPeriod);
        }
        stomachCapacity = acfg.stomachCapacity;
        stomach = stomachCapacity;
        
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
        UpdateAttached();
        
        if( stomachTimer != null && stomachTimer.Tick(deltaTime) )
        {
            ad.stomach = Mathf.Max(ad.stomach-1,0);
            if( ad.stomach == 0)
            {
                ad.hp -= 1;
            }
        }
    }
    
    
    public void AddToStomach(int amount)
    {
        ad.stomach = Mathf.Min(ad.stomach+amount, stomachCapacity);
    }
    
    

}
