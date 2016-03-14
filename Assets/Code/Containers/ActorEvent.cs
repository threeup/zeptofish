using System.Collections;

[System.Serializable]
public enum ControllerBinding
{
    NONE = 0,
    AI_DRIFT = 1,
    AI_BRAIN = 2,
    KEYBOARD = 3,
    CONTROLLER0 = 4,   
    CONTROLLER1 = 5,   
}
    
[System.Serializable]
public enum ActorEventType
{
    INTERSECT,
    SPAWN,
    EAT,
    NOISE,    
}
[System.Serializable]
public class ActorEvent {

    public ActorEventType aetype;
	public Actor source;
	public Actor victim;
	public float magnitude;
    public float duration;
    
    public ActorEvent(Actor source, Actor victim, ActorEventType aetype)
    {
        this.source = source;
        this.victim = victim;
        this.aetype = aetype;
    }
}