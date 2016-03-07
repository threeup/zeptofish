using UnityEngine;
using System.Collections;

public class Utils {

    public static float GetOffsetAngle(Vector3 offestPosition, Vector3 forward)
    {
        float offsetAngle = Mathf.Atan2(offestPosition.y, offestPosition.x);
        float forwardAngle = Mathf.Atan2(forward.y, forward.x);
        float angleDiff = 0;
        if( Mathf.Abs(forwardAngle) > Mathf.PI/2 )
        {
            angleDiff = forwardAngle - offsetAngle;
        }
        else
        {
            angleDiff = offsetAngle - forwardAngle;
        }

        while(angleDiff > Mathf.PI) { angleDiff -= Mathf.PI; }
        while(angleDiff < -Mathf.PI) { angleDiff += Mathf.PI; }
        return angleDiff;
    }
}
