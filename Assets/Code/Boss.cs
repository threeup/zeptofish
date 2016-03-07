using UnityEngine;
using System.Collections;
using BasicCommon;

public class Boss : SceneSingletonBehaviour<Boss> {
    
    
    void Awake()
    {
        _instance = this;
    }

    void Start () 
    {
	   Rules.Setup();
	}
	
	void Update () 
    {
	
	
    }
    
   
}
