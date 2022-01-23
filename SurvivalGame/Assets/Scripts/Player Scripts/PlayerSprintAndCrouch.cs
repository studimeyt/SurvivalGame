using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private CharacterController characterController;

    public float sprint_speed = 8f;
    public float move_speed = 4f;
    public float crouch_speed = 1.5f;
    public float LyingOnGround_speed = 1f; //for sniping maybe
    
    private Transform look_root;
    private float stand_height = 1.6f;
    private float crouch_height = 1f;
    private float LyingOnGround_height = 1f;

    private bool is_crouching;
    private bool is_lyingOnGround;
    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        look_root = transform.GetChild(0);
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded == true)
        {
            Sprint();
            Crouch();
        }

        else
        {
            playerMovement.speed = move_speed;
        }
        
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !is_crouching )
        {
            playerMovement.speed = sprint_speed;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && !is_crouching )
        {
            playerMovement.speed = move_speed;
        }
    }//sprint

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (is_crouching)
            {
                //if already crouching- stand up
                look_root.localPosition = new Vector3(0f, stand_height, 0f);
            }
            else
            {
                // if not crouching - crouch
                look_root.localPosition = new Vector3(0f, crouch_height, 0f);
            }
        }// if we press L.Ctrl

    }//crouch

    void LyingOnGround()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (is_lyingOnGround)
            {
                //if already - stand up
                look_root.localPosition = new Vector3(0f, stand_height, 0f);
            }
            else
            {
                // if not crouching - crouch
                look_root.localPosition = new Vector3(0f, crouch_height, 0f);
            }
        }// if we press L.Ctrl

    }//crouch
}//class
