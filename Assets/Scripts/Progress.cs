using UnityEngine;
using System.Collections;

public class Progress : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    [SerializeField]
    int topOffSetPixels;
    [SerializeField]
    int leftOffSetPixels;

    int progress = 0;
    TextMesh textMesh;

    void Start()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(leftOffSetPixels, Screen.height - topOffSetPixels, 10));
        textMesh = gameObject.GetComponent<TextMesh>();
    }

    void Update()
    {
        textMesh.text = "Progress: " + progress + "%";
    }

}
