using System;
using UnityEngine;

public class CameraManager:MonoBehaviour
{
        [SerializeField] private Vector3 camOffset;
        [SerializeField] private Transform firstPlayer;
        [SerializeField] private Transform secondPlayer;

        private Vector3 _velocity;
        [SerializeField] private float smoothTime;
        private void LateUpdate()
        {
                var centerPoint = GetCenterPoint();
                var newPosition = centerPoint + camOffset;
                transform.position = Vector3.SmoothDamp(transform.position,newPosition,ref _velocity,smoothTime);
        }

        private Vector3 GetCenterPoint()
        {
                var bounds=new Bounds(firstPlayer.position,Vector3.zero);
                bounds.Encapsulate(secondPlayer.position);
                return bounds.center;
        }
}