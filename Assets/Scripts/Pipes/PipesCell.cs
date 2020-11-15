using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesCell : MonoBehaviour
{
    bool wasTouched = false;
    public bool canModify = true;

    public Material mouseOver;
    public Material defaultM;

    Renderer rend;

    public PipesObject objectInCell = null;

    private PipesCell[] neighbors;

    private void Awake()
    {
        neighbors = new PipesCell[4];
        rend = GetComponentInChildren<Renderer>();
    }

    private void OnMouseOver()
    {
        if (!canModify)
            return;


        if (!wasTouched)
        {
            if (objectInCell == null)
            {
                rend.material = mouseOver;
                PipesMapEditor.editorInstance.CreatePipe(this);
                objectInCell.SetPreLook();
            }
            else
            {
                objectInCell.RotationAngle = PipesMapEditor.editorInstance.RotationAngle;
            }
        }
        else
        {
            float wheel = Input.GetAxis("Mouse ScrollWheel");
            if (wheel > 0f)
            {
                objectInCell.RotationAngle += 90;
                PipesMapEditor.editorInstance.RotationAngle -= 90f;
            }
            else if (wheel < 0f)
            {
                objectInCell.RotationAngle -= 90;
                PipesMapEditor.editorInstance.RotationAngle += 90f;
            }
            if (Input.GetMouseButtonDown(1))
            {
                DestroyObject();
            }
        }
    }

    private void OnMouseExit()
    {
        if (!canModify)
            return;


        if (!wasTouched)
        {
            rend.material = defaultM;
            DestroyObject();
        }
    }
    private void OnMouseDown()
    {
        if (!canModify)
            return;


        if (!wasTouched)
        {
            wasTouched = true;
            if (objectInCell==null)
            {
                rend.material = mouseOver;
                PipesMapEditor.editorInstance.CreatePipe(this);
            }
            objectInCell.SetPreLook(false);
        }
        else
        {
            
        }
    }

    public void DestroyObject()
    {
        wasTouched = false;
        Destroy(objectInCell.gameObject);
    }

    public void SetNeighbor(PipesCell other, PipesDirection direction)
    {
        neighbors[(int)direction] = other;
        other.neighbors[(int)direction.Opposite()] = this;
    }

    public PipesCell GetNeighbor(PipesDirection direction)
    {
        return neighbors[(int)direction];
    }
}
