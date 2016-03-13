using UnityEngine;
using ProtoBuf;

// We need to utilize some features of System.IO for this
using System.IO;

public class ConfigUtils
{
    public static string GetFilePath(RuleConfigCategory category)
    {
        return Application.streamingAssetsPath + "/Config"+category.ToString()+".bytes";
    }

    public static void DefaultConfig(out RuleConfig cfg)
    {
        
        cfg = new RuleConfig();

        cfg.gameCfg = new GameConfig();
        cfg.actorCfgs = new ActorConfig[4];
        cfg.levelCfgs = new LevelConfig[3];
        
        
        cfg.gameCfg.energyScale = 1f;
        cfg.gameCfg.speedScale = 1f;
        

        cfg.levelCfgs[0] = new LevelConfig();
        cfg.levelCfgs[1] = new LevelConfig();
        cfg.levelCfgs[2] = new LevelConfig();
        
        cfg.actorCfgs[0] = new ActorConfig();
        cfg.actorCfgs[0].atype = ActorType.HOOK;
        cfg.actorCfgs[0].amodel = ActorModel.HookNormal;
        cfg.actorCfgs[0].hp = 20;
        cfg.actorCfgs[0].size = 20;
        
        cfg.actorCfgs[1] = new ActorConfig();
        cfg.actorCfgs[1].atype = ActorType.BOAT;
        cfg.actorCfgs[1].amodel = ActorModel.BoatNormal;
        cfg.actorCfgs[1].hp = 2000;
        cfg.actorCfgs[1].size = 50;
        
                
        cfg.actorCfgs[2] = new ActorConfig();
        cfg.actorCfgs[2].atype = ActorType.FOOD;
        cfg.actorCfgs[2].amodel = ActorModel.FoodSmall;
        cfg.actorCfgs[2].hp = 1;
        cfg.actorCfgs[2].size = 1;
        
        cfg.actorCfgs[3] = new ActorConfig();
        cfg.actorCfgs[3].atype = ActorType.FISH;
        cfg.actorCfgs[3].amodel = ActorModel.FishMediumAlpha;
        cfg.actorCfgs[3].hp = 10;
        cfg.actorCfgs[3].size = 10;
    }
    /// <summary>
    /// Saves a cfg to file
    /// </summary>
    public static void SaveConfig(string path, RuleConfig cfg)
    {

        // Open up a file stream for ProtoBuf to write to
        using (var fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            // Truncate the file, in case there's any data there from before
            fs.SetLength(0);
            
            // Serialize!
            Serializer.Serialize(fs, cfg);
        }

        Debug.Log("Config serialized to: " + path);
    }

    /// <summary>
    /// Loads a game state from file
    /// </summary>
    public static void LoadConfig(string path, out RuleConfig cfg)
    {
        // Break if file doesn't exit.
        if (!File.Exists(path))
        {
            Debug.LogWarning("Failed to load state, file doesn't exist.");
            cfg = new RuleConfig();
            return;
        }

        using (var fs = new FileStream(path, FileMode.Open))
        {
            cfg = Serializer.Deserialize<RuleConfig>(fs);
            Debug.Log("Config read from: " + path);
        }
    }
}
