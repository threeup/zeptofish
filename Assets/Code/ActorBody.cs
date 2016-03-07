using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorBody : MonoBehaviour 
{

    public List<GameObject> bodyParts;

     public Vector3 desiredScale = Vector3.one;
     
     public void Start()
     {
         for(int i=0; i<bodyParts.Count; ++i)
         {
             bodyParts[i].transform.parent = this.transform;
         }
     }
        
     public void SetFacing(bool right)
    {
        Vector3 realScale = desiredScale;
        realScale.y *= right ? 1f : -1f;
        this.transform.localScale = realScale;
    }
    
    public void SetCollidable(bool val)
    {
        
    }
}
