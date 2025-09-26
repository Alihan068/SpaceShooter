using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {
    [SerializeField] BossDifficulityEnum[] bossDifficulityEnum;
    [System.Serializable]
    public class BossModifiers {
        //[SerializeField] GameObject[] projectilePrefab;
        //[SerializeField] Transform[] weaponTransform;
        //[SerializeField] float projectileSpeed;
        //[SerializeField] float projectileTurnSpeed;
        //[SerializeField] float projectileLifeTime = 5f;

        //[SerializeField] float minimumFireRate = 0.2f;
        //[SerializeField] float baseFireRate = 0.2f;
        //[SerializeField] float firingRateVariance = 0.2f;
        [SerializeField] AttackPatternSO[] attackPatterns;
        AttackPatternSO currentAttackPattern;
        Transform transform;
        AudioManager audioPlayer;

        Coroutine coroutine;

        public int extraHp;
        public int extraDamage;
        public bool shield;

        bool canFire;
        bool canFireMissle;
        bool canShield;

        float timeBetweenAttackSequence = 10;

        void Start() {
            //StartCoroutine(BossAttackOrder(currentAttackPattern, timeBetweenAttackSequence));
        }

        void Update() {
            
        }
        IEnumerator BossAttackOrder(AttackPatternSO attackPattern, float cooldownBetweenAttacks) {
            foreach (AttackPatternSO currentAttackPattern in attackPatterns) {

                //StartCoroutine(AttackSequence(currentAttackPattern.GetProjectilePrefab(), currentAttackPattern.GetProjectileSpawnPos(), currentAttackPattern.GetBaseFireRate(), currentAttackPattern.GetMinimumFireRate(), currentAttackPattern.GetFiringRateVariance(), currentAttackPattern.GetProjectileLifeTime(), currentAttackPattern.GetProjectileSpeed()));

                yield return new WaitForSeconds(cooldownBetweenAttacks);
            }
            yield return new WaitForSeconds(cooldownBetweenAttacks);
        }

        IEnumerator AttackSequence(GameObject projectilePrefab, List<Transform> projectileSpawnPos, float baseFireRate, float minimumFireRate, float firingRateVariance, float projectileLifeTime, float projectileSpeed) {
            foreach (Transform weaponPos in projectileSpawnPos) {
                while (true) {
                    GameObject instance = Instantiate(projectilePrefab, weaponPos.position, Quaternion.identity);

                    Rigidbody2D rb2d = instance.GetComponent<Rigidbody2D>();

                    if (rb2d != null) {
                        rb2d.linearVelocity = transform.up * projectileSpeed;
                    }
                    Destroy(instance, projectileLifeTime);

                    float timeToNextProjectile = Random.Range(baseFireRate - firingRateVariance, baseFireRate + firingRateVariance);
                    timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFireRate, float.MaxValue);

                    if (audioPlayer) {
                        audioPlayer.PlayShootingClip();
                    }
                    yield return new WaitForSeconds(timeToNextProjectile);
                }
            }
            yield return null;
        }

        //    IEnumerator MissleLauncher(float fireTime, float baseFireRate, float minimumFireRate, float firingRateVariance, float projectileLifeTime, float projectileSpeed) {
        //        foreach (Transform weapon in weaponTransform) {
        //            while (true) {
        //                GameObject instance = Instantiate(projectilePrefab[2], weapon.position, Quaternion.identity);

        //                Rigidbody2D rb2d = instance.GetComponent<Rigidbody2D>();

        //                if (rb2d != null) {
        //                    rb2d.linearVelocity = transform.up * projectileSpeed;
        //                }
        //                Destroy(instance, projectileLifeTime);

        //                float timeToNextProjectile = Random.Range(baseFireRate - firingRateVariance, baseFireRate + firingRateVariance);
        //                timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFireRate, float.MaxValue);

        //                if (audioPlayer) {
        //                    audioPlayer.PlayShootingClip();
        //                }
        //            }
        //        }
        //        yield return new WaitForSeconds(fireTime);
        //    }
        //}

    }
}



