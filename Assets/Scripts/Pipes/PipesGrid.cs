using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesGrid : MonoBehaviour
{
    int xSize = 6;
    int zSize = 6;


    public PipesCell cellPrefab;
    public PipesCell[] cells;

    private void Awake()
    {
        cells = new PipesCell[xSize * zSize];

        for (int z =0,i=0;z<zSize;z++)
        {
            for(int x = 0; x<xSize; x++, i++)
            {
                PipesCell cell = Instantiate(cellPrefab);
                cell.transform.position = new Vector3(x * 2f, 0f, z * 2f);
                cell.transform.SetParent(transform, false);
                cells[i] = cell;

                if (x > 0)
                {
                    cell.SetNeighbor(cells[i - 1], PipesDirection.W);
                }
                if (z > 0)
                {
                    cell.SetNeighbor(cells[i - xSize], PipesDirection.S);
                }
                if (x == 0 || x == xSize-1 || z == 0 || z == zSize-1)
                {
                    cell.canModify = false;
                }
            }
        }
    }
}
