using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LVITHeGame.MapEditor
{
    public class MapGridChunk : MonoBehaviour
    {
        public Cell[] cells;

        public MapMesh mesh;

        private bool needRefresh = true;

        private void Update()
        {
            Vector3? pos = mesh.MouseOver();
            if (pos != null)
            {
                Cell cell = MapGrid.instance.GetCellFromPosition((Vector3)pos);
            }
        }

        private void LateUpdate()
        {
            if (needRefresh)
            {
                Triangulate(cells);
            }
        }

        public void Refresh()
        {
            needRefresh = true;
        }

        private void Triangulate(Cell[] cells)
        {
            mesh.Clear();
            for (int i = 0; i< cells.Length; i++)
                TriangulateCell(cells[i]);
            needRefresh = false;
            mesh.Apply();
        }

        private void TriangulateCell(Cell cell)
        {
            Vector3 center = cell.transform.position;
            mesh.AddQuad(center + MapMetrics.vertices[0], center + MapMetrics.vertices[3],
                center + MapMetrics.vertices[1], center + MapMetrics.vertices[2]);
        }
    }
}