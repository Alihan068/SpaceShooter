using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] int loopCount = 1;
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
                for (int i = 0; i < currentWave.GetEnemyCount(); i++) {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0, 0, 180), transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                if (activeBoss) yield return new WaitUntil(() => !activeBoss);
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while (isLooping && !activeBoss);
    }


}
