using UnityEngine;
using System.Collections;
using BasicCommon;

public class ActionCam : SceneSingletonBehaviour<ActionCam> {
    
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
            this.transform.rotation = Quaternion.LookRotation(target.transform.position - this.transform.position, Vector3.up);
        }
	
    }
    
   
}
