using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    public float speed = 2.5f;
    [SerializeField]
    DartGun dartGun;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, 0) + new Vector3(0, vertical, 0);
        gameObject.transform.Translate(direction * speed * Time.deltaTime);

        if (Input.GetButtonDown("Fire1"))
        {
            dartGun.FireDart();
        }
    }
}
