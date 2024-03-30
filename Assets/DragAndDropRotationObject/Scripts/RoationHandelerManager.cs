using System.Reflection;
using UnityEngine;

namespace ClickAndDragRotation
{
    public class RoationHandelerManager : MonoBehaviour
    {
        private const int AmountOfFramesToCheckForChange = 30;

        [SerializeField] private RotatableObject[] objectsToRotate = null;

        [Tooltip("Has Issues With Gimble Lock")]
        [SerializeField] private bool AllowOutsideObjectsToRoateObject = false;

        [SerializeField] private ClickandDragRotateObjects Xaxis;

        [SerializeField] private ClickandDragRotateObjects Yaxis;

        [SerializeField] private ClickandDragRotateObjects Zaxis;

        private Quaternion[] lastRotationOfObjects = null;

        private void Start()
        {
            Xaxis.AddWithNoDuplicates(objectsToRotate);
            Yaxis.AddWithNoDuplicates(objectsToRotate);
            Zaxis.AddWithNoDuplicates(objectsToRotate);

            if (AllowOutsideObjectsToRoateObject)
            {
                InitLastRotationOfObjects();

                Xaxis.listener = new RotationHasBeenSetListener(this);
                Yaxis.listener = new RotationHasBeenSetListener(this);
                Zaxis.listener = new RotationHasBeenSetListener(this);
            }
        }

        private void LateUpdate()
        {
            if (AllowOutsideObjectsToRoateObject && 
                !(Xaxis.Dragging || Yaxis.Dragging || Zaxis.Dragging)
                && Time.frameCount % AmountOfFramesToCheckForChange == 0)
            {
                CheckIfObjectsHaveBeenMoved();
            }
        }

        private void CheckIfObjectsHaveBeenMoved()
        {
            for (int index = 0; index < lastRotationOfObjects.Length; index++)
            {
                if (!lastRotationOfObjects[index].Equals(objectsToRotate[index].GetValidRotation()))
                {
                    Debug.Log(lastRotationOfObjects[index].eulerAngles);
                    Vector3 axis = lastRotationOfObjects[index].eulerAngles - objectsToRotate[index].GetValidRotationAsEuler();
                    ForceUpdateAllObjetsToNewAngleBasedOnObjectAtIndex(axis, objectsToRotate[index].RotatableObjectTransform);
                    return;
                }
            }
        }

        internal void ForceUpdateAllObjetsToNewAngleBasedOnObjectAtIndex(Vector3 AmountToRotate, Transform exceptForThis)
        {
            // force all the related objects to repair them selves
            Xaxis.CalculateTheRotationOfAllTheObjects(AmountToRotate.x, exceptForThis);
            Yaxis.CalculateTheRotationOfAllTheObjects(AmountToRotate.y, exceptForThis);
            Zaxis.CalculateTheRotationOfAllTheObjects(AmountToRotate.z, exceptForThis);

            UpdateAllObjectsLastTransforms();
        }

        private void InitLastRotationOfObjects()
        {
            lastRotationOfObjects = new Quaternion[objectsToRotate.Length];
            UpdateAllObjectsLastTransforms();
        }

        internal void UpdateAllObjectsLastTransforms()
        {
            for (int index = 0; index < lastRotationOfObjects.Length; index++)
            {
                lastRotationOfObjects[index] = objectsToRotate[index].GetValidRotation();
            }
        }
    }
}
