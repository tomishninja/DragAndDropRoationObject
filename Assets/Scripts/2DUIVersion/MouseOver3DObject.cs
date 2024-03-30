using UnityEngine;
using UnityEngine.UI;

public class MouseOver3DObject : MonoBehaviour
{
    [Header("Logical Variables for UI Interaction")]
    public Camera renderTextureCamera; // Assign the camera rendering to the texture
    public RenderTexture renderTexture; // Assign the render texture
    public LayerMask objectLayer;
    public RawImage rawImage;

    [Header("Object To Be Rotated")]
    InteractableObjects[] interactableObjects = new InteractableObjects[0];

    void Update()
    {
        GameObject gameObjectThatIsBeingHoveredOver = ObjectIsHoveringOver3DObjectThoughRenderTexture();

        if (gameObjectThatIsBeingHoveredOver != null)
        {
                Debug.Log(interactableObjects.Length);
            for (int i = 0; i < interactableObjects.Length; i++)
            {
                if (gameObjectThatIsBeingHoveredOver.Equals(interactableObjects[i].objectToCollideOnTo.gameObject))
                {
                    Debug.Log("Object is over the this");
                }
            }
        }
        
    }

    private GameObject ObjectIsHoveringOver3DObjectThoughRenderTexture()
    {
        if (renderTexture != null && rawImage != null)
        {
            // Get the local position of the mouse within the RawImage
            Vector2 localMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rawImage.rectTransform, Input.mousePosition, null, out localMousePosition);

            // Normalize the local position to get texture coordinates
            float width = rawImage.rectTransform.rect.width;
            float height = rawImage.rectTransform.rect.height;
            Vector2 textureCoord = new Vector2(
                Mathf.InverseLerp(-width, width, localMousePosition.x),
                Mathf.InverseLerp(-height, height, localMousePosition.y)
            );

            // Translate the texture coordinates to pixel coordinates
            textureCoord.x *= renderTexture.width;
            textureCoord.y *= renderTexture.height;

            // Create a ray from the mouse position on the render texture
            Ray ray = renderTextureCamera.ScreenPointToRay(textureCoord);

            // RaycastHit for storing the hit information
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 0.2f);


            // Perform the raycast against the 3D object in the scene
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, objectLayer))
            {
                // Do something when the mouse is over the 3D object in the scene
                //Debug.Log("Mouse is over the 3D object in the scene!");

                return hit.collider.gameObject;

            }
        }

        return null;
    }

    public class InteractableObjects
    {
        public Transform ObjectToBeRotated;
        public Collider objectToCollideOnTo;
    }
}
