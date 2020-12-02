using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour
{   public float speed = 100f;
    Rigidbody player;

    //two kind of sesitivity rotation   
    public float sensitivity;
    public float yRotSpeed;

    Camera cam;

    PauseMenu pauseMenu;
    public GameObject jumpCheck;
    public Vector3 jumpCheckSize;
    public float jumpForce = 100f;

    
    void Start()
    {
        player = GetComponent<Rigidbody>();
        cam = Camera.main;
        pauseMenu = FindObjectOfType<PauseMenu>();

        //When you start hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }// [m] Start()

    // Update is called once per frame
    void FixedUpdate()
    {   //Movement
        Vector2 xMov = new Vector2(Input.GetAxisRaw("Horizontal") * transform.right.x, Input.GetAxisRaw("Horizontal") * transform.right.z);
        Vector2 zMov = new Vector2(Input.GetAxisRaw("Vertical") * transform.forward.x, Input.GetAxisRaw("Vertical") * transform.forward.z);

        //Velocity
        Vector2 velocity = (xMov + zMov).normalized * speed * Time.fixedDeltaTime;

        //Rotation player around X-Axis
        float yRot = Input.GetAxisRaw("Mouse X") * yRotSpeed;
        player.rotation *= Quaternion.Euler(0, yRot * Time.fixedDeltaTime, 0);
        //Rotation camera around Y-Axis
        float xRot = Input.GetAxisRaw("Mouse Y") * sensitivity;
        float x_cam_rot = cam.transform.eulerAngles.x;

        float camEulerAnglesX = cam.transform.localEulerAngles.x;

        camEulerAnglesX -= xRot;

        //remove the glitch
        if (camEulerAnglesX < 180)
        {
            camEulerAnglesX += 360;
        }

        //Start Pause-Menù
        if (!pauseMenu.isPaused)
        {
            cam.transform.localEulerAngles = new Vector3(camEulerAnglesX, 0, 0);
        }
        //JUMP
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("JumpMethod");
            jump();
        }

        //setUp Velocity
        player.velocity = new Vector3(velocity.x, player.velocity.y, velocity.y);
    }//[m] end FixedUpdate()

    public void changeSensitivity(float sensitivity)
    {
        this.sensitivity = sensitivity;
    }//[m] end changeSensitivity(float sensitivity)

    void jump()
    {
        player.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
    }//[m] end jump()

    bool isGrounded()
    {
        if (Physics.OverlapBox(jumpCheck.transform.position, jumpCheckSize).Length > 0);
        return true;
    }//[m] end isGrounded()
}
