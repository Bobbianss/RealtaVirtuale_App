using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Movement")]
    public float playerSpeed;
    public float jumpForce;
    [HideInInspector]
    public Rigidbody rb;

    Vector3 x, z;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    void Update()
    {
        //-----Inputs-----//
        x = transform.right * Input.GetAxisRaw("Horizontal");
        z = transform.forward * Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 movement = (x + z).normalized * playerSpeed;
        rb.AddForce(movement);
        
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
