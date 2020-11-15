using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkOnTheGround : MonoBehaviour
{

    Rigidbody rb;
    public Transform modelTransform;
    public Transform camera;
    public float walkSpeed = 10;
    public float turnSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
       // transform.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x,0, rb.velocity.z));
        rb.AddForce(GetWalkVector() * walkSpeed);
    }

    Vector3 GetAxisVector()
    {
        float walkH = Input.GetAxis("Horizontal");
        float walkV = Input.GetAxis("Vertical");
        return new Vector3(walkH, 0, walkV); //make this vector reorient itself relative ti the camera
    }

    Vector3 GetWalkVector()
    {
        Vector3 WalkVector = camera.rotation * GetAxisVector();
        WalkVector = new Vector3(WalkVector.x, 0, WalkVector.z);
        return WalkVector;
    }
}
