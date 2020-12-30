using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Regex psRgx, pasRgx;

    // Start is called before the first frame update
    void Start()
    {
        psRgx = new Regex(@"ps_\w+");
        pasRgx = new Regex(@"pas_\w+");

        animator.SetBool("ps_idle", true);
        animator.SetBool("pas_idle", true);

        animator.SetBool("isArmed", true);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isArmed", true);
        /*
         *  foreach(AnimatorControllerParameter parameter in timeReflectionAnimator.parameters) {            
        timeReflectionAnimator.SetBool(parameter.name, false);            
 }
         */
        switch (GetComponent<PlayerControllerSystem>().playerState)
        {
            case PlayerControllerSystem.PlayerState.Idle:
                ResetAnimStates("ps");
                animator.SetBool("ps_idle", true);
                SetPlayerActionStateAnim();
                break;
            case PlayerControllerSystem.PlayerState.Run:
                ResetAnimStates("ps");
                animator.SetBool("ps_run", true);
                SetPlayerActionStateAnim();
                break;
            case PlayerControllerSystem.PlayerState.Move:
                ResetAnimStates("ps");
                animator.SetBool("ps_move", true);
                SetPlayerActionStateAnim();
                break;
            case PlayerControllerSystem.PlayerState.Crouch:
                ResetAnimStates("ps");
                animator.SetBool("ps_crouch", true);
                SetPlayerActionStateAnim();
                break;
            case PlayerControllerSystem.PlayerState.Process:
                ResetAnimStates("ps");
                animator.SetBool("ps_process", true);
                SetPlayerActionStateAnim();
                break;
            case PlayerControllerSystem.PlayerState.Attack:
                ResetAnimStates("ps");
                animator.SetBool("ps_attack", true);
                SetPlayerActionStateAnim();
                break;
            case PlayerControllerSystem.PlayerState.Jump:
                ResetAnimStates("ps");
                animator.SetBool("ps_jump", true);
                SetPlayerActionStateAnim();
                break;
            case PlayerControllerSystem.PlayerState.Roar:
                ResetAnimStates("ps");
                animator.SetBool("ps_roar", true);
                SetPlayerActionStateAnim();
                break;
            case PlayerControllerSystem.PlayerState.Dead:
                ResetAnimStates("ps");
                animator.SetBool("ps_dead", true);
                SetPlayerActionStateAnim();
                break;
        }
    }

    void ResetAnimStates(String option)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (option.Equals("ps"))
            {
                if (psRgx.IsMatch(parameter.name))
                    animator.SetBool(parameter.name, false);
            }
            else if (option.Equals("pas"))
            {
                if (pasRgx.IsMatch(parameter.name))
                    animator.SetBool(parameter.name, false);
            }
        }
    }

    void SetPlayerActionStateAnim()
    {
        switch (GetComponent<PlayerControllerSystem>().playerActionState)
        {
            case PlayerControllerSystem.PlayerState.Idle:
                ResetAnimStates("pas");
                animator.SetBool("pas_idle", true);
                break;
            case PlayerControllerSystem.PlayerState.Run:
                ResetAnimStates("pas");
                animator.SetBool("pas_run", true);
                break;
            case PlayerControllerSystem.PlayerState.Move:
                ResetAnimStates("pas");
                animator.SetBool("pas_move", true);
                break;
            case PlayerControllerSystem.PlayerState.Crouch:
                ResetAnimStates("pas");
                animator.SetBool("pas_crouch", true);
                break;
            case PlayerControllerSystem.PlayerState.Process:
                ResetAnimStates("pas");
                animator.SetBool("pas_process", true);
                break;
            case PlayerControllerSystem.PlayerState.Attack:
                ResetAnimStates("pas");
                animator.SetBool("pas_attack", true);
                break;
            case PlayerControllerSystem.PlayerState.Jump:
                ResetAnimStates("pas");
                animator.SetBool("pas_jump", true);
                break;
            case PlayerControllerSystem.PlayerState.Roar:
                ResetAnimStates("pas");
                animator.SetBool("pas_roar", true);
                break;
            case PlayerControllerSystem.PlayerState.Dead:
                ResetAnimStates("pas");
                animator.SetBool("pas_dead", true);
                break;
        }
    }
}