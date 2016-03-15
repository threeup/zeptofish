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
    
    [ProtoMember(10)]
    public float scale;
    
    [ProtoMember(20)]
    public int hp;
    [ProtoMember(21)]
    public int minSize;
    [ProtoMember(22)]
    public int maxSize;
    [ProtoMember(23)]
    public int protein;
    [ProtoMember(24)]
    public int stomachCapacity;
    [ProtoMember(25)]
    public float stomachPeriod;
    
    [ProtoMember(30)]
    public float forceAccel;
    [ProtoMember(31)]
    public float frictionDecel;
    [ProtoMember(32)]
    public float rotationAccel;
    [ProtoMember(33)]
    public float speedMaximum;
    
    public ActorConfig()
    {
        scale = 1.0f;
        hp = 1;
        minSize = 1;
        maxSize = 1;
        protein = 1;
        stomachCapacity = 1;
        stomachPeriod = 1;
        forceAccel = 1.2f;
        frictionDecel = 1.0f;
        rotationAccel = 1.0f;
        speedMaximum = 3f;
    }
}
