using System.Collections;
using UI;
using UnityEngine;

namespace GamePlay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody firstPlayer;
        [SerializeField] private Rigidbody secondPlayer;
        
        [SerializeField] private float rotateSpeed = 1f;

        private Coroutine _firstPlayerLook;
        private Coroutine _secondPlayerLook;
        [SerializeField] private EndGame endGame;
        private void Start()
        {
            StartRotating();
        }
        private void FixedUpdate()
        {
            FirstPlayerMovement();
            SecondPlayerMovement();
        }
        
        private void FirstPlayerMovement()
        {
            if (Input.GetKey(KeyCode.A))
            {
                firstPlayer.velocity = Vector3.left * PlayerManager.Instance.speed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                firstPlayer.velocity = Vector3.right * PlayerManager.Instance.speed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                firstPlayer.velocity = Vector3.back * PlayerManager.Instance.speed;
            }

            if (Input.GetKey(KeyCode.W))
            {
                firstPlayer.velocity = Vector3.forward * PlayerManager.Instance.speed;
            }
        }

        private void SecondPlayerMovement()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                secondPlayer.velocity = Vector3.left * PlayerManager.Instance.speed;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                secondPlayer.velocity = Vector3.right * PlayerManager.Instance.speed;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                secondPlayer.velocity = Vector3.back * PlayerManager.Instance.speed;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                secondPlayer.velocity = Vector3.forward * PlayerManager.Instance.speed;
            }
        }

        private void StartRotating()
        {
            if (_firstPlayerLook != null)
            {
                StopCoroutine(_firstPlayerLook);
            }

            if (_secondPlayerLook != null)
            {
                StopCoroutine(_secondPlayerLook);
            }

            _firstPlayerLook = StartCoroutine(LookAtFirstPlayer());
            _secondPlayerLook = StartCoroutine(LookAtSecondPlayer());
        }

        private IEnumerator LookAtFirstPlayer()
        {
            var lookRotation = Quaternion.LookRotation(secondPlayer.position - firstPlayer.position);
            float timed = 0;
            while (timed < 1)
            {
                firstPlayer.rotation=Quaternion.Slerp(firstPlayer.rotation,lookRotation,timed);
                timed += Time.deltaTime * rotateSpeed;
                yield return null;
            }
        }

        private IEnumerator LookAtSecondPlayer()
        {
            var lookRotation = Quaternion.LookRotation(firstPlayer.position - secondPlayer.position);
            float timed = 0;
            while (timed < 1)
            {
                secondPlayer.rotation=Quaternion.Slerp(secondPlayer.rotation,lookRotation,timed);
                timed += Time.deltaTime * rotateSpeed;
                yield return null;
            }
        }

        public void HitDamage(int damage)
        {
            PlayerManager.Instance.health -= damage;
            var obj = FindObjectOfType<GameManager>();
            obj.UpdateHealth();
            if (PlayerManager.Instance.health <= 0)
            {
                Time.timeScale = 0.1f;
                endGame.gameObject.SetActive(true);
                Debug.Log("Game Over");
            }
        }

    }
}