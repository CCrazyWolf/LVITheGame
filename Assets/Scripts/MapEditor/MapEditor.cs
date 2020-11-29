using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LVITHeGame.MapEditor
{
    public enum MapEditorMode
    {
        building, connecting, deleting
    }

    public class MapEditor : MonoBehaviour
    {
        public static MapEditor editorInstance;

        public Image image;
        public MapGrid grid;

        public MapObject[] pipes;

        public int activePipe = -1;
        public MapObject ActivePipe
        {
            get { return pipes[activePipe]; }
        }

        public MapObject obj = null;

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
                    foreach (Cell cell in grid.cells)
                    {
                    }
                    break;
                case MapEditorMode.connecting:
                    foreach (Cell cell in grid.cells)
                    {
                    }
                    break;
                case MapEditorMode.deleting:
                    break;
                default:
                    break;
            }
        }

        // Set MapObject to be created in chosen cell
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

        public void putPipe(Cell cell)
        {
            obj.transform.position = cell.transform.position;
        }

        // Creating MapObject in cell
        public void CreatePipe(Cell cell)
        {
            obj.transform.SetParent(cell.transform, false);
            obj.transform.localPosition = Vector3.zero;
            cell.objectInCell = obj;

            obj = Instantiate(ActivePipe);
            obj.transform.position = new Vector3(0, 100f, 0);
        }
    }
}
