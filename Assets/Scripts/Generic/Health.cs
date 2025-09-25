using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int health = 100;
    [SerializeField] bool isPlayer;
    [SerializeField] int score = 20;

    LevelManager levelManager;
    AudioManager audioPlayer;
    ScoreKeeper scoreKeeper;
    DamageDealer damageDealer;
    EnemySpawner enemySpawner;
    public int GetHealth() {
        return health;
    }

    private void Awake() {
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        audioPlayer = FindFirstObjectByType<AudioManager>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null ) {
            TakeDamage(damageDealer.GetDamage());
            audioPlayer.PlayDamagingClip();
            damageDealer.Hit();
        }
    }

    private void OnParticleTrigger() {
        
    }

    void TakeDamage(int damage) {

        health -= damage;

        if (health <= 0) {
            Death();
        }
    }
    void Death() {

        if (gameObject.tag == "Boss") { 
            enemySpawner.activeBoss = false;
            enemySpawner.bossesDefeated++;
        }

        if (!isPlayer) { 
            scoreKeeper.ModifyScore(score);
            audioPlayer.PlayDeathClip();
        }
        else {
            levelManager.LoadGameOver();
        }

            Destroy(gameObject);
    }


    private void OnParticleCollision(GameObject other)
    {
        damageDealer = other.GetComponent<DamageDealer>();

        Debug.Log($"{name} was hit by particles from {other.name}", this);
        if (damageDealer != null) {
            TakeDamage(damageDealer.GetDamage());
        }
    }
}
