using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float windModulus;

    private Vector3 windForce;

    public Vector3 WindForce()
    {
        return windForce = transform.TransformDirection(Vector3.forward * windModulus);
    }

}
