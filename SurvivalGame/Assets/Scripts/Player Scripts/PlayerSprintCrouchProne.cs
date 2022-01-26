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

    private PlayerFootsteps player_Footsteps;

    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.2f;
    private float walk_VolumeMin = 0.3f, walk_VolumeMax = 0.7f;
    private float prone_Volume = 0.05f;

    //its basically how often the sound is played or a step is made

    private float walk_Step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.25f;
    private float crouch_Step_Distance = 0.5f;
    private float prone_Step_Distance = 0.6f;

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        LookRoot = transform.GetChild(1);
        player_Footsteps = GetComponentInChildren<PlayerFootsteps>();
        characterController = GetComponent<CharacterController>();//mine
    }

    void Start()
    {
        player_Footsteps.volume_Max = walk_VolumeMax;
        player_Footsteps.volume_Min = walk_VolumeMin;
        player_Footsteps.step_Distance = walk_Step_Distance;
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

            player_Footsteps.step_Distance = sprint_Step_Distance;
            player_Footsteps.volume_Min = sprint_Volume;
            player_Footsteps.volume_Max = sprint_Volume;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_crouching && !is_prone)
        {
            playerMovement.speed = move_speed;

            player_Footsteps.step_Distance = walk_Step_Distance;
            player_Footsteps.volume_Max = walk_VolumeMax;
            player_Footsteps.volume_Min = walk_VolumeMin;
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

                player_Footsteps.step_Distance = walk_Step_Distance;
                player_Footsteps.volume_Max = walk_VolumeMax;
                player_Footsteps.volume_Min = walk_VolumeMin;
                
            }
            else
            {
                //stand to crouch
                LookRoot.localPosition = new Vector3(0f, crouch_height, 0f);
                playerMovement.speed = crouch_speed;

                is_crouching = true;
                player_Footsteps.step_Distance = crouch_Step_Distance;
                player_Footsteps.volume_Max = crouch_Volume;
                player_Footsteps.volume_Min = crouch_Volume;
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

                player_Footsteps.step_Distance = prone_Step_Distance;
                player_Footsteps.volume_Max = prone_Volume;
                player_Footsteps.volume_Min = prone_Volume;
            }
            else
            {
                //prone to crouch
                LookRoot.localPosition = new Vector3(0f, crouch_height, 0f);
                playerMovement.speed = crouch_speed;

                is_crouching = true;
                is_prone = false;

                player_Footsteps.step_Distance = crouch_Step_Distance;
                player_Footsteps.volume_Max = crouch_Volume;
                player_Footsteps.volume_Min = crouch_Volume;
            }
        }

    }//prone

}//class