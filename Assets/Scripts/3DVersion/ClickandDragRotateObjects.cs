using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickandDragRotateObjects : MonoBehaviour
{
    // Enumeration for different rotation axes
    public enum RotationAxis { X, Y, Z }

    // Speed of rotation
    public float rotationSpeed = 5f;

    // Stores the initial mouse position
    private Vector3 initialMousePos;

    // Stores whether dragging has started
    private bool dragging = false;

    // The collider associated with the objects
    public Collider rotationCollider;

    public RotatableObject[] objectsToRotate;

    // Rotation axis
    public RotationAxis rotationAxis = RotationAxis.Y;

    // Cacluation of the current angle in Euler degrees
    public Angle angle;

    private int dragDirection = -1;

    // Update is called once per frame
    void Update()
    {
        DetermineMouseInput();

        // Rotate objects if dragging
        if (dragging)
        {
            // Calculate the rotation amount based on drag distance and the selected axis
            float rotationAmount = CalculateRotation();

            // Rotate each object in the array
            CalculateTheRotationOfAllTheObjects(rotationAmount);

            // update the angle
            angle.Degrees = rotationAmount;

            // Update the initial mouse position for the next frame
            initialMousePos = Input.mousePosition;
        }
    }

    private void CalculateTheRotationOfAllTheObjects(float rotationAmount)
    {
        // Rotate each object in the array
        for (int index = 0; index < objectsToRotate.Length; index++)
        {
            objectsToRotate[index].RotateObject(getDir(), rotationAmount);
        }
    }

    private void DetermineMouseInput()
    {
        // Check if mouse button is pressed down
        if (Input.GetMouseButtonDown(0) && IsMouseOverCollider())
        {
            // Store the initial mouse position
            initialMousePos = Input.mousePosition;
            dragging = true;

            if (angle.Degrees > 90 && angle.Degrees < 270)
            {
                dragDirection = 1;
            }
            else
            {
                dragDirection = -1;
            }
        }
        // Check if mouse button is released
        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }

    private float CalculateRotation()
    {
        return GetMouseDragDistance() * rotationSpeed * Time.deltaTime;
    }

    private float GetMouseDragDistance()
    {
        // Calculate the distance the mouse has been dragged
        Vector3 dragDistance = Input.mousePosition - initialMousePos;



        switch (rotationAxis)
        {
            case RotationAxis.Y:
                return dragDirection * dragDistance.x;
            default:
                return dragDirection * dragDistance.y;
        }
    }

    private Vector3 getDir()
    {
        switch (rotationAxis)
        {
            case RotationAxis.X:
                return Vector3.right;
            case RotationAxis.Y:
                return Vector3.up;
            case RotationAxis.Z:
                return Vector3.back;
            default:
                Debug.Log("Impossible Value Found");
                return Vector3.zero;
        }
    }

    public void AddWithNoDuplicates(RotatableObject[] objects)
    {
        // get all the new objects to be added
        HashSet<RotatableObject> uniqueobjects = new HashSet<RotatableObject>(objects);
        
        // add all the current objects
        for(int index = 0; index < this.objectsToRotate.Length; index++)
        {
            uniqueobjects.Add(this.objectsToRotate[index]);
        }

        // save these in the objects to rotate array
        this.objectsToRotate = uniqueobjects.ToArray<RotatableObject>();
    }

    // Check if mouse is hovering over the collider
    private bool IsMouseOverCollider()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == rotationCollider)
            {
                return true;
            }
        }

        return false;
    }
}
