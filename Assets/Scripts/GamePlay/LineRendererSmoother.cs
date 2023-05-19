using UnityEngine;

namespace GamePlay
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineRendererSmoother : MonoBehaviour
    {
        public LineRenderer line;
        public Vector3[] initialState = new Vector3[1];
        public float smoothingLength = 2f;
        public int smoothingSections = 10;

        // private void OnGUI()
        // {
        //     if (GUI.Button(new Rect(10, 10, 300, 40), "Smooth Line"))
        //     {
        //         Smooth();
        //     }
        //
        //     if (GUI.Button(new Rect(10, 60, 300, 40), "Generate Collider"))
        //     {
        //         GenerateMeshCollider();
        //     }
        //
        //     if (GUI.Button(new Rect(10, 110, 300, 40), "Simplify Mesh"))
        //     {
        //         line.Simplify(0.1f);
        //     }
        // }

        public void GenerateMeshCollider()
        {
            var col = GetComponent<MeshCollider>();

            if (col == null)
            {
                col = gameObject.AddComponent<MeshCollider>();
            }


            var mesh = new Mesh();
            line.BakeMesh(mesh, true);

            // if you need collisions on both sides of the line, simply duplicate & flip facing the other direction!
            // This can be optimized to improve performance ;)
            var meshIndices = mesh.GetIndices(0);
            var newIndices = new int[meshIndices.Length * 2];

            var j = meshIndices.Length - 1;
            for (var i = 0; i < meshIndices.Length; i++)
            {
                newIndices[i] = meshIndices[i];
                newIndices[meshIndices.Length + i] = meshIndices[j];
            }

            mesh.SetIndices(newIndices, MeshTopology.Triangles, 0);

            col.sharedMesh = mesh;
            // gameObject.GetComponent<MeshCollider>().convex = true;
            // gameObject.GetComponent<MeshCollider>().isTrigger = true;
        }

        private void Smooth()
        {
            var curves = new BezierCurve[line.positionCount - 1];
            for (var i = 0; i < curves.Length; i++)
            {
                curves[i] = new BezierCurve();
            }

            for (int i = 0; i < curves.Length; i++)
            {
                var position = line.GetPosition(i);
                var lastPosition = i == 0 ? line.GetPosition(0) : line.GetPosition(i - 1);
                var nextPosition = line.GetPosition(i + 1);

                var lastDirection = (position - lastPosition).normalized;
                var nextDirection = (nextPosition - position).normalized;

                var startTangent = (lastDirection + nextDirection) * smoothingLength;
                var endTangent = (nextDirection + lastDirection) * -1 * smoothingLength;


                curves[i].points[0] = position; // Start Position (P0)
                curves[i].points[1] = position + startTangent; // Start Tangent (P1)
                curves[i].points[2] = nextPosition + endTangent; // End Tangent (P2)
                curves[i].points[3] = nextPosition; // End Position (P3)
            }

            // Apply look-ahead for first curve and retroactively apply the end tangent
            {
                var nextDirection = (curves[1].EndPosition - curves[1].StartPosition).normalized;
                var lastDirection = (curves[0].EndPosition - curves[0].StartPosition).normalized;

                curves[0].points[2] = curves[0].points[3] +
                                      (nextDirection + lastDirection) * -1 * smoothingLength;
            }

            line.positionCount = curves.Length * smoothingSections;
            var index = 0;
            foreach (var t in curves)
            {
                var segments = t.GetSegments(smoothingSections);
                foreach (var t1 in segments)
                {
                    line.SetPosition(index, t1);
                    index++;
                }
            }
        }
    }
}