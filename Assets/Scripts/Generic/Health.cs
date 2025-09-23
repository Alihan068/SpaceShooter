using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int health = 100;
    [SerializeField] bool isPlayer;
    [SerializeField] int score = 20;

   LevelManager levelManager;
    AudioManager audioPlayer;
    ScoreKeeper scoreKeeper;
    public int GetHealth() {
        return health;
    }

    private void Awake() {
        audioPlayer = FindFirstObjectByType<AudioManager>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null ) {
            TakeDamage(damageDealer.GetDamage());
            audioPlayer.PlayDamagingClip();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage) {

        health -= damage;

        if (health < 0) {
            Death();
        }
    }
    void Death() {
        if (!isPlayer) { 
            scoreKeeper.ModifyScore(score);
        }
        else {
            levelManager.LoadGameOver();
        }

            Destroy(gameObject);
    }
}
