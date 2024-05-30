using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManger : MonoBehaviour
{
   private PlayerMovementAdvanced pm;
    public Animator animator;
    private AudioSource audioSource;

    public AudioClip[] footstepAudioClips;
    public float footstepVolume = 0.5f;

    private void Start()
    {
        pm = GetComponent<PlayerMovementAdvanced>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        UpdateAnimation();
        //xsUpdateSound();
    }

    private void UpdateAnimation()
    {
        // Update animator parameters based on movement state
        switch (pm.state)
        {
            case PlayerMovementAdvanced.MovementState.walking:
                animator.SetBool("Walking", true);
                animator.SetBool("Sprinting", false);
                animator.SetBool("Crouching", false);
                animator.SetBool("Jumping", false);
                animator.SetBool("Aiming", false);
                break;
            case PlayerMovementAdvanced.MovementState.sprinting:
                animator.SetBool("Walking", false);
                animator.SetBool("Sprinting", true);
                animator.SetBool("Crouching", false);
                animator.SetBool("Jumping", false);
                animator.SetBool("Aiming", false);
                break;
            case PlayerMovementAdvanced.MovementState.crouching:
                animator.SetBool("Walking", false);
                animator.SetBool("Sprinting", false);
                animator.SetBool("Crouching", true);
                animator.SetBool("Jumping", false);
                animator.SetBool("Aiming", false);
                break;
            case PlayerMovementAdvanced.MovementState.air:
                animator.SetBool("Walking", false);
                animator.SetBool("Sprinting", false);
                animator.SetBool("Crouching", false);
                animator.SetBool("Jumping", true);
                break;
            case PlayerMovementAdvanced.MovementState.aim:
                animator.SetBool("Walking", false);
                animator.SetBool("Sprinting", false);
                animator.SetBool("Crouching", false);
                animator.SetBool("Jumping", false);
                animator.SetBool("Aiming", true);
                break;
                  case PlayerMovementAdvanced.MovementState.CantedAim:
                animator.SetBool("Walking", false);
                animator.SetBool("Sprinting", false);
                animator.SetBool("Crouching", false);
                animator.SetBool("Jumping", false);
                animator.SetBool("Aiming", false);
                animator.SetBool("CantedAim", true);
                break;
            default:
                // Reset all animator parameters if none of the above cases match
                animator.SetBool("Walking", false);
                animator.SetBool("Sprinting", false);
                animator.SetBool("Crouching", false);
                animator.SetBool("Jumping", false);
                break;
        }
    }

    // private void UpdateSound()
    // {
    //     // Play footstep sound when walking or sprinting
    //     if (pm.state == PlayerMovementAdvanced.MovementState.walking ||
    //         pm.state == PlayerMovementAdvanced.MovementState.sprinting)
    //     {
    //         PlayFootstepSound();
    //     }
    //     else
    //     {
    //         // Stop footstep sound when not walking or sprinting
    //         audioSource.Stop();
    //     }
    // }

    // private void PlayFootstepSound()
    // {
    //     if (footstepAudioClips.Length > 0 && audioSource != null)
    //     {
    //         // Randomly select a footstep sound from the array
    //         int randomIndex = Random.Range(0, footstepAudioClips.Length);
    //         AudioClip footstepClip = footstepAudioClips[randomIndex];

    //         // Play the selected footstep sound
    //         audioSource.PlayOneShot(footstepClip, footstepVolume);
    //     }
    // }
}