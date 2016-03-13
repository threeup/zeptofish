
using UnityEngine;
using UnityEditor;
using System.Collections;

class ConfigEditor : EditorWindow {
    [MenuItem ("Window/ConfigEditor")]

    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(ConfigEditor));
    }

    RuleConfigCategory category;
    
    RuleConfig workingConfig;
    
    void OnGUI () {
        
        category = (RuleConfigCategory)EditorGUILayout.EnumPopup("Cfg Category:", category);
        if (GUILayout.Button("Load "+category))
        {
            string configFilePath = ConfigUtils.GetFilePath(category);
            ConfigUtils.LoadConfig(configFilePath, out workingConfig);
        }   
        if (GUILayout.Button("Reset "+category))
        {
            ConfigUtils.DefaultConfig(out workingConfig);
            string configFilePath = Application.streamingAssetsPath + "/Config"+category.ToString()+".bytes";
            ConfigUtils.SaveConfig(configFilePath, workingConfig);
        }
        if( workingConfig != null )
        {
            if (GUILayout.Button("Save "+category))
            {
                string configFilePath = Application.streamingAssetsPath + "/Config"+category.ToString()+".bytes";
                ConfigUtils.SaveConfig(configFilePath, workingConfig);
            }
            GUILayout.Label ("Working", EditorStyles.boldLabel);
            for(int i=0; i<workingConfig.actorCfgs.Length; ++i)
            {
                ActorConfig cfg = workingConfig.actorCfgs[i]; 
                GUILayout.Label ("Actor "+cfg.atype, EditorStyles.boldLabel);
                cfg.scale = EditorGUILayout.Slider ("Speed", cfg.scale, 0.01f, 6f);
                cfg.hp = EditorGUILayout.DelayedIntField ("HP", cfg.hp);
                cfg.size = EditorGUILayout.DelayedIntField ("Size", cfg.size);
                cfg.stomachCapacity = EditorGUILayout.DelayedIntField ("Stomach", cfg.stomachCapacity);
                cfg.stomachPeriod = EditorGUILayout.DelayedFloatField ("stomachPeriod", cfg.stomachPeriod);
                cfg.hp = EditorGUILayout.DelayedIntField ("HP", cfg.hp);
            }
        }
        else
        {
            
        }
    }
}