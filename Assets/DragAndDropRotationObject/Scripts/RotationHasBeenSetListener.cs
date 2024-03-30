
using TMPro;

namespace ClickAndDragRotation
{
    public class RotationHasBeenSetListener
    {
        RoationHandelerManager managerToUpdate;

        public RotationHasBeenSetListener(RoationHandelerManager managerToUpdate)
        {
            this.managerToUpdate = managerToUpdate;
        }

        public void UpdateManager()
        {
            managerToUpdate.UpdateAllObjectsLastTransforms();
        }
    }
}
