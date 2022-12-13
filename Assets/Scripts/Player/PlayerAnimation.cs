using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator Animator;
    public InputManager InputManager;
    public PlayerController PlayerController;
    public bool hasJumped = false;
    public bool InMenu = false;
    public bool DeathAnimationWasPlayed = false;
    
    // Update is called once per frame
    void Update()
    {
        if (StateManager.IsDead && DeathAnimationWasPlayed)
            return;

        if (StateManager.IsDead && !DeathAnimationWasPlayed)
        {
            Animator.SetTrigger("Death");
            DeathAnimationWasPlayed = true;
            Animator.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            return;
        }

        Animator.SetFloat("Speed", Mathf.Abs(InputManager.MovementInput.x));
        if (!StateManager.IsGrounded && InputManager.Jump == 1)
        {
            Animator.SetFloat("Jump", 1);
            //Animator.SetBool("Fall", false);
        }

        if (StateManager.IsGrounded)
        {
            //Animator.SetBool("Fall", true);
            Animator.SetFloat("Jump", -1);
        }
        Animator.SetBool("Attack", InputManager.Attack);
    }
}
