using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class GameConfig
{
    [ProtoMember(1)]
    public float energyScale;
    [ProtoMember(2)]
    public float speedScale;
}
