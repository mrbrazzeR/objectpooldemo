using System.Collections;
using UnityEngine;

namespace GamePlay
{
    public class EarnBuff : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("AddDamage"))
            {
                PlayerManager.Instance.damage += 5;
                other.gameObject.SetActive(false);
            }

            if (other.collider.CompareTag("Speed"))
            {
                StartCoroutine(Boost());
                other.gameObject.SetActive(false);
            }
        }

        private IEnumerator Boost()
        {
            {
                PlayerManager.Instance.speed += 2f;
                yield return new WaitForSeconds(3f);
                PlayerManager.Instance.speed -= 2f;
            }
        }
    }
}
