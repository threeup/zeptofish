using UnityEngine;
using ProtoBuf;

[ProtoContract]
public enum FishType
{
    Wizard = 0,
    Troll = 1,
    Orc = 2,
    Pepe = 3
}

[ProtoContract]
public class FishConfig
{
    [ProtoMember(1)]
    public FishType type;
    [ProtoMember(2)]
    public float speed;
    [ProtoMember(3)]
    public float size;
}
