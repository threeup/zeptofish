using UnityEngine;
using System.Collections;
using BasicCommon;

public class Spawner : Actor {

    public GameObject spawnPrefab;
    public ControllerBinding binding;
    
    public BasicTimer spawnTimer = null;
    public int remaining = 1;
    public float scatterRadius = 0.5f;
    
	// Use this for initialization
	public override void Reset()
    {
	   base.Reset();
       binding = ControllerBinding.NONE;
       remaining = 1;
       attachBone = this.transform;
	}
    
	
	// Update is called once per frame
	public override void Update()
    { 
        float deltaTime = Time.deltaTime;
        if( remaining > 0 && spawnTimer != null && spawnTimer.Tick(deltaTime) )
        {
            ActorEvent ae = new ActorEvent(this, null, ActorEventType.SPAWN);
            HandleEvent(ref ae);
            
        }
    }
    
    public void OnDrawGizmos()
    {
        Color c = remaining > 0 ? new Color(1f,0.5f,0.2f,0.7f) : new Color(0.2f,0.2f,0.2f,0.1f); 
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, 1.1f);
    }
}
