using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    public int bossesDefeated = 1;
    WaveConfigSO currentWave;

    [SerializeField] bool isLooping;

    //public bool ActiveBoss(bool activeBoss) {
    //    return activeBoss;
    //}

    public bool activeBoss = false;


    public WaveConfigSO GetCurrentWave() {
        return currentWave;
    }
    void Start() {
        StartCoroutine(SpawnEnemyWaves());
    }

    void Update() {
    }
    IEnumerator SpawnEnemyWaves() {

        do {
            foreach (WaveConfigSO wave in waveConfigs) {
                currentWave = wave;
                if (currentWave.IsBossWave()) {
                    Debug.Log("BossWave!");
                    activeBoss = true;
                }
                Debug.Log("Difficulty Multiplier = " + TotalDiffIncreaseRate());
                Debug.Log(("Target i = " + (WaveSummonAmount())));

                for (int i = 0; i < WaveSummonAmount(); i++) {
                    Instantiate(currentWave.GetEnemyPrefab(Random.Range(0, currentWave.GetEnemyCount())),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0, 0, 180), transform);
                    Debug.Log(i);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                if (activeBoss) yield return new WaitUntil(() => !activeBoss);
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while (isLooping && !activeBoss);
    }
    void RandomSummonIndex() {
        Instantiate(currentWave.GetEnemyPrefab(Random.Range(0, currentWave.GetEnemyCount())));
    }
    int WaveSummonAmount() {
        if (currentWave.IsBossWave()) {
            return currentWave.GetWaveEnemyCount();
        }
        else {
            Debug.Log("Returned = " + Mathf.FloorToInt(currentWave.GetWaveEnemyCount() + TotalDiffIncreaseRate()));
            return Mathf.FloorToInt(currentWave.GetWaveEnemyCount() + TotalDiffIncreaseRate());
        }
    }
    float TotalDiffIncreaseRate() {
        return currentWave.GetWaveEnemyCount() * (TimeDifficulity() + DefeatedBossDiffIncrease());
    }
    float TimeDifficulity() {
        return RoundFloatToOneDigit(Time.fixedTime / 50);
    }
    float DefeatedBossDiffIncrease() {
        return RoundFloatToOneDigit(bossesDefeated / 4);

    }
    float RoundFloatToOneDigit(float x) {
        return Mathf.Round(x * 10f) / 10f;
    }

}



//    IEnumerator SpawnEnemyWaves() {

//        do {
//            foreach (WaveConfigSO wave in waveConfigs) {
//                currentWave = wave;
//                if (currentWave.IsBossWave()) {
//                    Debug.Log("BossWave!");
//                    activeBoss = true;

//                }
//                for (int i = 0; i < currentWave.GetEnemyCount(); i++) {
//                    Instantiate(currentWave.GetEnemyPrefab(i),
//                        currentWave.GetStartingWaypoint().position,
//                        Quaternion.Euler(0, 0, 180), transform);
//                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
//                }
//                if (activeBoss) yield return new WaitUntil(() => !activeBoss);
//                yield return new WaitForSeconds(timeBetweenWaves);
//            }
//        }
//        while (isLooping && !activeBoss);
//    }


//}