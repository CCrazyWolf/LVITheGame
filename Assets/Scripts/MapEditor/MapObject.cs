using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LVITHeGame.MapEditor
{
    public class MapObject : MonoBehaviour
    {
        public Cell cell;

        public bool canRotate;          // Object can be rotated
        public bool isRotating = false; // Object is rotating in Oy axis


        private float rotationAngle = 0f;
        public float RotationAngle
        {
            get { return rotationAngle; }
            set
            {
                if (!canRotate || rotationAngle == value)
                    return;
                rotationAngle = value;
                isRotating = true;
            }
        }

        public QubeDirections[] incomingDirections;
        public QubeDirections[] outgoingDirections;


        private void Awake()
        {
            if (incomingDirections == null || outgoingDirections == null)
                Debug.LogError("Object is without in/out direction.");
        }

        protected virtual void Update()
        {
            if (isRotating)
            {
                if (Mathf.Abs(rotationAngle - transform.eulerAngles.y) > 0.001f)
                {
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0f, rotationAngle, 0f), 0.5f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                    isRotating = false;
                }
            }
        }
    }
}
