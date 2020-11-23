using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithForce : MonoBehaviour
{
   public Rigidbody aereo;
    // Start is called before the first frame update
    void Start()
    {
        aereo = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
