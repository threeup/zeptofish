using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class BoatConfig
{
    [ProtoMember(1)]
    public int nothing;
    [ProtoMember(2)]
    public float speed;
    [ProtoMember(3)]
    public float size;
}
