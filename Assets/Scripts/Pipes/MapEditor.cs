using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    public static MapEditor editorInstance;

    public Image image;

    public PipesObject[] pipes;
    public PipeSource source;

    public int activePipe = 0;

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

    private void Update()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel > 0f)
        {
            RotationAngle += 90f;
        }
        else if (wheel < 0f)
        {
            RotationAngle -= 90f;
        }
    }

    public PipesObject ActivePipe
    {
        get { return pipes[activePipe]; }
    }

    private void Awake()
    {
        editorInstance = this;
    }

    public void SetActivePipe(int value)
    {
        if (activePipe == value)
            return;
        activePipe = value;
        RotationAngle = 0f;
        image.sprite = (GetComponentsInChildren<Button>())[activePipe].image.sprite;
    }

    public void CreatePipe(PipesCell cell)
    {
        cell.objectInCell = Instantiate(editorInstance.ActivePipe);
        cell.objectInCell.transform.SetParent(cell.transform, false);
        cell.objectInCell.transform.localPosition = Vector3.zero;
        if (cell.objectInCell.canRotate) 
            cell.objectInCell.transform.rotation = Quaternion.Euler(0f, RotationAngle, 0f);
    }

    public void CreateObject(PipesObject obj)
    {

    }
}
