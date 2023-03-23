using System;
using System.Collections;
using Levels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class SpawnObjectController : MonoBehaviour
    {
        [SerializeField] private Transform[] objects;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Transform parent;
        [SerializeField] private float minSpawnTime;
        [SerializeField] private float maxSpawnTime;

        private bool canSpawn;

        private void Awake()
        {
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelComplete += OnLevelComplete;
            LevelManager.OnLevelFail += OnLevelFail;
        }
        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelComplete -= OnLevelComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
        }

        private IEnumerator Spawner()
        {
            while (canSpawn)
            {
                float time = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(time);
                var spawnPoint = GetSpawnPoint();
                Instantiate(GetObject(), spawnPoint.position, spawnPoint.rotation, parent);
            }
        }

        private Transform GetSpawnPoint()
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            return spawnPoints[randomSpawnPoint];
        }
        private Transform GetObject()
        {
            int randomObject = Random.Range(0, objects.Length);
            return objects[randomObject];
        }
        
        #region Events

        private void OnLevelStart(Level level)
        {
            canSpawn = true;
            StartCoroutine(Spawner());
        }

        private void OnLevelComplete(Level level)
        {
            canSpawn = false;
        }

        private void OnLevelFail(Level level)
        {
            canSpawn = false;
        }

        #endregion
    }
}
