using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtRmK : MonoBehaviour
{
    public Transform target;


    enum FacingDirection
    {
        UP = 270,
        DOWN = 90,
        LEFT = 180,
        RIGHT = 0
    }

    void Update()
    {
        LookAt2D(target, 17f, FacingDirection.RIGHT);
    }

    void LookAt2D(Transform theTarget, float theSpeed, FacingDirection facing)
    {
        Vector3 vectorToTarget = theTarget.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle -= (float)facing;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * theSpeed);
    }
}

