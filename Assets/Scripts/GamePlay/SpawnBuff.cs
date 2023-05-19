using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace GamePlay
{
    public class SpawnBuff : MonoBehaviour
    {
        [SerializeField] private EnemiesPool pool;

        [SerializeField] private List<Transform> spawnPosition;
        [SerializeField] private float timeSpawn;
        [SerializeField] private int countSpawn;

        [SerializeField] private int timeSpawnSpeed;

        private void Start()
        {
            StartCoroutine(SpawnDamageBuff());
            StartCoroutine(SpawnSpeedBuff());
        }


        private IEnumerator SpawnDamageBuff()
        {
            {
                for (var i = 0; i < countSpawn; i++)
                {
                    var go = pool.Spawn("AddDamage");
                    if (go != null)
                    {
                        var randomPosition = Random.Range(0, spawnPosition.Count);
                        go.transform.position = spawnPosition[randomPosition].position;
                    }
                }

                yield return new WaitForSeconds(timeSpawn);
            }
        }

        private IEnumerator SpawnSpeedBuff()
        {
            var go = pool.Spawn("AddDamage");
            if (go != null)
            {
                var randomPosition = Random.Range(0, spawnPosition.Count);
                go.transform.position = spawnPosition[randomPosition].position;
            }

            yield return new WaitForSeconds(timeSpawnSpeed);
        }
    }
}