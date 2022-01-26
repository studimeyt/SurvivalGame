using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintCrouchProne : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private CharacterController characterController;

    public float sprint_speed = 8f;
    public float move_speed = 4f;
    public float crouch_speed = 1.5f;
    public float prone_speed = 1f; //for sniping maybe

    private Transform LookRoot;
    private float stand_height = 1.6f;
    private float crouch_height = 1f;
    private float prone_height = 0.8f;

    private bool is_crouching;
    private bool is_prone;
    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        LookRoot = transform.GetChild(1);
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded == true)
        {
            Sprint();
            Crouch();
            Prone();
        }

        else
        {
            playerMovement.speed = move_speed;
        }

    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !is_crouching && !is_prone)
        {
            playerMovement.speed = sprint_speed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_crouching && !is_prone)
        {
            playerMovement.speed = move_speed;
        }
    }//sprint



    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (is_crouching && !is_prone)
            {
                //crouch to stand
                LookRoot.localPosition = new Vector3(0f, stand_height, 0f);
                playerMovement.speed = move_speed;

                is_crouching = false;
            }
            else
            {
                //stand to crouch
                LookRoot.localPosition = new Vector3(0f, crouch_height, 0f);
                playerMovement.speed = crouch_speed;

                is_crouching = true;
            }
        }

    }//crouch

    void Prone()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (is_crouching)
            {
                //crouch to prone
                LookRoot.localPosition = new Vector3(0f, prone_height, 0f);
                playerMovement.speed = prone_speed;

                is_crouching = false;
                is_prone = true;
            }
            else
            {
                //prone to crouch
                LookRoot.localPosition = new Vector3(0f, crouch_height, 0f);
                playerMovement.speed = crouch_speed;

                is_crouching = true;
                is_prone = false;
            }
        }

    }//prone

}//class