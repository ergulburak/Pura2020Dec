using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class BanditWeaponSystem : MonoBehaviour
{
    public WeaponSystem.WeaponState weaponState = WeaponSystem.WeaponState.Empty;
    public WeaponSystem.WeaponType weaponType = WeaponSystem.WeaponType.Empty;
    public Transform bulletSpawnPoint;

    public WeaponConfig config;
    private List<GameObject> bulletPool;

    void Start()
    {
        bulletPool = new List<GameObject>();
        config = ScriptableObject.Instantiate(Resources.Load("Weapons/BanditWeapon") as WeaponConfig);
        GetComponent<BanditSystem>().attackRate = config.fireRate;
        if (config != null)
        {
            weaponState = WeaponSystem.WeaponState.ReadyToFire;
            weaponType = config.weaponType;
        }
    }

    void Update()
    {
        if (GetComponent<BanditSystem>().banditState != BanditSystem.BanditState.Dead)
        {
            if (weaponType == WeaponSystem.WeaponType.Empty)
            {
            }

            if (weaponType == WeaponSystem.WeaponType.SemiAuto)
            {
                if (weaponState == WeaponSystem.WeaponState.ReadyToFire &&
                    GetComponent<BanditSystem>().isReadyToFire)
                {
                    if (config.bulletInMagazine <= 0)
                    {
                        if (config.bulletCount > 0)
                        {
                            weaponState = WeaponSystem.WeaponState.Reload;
                        }
                        else
                            weaponState = WeaponSystem.WeaponState.Empty;
                    }
                    else
                    {
                        SemiAutoWeaponAction();
                        config.bulletInMagazine--;
                        weaponState = WeaponSystem.WeaponState.Cooldown;
                    }
                }

                if (weaponState == WeaponSystem.WeaponState.Cooldown)
                {
                    weaponState = WeaponSystem.WeaponState.Waiting;
                    WaitingCooldown(config.fireRate);
                }

                if (weaponState == WeaponSystem.WeaponState.Reload)
                {
                    weaponState = WeaponSystem.WeaponState.Waiting;
                    WaitingReload(config.reloadTime);
                }

                if (weaponState == WeaponSystem.WeaponState.Empty)
                {
                    Debug.Log("Mermi Bitti.");
                    if (config.bulletCount > 0)
                        weaponState = WeaponSystem.WeaponState.Reload;
                }
            }

            if (weaponType == WeaponSystem.WeaponType.FullAuto)
            {
                if (weaponState == WeaponSystem.WeaponState.ReadyToFire &&
                    GetComponent<BanditSystem>().isReadyToFire)
                {
                    if (config.bulletInMagazine <= 0)
                    {
                        if (config.bulletCount > 0)
                        {
                            weaponState = WeaponSystem.WeaponState.Reload;
                        }
                        else
                            weaponState = WeaponSystem.WeaponState.Empty;
                    }
                    else
                    {
                        FullAutoWeaponAction();
                        config.bulletInMagazine--;
                        weaponState = WeaponSystem.WeaponState.Cooldown;
                    }
                }

                if (weaponState == WeaponSystem.WeaponState.Cooldown)
                {
                    weaponState = WeaponSystem.WeaponState.Waiting;
                    WaitingCooldown(config.fireRate);
                }

                if (weaponState == WeaponSystem.WeaponState.Reload)
                {
                    weaponState = WeaponSystem.WeaponState.Waiting;
                    WaitingReload(config.reloadTime);
                }

                if (weaponState == WeaponSystem.WeaponState.Empty)
                {
                    Debug.Log("Mermi Bitti.");
                    if (config.bulletCount > 0)
                        weaponState = WeaponSystem.WeaponState.Reload;
                }
            }

            if (weaponType == WeaponSystem.WeaponType.Shotgun)
            {
                if (weaponState == WeaponSystem.WeaponState.ReadyToFire &&
                    GetComponent<BanditSystem>().isReadyToFire)
                {
                    if (config.bulletInMagazine <= 0)
                    {
                        if (config.bulletCount > 0)
                        {
                            weaponState = WeaponSystem.WeaponState.Reload;
                        }
                        else
                            weaponState = WeaponSystem.WeaponState.Empty;
                    }
                    else
                    {
                        ShotgunAutoWeaponAction();
                        config.bulletInMagazine--;
                        weaponState = WeaponSystem.WeaponState.Cooldown;
                    }
                }

                if (weaponState == WeaponSystem.WeaponState.Cooldown)
                {
                    weaponState = WeaponSystem.WeaponState.Waiting;
                    WaitingCooldown(config.fireRate);
                }

                if (weaponState == WeaponSystem.WeaponState.Reload)
                {
                    weaponState = WeaponSystem.WeaponState.Waiting;
                    WaitingReload(config.reloadTime);
                }

                if (weaponState == WeaponSystem.WeaponState.Empty)
                {
                    Debug.Log("Mermi Bitti.");
                    if (config.bulletCount > 0)
                        weaponState = WeaponSystem.WeaponState.Reload;
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
        if (GetComponent<BanditSystem>().banditState != BanditSystem.BanditState.Dead)
        {
            if (Input.GetKeyDown(GetComponent<PlayerControllerSystem>().fireKeyCode) &&
                weaponState == WeaponSystem.WeaponState.ReadyToFire)
            {
                Debug.Log("Melee");
                other.GetComponent<DamageSystem>().TakesDamage(config.bulletDamage);
                weaponState = weaponState = WeaponSystem.WeaponState.Cooldown;
            }

            if (weaponState == WeaponSystem.WeaponState.Cooldown)
            {
                weaponState = WeaponSystem.WeaponState.Waiting;
                WaitingCooldown(config.fireRate);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (weaponType == WeaponSystem.WeaponType.Melee)
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

        weaponState = WeaponSystem.WeaponState.ReadyToFire;
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

        weaponState = WeaponSystem.WeaponState.ReadyToFire;
    }
}