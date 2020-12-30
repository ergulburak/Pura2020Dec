using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ZombieSystem : MonoBehaviour
{
    public enum ZombieState
    {
        Wait,
        Cooldown,
        Follow,
        Attack,
        Dead
    }

    [Header("Düşman Özellikleri")] public float moveSpeed = 5.0f;
    public float turnSpeed = 5.0f;
    public float sightDistance = 10.0f;
    public float hearingRadius = 15.0f;
    public float hearingSensitivity = 30.0f;
    public float damageValue = 10.0f;
    public float attackRange = 2.0f;
    public float attackRate = 1.0f;
    public float visionQuality = 20.0f;
    private GameObject target;
    public ZombieState zombieState = ZombieState.Wait;
    private float distance = 999.0f;

    void Start()
    {
        zombieState = ZombieState.Wait;
    }

    void Update()
    {
        if (zombieState != ZombieState.Dead){
            FindTarget();

            if (!GetComponentInChildren<SightController>().isSight &&
                !GetComponentInChildren<HearingController>().isHear)
            {
                target = null;
            }

            if (target != null)
            {
                distance = Vector3.Distance(this.transform.position, target.transform.position);
                if (zombieState == ZombieState.Wait)
                {
                    if (GetComponentInChildren<SightController>().isSight)
                    {
                        if (distance <= sightDistance)
                            zombieState = ZombieState.Follow;
                    }
                    else if (GetComponentInChildren<HearingController>().isHear)
                    {
                        if (distance <= hearingRadius)
                            zombieState = ZombieState.Follow;
                    }
                }

                if (zombieState == ZombieState.Follow)
                {
                    if (GetComponentInChildren<SightController>().isSight &&
                        GetComponentInChildren<HearingController>().isHear)
                    {
                        if (distance > hearingRadius)
                            zombieState = ZombieState.Wait;
                    }
                    else if (GetComponentInChildren<SightController>().isSight)
                    {
                        if (distance > sightDistance)
                            zombieState = ZombieState.Wait;
                    }
                    else if (GetComponentInChildren<HearingController>().isHear)
                    {
                        if (distance > hearingRadius)
                            zombieState = ZombieState.Wait;
                    }

                    if (target.CompareTag("Bandit") || target.CompareTag("Player") || target.CompareTag("EnemyWall"))
                    {
                        if (distance <= attackRange)
                            zombieState = ZombieState.Attack;
                    }

                    if (target != null)
                    {
                        Vector3 dirToTarget = target.transform.position - transform.position;
                        dirToTarget.y = 0;
                        transform.position += dirToTarget.normalized * moveSpeed * Time.deltaTime;
                        Quaternion targetRot = Quaternion.LookRotation(dirToTarget, Vector3.up);
                        targetRot.x = 0;
                        targetRot.z = 0;
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed);
                    }
                }

                if (zombieState == ZombieState.Attack)
                {
                    if (distance >= sightDistance)
                    {
                        zombieState = ZombieState.Wait;
                    }

                    if (distance > attackRange)
                    {
                        zombieState = ZombieState.Follow;
                    }
                    else
                    {
                        if (target.gameObject.CompareTag("Player"))
                        {
                            if (target.gameObject.GetComponent<PlayerControllerSystem>().camouflage > visionQuality)
                            {
                                zombieState = ZombieState.Wait;
                                target = null;
                            }
                            else
                            {
                                Debug.Log("Isırdı.");
                                target.gameObject.GetComponent<DamageSystem>().TakesDamage(damageValue);
                                zombieState = ZombieState.Cooldown;
                                WaitingCooldown(attackRate);
                            }
                        }
                        else
                        {
                            target.gameObject.GetComponent<DamageSystem>().TakesDamage(damageValue);
                            zombieState = ZombieState.Cooldown;
                            WaitingCooldown(attackRate);
                        }
                    }
                }
            }
        }
    }

    private void FindTarget()
    {
        if (zombieState != ZombieState.Cooldown)
        {
            if (GetComponentInChildren<HearingController>().isHear && GetComponentInChildren<SightController>().isSight)
            {
                target = GetComponentInChildren<SightController>().target;
            }
            else if (GetComponentInChildren<HearingController>().isHear)
            {
                target = GetComponentInChildren<HearingController>().target;
            }
            else if (GetComponentInChildren<SightController>().isSight)
            {
                target = GetComponentInChildren<SightController>().target;
            }
            else
            {
                zombieState = ZombieState.Wait;
            }
        }
    }

    private async Task WaitingCooldown(float delayTime)
    {
        await Task.Delay(TimeSpan.FromSeconds(delayTime));

        zombieState = ZombieState.Attack;
    }
}