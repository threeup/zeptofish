using UnityEngine;
using System.Collections;
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
        GameObject go = GameObject.Instantiate(spawner.spawnPrefab, pos, rot) as GameObject;
        spawner.remaining--;
        PawnController pawnController = go.GetComponent<PawnController>();
        if( pawnController != null )
        {
            pawnController.binding = spawner.binding;
        }
        return go;
    }
}
