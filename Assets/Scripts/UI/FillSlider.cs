using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FillSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField]private float maxStamina;
        private float _currentStamina;

        public static FillSlider Instance;

        private Coroutine _coroutine;

        public bool canDraw;
        private const float AmountStamina = 2f;
        private WaitForSeconds _waitForRegen = new WaitForSeconds(2f);
        private WaitForSeconds _waitForPerTime = new WaitForSeconds(0.1f);
        
        private void Awake()
        {
            Instance = this;
            canDraw = true;
        }

        private void Start()
        {
            _currentStamina = maxStamina;
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }

        private void Update()
        {
            canDraw = !(_currentStamina <= AmountStamina);
        }

        public void UseStamina(float amount)
        {
            if (_currentStamina - amount >= 0)
            {
                canDraw = true;
                _currentStamina -= amount;
                slider.value = _currentStamina;
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = StartCoroutine(RegenStamina());
            }
            else
            {
                canDraw = false;
            }
        }

        private IEnumerator RegenStamina()
        {
            yield return _waitForRegen;
            while (_currentStamina < maxStamina)
            {
                _currentStamina += maxStamina / 50;
                slider.value = _currentStamina;
                yield return _waitForPerTime;
            }

            _coroutine = null;
        }
    }
}