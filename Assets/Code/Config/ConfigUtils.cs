using UnityEngine;
using System.Collections.Generic;
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
        
        cfg.levelCfgs = new LevelConfig[3];
        
        
        cfg.gameCfg.energyScale = 1f;
        cfg.gameCfg.speedScale = 1f;
        

        cfg.levelCfgs[0] = new LevelConfig();
        cfg.levelCfgs[1] = new LevelConfig();
        cfg.levelCfgs[2] = new LevelConfig();
        
        List<ActorConfig> actorCfgList = new List<ActorConfig>();
        var speciesVals = System.Enum.GetValues(typeof(ActorSpecies));
        foreach(var species in speciesVals)
        {
            ActorConfig acfg = new ActorConfig();
            acfg.aspecies = (ActorSpecies)species;
            string speciesName = acfg.aspecies.ToString().ToLower();
            if( speciesName.StartsWith("fish") )
            {
                acfg.atype = ActorType.FISH;
            } 
            else if( speciesName.StartsWith("hook") )
            {
                acfg.atype = ActorType.HOOK;
            }
            if( speciesName.StartsWith("boat") )
            {
                acfg.atype = ActorType.BOAT;
            }
            actorCfgList.Add(acfg);
        }
        
        ActorConfig hookCfg = actorCfgList.Find(x=>x.aspecies == ActorSpecies.HookNormal);
        hookCfg.atype = ActorType.HOOK;
        hookCfg.hp = 20;
        
        ActorConfig boatCfg = actorCfgList.Find(x=>x.aspecies == ActorSpecies.BoatNormal);
        boatCfg.atype = ActorType.BOAT;
        boatCfg.hp = 2000;
               
        cfg.actorCfgs = actorCfgList.ToArray(); 
                
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
