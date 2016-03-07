[System.Serializable]
public enum ActorType
{
    FOOD = 0,
    FISH = 5,
    HOOK = 10,
    BOAT = 11,
}

[System.Serializable]
public struct ActorData {

    
    public ActorType atype;
    public int hp;
    public int speed;
    public int hunger;
    
    
}
