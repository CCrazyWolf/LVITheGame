using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LVITHeGame.MapEditor
{
    public class Cell : MonoBehaviour
    {
        public bool canModify = true;

        private MapGridChunk chunk;

        public MapObject objectInCell = null;

        private Cell[] neighbors;

        private void Awake()
        {
            neighbors = new Cell[4];
        }

        public void SetNeighbor(Cell other, QubeDirections direction)
        {
            neighbors[(int)direction] = other;
            other.neighbors[(int)direction.Opposite()] = this;
        }

        public Cell GetNeighbor(QubeDirections direction)
        {
            return neighbors[(int)direction];
        }

        public void SetChunk(int i, MapGridChunk chunk)
        {
            chunk.cells[i] = this;
            transform.SetParent(chunk.transform, false);
            this.chunk = chunk;
        }

        public void Refresh()
        {
            chunk.Refresh();
        }
    }
}
