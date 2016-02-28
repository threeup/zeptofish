using UnityEngine;
using System.Collections;

public class BoatMotor : Motor {

	public override void SetDesiredMoveVector(Vector3 desiredMoveVector)
    {
        desiredMoveVector.y = 0;
        desiredMoveVector.z = 0;
        forceMultiplier = desiredMoveVector.magnitude;
        forceDirection = desiredMoveVector.normalized;
    }
}
