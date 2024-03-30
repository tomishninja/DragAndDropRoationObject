using UnityEngine;

[System.Serializable]
public class Angle
{
    public const int MAX_ANGLE = 360;

    [SerializeField] float _angle = 0;

    public float Degrees {
        get
        {
            return _angle;
        }

        set
        {
            float newValue = _angle + value;
            if (newValue < 0)
            {
                _angle = MAX_ANGLE + newValue;
            }
            else if (newValue > MAX_ANGLE)
            {
                _angle = MAX_ANGLE - newValue;
            }
            else
            {
                _angle = newValue;
            }
        }
    }
}
