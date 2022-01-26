using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footstep_Sound;

    [SerializeField]
    private AudioClip[] footstep_Clip;

    private CharacterController characterController;

    [HideInInspector]
    public float volume_Min, volume_Max;

    private float accumulate_Distance;

    [HideInInspector]
    public float step_Distance;
  
    void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();

        characterController = GetComponentInParent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        if (!characterController.isGrounded)
        {
            // once it hits return other codes wont be executed
            return;
        }

        if(characterController.velocity.sqrMagnitude > 0)
        {
            accumulate_Distance += Time.deltaTime;
            if(accumulate_Distance > step_Distance)
            {
                //accumulated distance is how far we can go
                //i.e. move step, sprint, crouch,prone
                //until we play footstep sound
                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_Sound.Play();

                accumulate_Distance = 0f;
            }
        }
        else
        {
            accumulate_Distance = 0;
        }
    }
}//class
