using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelYawAndPitch : MonoBehaviour //QUESTA CLASSE RUOTA IL MODELLO IN BASE AI MOVIMENTI DEL RIGIDBODY QUANDO SI è IN ARIA
{
    public Rigidbody rb;
    public float pitchSensitivity = 1f;
    public float rollSensitivity = 1f;
    private float transitionVar = 0f;
    public float transitionTimeInSeconds = 0.7f;

    void Start()
    {
        
    }

    void Update()
    {
        if (GetComponentInParent<PlayerMovement>().isWalkingNotFlying)
        {
            transitionVar += Time.deltaTime / transitionTimeInSeconds;  
            transform.rotation = Quaternion.Slerp(transform.rotation, rb.rotation, Mathf.SmoothStep(0,1, transitionVar));
        } else
        {
            transitionVar = 0f;
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
