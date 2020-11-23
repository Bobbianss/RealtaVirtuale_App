using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyForce : MonoBehaviour
{   
    public Camera cam;
    public Rigidbody gabbiano;
    public Vector3 dragBody;
    public float windInfluence;
    public float gravity;
    public float dimPortanza;
    public float propulsion;
    public float portanza;
    private Vector3 totalForce;
    private Vector3 windTaken;
    // Start is called before the first frame update
    void Start()
    {
        gabbiano = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Vector3 dragVelocity = gabbiano.velocity + windTaken*windInfluence;
        Vector3 squareDragVelocity = Vector3.Scale(dragVelocity, dragVelocity); 

               
       Vector3 dragForce = Vector3.Scale(squareDragVelocity, gabbiano.transform.TransformDirection(dragBody));
       Vector3 verticalForce = Vector3.up * (portanza - dimPortanza - gravity);

       Vector3 forwardForce = cam.transform.forward * Input.GetAxis("Vertical") * propulsion ;

       Vector3 totalForce = (dragForce + verticalForce + forwardForce) * Time.fixedDeltaTime;  


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
