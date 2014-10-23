using UnityEngine;
using System.Collections;

public class RotationMovement : MonoBehaviour
{
    public enum RotationDirection { CW, CCW };
    public RotationDirection rotationDirection;
    public float angularSpeed;

    void Update()
    {
        int sign = rotationDirection == RotationDirection.CW ? 1 : -1;
        transform.Rotate(transform.forward, angularSpeed * Time.deltaTime * sign);
    }
}
