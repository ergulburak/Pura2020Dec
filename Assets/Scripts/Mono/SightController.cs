using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    public enum SightType
    {
        Zombie,
        Bandit
    }

    private SightType _type;
    public bool isSight = false;
    public GameObject target;
    private BoxCollider[] _collider;

    void Start()
    {
        if (transform.parent.CompareTag("Bandit"))
        {
            _type = SightType.Bandit;
            _collider = GetComponentsInChildren<BoxCollider>();
            foreach (var boxCollider in _collider)
            {
                boxCollider.center = new Vector3(0, 1.05f, GetComponentInParent<BanditSystem>().sightDistance / 2);
                boxCollider.size = new Vector3(4, 2, GetComponentInParent<BanditSystem>().sightDistance);
            }
        }

        if (transform.parent.CompareTag("Zombie"))
        {
            _type = SightType.Zombie;
            _collider = GetComponentsInChildren<BoxCollider>();
            foreach (var boxCollider in _collider)
            {
                boxCollider.center = new Vector3(0, 1.05f, GetComponentInParent<ZombieSystem>().sightDistance / 2);
                boxCollider.size = new Vector3(4, 2, GetComponentInParent<ZombieSystem>().sightDistance);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_type == SightType.Bandit)
        {
            if (other.gameObject.CompareTag("Zombie"))
            {
                isSight = true;
                target = other.gameObject;
            }
            else if (other.gameObject.CompareTag("Player") &&
                     other.gameObject.GetComponent<PlayerControllerSystem>().playerState !=
                     PlayerControllerSystem.PlayerState.Dead)
            {
                if (other.gameObject.GetComponent<PlayerControllerSystem>().camouflage >
                    GetComponentInParent<BanditSystem>().visionQuality)
                {
                    if (other.gameObject.GetComponent<PlayerControllerSystem>().noiceLevel <
                        GetComponentInParent<BanditSystem>().hearingSensitivity)
                    {
                        isSight = false;
                        target = null;
                    }
                    else
                    {
                        isSight = true;
                        target = other.gameObject;
                    }
                }
                else
                {
                    isSight = true;
                    target = other.gameObject;
                }
            }
            else if (other.gameObject.CompareTag("DeadBandit") ||
                     other.gameObject.CompareTag("DeadZombie"))
            {
                other.tag = "Untagged";
                isSight = false;
                target = null;
            }
            else if (other.gameObject.CompareTag("Untagged") || other.gameObject.CompareTag("Bandit") ||
                     other.gameObject.CompareTag("Floor"))
            {
            }
            else
            {
                isSight = false;
                target = null;
            }
        }

        if (_type == SightType.Zombie)
        {
            if (other.gameObject.CompareTag("Bandit") &&
                other.gameObject.GetComponent<BanditSystem>().banditState !=
                BanditSystem.BanditState.Dead)
            {
                isSight = true;
                target = other.gameObject;
            }
            else if (other.gameObject.CompareTag("Player") &&
                     other.gameObject.GetComponent<PlayerControllerSystem>().playerState !=
                     PlayerControllerSystem.PlayerState.Dead)
            {
                if (other.gameObject.GetComponent<PlayerControllerSystem>().camouflage >
                    GetComponentInParent<ZombieSystem>().visionQuality)
                {
                    if (other.gameObject.GetComponent<PlayerControllerSystem>().noiceLevel <
                        GetComponentInParent<ZombieSystem>().hearingSensitivity)
                    {
                        isSight = false;
                        target = null;
                    }
                    else
                    {
                        isSight = true;
                        target = other.gameObject;
                    }
                }
                else
                {
                    isSight = true;
                    target = other.gameObject;
                }
            }
            else if (other.gameObject.CompareTag("EnemyWall"))
            {
                isSight = true;
                target = other.gameObject;
            }
            else if (other.gameObject.CompareTag("DeadBandit") ||
                     other.gameObject.CompareTag("DeadZombie"))
            {
                other.tag = "Untagged";
                isSight = false;
                target = null;
            }
            else if (other.gameObject.CompareTag("Untagged") || other.gameObject.CompareTag("Zombie") ||
                     other.gameObject.CompareTag("Floor"))
            {
            }
            else
            {
                isSight = false;
                target = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bandit") ||
            other.gameObject.CompareTag("EnemyWall"))
        {
            target = null;
            isSight = false;
        }
    }
}