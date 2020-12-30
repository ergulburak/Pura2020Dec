using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditAnimationController : MonoBehaviour
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
        switch (GetComponent<BanditSystem>().banditState)
        {
            case BanditSystem.BanditState.Attack:
                foreach (AnimatorControllerParameter parameter in _animator.parameters)
                    _animator.SetBool(parameter.name, false);
                break;
            case BanditSystem.BanditState.Cooldown:
                foreach (AnimatorControllerParameter parameter in _animator.parameters)
                    _animator.SetBool(parameter.name, false);
                _animator.SetBool("isAttacking", true);
                break;
            case BanditSystem.BanditState.Dead:
                foreach (AnimatorControllerParameter parameter in _animator.parameters)
                    _animator.SetBool(parameter.name, false);
                _animator.SetBool("isDead", true);
                break;
            case BanditSystem.BanditState.Follow:
                foreach (AnimatorControllerParameter parameter in _animator.parameters)
                    _animator.SetBool(parameter.name, false);
                _animator.SetBool("isWalking", true);
                break;
            case BanditSystem.BanditState.Wait:
                foreach (AnimatorControllerParameter parameter in _animator.parameters)
                    _animator.SetBool(parameter.name, false);
                break;
        }
    }
}
