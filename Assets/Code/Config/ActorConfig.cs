using UnityEngine;
using ProtoBuf;


[ProtoContract]
public enum ActorType
{
    FOOD = 0,
    FISH = 1,
    HOOK = 2,
    BOAT = 3,
}

[ProtoContract]
public enum ActorSpecies
{
    FoodSmall = 0,
    FoodLarge = 1,
    
    FishSmall = 5,
    FishMediumAlpha = 6,
    FishMediumBravo = 7,
    FishLarge = 8,
    
    HookNormal = 25,
    BoatNormal = 30,
}

[ProtoContract]
public class ActorConfig
{
    [ProtoMember(1)]
    public string placeholder;
    [ProtoMember(2)]
    public ActorType atype;
    [ProtoMember(3)]
    public ActorSpecies aspecies;
    
    [ProtoMember(4)]
    public float scale;
    
    [ProtoMember(5)]
    public int hp;
    [ProtoMember(6)]
    public int size;
    [ProtoMember(7)]
    public int stomachCapacity;
    [ProtoMember(8)]
    public float stomachPeriod;
    
    [ProtoMember(9)]
    public float forceAccel;
    [ProtoMember(10)]
    public float frictionDecel;
    [ProtoMember(11)]
    public float rotationAccel;
    [ProtoMember(12)]
    public float speedMaximum;
    
    public ActorConfig()
    {
        scale = 1.0f;
        hp = 10;
        size = 10;
        stomachCapacity = 10;
        stomachPeriod = 10;
        forceAccel = 1.2f;
        frictionDecel = 0.6f;
        rotationAccel = 1.0f;
        speedMaximum = 3f;
    }
}
