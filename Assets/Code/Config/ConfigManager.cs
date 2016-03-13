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
        
        // Set the file path to Unity's Streaming Assets folder, location in
        // Assets/StreamingAssets/
        string configFilePath = ConfigUtils.GetFilePath(category);
        ConfigUtils.LoadConfig(configFilePath, out currentConfig);
    }
    
    public ActorConfig FindActorConfig(string name)
    {
        foreach(ActorConfig acfg in currentConfig.actorCfgs)
        {
            if( string.Compare(acfg.name, name) == 0 )
            {
                return acfg;
            }
        }
        return null;
    }

}
