using System.Collections;


public enum ActorEventType
{
    COLLIDE,
    NOISE,    
}
public class ActorEvent {

    public ActorEventType etype;
	public Actor source;
	public Actor target;
	public float magnitude;
    public float duration;
}
