
using UnityEngine;
using UnityEditor;
using System.Collections;

class ConfigEditor : EditorWindow {
    [MenuItem ("Window/ConfigEditor")]

    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(ConfigEditor));
    }
    public enum ConfigCategory
    {
        Easy,
        Hard,
    }
    ConfigCategory category;
    
    RuleConfig workingConfig;
    string myString;
    bool groupEnabled;
    bool myBool;
    float myFloat;
    
    void OnGUI () {
        
        category = (ConfigCategory)EditorGUILayout.EnumPopup("Cfg Category:", category);
        if (GUILayout.Button("Load "+category))
        {
            string configFilePath = Application.streamingAssetsPath + "/Config"+category.ToString()+".bytes";
            ConfigUtils.LoadConfig(configFilePath, out workingConfig);
        }   
        if( workingConfig != null )
        {
            if (GUILayout.Button("Save "+category))
            {
                string configFilePath = Application.streamingAssetsPath + "/Config"+category.ToString()+".bytes";
                ConfigUtils.SaveConfig(configFilePath, workingConfig);
            }
            GUILayout.Label ("WorkingConfig", EditorStyles.boldLabel);
            if(workingConfig.boatCfg != null)
            {
                BoatConfig cfg = workingConfig.boatCfg; 
                GUILayout.Label ("Boat", EditorStyles.boldLabel);
                cfg.speed = EditorGUILayout.Slider ("Speed", cfg.speed, -3, 3);
            }
            for(int i=0; i<workingConfig.fishCfg.Length; ++i)
            {
                FishConfig cfg = workingConfig.fishCfg[i]; 
                GUILayout.Label ("Fish"+cfg.type, EditorStyles.boldLabel);
                cfg.speed = EditorGUILayout.Slider ("Speed", cfg.speed, 1, 6);
            }
        }
    }
}