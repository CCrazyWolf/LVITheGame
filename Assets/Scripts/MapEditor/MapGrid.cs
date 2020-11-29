using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LVITHeGame.MapEditor
{
    public class MapGrid : MonoBehaviour
    {
        public static MapGrid instance;

        int cellCountX = 10;
        int cellCountZ= 6;

        int xChunk = 2;
        int zChunk = 2;

        int xSize => cellCountX / xChunk;
        int zSize => cellCountZ / zChunk;


        public Cell cellPrefab;
        public Cell[] cells;

        public MapGridChunk chunkPrefab;
        public MapGridChunk[] chunks;

        private void Awake()
        {
            instance = this;

            chunks = new MapGridChunk[xChunk * zChunk];
            for (int i = 0; i < chunks.Length; i++)
            {
                chunks[i] = Instantiate(chunkPrefab);
                chunks[i].cells = new Cell[xSize * zSize];
            }


            cells = new Cell[cellCountX * cellCountZ];

            for (int z = 0, i = 0; z < cellCountZ; z++)
            {
                for (int x = 0; x < cellCountX; x++, i++)
                {
                    Cell cell = Instantiate(cellPrefab);
                    cell.transform.position = new Vector3(x * 2f, 0f, z * 2f);
                    cells[i] = cell;

                    if (x > 0)
                    {
                        cell.SetNeighbor(cells[i - 1], QubeDirections.W);
                    }
                    if (z > 0)
                    {
                        cell.SetNeighbor(cells[i - xSize], QubeDirections.S);
                    }

                    int chunkZ = z / zSize;
                    int chunkX = x / xSize;
                    int chunkI = chunkZ * xChunk + chunkX;

                    int xInChunk = x % xSize;
                    int zInChunk = z % zSize;
                    int iInChunk = zInChunk * xSize + xInChunk;

                    cell.SetChunk(iInChunk, chunks[chunkI]);
                }
            }
        }

        public Cell GetCell(int x, int z)
        {
            return cells[z * cellCountX + x];
        }
        public Cell GetCellFromPosition(Vector3 position)
        {
            float z = position.z / MapMetrics.cellLength;
            float x = position.x / MapMetrics.cellLength;

            int X = Mathf.RoundToInt(x);
            int Z = Mathf.RoundToInt(z);

            return null;
        }
    }
}
