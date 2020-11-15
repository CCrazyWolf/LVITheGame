using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesObject : MonoBehaviour
{
    public PipesCell cell;

    public bool canRotate;          // Object can be rotated
    public bool isRotating =false;  // Object is rotating in Oy axis
    public bool preLook = true;     // Object is not set into cell yet, has transparent material

    public Material pipeM;
    public Material pipeMT;


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

    // here should be directions for object
    public PipesDirection[] incomingDirections;
    public PipesDirection[] outgoingDirections;


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


    public void SetPreLook(bool value = true)
    {
        preLook = value;
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            if (value)
                r.material = pipeMT;
            else
                r.material = pipeM;
        }
    }
}
