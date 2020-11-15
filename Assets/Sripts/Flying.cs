using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    public float movementSpeed = 20f;
    public float resetSpeed = 20f;
    public float shiftSpeed = 50f;
    public float controlSpeed = 50f;

    public float horizzontalSensitivity=2f;
    public float resetHorizontalSensitivity = 2;
    public float verticalSensitivity = 2f;
    public float resetVerticalSensitivity = 2f;

    private float yaw = 0f;
    private float pitch = 0f;

    public Animator getAnim;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        yaw += horizzontalSensitivity * Input.GetAxis("Mouse X");
        pitch -= verticalSensitivity * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = shiftSpeed;
        }
        else
        {
            movementSpeed = resetSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += transform.forward * Time.deltaTime * controlSpeed;
            getAnim.SetBool("Down", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += -transform.right * Time.deltaTime * controlSpeed;
            getAnim.SetBool("Left", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += -transform.forward * Time.deltaTime * controlSpeed;
            getAnim.SetBool("Up", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * Time.deltaTime * controlSpeed;
            getAnim.SetBool("Right", true);
        }

        if(Input.GetKey(KeyCode.W)&& !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            getAnim.SetBool("Forward", true);
            getAnim.SetBool("Down", false);
            getAnim.SetBool("Up", false);
            getAnim.SetBool("Left", false);
            getAnim.SetBool("Right", false);
        }
        else
        {
            getAnim.SetBool("Forward", false);
        }
    }
}
