using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //generali
    public Camera cam;
    public Rigidbody gabbiano;
    //PauseMenu pauseMenu; //scollegato
    private float viewSensitivity; //scollegato
    public bool isWalkingNotFlying;
    public bool canFly;
    //terra
    public float jumpForce = 100f;
    public float walkForce = 10f;
    public float feetOffset = 1f;
    //aria
    public GameObject backFin; //rigidbody che tiene il gabbiano dritto in volo
    public Vector3 dragBody;
    public float windInfluence = 1f;
    public float dragInfluence = 1f;
    public float gravity;
    public float dimPortanza;
    public float propulsion = 1000f;
    public float portanza;
    private Vector3 windTaken;
   

    void Start()
    {
        gabbiano = GetComponent<Rigidbody>();
        feetOffset = gabbiano.GetComponent<Collider>().bounds.size.z/2;
    }

    private void Update() //usato per controllare quando saltare, quando camminare e quando volare
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && isWalkingNotFlying)
        {
            Debug.Log("JumpMethod");
            Jump();
        }

        if (!IsGrounded() && Input.GetMouseButtonDown(0) && isWalkingNotFlying && canFly)
        {
            TakeOff();
        }

        Debug.Log("Is grounded " + IsGrounded() + "  is walking " + isWalkingNotFlying + "  can fly " + canFly);
    }

    void FixedUpdate()
        {
            if (isWalkingNotFlying)
            {
                WalkPhysics();
            }
            else
            {
                FlyPhysics();
            }          
        }  //dove vengono applicate tutte le forze tranne quella di salto

    private void Jump() //applica la forza di salto
    {
        gabbiano.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded() //controlla se si sta toccando terra SERVE SOLO PER SALTARE
    {
        Debug.DrawRay(transform.position - (Vector3.up * feetOffset), -Vector3.up);
        return Physics.Raycast(transform.position-(Vector3.up*feetOffset), -Vector3.up, 0.1f);
    } 

    private void Land() //fa atterrare il gabbiano
    {      
        gabbiano.useGravity = true;
        //backFin.SetActive(false); //spegne pinna caudale
        isWalkingNotFlying = true;
    }

    private void TakeOff() //serve davvero?
    {
        gabbiano.useGravity = false;
        //backFin.SetActive(true); //spegne pinna caudale
        isWalkingNotFlying = false;
    }

    private void WalkPhysics() //usa i controlli per far camminare il gabbiano con WASD
    {
        Vector3 forward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;   //proietta camera.forward e camera.right sul piano XZ, li normalizza.
        Vector3 right = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized;
        Vector3 walk = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");
        gabbiano.AddForce(walkForce * walk);//li usa per applicare forze proporzionali agli axis horizontal e vertical.
        
    } 

    private void FlyPhysics() //Quando si vola, questo metodo calcola la forza totale sul gabbiano.
    {
        Vector3 totalFlyForce = Vector3.zero;

        Vector3 flowVelocityG = -gabbiano.velocity + (windTaken * windInfluence); //https://en.wikipedia.org/wiki/Flow_velocity
        Vector3 flowVelocityL = gabbiano.transform.InverseTransformVector(flowVelocityG);
        Vector3 unsignedFlowVelocityL = new Vector3(Mathf.Abs(flowVelocityL.x), Mathf.Abs(flowVelocityL.y), Mathf.Abs(flowVelocityL.z));
        Vector3 dragForceL = Vector3.Scale(Vector3.Scale(flowVelocityL, unsignedFlowVelocityL), dragBody * dragInfluence);
        Vector3 dragForceG = gabbiano.transform.TransformVector(dragForceL);



        Vector3 verticalForce = Vector3.up * (portanza - dimPortanza - gravity);

        Vector3 forwardForce = Input.GetAxis("Fire1") * cam.transform.forward * propulsion;

        totalFlyForce = (dragForceG + verticalForce + forwardForce) * Time.fixedDeltaTime;

        gabbiano.AddForce(totalFlyForce);
    }

    private void OnTriggerEnter(Collider collision)  //aggiungi vento (AGGIUNGERE UNO SMOOTHING) ---- RILEVA QUANDO ATTERRARE E MEMORIZZA SE SI PUò DECOLLARE
    {
        if (collision.gameObject.GetComponent<Wind>())
        {
            windTaken = windTaken + collision.gameObject.GetComponent<Wind>().windForce;
        }

        if (collision.gameObject.GetComponent<LandingZone>())
        {
            canFly = collision.gameObject.GetComponent<LandingZone>().canFly;
            Land();
        }
    }

    private void OnTriggerExit(Collider collision) //togli vento
    {
        if (collision.gameObject.GetComponent<Wind>())
        {
            windTaken = windTaken - collision.gameObject.GetComponent<Wind>().windForce;
        }
    }

}
