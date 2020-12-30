using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    enum EntityType
    {
        Player,
        Bandit,
        Zombie,
        EnemyWall
    }

    public float health = 100.0f;
    public float currentHealth = 0.0f;
    private EntityType _type;

    void Start()
    {
        currentHealth = health;
        _type = this.CompareTag("Bandit") ? EntityType.Bandit :
            this.CompareTag("Player") ? EntityType.Player :
            this.CompareTag("Zombie") ? EntityType.Zombie :
            EntityType.EnemyWall;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            if (_type == EntityType.Player)
            {
                GetComponent<PlayerControllerSystem>().playerActionState = PlayerControllerSystem.PlayerState.Dead;
                GetComponent<PlayerControllerSystem>().playerState = PlayerControllerSystem.PlayerState.Dead;
            }
            else if (_type == EntityType.Bandit)
            {
                GetComponent<BanditSystem>().banditState = BanditSystem.BanditState.Dead;
                this.tag = "DeadBandit";
            }
            else if (_type == EntityType.Zombie)
            {
                GetComponent<ZombieSystem>().zombieState = ZombieSystem.ZombieState.Dead;
                this.tag = "DeadZombie";
            }
            else
            {
            }
        }
    }

    public void TakesDamage(float damage)
    {
        currentHealth -= damage;
    }
}