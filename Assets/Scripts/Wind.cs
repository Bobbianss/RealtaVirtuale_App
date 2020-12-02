using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Vector3 windForce = Vector3.zero;

    private void Update()
    {
        Debug.DrawRay(transform.position, windForce);
    }


}
