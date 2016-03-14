using UnityEngine;
using System.Collections;
using BasicCommon;

public class Pawn : Actor {

    
    [SerializeField]
    private int stomachCapacity;
    [SerializeField]
    private float stomachPeriod;
    
    private BasicTimer stomachTimer;
    
    private BasicTimer biteTimer;
    
    [HideInInspector]
    public PawnController controller;
    
    [ReadOnly] [SerializeField]
    private PawnInputData lastInput;
    
    [ReadOnly] [SerializeField]
    private PawnInputData curInput;
    
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
    
    public override void Configure()
    {
        base.Configure();
        stomachPeriod = acfg.stomachPeriod;
        if( stomachPeriod > 0.01f)
        {
            stomachTimer = new BasicTimer(acfg.stomachPeriod);
        }
        stomachCapacity = acfg.stomachCapacity;
        ad.stomach = acfg.stomachCapacity;
        
        float biteTimerPeriod = 1f;//acfg.biteTimerPeriod;
        if( biteTimerPeriod > 0.01f)
        {
            biteTimer = new BasicTimer(biteTimerPeriod);
        }
        
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
        UpdateAttached(deltaTime);
        
        if( stomachTimer != null && stomachTimer.Tick(deltaTime) )
        {
            ad.stomach = Mathf.Max(ad.stomach-1,0);
            if( ad.stomach == 0)
            {
                ad.hp -= 1;
            }
        }
    }
    
    protected override void UpdateAttached(float deltaTime)
    {
        bool bite = attached.Count > 0 && biteTimer != null && biteTimer.Tick(deltaTime);
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
                if( bite )
                {
                    child.ad.hp -= 1;
                    if( child.ad.hp == 0)
                    {
                        ActorEvent ae = new ActorEvent(this, child, ActorEventType.EAT);
                        HandleEvent(ref ae);
                    }
                }
            }
        }
    }
    
    public void AddToStomach(int amount)
    {
        ad.stomach = Mathf.Min(ad.stomach+amount, stomachCapacity);
    }
    
    

}
