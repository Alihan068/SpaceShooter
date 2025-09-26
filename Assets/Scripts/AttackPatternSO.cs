using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Attack Pattern", fileName = "AttackPattern")]
public class AttackPatternSO : ScriptableObject
{
    [Header("Projectiles & Muzzles")]
    [SerializeField] private GameObject[] projectilePrefab;

    [Header("Kinetics")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileTurnSpeed = 0f;
    [SerializeField] private float projectileLifeTime = 5f;

    [Header("Fire Rate (used within bursts)")]
    [SerializeField] private float minimumFireRate = 0.2f;
    [SerializeField] private float baseFireRate = 0.2f;
    [SerializeField] private float firingRateVariance = 0.2f;

    public void ExecuteOnce(Transform[] firePoints, AudioManager audioPlayer = null)
    {
        if (firePoints == null || firePoints.Length == 0) return;

        foreach (var point in firePoints)
        {
            if (point == null) continue;
            FireFrom(point, audioPlayer);
        }
    }

    public IEnumerator ExecuteBurst(Transform[] firePoints, float duration, AudioManager audioPlayer = null)
    {
        float endTime = Time.time + Mathf.Max(0f, duration);
        while (Time.time < endTime)
        {
            ExecuteOnce(firePoints, audioPlayer);

            float dt = Random.Range(baseFireRate - firingRateVariance,
                                    baseFireRate + firingRateVariance);
            dt = Mathf.Clamp(dt, minimumFireRate, float.MaxValue);
            yield return new WaitForSeconds(dt);
        }
    }

    void FireFrom(Transform firePoint, AudioManager audioPlayer)
    {
        if (firePoint == null || projectilePrefab == null || projectilePrefab.Length == 0) return;

        int idx = Random.Range(0, projectilePrefab.Length);
        GameObject prefab = projectilePrefab[idx];
        if (!prefab) return;

        var instance = Instantiate(prefab, firePoint.position, firePoint.rotation);
        var rb2d = instance.GetComponent<Rigidbody2D>();
        if (rb2d != null)
            rb2d.linearVelocity = firePoint.up * projectileSpeed;

        if (audioPlayer)
            audioPlayer.PlayShootingClip();

        Destroy(instance, projectileLifeTime);
    }
}
