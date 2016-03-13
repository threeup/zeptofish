using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class RuleConfig
{
    [ProtoMember(1)]
    public BoatConfig boatCfg;
    
    [ProtoMember(2)]
    public FishConfig[] fishCfg;
}