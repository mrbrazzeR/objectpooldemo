using UnityEngine;

namespace GamePlay
{
    [System.Serializable]
    public class BezierCurve
    {
        public Vector3[] points;

        public BezierCurve()
        {
            points = new Vector3[4];
        }

        public BezierCurve(Vector3[] points)
        {
            this.points = points;
        }

        public Vector3 StartPosition => points[0];

        public Vector3 EndPosition => points[3];

        // Equations from: https://en.wikipedia.org/wiki/B%C3%A9zier_curve
        public Vector3 GetSegment(float Time)
        {
            Time = Mathf.Clamp01(Time);
            var time = 1 - Time;
            return (time * time * time * points[0])
                   + (3 * time * time * Time * points[1])
                   + (3 * time * Time * Time * points[2])
                   + (Time * Time * Time * points[3]);
        }

        public Vector3[] GetSegments(int subdivisions)
        {
            var segments = new Vector3[subdivisions];

            float time;
            for (var i = 0; i < subdivisions; i++)
            {
                time = (float)i / subdivisions;
                segments[i] = GetSegment(time);
            }

            return segments;
        }
    }
}