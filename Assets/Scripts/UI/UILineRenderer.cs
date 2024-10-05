using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
    {
        public List<Vector2> points;

        [SerializeField] float thickness = 10f;
        public bool center = true;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            if (points.Count < 2)
                return;

            for (int i = 0; i < points.Count-1; i++)
            {
                CreateLineSegment(points[i], points[i+1], vh);

                int index = i * 5;

                vh.AddTriangle(index, index+1, index+3);
                vh.AddTriangle(index+3, index+2, index);

                if (i != 0)
                {
                    vh.AddTriangle(index, index-1, index-3);
                    vh.AddTriangle(index+1, index-1, index-2);
                }
            }
        }

        private void CreateLineSegment(Vector3 point1, Vector3 point2, VertexHelper vh)
        {
            Vector3 offset = center ? (rectTransform.sizeDelta / 2) : Vector2.zero;

            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            Quaternion point1Rotation = Quaternion.Euler(0, 0, RotatePointTowards(point1, point2) + 90);
            vertex.position = point1Rotation * new Vector3(-thickness / 2, 0);
            vertex.position += point1 - offset;
            vh.AddVert(vertex);
            vertex.position = point1Rotation * new Vector3(thickness / 2, 0);
            vertex.position += point1 - offset;
            vh.AddVert(vertex);

            Quaternion point2Rotation = Quaternion.Euler(0, 0, RotatePointTowards(point2, point1) - 90);
            vertex.position = point2Rotation * new Vector3(-thickness / 2, 0);
            vertex.position += point2 - offset;
            vh.AddVert(vertex);
            vertex.position = point2Rotation * new Vector3(thickness / 2, 0);
            vertex.position += point2 - offset;
            vh.AddVert(vertex);

            vh.AddVert(vertex);
        }

        private float RotatePointTowards(Vector2 vertex, Vector2 target)
        {
            return (float)(Mathf.Atan2(target.y - vertex.y, target.x - vertex.x) * (180 / Mathf.PI));
        }
}