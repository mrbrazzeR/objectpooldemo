using System;
using System.Collections.Generic;
using GamePlay;
using UI;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    public class Enemy : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Transform _transform;

        [SerializeField] private int health;
        private int _baseHealth;
        [SerializeField] private int damage;
        [SerializeField] private int score;

        private const float MaxSpeed = 5f;
        private const float StepSpeed = 0.1f;
        private GameObject[] player;

        struct MoveJob : IJobParallelFor
        {
            public NativeArray<Vector3> positions;
            public Vector3 targetPosition;
            public float speed;
            public float deltaTime;

            public void Execute(int index)
            {
                Vector3 direction = targetPosition - positions[index];
                positions[index] += direction.normalized * speed * deltaTime;
            }
        }

        NativeArray<Vector3> positions;

        JobHandle moveHandle;
        public float speed = 10f;
        public int count = 100000;

        // Start is called before the first frame update
        private void Awake()
        {
            player = GameObject.FindGameObjectsWithTag("Player");
            positions = new NativeArray<Vector3>(count, Allocator.Persistent);
            for (int i = 0; i < count; i++)
            {
                positions[i] = new Vector3(i * 0.1f - count * 0.05f, 0f, 0f);
            }

            _navMeshAgent = GetComponent<NavMeshAgent>();
            health = _baseHealth;
            if (SpawnEnemies.CountWave % 5 == 0)
            {
                _navMeshAgent.speed += StepSpeed;
            }

            if (_navMeshAgent.speed > MaxSpeed)
            {
                _navMeshAgent.speed = MaxSpeed;
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (moveHandle.IsCompleted)
            {
                var nearestObject = ComparePosition(player);
                _transform = nearestObject.transform;
                if (_transform != null)
                {
                    _navMeshAgent.destination = _transform.position;
                }

                moveHandle.Complete();

                MoveJob moveJob = new MoveJob()
                {
                    positions = positions,
                    targetPosition = _navMeshAgent.destination,
                    speed = speed,
                    deltaTime = Time.deltaTime
                };

                moveHandle = moveJob.Schedule(count, 64);
            }
        }

        // private void OnDisable()
        // {
        //     moveHandle.Complete();
        //     positions.Dispose();
        // }

        private GameObject ComparePosition(IEnumerable<GameObject> gameObjects)
        {
            GameObject tMin = null;
            var minDist = Mathf.Infinity;
            var currentPos = transform.position;
            foreach (var t in gameObjects)
            {
                var dist = Vector3.Distance(t.transform.position, currentPos);
                if (!(dist < minDist)) continue;
                tMin = t;
                minDist = dist;
            }

            return tMin;
        }

        public void HitDamage(int hitDamage)
        {
            health -= hitDamage;
            if (health <= 0)
            {
                var obj = FindObjectOfType<GameManager>();
                obj.UpdateScore(score);
                moveHandle.Complete();
                positions.Dispose();
                gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Player"))
            {
                var obj = other.gameObject.GetComponentInParent<PlayerController>();
                obj.HitDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }
}