using UnityEngine;
using ProtoBuf;

// We need to utilize some features of System.IO for this
using System.IO;

public class ConfigUtils
{

    /// <summary>
    /// Saves a currentConfig to file
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
