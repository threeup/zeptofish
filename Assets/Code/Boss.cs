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
    
    public void ReloadConfig()
    {
        ConfigManager.Instance.LoadConfig();
        foreach(Actor a in World.Instance.ActiveActors)
        {
            ActorSpecies oldSpecies = a.acfg.aspecies;
            a.acfg = ConfigManager.Instance.FindActorConfig(oldSpecies);
            a.BroadcastMessage("Configure");
        }
    }
   
}
