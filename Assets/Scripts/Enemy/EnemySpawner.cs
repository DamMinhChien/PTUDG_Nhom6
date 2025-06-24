using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave {
        public GameObject enemyPrefab;
        public float spawnTimer;
        public float spawnInterval;
        public int enemiesPerWave;
        public int spawnedEnemyCount;
    }
    public List<Wave> waves;
    public int waveNumber;
    public Transform minPos;
    public Transform maxPos;

    // Thêm biến lưu thời gian cập nhật độ khó
    private float difficultyUpdateTimer = 0f;
    private float difficultyUpdateInterval = 10f; // mỗi 10 giây tăng độ khó

    void Update()
    {
        if (PlayerController.Instance.gameObject.activeSelf){
            // Tăng độ khó theo thời gian
            difficultyUpdateTimer += Time.deltaTime;
            if (difficultyUpdateTimer >= difficultyUpdateInterval)
            {
                difficultyUpdateTimer = 0f;
                IncreaseDifficulty();
            }

            waves[waveNumber].spawnTimer += Time.deltaTime;
            if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval){
                waves[waveNumber].spawnTimer = 0;
                SpawnEnemy();
            }
            if (waves[waveNumber].spawnedEnemyCount >= waves[waveNumber].enemiesPerWave){
                waves[waveNumber].spawnedEnemyCount = 0;
                if (waves[waveNumber].spawnInterval > 0.15f){
                    waves[waveNumber].spawnInterval *= 0.8f;
                }
                waveNumber++;
            }
            if (waveNumber >= waves.Count){
                waveNumber = 0;
            }
        }
    }

    // Hàm tăng độ khó
    private void IncreaseDifficulty()
    {
        foreach (var wave in waves)
        {
            wave.enemiesPerWave += 1; // tăng số lượng enemy mỗi wave
            wave.spawnInterval = Mathf.Max(0.1f, wave.spawnInterval * 0.95f); // giảm thời gian spawn, tối thiểu 0.1s
        }
    }

    private void SpawnEnemy(){
        Instantiate(waves[waveNumber].enemyPrefab, RandomSpawnPoint(), transform.rotation);
        waves[waveNumber].spawnedEnemyCount++;
    }

    private Vector2 RandomSpawnPoint(){
        Vector2 spawnPoint;
        if (Random.Range(0f, 1f) > 0.5){
            spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);
            if (Random.Range(0f, 1f) > 0.5){
                spawnPoint.y = minPos.position.y;
            } else {
                spawnPoint.y = maxPos.position.y;
            }
        } else {
            spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);
            if (Random.Range(0f, 1f) > 0.5){
                spawnPoint.x = minPos.position.x;
            } else {
                spawnPoint.x = maxPos.position.x;
            }
        }
        return spawnPoint;
    }
}
