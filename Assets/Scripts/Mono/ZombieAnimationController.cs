using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GetComponent<ZombieSystem>().zombieState)
        {
            case ZombieSystem.ZombieState.Attack:
                foreach (AnimatorControllerParameter parameter in _animator.parameters)
                    _animator.SetBool(parameter.name, false);
                break;
            case ZombieSystem.ZombieState.Cooldown:
                foreach (AnimatorControllerParameter parameter in _animator.parameters)
                    _animator.SetBool(parameter.name, false);
                _animator.SetBool("isAttacking", true);
                break;
            case ZombieSystem.ZombieState.Follow:
                foreach (AnimatorControllerParameter parameter in _animator.parameters)
                    _animator.SetBool(parameter.name, false);
                _animator.SetBool("isWalking", true);
                break;
            case ZombieSystem.ZombieState.Wait:
                foreach (AnimatorControllerParameter parameter in _animator.parameters)
                    _animator.SetBool(parameter.name, false);
                break;
            case ZombieSystem.ZombieState.Dead:
                foreach (AnimatorControllerParameter parameter in _animator.parameters)
                    _animator.SetBool(parameter.name, false);
                _animator.SetBool("isDead", true);
                break;
        }
    }
}