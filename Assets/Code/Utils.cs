using UnityEngine;
using System.Collections;

public class Utils {

    public static float GetOffsetAngle(Vector3 offsetPosition, Vector3 forward)
    {
        float offsetAngle = Mathf.Atan2(offsetPosition.y, offsetPosition.x);
        float forwardAngle = Mathf.Atan2(forward.y, forward.x);
        float angleDiff = forwardAngle - offsetAngle;

        return Sanitize(angleDiff);
    }
    
    public static float Sanitize(float angleDiff) 
    {
        while(angleDiff > Mathf.PI) { angleDiff -= 2*Mathf.PI; }
        while(angleDiff < -Mathf.PI) { angleDiff += 2*Mathf.PI; }
        return angleDiff;
    }
    
    public static float RandomAngle()
    {
        return UnityEngine.Random.Range(0f,360f);
    }
}
