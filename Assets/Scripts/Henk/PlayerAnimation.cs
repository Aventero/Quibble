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
    
    // Update is called once per frame
    void Update()
    {
        if (InMenu)
            return;

        Animator.SetFloat("Speed", Mathf.Abs(InputManager.MovementInput.x));
        if (!StateManager.IsGrounded && InputManager.Jump == 1)
            Animator.SetFloat("Jump", 1);
        else
            Animator.SetFloat("Jump", 0);
        Animator.SetBool("Attack", InputManager.Attack);
    }
}
