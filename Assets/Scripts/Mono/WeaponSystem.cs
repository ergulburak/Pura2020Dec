using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public enum WeaponState
    {
        ReadyToFire,
        Cooldown,
        Reload,
        Empty,
        Waiting
    }

    public enum WeaponType
    {
        Empty,
        SemiAuto,
        FullAuto,
        Shotgun,
        Melee,
    }

    public WeaponState weaponState = WeaponState.Empty;
    public WeaponType weaponType = WeaponType.Empty;
    public Transform bulletSpawnPoint;

    public WeaponConfig config;
    private List<GameObject> bulletPool;

    void Start()
    {
        bulletPool = new List<GameObject>();
        config = Resources.Load("Weapons/Ak47") as WeaponConfig;
        if (config != null)
        {
            weaponState = WeaponState.ReadyToFire;
            weaponType = config.weaponType;
        }
    }

    void Update()
    {
        if (GetComponent<PlayerControllerSystem>().playerState != PlayerControllerSystem.PlayerState.Dead)
        {
            if (weaponType == WeaponType.Empty)
            {
            }

            if (weaponType == WeaponType.SemiAuto)
            {
                if (weaponState == WeaponState.ReadyToFire &&
                    Input.GetKeyDown(GetComponent<PlayerControllerSystem>().fireKeyCode))
                {
                    if (config.bulletInMagazine <= 0)
                    {
                        if (config.bulletCount > 0)
                        {
                            weaponState = WeaponState.Reload;
                        }
                        else
                            weaponState = WeaponState.Empty;
                    }
                    else
                    {
                        SemiAutoWeaponAction();
                        config.bulletInMagazine--;
                        weaponState = WeaponState.Cooldown;
                    }
                }

                if (weaponState == WeaponState.Cooldown)
                {
                    weaponState = WeaponState.Waiting;
                    WaitingCooldown(config.fireRate);
                }

                if (weaponState == WeaponState.Reload)
                {
                    weaponState = WeaponState.Waiting;
                    WaitingReload(config.reloadTime);
                }

                if (weaponState == WeaponState.Empty)
                {
                    Debug.Log("Mermi Bitti.");
                    if (config.bulletCount > 0)
                        weaponState = WeaponState.Reload;
                }
            }

            if (weaponType == WeaponType.FullAuto)
            {
                if (weaponState == WeaponState.ReadyToFire &&
                    Input.GetKey(GetComponent<PlayerControllerSystem>().fireKeyCode))
                {
                    if (config.bulletInMagazine <= 0)
                    {
                        if (config.bulletCount > 0)
                        {
                            weaponState = WeaponState.Reload;
                        }
                        else
                            weaponState = WeaponState.Empty;
                    }
                    else
                    {
                        FullAutoWeaponAction();
                        config.bulletInMagazine--;
                        weaponState = WeaponState.Cooldown;
                    }
                }

                if (weaponState == WeaponState.Cooldown)
                {
                    weaponState = WeaponState.Waiting;
                    WaitingCooldown(config.fireRate);
                }

                if (weaponState == WeaponState.Reload)
                {
                    weaponState = WeaponState.Waiting;
                    WaitingReload(config.reloadTime);
                }

                if (weaponState == WeaponState.Empty)
                {
                    Debug.Log("Mermi Bitti.");
                    if (config.bulletCount > 0)
                        weaponState = WeaponState.Reload;
                }
            }

            if (weaponType == WeaponType.Shotgun)
            {
                if (weaponState == WeaponState.ReadyToFire &&
                    Input.GetKeyDown(GetComponent<PlayerControllerSystem>().fireKeyCode))
                {
                    if (config.bulletInMagazine <= 0)
                    {
                        if (config.bulletCount > 0)
                        {
                            weaponState = WeaponState.Reload;
                        }
                        else
                            weaponState = WeaponState.Empty;
                    }
                    else
                    {
                        ShotgunAutoWeaponAction();
                        config.bulletInMagazine--;
                        weaponState = WeaponState.Cooldown;
                    }
                }

                if (weaponState == WeaponState.Cooldown)
                {
                    weaponState = WeaponState.Waiting;
                    WaitingCooldown(config.fireRate);
                }

                if (weaponState == WeaponState.Reload)
                {
                    weaponState = WeaponState.Waiting;
                    WaitingReload(config.reloadTime);
                }

                if (weaponState == WeaponState.Empty)
                {
                    Debug.Log("Mermi Bitti.");
                    if (config.bulletCount > 0)
                        weaponState = WeaponState.Reload;
                }
            }
        }
    }

    private void SemiAutoWeaponAction()
    {
        if (config != null)
        {
            if (bulletPool.Count <= 0)
            {
                GameObject bullet = Instantiate(Resources.Load("Bullets/" + config.bulletPrefabName) as GameObject);
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
                    bulletSpawnPoint.parent.GetComponent<Collider>());

                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                bullet.GetComponent<Rigidbody>()
                    .AddForce(bulletSpawnPoint.forward * config.bulletSpeed, ForceMode.Impulse);

                BulletLife(bullet, config.lifeTime);
            }
            else
            {
                GameObject bullet = bulletPool.Last();
                bullet.SetActive(true);
                bulletPool.Remove(bullet);
                bullet.transform.rotation = new Quaternion(0, 0, 0, 0);
                bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
                bullet.GetComponent<MeshRenderer>().enabled = true;
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
                    bulletSpawnPoint.parent.GetComponent<Collider>());

                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                bullet.GetComponent<Rigidbody>()
                    .AddForce(bulletSpawnPoint.forward * config.bulletSpeed, ForceMode.Impulse);

                BulletLife(bullet, config.lifeTime);
            }
        }
    }

    private void FullAutoWeaponAction()
    {
        if (config != null)
        {
            if (bulletPool.Count <= 0)
            {
                GameObject bullet = Instantiate(Resources.Load("Bullets/" + config.bulletPrefabName) as GameObject);
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
                    bulletSpawnPoint.parent.GetComponent<Collider>());

                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                bullet.GetComponent<Rigidbody>()
                    .AddForce(bulletSpawnPoint.forward * config.bulletSpeed, ForceMode.Impulse);

                BulletLife(bullet, config.lifeTime);
            }
            else
            {
                GameObject bullet = bulletPool.Last();
                bullet.SetActive(true);
                bulletPool.Remove(bullet);
                bullet.transform.rotation = new Quaternion(0, 0, 0, 0);
                bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
                bullet.GetComponent<MeshRenderer>().enabled = true;
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
                    bulletSpawnPoint.parent.GetComponent<Collider>());

                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                bullet.GetComponent<Rigidbody>()
                    .AddForce(bulletSpawnPoint.forward * config.bulletSpeed, ForceMode.Impulse);

                BulletLife(bullet, config.lifeTime);
            }
        }
    }

    private void ShotgunAutoWeaponAction()
    {
        if (bulletPool.Count <= 0)
        {
            for (int i = 0; i < config.projectileCount; i++)
            {
                GameObject projectile = Instantiate(Resources.Load("Bullets/" + config.bulletPrefabName) as GameObject);
                projectile.transform.position = bulletSpawnPoint.transform.position;
                Physics.IgnoreCollision(projectile.GetComponent<Collider>(),
                    bulletSpawnPoint.parent.GetComponent<Collider>());


                projectile.transform.rotation = Quaternion.Euler(
                    UnityEngine.Random.Range(-config.spreadAngel, config.spreadAngel),
                    transform.eulerAngles.y + UnityEngine.Random.Range(-config.spreadAngel, config.spreadAngel),
                    0);

                projectile.GetComponent<Rigidbody>()
                    .AddForce(projectile.transform.forward * config.bulletSpeed, ForceMode.Impulse);
                BulletLife(projectile, config.lifeTime);
            }
        }
        else
        {
            for (int i = 0; i < config.projectileCount; i++)
            {
                GameObject projectile = bulletPool.Last();
                projectile.SetActive(true);
                bulletPool.Remove(projectile);
                projectile.transform.rotation = new Quaternion(0, 0, 0, 0);
                projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
                projectile.GetComponent<MeshRenderer>().enabled = true;
                projectile.transform.position = bulletSpawnPoint.transform.position;
                Physics.IgnoreCollision(projectile.GetComponent<Collider>(),
                    bulletSpawnPoint.parent.GetComponent<Collider>());


                projectile.transform.rotation = Quaternion.Euler(
                    UnityEngine.Random.Range(-config.spreadAngel, config.spreadAngel),
                    transform.eulerAngles.y + UnityEngine.Random.Range(-config.spreadAngel, config.spreadAngel),
                    0);

                projectile.GetComponent<Rigidbody>()
                    .AddForce(projectile.transform.forward * config.bulletSpeed, ForceMode.Impulse);
                BulletLife(projectile, config.lifeTime);
            }
        }
    }

    private void MeleeWeaponAction(GameObject other)
    {
        if (GetComponent<PlayerControllerSystem>().playerState != PlayerControllerSystem.PlayerState.Dead)
        {
            if (Input.GetKeyDown(GetComponent<PlayerControllerSystem>().fireKeyCode) &&
                weaponState == WeaponState.ReadyToFire)
            {
                Debug.Log("Melee");
                other.GetComponent<DamageSystem>().TakesDamage(config.bulletDamage);
                weaponState = weaponState = WeaponState.Cooldown;
            }

            if (weaponState == WeaponState.Cooldown)
            {
                weaponState = WeaponState.Waiting;
                WaitingCooldown(config.fireRate);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (weaponType == WeaponType.Melee)
        {
            if (other.CompareTag("Bandit") || other.CompareTag("Zombie"))
                MeleeWeaponAction(other.gameObject);
        }
    }


    private async Task BulletLife(GameObject bullet, float delayTime)
    {
        await Task.Delay(TimeSpan.FromSeconds(delayTime));
        if (bullet.activeSelf)
        {
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    private async Task WaitingCooldown(float delayTime)
    {
        await Task.Delay(TimeSpan.FromSeconds(delayTime));

        weaponState = WeaponState.ReadyToFire;
    }

    private async Task WaitingReload(float delayTime)
    {
        await Task.Delay(TimeSpan.FromSeconds(delayTime));

        if (config.bulletCount < config.magazineBulletCount)
        {
            config.bulletInMagazine = config.bulletCount;
            config.bulletCount = 0;
        }
        else
        {
            config.bulletInMagazine = config.magazineBulletCount;
            config.bulletCount -= config.magazineBulletCount;
        }

        weaponState = WeaponState.ReadyToFire;
    }
}