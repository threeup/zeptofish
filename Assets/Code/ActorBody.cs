using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorBody : MonoBehaviour 
{
    public Actor actor;

    public List<GameObject> bodyParts;

    //public Vector3 desiredScale = Vector3.one;

    public void Start()
    {
        for(int i=0; i<bodyParts.Count; ++i)
        {
            bodyParts[i].transform.parent = this.transform;
        }
    }

    public void SetFacing(bool right)
    {
        //Vector3 realScale = desiredScale;
        //realScale.y *= right ? 1f : -1f;
        //this.transform.localScale = realScale;
    }

    public void SetCollidable(bool val)
    {

    }

    public void UpdateScale()
    {
        int minToMax = actor.acfg.maxSize - actor.acfg.minSize;
        
        float fromMiddle = actor.ad.size - minToMax/2.0f;
        
        float relativeScale = 1.0f+fromMiddle*0.25f;
        
        this.transform.localScale = Vector3.one*relativeScale;
    }
}
