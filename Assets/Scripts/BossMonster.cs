using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;

public class BossMonster : MonoBehaviour {
    [SerializeField] BossStage[] bossStage;

    [System.Serializable]
    private class BossStage {
        [SerializeField] GameObject[] projectile;
        [SerializeField] float projectileSpeed;
        [SerializeField] float projectileTurnSpeed;
        [SerializeField] float projectileLifeTime = 5f;
        [SerializeField] float baseFireRate = 0.2f;
       
        public int extraHp;
        public int extraDamage;
        public bool shield;

        bool canFire;
        bool canFireMissle;
        bool canShield;

        IEnumerator AttackSequence() {
            yield return null;
            }
        }
        
    }



