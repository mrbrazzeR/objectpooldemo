using System.Collections;
using UI;
using UnityEngine;

namespace GamePlay
{
    public class LineRendererDraw : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform[] transforms;

        [SerializeField] private LineRendererSmoother lineRendererSmoother;

        [SerializeField] private float energyLine;

        private Coroutine _coroutine;
        
        private readonly WaitForSeconds _timeForTurnoff= new WaitForSeconds(0.2f);
        private void Start()
        {
            SetupLine();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            LineRenderTask();
        }

        private void LineRenderTask()
        {
            lineRenderer.SetPosition(0, transforms[0].position);
            lineRenderer.SetPosition(1, transforms[1].position);
            if (FillSlider.Instance.canDraw)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    FillSlider.Instance.UseStamina(energyLine);
                    lineRendererSmoother.GenerateMeshCollider();
                    lineRenderer.gameObject.SetActive(true);
                    if (_coroutine != null)
                    {
                        StopCoroutine(_coroutine);
                    }
                }
            }

            _coroutine = StartCoroutine(SetStatusLine());
        }

        private void SetupLine()
        {
            lineRenderer.positionCount = transforms.Length;
        }

        private IEnumerator SetStatusLine()
        {
            yield return _timeForTurnoff;
            lineRenderer.gameObject.SetActive(false);
        }
    }
}