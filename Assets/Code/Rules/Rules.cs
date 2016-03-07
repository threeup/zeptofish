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
        genericRules[(int)ActorEventType.SPAWN] = new SpawnRule();
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

public class SpawnRule : Rule {
    public override void Invoke(ref ActorEvent ae)
    {
        Spawner sourceSpawner = ae.source as Spawner;
        Vector3 origin = sourceSpawner.AttachBonePosition;
        float randomAngle = Utils.RandomAngle();
        origin.x += Mathf.Sin(randomAngle)*UnityEngine.Random.Range(0f,sourceSpawner.scatterRadius);
        origin.y += Mathf.Cos(randomAngle)*UnityEngine.Random.Range(0f,sourceSpawner.scatterRadius);
        Vector3 forward = sourceSpawner.transform.forward;
        ae.victim = World.Instance.Spawn(sourceSpawner, origin, forward).GetComponent<Actor>();
    }
}
