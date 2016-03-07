using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rules {

	public static List<Rule> rules;
    public static Dictionary<int, Dictionary<int, Rule>> intersectMap;
    
    public static void Setup()
    {
        intersectMap = new Dictionary<int, Dictionary<int, Rule>>();
        
        Dictionary<int, Rule> fishDict = new Dictionary<int, Rule>();
        fishDict[(int)ActorType.FOOD] = new AttachRule();
        intersectMap[(int)ActorType.FISH] = fishDict;
        
        Dictionary<int, Rule> hookDict = new Dictionary<int, Rule>();
        hookDict[(int)ActorType.FISH] = new AttachRule();
        intersectMap[(int)ActorType.HOOK] = hookDict;
        
        Dictionary<int, Rule> boatDict = new Dictionary<int, Rule>();
        boatDict[(int)ActorType.HOOK] = new Rule();
        intersectMap[(int)ActorType.BOAT] = boatDict;
        
        
        rules = new List<Rule>();
        rules.Add(new Rule());
        rules.Add(new AttachRule());
        
    }
    
    public static Rule GetIntersect(EffectZone source, EffectZone victim)
    {
        if( source.eztype == EffectZoneType.ATTACHER &&
            victim.eztype == EffectZoneType.SURFACE )
        {
            int sourceKey = (int)source.OwnerActorType;
            int targetKey = (int)victim.OwnerActorType;
            
            if( intersectMap.ContainsKey(sourceKey) )
            {
                var ruleRow = intersectMap[sourceKey];
                if( ruleRow.ContainsKey(targetKey))
                {
                    return ruleRow[targetKey];
                }
            }
        }
        return null;
    }
    
    public static Rule GetGameRule(Actor source, Actor victim, ActorEventType aetype)
    {
        return rules[0];
    }
}

public class Rule {
    public virtual void Invoke(ActorEvent ae)
    {
        
    }
}

public class AttachRule : Rule {
    public override void Invoke(ActorEvent ae)
    {
        Vector3 offsetPos = ae.victim.AttachBonePosition - ae.source.AttachBonePosition;
        float offset = Utils.GetOffsetAngle(offsetPos, ae.source.transform.forward);
        ae.source.AttachToParent(ae.victim, offset);
    }
}
