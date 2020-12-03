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
    //aria
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
    }

    private void Update() //usato per controllare quando saltare, quando camminare e quando volare
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && isWalkingNotFlying)
        {
            Debug.Log("JumpMethod");
            Jump();
        }
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

    private bool IsGrounded()
    {
        throw new NotImplementedException();
    } //controlla se si sta toccando terra SERVE SOLO PER SALTARE

    private void Land()
    {
        //setta il rigidbody affinché cammini (attiva la gravità, spegne la pinna caudale, azzera la trasformata del modello del gabbiano)
        isWalkingNotFlying = true;
    }//fa atterrare il gabbiano

    private void WalkPhysics()
    {
        //proietta camera.forward e camera.right sul piano XZ, li normalizza, e li usa per applicare forze proporzionali agli axis horizontal e vertical.
        //
        throw new NotImplementedException();
    } //usa i controlli per far camminare il gabbiano con WASD

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
