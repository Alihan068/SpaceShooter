using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int health = 100;

    AudioManager audioPlayer;

    private void Awake() {
        audioPlayer = GetComponent<AudioManager>();
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
            Destroy(gameObject);
        }
    }
}
