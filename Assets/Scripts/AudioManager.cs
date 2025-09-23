using UnityEngine;

public class AudioManager : MonoBehaviour {
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, (1f))] float shootingVolume;
    [Header("Damaging")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, (1f))] float damageVolume = 1f;

    static AudioManager instance;

    private void Awake() {
        ManageSingleton();
    }

    void ManageSingleton() {
        if (instance != null) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

            public void PlayShootingClip() {
                PlayClip(shootingClip, shootingVolume);
            }

    public void PlayDamagingClip() {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayDeathClip() {
        PlayClip(damageClip, damageVolume);
    }

    void PlayClip(AudioClip clip, float volume) {
        if (clip != null  /* && audioSource != null */ ) { //add another nullcheck
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }

    }
}