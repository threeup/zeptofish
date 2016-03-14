
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
    
    bool[] actorFoldOut;
    
    string lastLoaded = "";
    
    bool resetConfirm = false;
    
    void OnGUI () {
        RuleConfigCategory nextCategory = (RuleConfigCategory)EditorGUILayout.EnumPopup("Cfg Category:", category); 
        if( nextCategory != category )
        {
            category = nextCategory;
            string configFilePath = ConfigUtils.GetFilePath(category);
            ConfigUtils.LoadConfig(configFilePath, out workingConfig);
            lastLoaded = category.ToString();
        }   
        if( resetConfirm )
        {
            if (GUILayout.Button("Yes Reset "+category))
            {
                ConfigUtils.DefaultConfig(out workingConfig);
                string configFilePath = Application.streamingAssetsPath + "/Config"+category.ToString()+".bytes";
                ConfigUtils.SaveConfig(configFilePath, workingConfig);
                lastLoaded = category.ToString();
                resetConfirm = false;
            }
            if (GUILayout.Button("No dont "+category))
            {
                resetConfirm = false;
            }
        }
        else
        {
            if (GUILayout.Button("Reset "+category))
            {
                resetConfirm = true;
            }
        }
        if( workingConfig != null )
        {
            if( actorFoldOut == null || actorFoldOut.Length != workingConfig.actorCfgs.Length)
            {
                actorFoldOut = new bool[workingConfig.actorCfgs.Length];
            }
            if (GUILayout.Button("Save "+category))
            {
                string configFilePath = Application.streamingAssetsPath + "/Config"+category.ToString()+".bytes";
                ConfigUtils.SaveConfig(configFilePath, workingConfig);
                lastLoaded = category.ToString();
            }
            
            for(int i=0; i<workingConfig.actorCfgs.Length; ++i)
            {
                ActorConfig cfg = workingConfig.actorCfgs[i]; 
                actorFoldOut[i] = EditorGUILayout.Foldout(actorFoldOut[i], "Actor "+cfg.aspecies);
                if( actorFoldOut[i])
                {
                    //cfg.name = EditorGUILayout.TextField("Actor:",cfg.name);
                    cfg.atype = (ActorType)EditorGUILayout.EnumPopup("AType:", cfg.atype);
                    //cfg.aspecies = (ActorSpecies)EditorGUILayout.EnumPopup("ActorSpecies:", cfg.aspecies);
                    cfg.scale = EditorGUILayout.Slider ("Scale:", cfg.scale, 0.01f, 6f);
                    cfg.hp = EditorGUILayout.IntField ("HP:", cfg.hp);
                    cfg.size = EditorGUILayout.IntField ("Size:", cfg.size);
                    cfg.stomachCapacity = EditorGUILayout.IntField ("Stomach", cfg.stomachCapacity);
                    cfg.stomachPeriod = EditorGUILayout.FloatField ("StomachPeriod", cfg.stomachPeriod);
                    cfg.forceAccel = EditorGUILayout.FloatField ("ForceAccel", cfg.forceAccel);
                    cfg.frictionDecel = EditorGUILayout.FloatField ("FrictionDecel", cfg.frictionDecel);
                    cfg.rotationAccel = EditorGUILayout.FloatField ("RotationAccel", cfg.rotationAccel);
                    cfg.speedMaximum = EditorGUILayout.FloatField ("SpeedMaximum", cfg.speedMaximum);
                }
            }
        }
        else
        {
            
        }
    }
}