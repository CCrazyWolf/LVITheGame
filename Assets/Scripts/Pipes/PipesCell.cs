using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesCell : MonoBehaviour
{
    bool isTouched = false;
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


        if (!isTouched)
        {
            if (objectInCell == null)
            {
                rend.material = mouseOver;
                MapEditor.editorInstance.CreatePipe(this);
                objectInCell.SetPreLook();
            }
            else
            {
                objectInCell.RotationAngle = MapEditor.editorInstance.RotationAngle;
            }
        }
        else
        {
            float wheel = Input.GetAxis("Mouse ScrollWheel");
            if (wheel > 0f)
            {
                objectInCell.RotationAngle += 90;
                MapEditor.editorInstance.RotationAngle -= 90f;
            }
            else if (wheel < 0f)
            {
                objectInCell.RotationAngle -= 90;
                MapEditor.editorInstance.RotationAngle += 90f;
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


        if (!isTouched)
        {
            rend.material = defaultM;
            DestroyObject();
        }
    }
    private void OnMouseDown()
    {
        if (!canModify)
            return;


        if (!isTouched)
        {
            isTouched = true;
            if (objectInCell==null)
            {
                rend.material = mouseOver;
                MapEditor.editorInstance.CreatePipe(this);
            }
            objectInCell.SetPreLook(false);
        }
    }

    public void DestroyObject()
    {
        isTouched = false;
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
