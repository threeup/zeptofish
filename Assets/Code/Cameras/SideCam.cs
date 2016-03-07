using UnityEngine;
using System.Collections;
using BasicCommon;

public class SideCam : SceneSingletonBehaviour<SideCam> {
    
    public Transform target;
    public Vector3 offset = Vector3.zero;
    
    void Awake()
    {
        _instance = this;
    }

   
	
	void Update () 
    {
        if( target != null )
        {
            this.transform.position = target.transform.position + offset;
        }
        else
        {
            this.transform.position = offset;
        }
	
    }
    
   
}
