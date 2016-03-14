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
            spawner.Setup(controller.binding,0,spawnRate*spawners.Count,i*spawnRate);

        }
        for(int i=0, j=0; i<spawnCount; ++i, ++j)
        {
            Spawner spawner = spawners[j];
            spawner.ModifyRemaining(1);
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
