/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController_Anto : MonoBehaviour
{   public float speed = 100f;
    Rigidbody player;

    //two kind of sesitivity rotation   
    public float sensitivity;
    public float yRotSpeed;

    Camera cam;

    //PauseMenu pauseMenu;
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
    {   
        
        //Start Pause-Menù DA METTERE IN UPDATE()
        if (!pauseMenu.isPaused)
        {
            cam.transform.localEulerAngles = new Vector3(camEulerAnglesX, 0, 0);
        }
        //JUMP ANCHE QUESTO IN UPDATE()
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("JumpMethod");
            jump();
        }

    }//[m] end FixedUpdate()

    public void changeSensitivity(float sensitivity) //DA TENERE
    {
        this.sensitivity = sensitivity;
    }//[m] end changeSensitivity(float sensitivity)

    void jump() //DA TENERE
    {
        player.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
    }//[m] end jump()

    bool isGrounded() //DA TRASFORMARE IN RAYCAST
    {
        if (Physics.OverlapBox(jumpCheck.transform.position, jumpCheckSize).Length > 0) //render questo un raycast che parte da sotto il collider del personaggio
        return true;
    }//[m] end isGrounded()
}
*/