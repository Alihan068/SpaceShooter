using UnityEngine;

public class AudioManager : MonoBehaviour {
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, (1f))] float shootingVolume;
    [Header("Damaging")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, (1f))] float damageVolume = 1f;

    public void PlayShootingClip() {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamagingClip() {
        PlayClip(damageClip, damageVolume);
    }

    void PlayClip(AudioClip clip, float volume) {
        if (clip != null) {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }

    }
}