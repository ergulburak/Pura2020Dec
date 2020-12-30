using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingController : MonoBehaviour
{
    public enum HearType
    {
        Zombie,
        Bandit
    }

    private HearType _type;
    public bool isHear = false;
    public GameObject target;
    private SphereCollider _collider;

    void Start()
    {
        if (transform.parent.CompareTag("Bandit"))
        {
            _type = HearType.Bandit;
            _collider = GetComponent<SphereCollider>();
            _collider.radius = GetComponentInParent<BanditSystem>().hearingRadius;
        }

        if (transform.parent.CompareTag("Zombie"))
        {
            _type = HearType.Zombie;
            _collider = GetComponent<SphereCollider>();
            _collider.radius = GetComponentInParent<ZombieSystem>().hearingRadius;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_type == HearType.Zombie)
        {
            if (other.gameObject.CompareTag("EnemyWall"))
            {
                isHear = true;
                target = other.gameObject;
            }
            else if (other.gameObject.CompareTag("Bandit") &&
                     other.gameObject.GetComponent<BanditSystem>().banditState !=
                     BanditSystem.BanditState.Dead)
            {
                isHear = true;
                target = other.gameObject;
            }
            else if (other.gameObject.CompareTag("Player") &&
                     other.gameObject.GetComponent<PlayerControllerSystem>().playerState !=
                     PlayerControllerSystem.PlayerState.Dead)
            {
                if (other.gameObject.GetComponent<PlayerControllerSystem>().noiceLevel >
                    GetComponentInParent<ZombieSystem>().hearingSensitivity)
                {
                    isHear = true;
                    target = other.gameObject;
                }
                else
                {
                    isHear = false;
                    target = null;
                }
            }
            else if (other.gameObject.CompareTag("Untagged") || other.gameObject.CompareTag("Zombie") ||
                     other.gameObject.CompareTag("Floor"))
            {
            }
            else
            {
                isHear = false;
                target = null;
            }
        }

        if (_type == HearType.Bandit)
        {
            if (other.gameObject.CompareTag("Zombie") && other.gameObject.GetComponent<ZombieSystem>().zombieState !=
                ZombieSystem.ZombieState.Dead)
            {
                isHear = true;
                target = other.gameObject;
            }
            else if (other.gameObject.CompareTag("Player") &&
                     other.gameObject.GetComponent<PlayerControllerSystem>().playerState !=
                     PlayerControllerSystem.PlayerState.Dead)
            {
                if (other.gameObject.GetComponent<PlayerControllerSystem>().noiceLevel >
                    GetComponentInParent<BanditSystem>().hearingSensitivity)
                {
                    isHear = true;
                    target = other.gameObject;
                }
                else
                {
                    isHear = false;
                    target = null;
                }
            }
            else if (other.gameObject.CompareTag("Untagged") || other.gameObject.CompareTag("Bandit") ||
                     other.gameObject.CompareTag("Floor"))
            {
            }
            else
            {
                isHear = false;
                target = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_type == HearType.Zombie)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bandit") ||
                other.gameObject.CompareTag("EnemyWall"))
            {
                target = null;
                isHear = false;
            }
        }

        if (_type == HearType.Bandit)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Zombie"))
            {
                target = null;
                isHear = false;
            }
        }
    }
}