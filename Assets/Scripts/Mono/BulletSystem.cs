using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<DamageSystem>() != null)
        {
            other.gameObject.GetComponent<DamageSystem>()
                .TakesDamage(GameObject.FindWithTag("Player").GetComponent<WeaponSystem>().config.bulletDamage);
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}