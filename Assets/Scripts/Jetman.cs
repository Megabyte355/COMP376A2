using UnityEngine;
using System.Collections;

public class Jetman : MonoBehaviour
{
    [SerializeField]
    DartGun dartGun;

    void Update()
    {
        Vector3 eulerRotation = dartGun.GetAngle() < 90f ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
        transform.rotation = Quaternion.Euler(eulerRotation);
    }
}
