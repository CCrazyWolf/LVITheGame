using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MapEditorMode
{
    building, connecting, deleting
}

public class PipesMapEditor : MonoBehaviour
{
    public static PipesMapEditor editorInstance;

    public Image image;
    public PipesGrid grid;

    public PipesObject[] pipes;

    public int activePipe = 0;
    public PipesObject ActivePipe
    {
        get { return pipes[activePipe]; }
    }

    private float rotationAngle;
    public float RotationAngle
    {
        get
        {
            return rotationAngle;
        }
        set
        {
            value %= 360;
            if (value < 0f)
            {
                value = 360 + value;
            }
            rotationAngle = (value / 90) * 90f;
        }
    }

    private MapEditorMode currentMode = MapEditorMode.building;


    private void Awake()
    {
        editorInstance = this;
    }

    private void Update()
    {
        switch (currentMode)
        {
            case MapEditorMode.building:                            // To build grid with different PipesObjects
                float wheel = Input.GetAxis("Mouse ScrollWheel");
                if (wheel > 0f)
                {
                    RotationAngle += 90f;
                }
                else if (wheel < 0f)
                {
                    RotationAngle -= 90f;
                }
                break;
            case MapEditorMode.connecting:                          // Connecting sources
                Debug.LogWarning("CONNECTION CAN NOT BE BUILT NOW!");
                break;
            case MapEditorMode.deleting:                            // Deleting PipesObjects
                break;
            default:
                break;
        }
    }


    // Setting up editor's mode 
    public void SetEditorMode(int value)
    {
        currentMode = (MapEditorMode)value;
        switch (currentMode)
        {
            case MapEditorMode.building:
                foreach (PipesCell cell in grid.cells)
                {
                    if (cell.objectInCell != null)
                        cell.objectInCell.SetPreLook(false);
                }
                break;
            case MapEditorMode.connecting:
                foreach (PipesCell cell in grid.cells)
                {
                    if (cell.objectInCell != null && cell.objectInCell.GetComponent<PipeSource>() == null)
                        cell.objectInCell.SetPreLook();
                }
                break;
            case MapEditorMode.deleting:
                break;
            default:
                break;
        }
    }

    // Set PipesObject to be created in chosen cell
    public void SetActivePipe(int value)
    {
        if (activePipe == value)
            return;
        activePipe = value;
        RotationAngle = 0f;
        image.sprite = (GetComponentsInChildren<Button>())[activePipe].image.sprite;
    }


    // Creating PipesObject in cell
    public void CreatePipe(PipesCell cell)
    {
        PipesObject obj = Instantiate(editorInstance.ActivePipe);
        obj.cell = cell;
        obj.transform.SetParent(cell.transform, false);
        obj.transform.localPosition = Vector3.zero;
        if (obj.canRotate)
        {
            obj.transform.rotation = Quaternion.Euler(0f, RotationAngle, 0f);
            obj.RotationAngle = this.RotationAngle;
        }
        cell.objectInCell = obj;
    }
}
