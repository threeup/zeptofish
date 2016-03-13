using UnityEngine;
using ProtoBuf;

public enum RuleConfigCategory
{
    Easy,
    Hard,
}
    
[ProtoContract]
public class RuleConfig
{
    [ProtoMember(1)]
    public GameConfig gameCfg;
    
    [ProtoMember(2)]
    public LevelConfig[] levelCfgs;
    
    [ProtoMember(3)]
    public ActorConfig[] actorCfgs;
}