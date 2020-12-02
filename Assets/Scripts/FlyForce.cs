using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyForce : MonoBehaviour
{   
    public Camera cam;
    public Rigidbody gabbiano;
    public Vector3 dragBody;
    public float windInfluence = 1f;
    public float dragInfluence = 1f;
    public float gravity;
    public float dimPortanza;
    public float propulsion = 1000f;
    public float portanza;
    private Vector3 windTaken;
    // Start is called before the first frame update
    void Start()
    {
        gabbiano = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 totalForce = Vector3.zero;
        
        Vector3 flowVelocityG = - gabbiano.velocity + (windTaken * windInfluence); //https://en.wikipedia.org/wiki/Flow_velocity
        Vector3 flowVelocityL = gabbiano.transform.InverseTransformVector(flowVelocityG);
        Vector3 unsignedFlowVelocityL = new Vector3(Mathf.Abs(flowVelocityL.x), Mathf.Abs(flowVelocityL.y), Mathf.Abs(flowVelocityL.z));
        Vector3 dragForceL = Vector3.Scale(Vector3.Scale(flowVelocityL, unsignedFlowVelocityL), dragBody*dragInfluence);
        Vector3 dragForceG = gabbiano.transform.TransformVector(dragForceL);



        Vector3 verticalForce = Vector3.up * (portanza - dimPortanza - gravity);

        Vector3 forwardForce = Input.GetAxis("Fire1") * cam.transform.forward * propulsion;


        totalForce = (dragForceG + verticalForce + forwardForce) * Time.fixedDeltaTime;


        Debug.Log(dragForceG);



        gabbiano.AddForce(totalForce);

         
    }
    
    private void OnTriggerEnter(Collider collision )
    {
        if (collision.gameObject.GetComponent<Wind>())
        {
            windTaken = windTaken + collision.gameObject.GetComponent<Wind>().windForce;
            Debug.Log("dentro vento");
            Debug.Log(windTaken);
        }
       
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Wind>())
        {
            windTaken = windTaken - collision.gameObject.GetComponent<Wind>().windForce;
        }
    }
}
