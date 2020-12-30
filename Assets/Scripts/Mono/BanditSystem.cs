using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BanditSystem : MonoBehaviour
{
     public enum BanditState
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
    public BanditState banditState = BanditState.Wait;
    private float distance = 999.0f;
    public bool isReadyToFire = false;

    void Start()
    {
        banditState = BanditState.Wait;
    }

    void Update()
    {
        if (banditState != BanditState.Dead)
        {
            FindTarget();

            if (!GetComponentInChildren<SightController>().isSight &&
                !GetComponentInChildren<HearingController>().isHear)
            {
                target = null;
            }

            if (target != null)
            {
                distance = Vector3.Distance(this.transform.position, target.transform.position);
                if (banditState == BanditState.Wait)
                {
                    if (GetComponentInChildren<SightController>().isSight)
                    {
                        if (distance <= sightDistance)
                            banditState = BanditState.Follow;
                    }
                    else if (GetComponentInChildren<HearingController>().isHear)
                    {
                        if (distance <= hearingRadius)
                            banditState = BanditState.Follow;
                    }
                }

                if (banditState == BanditState.Follow)
                {
                    if (GetComponentInChildren<SightController>().isSight &&
                        GetComponentInChildren<HearingController>().isHear)
                    {
                        if (distance > hearingRadius)
                            banditState = BanditState.Wait;
                    }
                    else if (GetComponentInChildren<SightController>().isSight)
                    {
                        if (distance > sightDistance)
                            banditState = BanditState.Wait;
                    }
                    else if (GetComponentInChildren<HearingController>().isHear)
                    {
                        if (distance > hearingRadius)
                            banditState = BanditState.Wait;
                    }

                    if (target.CompareTag("Zombie") || target.CompareTag("Player") || target.CompareTag("EnemyWall"))
                    {
                        if (distance <= attackRange)
                            banditState = BanditState.Attack;
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

                if (isReadyToFire)
                {
                    Vector3 dirToTarget = target.transform.position - transform.position;
                    dirToTarget.y = 0;
                    Quaternion targetRot = Quaternion.LookRotation(dirToTarget, Vector3.up);
                    targetRot.x = 0;
                    targetRot.z = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed);
                }

                if (banditState == BanditState.Attack)
                {
                    if (distance >= sightDistance)
                    {
                        banditState = BanditState.Wait;
                    }

                    if (distance > attackRange)
                    {
                        banditState = BanditState.Follow;
                    }
                    else
                    {
                        if (target.gameObject.CompareTag("Player"))
                        {
                            if (target.gameObject.GetComponent<PlayerControllerSystem>().camouflage > visionQuality)
                            {
                                target.gameObject.GetComponent<PlayerControllerSystem>().camouflage -= 30;
                                banditState = BanditState.Wait;
                                target = null;
                            }
                            else
                            {
                                isReadyToFire = true;
                                banditState = BanditState.Cooldown;
                                WaitingCooldown(attackRate);
                            }
                        }
                        else
                        {
                            isReadyToFire = true;
                            banditState = BanditState.Cooldown;
                            WaitingCooldown(attackRate);
                        }
                    }
                }
                else
                    isReadyToFire = false;
            }
        }
    }

    private void FindTarget()
    {
        if (banditState != BanditState.Cooldown)
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
                banditState = BanditState.Wait;
            }
        }
    }

    private async Task WaitingCooldown(float delayTime)
    {
        await Task.Delay(TimeSpan.FromSeconds(delayTime));

        banditState = BanditState.Attack;
    }
}
