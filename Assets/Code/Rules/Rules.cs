using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rules {

	public static List<Rule> rules;
    
    public static Rule GetRule(ActorEventType etype, Actor source, Actor target)
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
        Vector3 offset = ae.target.attachPosition - ae.source.attachPosition;
        ae.target.AttachToParent(ae.source, offset);
        
    }
}
