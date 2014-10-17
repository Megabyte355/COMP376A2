using UnityEngine;
using System.Collections;

public class BalloonAnchorMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    Vector3 direction;

    void Start()
    {
        // Test data
        //direction = new Vector3(-1, 0.33f, 0).normalized;
    }

    void Update()
    {
        gameObject.transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
    }
}
