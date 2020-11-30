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

        int chunkCountX = 2;
        int chunkCountZ = 2;

        int xSize => cellCountX / chunkCountX;
        int zSize => cellCountZ / chunkCountZ;


        public Cell cellPrefab;
        public Cell[] cells;

        public MapGridChunk chunkPrefab;
        public MapGridChunk[] chunks;

        private void Awake()
        {
            instance = this;

            CreateChunks();
            CreateCells();            
        }

        void CreateChunks()
        {
            chunks = new MapGridChunk[chunkCountX * chunkCountZ];
            for (int z = 0, i=0; z < chunkCountZ; z++)
            {
                for (int x = 0; x < chunkCountX; x++, i++)
                {
                    MapGridChunk chunk = chunks[i] = Instantiate(chunkPrefab);
                    chunk.cells = new Cell[xSize * zSize];
                    chunk.transform.SetParent(this.transform, false);
                }
            }
        }

        void CreateCells()
        {
            cells = new Cell[cellCountX * cellCountZ];

            for (int z = 0, i = 0; z < cellCountZ; z++)
            {
                for (int x = 0; x < cellCountX; x++, i++)
                {
                    Cell cell = cells[i] = Instantiate(cellPrefab);
                    cell.transform.position = new Vector3(x * MapMetrics.cellLength, 0f, z * MapMetrics.cellLength);

                    // Setting up Neighbors
                    if (x > 0)
                    {
                        cell.SetNeighbor(cells[i - 1], QubeDirections.W);
                    }
                    if (z > 0)
                    {
                        cell.SetNeighbor(cells[i - xSize], QubeDirections.S);
                    }

                    // Setting up Chunk
                    int chunkX = x / xSize;
                    int chunkZ = z / zSize;
                    int chunkI = chunkZ * chunkCountX + chunkX;

                    int xInChunk = x % xSize;
                    int zInChunk = z % zSize;
                    int iInChunk = zInChunk * xSize + xInChunk;

                    cell.SetChunk(iInChunk, chunks[chunkI]);
                }
            }
        }

        public Cell GetCellFromPosition(Vector3 position)
        {
            float z = position.z / MapMetrics.cellLength;
            float x = position.x / MapMetrics.cellLength;

            int X = Mathf.RoundToInt(x);
            int Z = Mathf.RoundToInt(z);

            return cells[Z*cellCountX + X];
        }
    }
}
