using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rules {

	public static Dictionary<int, Rule> genericRules;
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
        
        
        genericRules = new Dictionary<int, Rule>();
        genericRules[(int)ActorEventType.INTERSECT] = new AttachRule();
        genericRules[(int)ActorEventType.SPAWNED] = new SpawnedRule();
        genericRules[(int)ActorEventType.EAT] = new EatRule();
        genericRules[(int)ActorEventType.NOISE] = new Rule();
        
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
    
    
    public static Rule GetGameRule(ActorEventType aetype)
    {
        return genericRules[(int)aetype];
    }
    
    public static void ProcessEvent(ref ActorEvent ae)
    {
        Rule r = GetGameRule(ae.aetype);
        r.Invoke(ref ae);
    }
}

public class Rule {
    public virtual void Invoke(ref ActorEvent ae)
    {
        
    }
}

public class AttachRule : Rule {
    public override void Invoke(ref ActorEvent ae)
    {
        Vector3 offsetPos = ae.victim.AttachBonePosition - ae.source.AttachBonePosition;
        float offset = Utils.GetOffsetAngle(offsetPos, ae.source.transform.forward);
        ae.source.AttachToParent(ae.victim, offset);
    }
}

public class SpawnedRule : Rule {
    public override void Invoke(ref ActorEvent ae)
    {
        ae.source.BroadcastMessage("Configure");
    }
}

public class EatRule : Rule {
    public override void Invoke(ref ActorEvent ae)
    {
        ae.source.ad.stomach += ae.victim.acfg.protein;
        while( ae.source.ad.stomach > ae.source.acfg.stomachCapacity )
        {
            if(ae.source.ad.size < ae.source.acfg.maxSize)
            {
                ae.source.ad.stomach -= ae.source.acfg.stomachCapacity;
                ae.source.LevelUp();
            }
            else
            {
                ae.source.ad.stomach = ae.source.acfg.stomachCapacity;
                break;
            }
        }
        ae.victim.Die();
    }
}