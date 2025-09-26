
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject {

    public List<GameObject> enemyPrefabs;

    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] bool isBossWave = false;
    [SerializeField] int waveEnemyCount = 10;

    [SerializeField] float timeBetweenEnemySpawn = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 0.2f;

    [SerializeField, HideInInspector] Vector3 basePathScale = Vector3.zero;

    public Transform GetStartingWaypoint() {
        return pathPrefab.GetChild(0);
    }


    public Vector2 PathLocalScale
    {
        get => pathPrefab.localScale;
        set => pathPrefab.localScale = value;
    }


    public Vector3 GetBasePathScale()
    {
        if (basePathScale == Vector3.zero && pathPrefab.transform)
            basePathScale = pathPrefab.localScale;
        return basePathScale == Vector3.zero ? Vector3.one : basePathScale;
    }

    public List<Transform> GetWaypoints() {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab) {
            waypoints.Add(child);
        }
        return waypoints;

    }

    public float GetMoveSpeed() {
        return moveSpeed;
    }

    public int GetEnemyCount() {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index) {
        return enemyPrefabs[index];
    }

    public float GetRandomSpawnTime() {
        float spawnTime = Random.Range(timeBetweenEnemySpawn - spawnTimeVariance, timeBetweenEnemySpawn + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }

    public bool IsBossWave() {
        return isBossWave;
    }
    public int GetWaveEnemyCount() {
        return waveEnemyCount;
    }
}
