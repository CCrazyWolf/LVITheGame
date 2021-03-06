using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LVITHeGame.Pipes
{
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

        public int activePipe = -1;
        public PipesObject ActivePipe
        {
            get { return pipes[activePipe]; }
        }

        public PipesObject obj = null;

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
            SetActivePipe(0);
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
                    if (obj.canRotate)
                        obj.RotationAngle = this.RotationAngle;
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

            if (obj != null)
                Destroy(obj.gameObject);
            obj = Instantiate(ActivePipe);
            obj.transform.position = new Vector3(0, 100f, 0);

            image.sprite = (GetComponentsInChildren<Button>())[activePipe].image.sprite;
        }
        private void CreatePipe()
        {
            obj = Instantiate(ActivePipe);
            obj.transform.position = new Vector3(0, 100f, 0);
        }

        public void putPipe(PipesCell cell)
        {
            obj.transform.position = cell.transform.position;
        }

        // Creating PipesObject in cell
        public void CreatePipe(PipesCell cell)
        {
            obj.transform.SetParent(cell.transform, false);
            obj.transform.localPosition = Vector3.zero;
            cell.objectInCell = obj;

            obj = Instantiate(ActivePipe);
            obj.transform.position = new Vector3(0, 100f, 0);
        }
    }
}
