using System;
using UnityEngine;

[System.Serializable]
public class RotatableObject : IComparable<RotatableObject>
{
    [SerializeField] public Transform RotatableObjectTransform;
    [SerializeField] public UnityEngine.Space typeOfRotation = UnityEngine.Space.Self;

    public void RotateObject(Vector3 dir, float rotationAmount)
    {
        RotatableObjectTransform.transform.Rotate(dir, rotationAmount, typeOfRotation);
    }

    // We just want to make sure the transform it's self isn't added twice
    public override bool Equals(object other)
    {
        return RotatableObjectTransform.Equals(RotatableObjectTransform);
    }

    // This mainly exists to ensure that objects that need to compare this for equality are able to for sorting purposes
    public int CompareTo(RotatableObject other)
    {
        if (RotatableObjectTransform.Equals(other.RotatableObjectTransform))
            return 0;
        else if (typeOfRotation == UnityEngine.Space.Self)
            return 1;
        else
            return -1;
    }
}
