using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelYawAndPitch : MonoBehaviour //QUESTA CLASSE RUOTA IL MODELLO IN BASE AI MOVIMENTI DEL RIGIDBODY QUANDO SI è IN ARIA
{
    public Rigidbody rb;
    private bool isGrounded = false;
    public float pitchSensitivity = 1f;
    public float rollSensitivity = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.eulerAngles = new Vector3(DeltaHeightToPitch(), transform.eulerAngles.y, DeltaYawToRoll());
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
