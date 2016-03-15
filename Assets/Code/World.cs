using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BasicCommon;

public class World : SceneSingletonBehaviour<World> {
    
    public enum LiveableArea
    {
        SURFACE,
        HOOK,
        WATER,
    }

    public Bounds waterBounds;
    public Bounds hookBounds;
    public Bounds surfaceBounds;
    
    private List<Actor> activeActors = new List<Actor>();
    public List<Actor> ActiveActors { get { return activeActors; } }

    
    void Awake()
    {
        _instance = this;
    }

    public Bounds GetLiveableArea(LiveableArea areaType)
    {
        switch(areaType)
        {
            case LiveableArea.SURFACE: return surfaceBounds;
            case LiveableArea.HOOK: return hookBounds;
            case LiveableArea.WATER: return waterBounds;
        }
        return new Bounds(Vector3.zero, Vector3.zero);
    }

    public GameObject Spawn(Spawner spawner, Vector3 pos, Vector3 forward)
    {
        Quaternion rot = Quaternion.LookRotation(forward, Vector3.up);
        GameObject go = GameObject.Instantiate(spawner.actorPrefab, pos, rot) as GameObject;
        spawner.ModifyRemaining(-1);
        Actor actor = go.GetComponent<Actor>();
        actor.liveableArea = spawner.liveableArea;
        PawnController pawnController = go.GetComponent<PawnController>();
        if( pawnController != null )
        {
            pawnController.binding = spawner.Binding;
        }
        activeActors.Add(actor);
        return go;
    }
    
    public void Despawn(Actor actor)
    {
        activeActors.Remove(actor);
        Destroy(actor.gameObject);
    }
}
