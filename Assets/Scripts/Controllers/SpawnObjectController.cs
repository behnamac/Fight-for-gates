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
            // Subscribe to level events
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelComplete += OnLevelComplete;
            LevelManager.OnLevelFail += OnLevelFail;
        }

        private void OnDestroy()
        {
            // Unsubscribe from level events
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

                // Get a random spawn point and instantiate a random object at that point
                var spawnPoint = GetSpawnPoint();
                Instantiate(GetObject(), spawnPoint.position, spawnPoint.rotation, parent);
            }
        }

        private Transform GetSpawnPoint()
        {
            // Get a random spawn point from the array
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            return spawnPoints[randomSpawnPoint];
        }

        private Transform GetObject()
        {
            // Get a random object from the array
            int randomObject = Random.Range(0, objects.Length);
            return objects[randomObject];
        }

        #region Events

        private void OnLevelStart(Level level)
        {
            // Start spawning objects on level start
            canSpawn = true;
            StartCoroutine(Spawner());
        }

        private void OnLevelComplete(Level level)
        {
            // Stop spawning objects on level complete
            canSpawn = false;
        }

        private void OnLevelFail(Level level)
        {
            // Stop spawning objects on level fail
            canSpawn = false;
        }

        #endregion
    }
}
