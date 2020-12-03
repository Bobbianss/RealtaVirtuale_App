using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelYawAndPitch : MonoBehaviour //QUESTA CLASSE RUOTA IL MODELLO IN BASE AI MOVIMENTI DEL RIGIDBODY QUANDO SI è IN ARIA
{
    public Rigidbody rb;
    public float pitchSensitivity = 1f;
    public float rollSensitivity = 1f;
    public float smoothSpeed = 0.5f;
    private Vector3 smoothVar = Vector3.zero;
    void Start()
    {
        
    }

    void Update()
    {
        if (GetComponentInParent<PlayerMovement>().isWalkingNotFlying)
        {
            Vector3.SmoothDamp(transform.eulerAngles, Vector3.zero, ref smoothVar, smoothSpeed);
        } else
        {
            transform.eulerAngles = new Vector3(DeltaHeightToPitch(), transform.eulerAngles.y, DeltaYawToRoll());
        }
        
    }

    private float DeltaHeightToPitch()
    {
       return rb.velocity.y * pitchSensitivity * -1f;
    }

    private float DeltaYawToRoll()
    {
        return rb.angularVelocity.y * rollSensitivity * -1f;
    }
}
