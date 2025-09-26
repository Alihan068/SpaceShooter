using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateAttackPattern", fileName = "NewAttackPattern")]

public class AttackPatternSO : ScriptableObject {
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] List<Transform> projectileSpawnPos;
    [SerializeField] float projectileLifeTime = 5f;

    [SerializeField] float minimumFireRate = 0.2f;
    [SerializeField] float baseFireRate = 0.2f;
    [SerializeField] float firingRateVariance = 0.2f;
    [SerializeField] float totalFireTime;
    [SerializeField] float projectileSpeed = 5f;



    Rigidbody2D rb2d;
    IEnumerator enumerator;
    void Start() {
        //StartCoroutine(AttackSequence());
    }

    private void StartCoroutine() {
        throw new System.NotImplementedException();
    }

    public GameObject GetProjectilePrefab() => projectilePrefab;
    public List<Transform> GetProjectileSpawnPos() => projectileSpawnPos;
    public float GetProjectileLifeTime() => projectileLifeTime;
    public float GetMinimumFireRate() => minimumFireRate;
    public float GetBaseFireRate() => baseFireRate;
    public float GetFiringRateVariance() => firingRateVariance;
    public float GetTotalFireTime() => totalFireTime;
    public float GetProjectileSpeed() => projectileSpeed;
   

}

//    public IEnumerator AttackSequence() {
//        foreach (Transform weapon in projectileSpawnPos) {
//            while (true) {
//                GameObject instance = Instantiate(projectilePrefab, weapon.position, Quaternion.identity);

//                rb2d = instance.GetComponent<Rigidbody2D>();

//                if (rb2d != null) {
//                    rb2d.linearVelocity = weapon.up * projectileSpeed;
//                }
//                Destroy(instance, projectileLifeTime);

//                float timeToNextProjectile = Random.Range(baseFireRate - firingRateVariance, baseFireRate + firingRateVariance);
//                Mathf.Clamp(timeToNextProjectile, minimumFireRate, float.MaxValue);

//                yield return new WaitForSeconds(timeToNextProjectile);

//            }
//        }
//        yield return null;
//    }

//}
