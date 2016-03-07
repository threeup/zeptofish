using System.Collections;

[System.Serializable]
public enum ActorEventType
{
    INTERSECT,
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