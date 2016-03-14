/// File: GameStateManager.cs
/// Purpose: Handles the serialization and deserialization of game states

using UnityEngine;
using ProtoBuf;

/// <summary>
/// GameState serialization/deserialization
/// </summary>
public class ConfigManager : MonoBehaviour
{

    public RuleConfig currentConfig;
    
    public RuleConfigCategory category;
    

    public static ConfigManager Instance;
    
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        LoadConfig();
    }
    
    public void LoadConfig()
    {
        string configFilePath = ConfigUtils.GetFilePath(category);
        ConfigUtils.LoadConfig(configFilePath, out currentConfig);
    }
    
    public ActorConfig FindActorConfig(ActorSpecies aspecies)
    {
        return System.Array.Find(currentConfig.actorCfgs,x=>x.aspecies == aspecies);
    }

}
