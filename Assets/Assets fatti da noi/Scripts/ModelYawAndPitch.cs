using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelYawAndPitch : MonoBehaviour //QUESTA CLASSE RUOTA IL MODELLO IN BASE AI MOVIMENTI DEL RIGIDBODY QUANDO SI è IN ARIA,
											  //E PER COMODITà ADESSO CONTROLLA ANCHE IL PARAMETRO INCLINAZIONE DELL'ANIMAZIONE!
											  //E ADESSO ANCHE IL PAMETRO SPINTA! WOOOO
{
	//roba dell'animatore
	Animator animator;
	public float animationRollSensitivity;
	public float animationPushSensitivity;
	public float walkSpeedSensitivity;
	float pushParam = 0f;
	//roba del rigidbody
	public Rigidbody rb;
    public float pitchSensitivity = 1f;
    public float rollSensitivity = 1f;
    private float transitionVar = 0f;
    public float transitionTimeInSeconds = 0.7f;
	

    void Start()
    {
		animator = GetComponentInChildren<Animator>();
	}

    void Update()
    {
		if (GetComponentInParent<PlayerMovement>().isWalkingNotFlying)
        {
            transitionVar += Time.deltaTime / transitionTimeInSeconds;  
            transform.rotation = Quaternion.Slerp(transform.rotation, rb.rotation, Mathf.SmoothStep(0,1, transitionVar));
			UpdateWalkAnimParameter();
        } else
        {
            transitionVar = 0f;
            transform.eulerAngles = new Vector3(DeltaHeightToPitch(), transform.eulerAngles.y, DeltaYawToRoll());
			UpdatePushAnimParameter();
		}
		animator.SetBool("A terra", GetComponentInParent<PlayerMovement>().isWalkingNotFlying);
    }

    private float DeltaHeightToPitch() 
    {
       return rb.velocity.y * pitchSensitivity * -1f;
    }

    private float DeltaYawToRoll() //leggera porcata: il parametro "Inclinazione" viene inviato da qui
    {
		animator.SetFloat("Inclinazione", 0.5f + rb.angularVelocity.y * animationRollSensitivity);
		return rb.angularVelocity.y * rollSensitivity * -1f;

    }

	private void UpdatePushAnimParameter()
	{
		if (Input.GetMouseButton(0)==true)
		{
			if(pushParam <= 1f)
			{
				pushParam += animationPushSensitivity * 0.01f;
			}
		}
		else
		{
			if (pushParam >= 0f)
			{
				pushParam -= animationPushSensitivity * 0.01f;
			}
		}

		animator.SetFloat("Spinta", pushParam);
	}

	private void UpdateWalkAnimParameter()
	{
		animator.SetFloat("Velocità camminata", rb.velocity.magnitude * walkSpeedSensitivity);
	}

}
