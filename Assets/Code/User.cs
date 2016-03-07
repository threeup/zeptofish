using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BasicCommon;

public class User : MonoBehaviour {

    public PawnController controller;
	public List<Spawner> spawners;
    public float spawnRate = 2f;
    public int spawnCount = 10;
    
    void Reset()
    {
        controller = this.GetComponent<PawnController>();
        spawners = new List<Spawner>(this.GetComponentsInChildren<Spawner>());
    }
	void Start () 
    {
        spawners = new List<Spawner>(this.GetComponentsInChildren<Spawner>());
	    for(int i=0; i<spawners.Count; ++i)
        {
            Spawner spawner = spawners[i];
            spawner.binding = controller.binding;
            spawner.remaining = 0;
            spawner.spawnTimer = new BasicTimer(spawnRate*spawners.Count);
            spawner.spawnTimer.ShiftOnce(i*spawnRate);
        }
        for(int i=0, j=0; i<spawnCount; ++i, ++j)
        {
            Spawner spawner = spawners[j];
            spawner.remaining++;
            if( j == spawners.Count-1 )
            {
                j = 0;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
