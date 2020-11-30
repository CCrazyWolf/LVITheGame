using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LVITHeGame.MapEditor
{
    public class MapEditor : MonoBehaviour
    {
        public static MapEditor editorInstance;

        public MapGrid grid;

        public MapObject currentObject = null;
        private MapObject currentObjectInstance;

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

        private void Awake()
        {
            editorInstance = this;
            currentObjectInstance = Instantiate(currentObject);
            currentObjectInstance.GetComponent<Renderer>().enabled = false;
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                currentObjectInstance.GetComponent<Renderer>().enabled = true;
                Cell cell = grid.GetCellFromPosition(hit.point);

                if (cell.objectInCell != null)
                {
                    currentObjectInstance.GetComponent<Renderer>().enabled = false;
                }
                else
                    currentObjectInstance.transform.position = cell.transform.position;

                if (Input.GetMouseButtonDown(0) && cell.objectInCell == null)
                {
                    currentObjectInstance.transform.SetParent(cell.transform);
                    cell.objectInCell = currentObjectInstance;
                    currentObjectInstance = Instantiate(currentObject);
                    currentObjectInstance.GetComponent<Renderer>().enabled = false;
                }
                else if (Input.GetMouseButtonDown(1) && cell.objectInCell != null)
                {
                    Destroy(cell.objectInCell.gameObject);
                }
            }
            else
            {
                currentObjectInstance.GetComponent<Renderer>().enabled = false;
            }

            float wheel = Input.GetAxis("Mouse ScrollWheel");
            if (wheel > 0f)
            {
                RotationAngle += 90f;
            }
            else if (wheel < 0f)
            {
                RotationAngle -= 90f;
            }
            if (currentObject.canRotate)
            {
                currentObjectInstance.RotationAngle = this.RotationAngle;
            }
        }
    }
}
