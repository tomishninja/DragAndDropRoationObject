using UnityEngine;

public class RoationHandelerManager : MonoBehaviour
{
    [SerializeField] private RotatableObject[] objectsToRotate = null;

    [SerializeField] private ClickandDragRotateObjects Xaxis;

    [SerializeField] private ClickandDragRotateObjects Yaxis;

    [SerializeField] private ClickandDragRotateObjects Zaxis;

    private void Start()
    {
        Xaxis.AddWithNoDuplicates(objectsToRotate);
        Yaxis.AddWithNoDuplicates(objectsToRotate);
        Zaxis.AddWithNoDuplicates(objectsToRotate);
    }
}
