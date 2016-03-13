using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class LevelConfig
{
    [ProtoMember(1)]
    public int fishCount;
    [ProtoMember(2)]
    public int fishMinLevel;
    [ProtoMember(3)]
    public int fishMaxLevel;
}
