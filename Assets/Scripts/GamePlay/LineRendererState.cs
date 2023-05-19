using System;
using System.Collections;
using Character;
using UnityEngine;

namespace GamePlay
{
    public class LineRendererState : MonoBehaviour
    {

        private float _recharge;
        private float _timeRecharge;

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Enemy"))
            {
                var obj = other.gameObject.GetComponent<Enemy>();
                obj.HitDamage(PlayerManager.Instance.damage);
            }
        }
        private void OnCollisionStay(Collision other)
        {
            if (other.collider.CompareTag("Enemy"))
            {
                var obj = other.gameObject.GetComponent<Enemy>();
                obj.HitDamage(PlayerManager.Instance.damage);
            }
        }
    }
}