using UnityEngine;
using System.Collections;

public class LinearMovement : MonoBehaviour
{
    public float speed;
    public Vector3 direction;

    void Update()
    {
        gameObject.transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    public void IncreaseSpeed(float s)
    {
        speed += s;
    }

    public void RotateDirection(float degrees)
    {
        direction = Quaternion.AngleAxis(degrees, gameObject.transform.forward) * direction;
    }


}
