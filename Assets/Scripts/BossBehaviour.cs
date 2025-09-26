using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private BossDifficultyData[] bossDifficultySettings;
    [SerializeField] private BossDifficulityEnum currentDifficulty = BossDifficulityEnum.Diff1;
    private Transform[] firePoints;

    AudioManager audioPlayer;
    Coroutine attackLoopCo;

    private void Awake()
    {
        audioPlayer = FindFirstObjectByType<AudioManager>();

        var firePointsList = new List<Transform>();
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("BossWeapon"))
                firePointsList.Add(child);
        }
        firePoints = firePointsList.ToArray();
    }

    private void OnEnable()
    {
        RestartAttackLoop();
    }

    private void OnDisable()
    {
        if (attackLoopCo != null) StopCoroutine(attackLoopCo);
        attackLoopCo = null;
    }

    public void SetDifficulty(BossDifficulityEnum difficulty)
    {
        currentDifficulty = difficulty;
        RestartAttackLoop();
    }

    void RestartAttackLoop()
    {
        if (attackLoopCo != null) StopCoroutine(attackLoopCo);
        var entry = GetEntry(currentDifficulty);
        if (entry != null && entry.patterns != null && entry.patterns.Length > 0)
            attackLoopCo = StartCoroutine(AttackLoop(entry));
    }

    IEnumerator AttackLoop(BossDifficultyData entry)
    {
        int i = 0;
        while (true)
        {
            var pattern = entry.patterns[i];
            if (pattern != null)
            {
                float duration = GetRandomPatternDuration(entry);
                yield return StartCoroutine(pattern.ExecuteBurst(firePoints, duration, audioPlayer));
            }
            else
            {
                yield return null;
            }

            i = (i + 1) % entry.patterns.Length;
        }
    }

    float GetRandomPatternDuration(BossDifficultyData entry)
    {
        float t = Random.Range(entry.timeBetweenPatterns - entry.patternTimeVariance,
                               entry.timeBetweenPatterns + entry.patternTimeVariance);
        return Mathf.Clamp(t, entry.minimumPatternTime, float.MaxValue);
    }

    BossDifficultyData GetEntry(BossDifficulityEnum diff)
    {
        if (bossDifficultySettings == null) return null;
        for (int i = 0; i < bossDifficultySettings.Length; i++)
        {
            var e = bossDifficultySettings[i];
            if (e != null && e.difficulty == diff) return e;
        }
        return null;
    }

    [System.Serializable]
    public class BossDifficultyData
    {
        public BossDifficulityEnum difficulty;

        public AttackPatternSO[] patterns;

        public float timeBetweenPatterns = 1f;
        public float patternTimeVariance = 0f;
        public float minimumPatternTime = 0.2f;
    }
}
