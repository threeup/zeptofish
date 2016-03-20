using UnityEngine;
using System.Collections;
using BasicCommon;

public class Spawner : MonoBehaviour {

    private Actor actor;
    public GameObject actorPrefab;
    public ActorSpecies aspecies;
    public World.LiveableArea liveableArea;
    private ControllerBinding binding;
    public ControllerBinding Binding { get { return binding; } }
    
    private BasicTimer spawnTimer = null;
    
    [ReadOnly] [SerializeField] 
    private int remaining = 1;
    
    public float scatterRadius = 0.5f;
    
	// Use this for initialization
	public void Reset()
    {
       binding = ControllerBinding.NONE;
       remaining = 1;
	}
    
    void Awake()
    {
        actor = GetComponent<Actor>();
    }
    
    public void Setup(ControllerBinding binding, int remaining, float period, float offset)
    {
        this.binding = binding;
        this.remaining = remaining;
        this.spawnTimer = new BasicTimer(period);
        this.spawnTimer.ShiftOnce(offset);
    }
    
    public void ModifyRemaining(int val)
    {
        this.remaining += val;
    }
	
	// Update is called once per frame
	public void Update()
    { 
        float deltaTime = Time.deltaTime;
        if( remaining > 0 && spawnTimer != null && spawnTimer.Tick(deltaTime) )
        {
            ActorConfig acfg = ConfigManager.Instance.FindActorConfig(this.aspecies);
            World.Instance.Spawn(this, acfg, GetSpawnPosition(), transform.forward).GetComponent<Actor>();
        }
    }
        
    public void OnDrawGizmos()
    {
        Color c = remaining > 0 ? new Color(1f,0.5f,0.2f,0.7f) : new Color(0.2f,0.2f,0.2f,0.1f); 
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, 1.1f);
        Gizmos.DrawCube(transform.position+transform.forward, new Vector3(1, 1, 1));
    }
    
    public Vector3 GetSpawnPosition()
    {
        Vector3 spawnPos = this.transform.position;
        float randomAngle = Utils.RandomAngle();
        spawnPos.x += Mathf.Sin(randomAngle)*UnityEngine.Random.Range(0f,scatterRadius);
        spawnPos.y += Mathf.Cos(randomAngle)*UnityEngine.Random.Range(0f,scatterRadius);
        return spawnPos;
    }
}
