using UnityEngine;
using System.Collections;

public class FallingDeath : MonoBehaviour
{
    [SerializeField]
    float fallingSpeed;

    void Update()
    {
        transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime, Space.World);
    }
}
