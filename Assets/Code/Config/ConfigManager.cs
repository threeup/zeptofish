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
    
    /// <summary>
    /// The file path we will be using
    /// </summary>
    private string configFilePath;

    void Start()
    {
        // Set the file path to Unity's Streaming Assets folder, location in
        // Assets/StreamingAssets/
        configFilePath = Application.streamingAssetsPath + "/ConfigEasy.bytes";
        SetupConfig();
    }
    
    void SetupConfig()
    {
        currentConfig = new RuleConfig();

        currentConfig.fishCfg = new FishConfig[1];
        currentConfig.boatCfg = new BoatConfig();
        
        currentConfig.fishCfg[0] = new FishConfig();
        currentConfig.fishCfg[0].type = FishType.Wizard;
        currentConfig.fishCfg[0].speed = 3f;
        
        currentConfig.boatCfg.speed = UnityEngine.Random.Range(20, 40)/10f;
    }


    void OnGUI()
    {
        if(GUI.Button(new Rect(16, 256, 128, 64), "Save Game State"))
        {
            ConfigUtils.SaveConfig(configFilePath, currentConfig);
            Debug.Log("speed"+currentConfig.boatCfg.speed);
        }
        if (GUI.Button(new Rect(16, 336, 128, 64), "Load Game State"))
        {
            ConfigUtils.LoadConfig(configFilePath, out currentConfig);
            Debug.Log("speed"+currentConfig.boatCfg.speed);
        }
    }

}
