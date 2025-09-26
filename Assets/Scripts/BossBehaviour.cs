using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {
    [SerializeField] BossDifficulityEnum[] bossDifficulityEnum;
    [System.Serializable]
    private class BossModifiers {
        [SerializeField] GameObject[] projectilePrefab;
        [SerializeField] Transform[] weaponTransform;
        [SerializeField] float projectileSpeed;
        [SerializeField] float projectileTurnSpeed;
        [SerializeField] float projectileLifeTime = 5f;

        [SerializeField] float minimumFireRate = 0.2f;
        [SerializeField] float baseFireRate = 0.2f;
        [SerializeField] float firingRateVariance = 0.2f;

        Transform transform;
        AudioManager audioPlayer;

        public int extraHp;
        public int extraDamage;
        public bool shield;

        bool canFire;
        bool canFireMissle;
        bool canShield;

        void Update() {
            //switch (BossDifficulityEnum[]) {
               
            //}
        }
        IEnumerator BossMachineGun(float fireTime) {
            while (true) {
                GameObject instance = Instantiate(projectilePrefab[1], transform.position, Quaternion.identity);

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
            }
            yield return new WaitForSeconds(fireTime);
        }

        IEnumerator MissleLauncher(float fireTime) {
            while (true) {
                GameObject instance = Instantiate(projectilePrefab[1], transform.position, Quaternion.identity);

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
            }
            yield return new WaitForSeconds(fireTime);
        }
    }

}



