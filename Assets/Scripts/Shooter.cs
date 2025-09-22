using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour {
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFireRate = 0.2f;

    [Header("Enemy")]
    [SerializeField] bool isAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;


    [HideInInspector] public bool isFiring;


    Coroutine firingCoroutine;

    void Start() {
        if (isAI) { 
            isFiring = true;
        }
    }


    // Update is called once per frame
    void Update() {
        Fire();
    }


    void Fire() {
        if (isFiring && firingCoroutine == null) {
            firingCoroutine = StartCoroutine(FireContiniously());

        }
        else if (!isFiring && firingCoroutine != null) {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContiniously() {
        while (true) {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb2d = instance.GetComponent<Rigidbody2D>();

            if (rb2d != null) {
                rb2d.linearVelocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifeTime);

            float timeToNextProjectile = Random.Range(baseFireRate - firingRateVariance, baseFireRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}