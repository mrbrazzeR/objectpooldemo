using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class SpawnEnemies : MonoBehaviour
    {
        [SerializeField] private EnemiesPool pool;

        [SerializeField] private List<Transform> spawnPosition;
        [SerializeField] private float timeSpawn;
        [SerializeField] private int countSpawn;

        public static int CountWave;

        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(SpawnPerWave());
        }


        private IEnumerator SpawnPerWave()
        {
            {
                for (var i = 0; i < countSpawn; i++)
                {
                    var go = pool.Spawn("Enemy");
                    if (go != null)
                    {
                        var randomPosition = Random.Range(0, spawnPosition.Count);
                        go.transform.position = spawnPosition[randomPosition].position;
                    }
                }

                yield return new WaitForSeconds(timeSpawn);
                timeSpawn -= 0.2f;
                CountWave++;
                if (CountWave % 5 == 0)
                {
                    countSpawn++;
                }

                if (countSpawn > 15)
                {
                    countSpawn = 15;
                }

                if (timeSpawn < 0.2f)
                {
                    timeSpawn = 0.2f;
                }
            }
        }
    }
}