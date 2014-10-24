using UnityEngine;
using System.Collections;

public class LifeCount : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    int topOffsetPixels;
    [SerializeField]
    int leftOffsetPixels;
    [SerializeField]
    int lives = 2;
    TextMesh textMesh;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        transform.position = cam.ScreenToWorldPoint(new Vector3(leftOffsetPixels, Screen.height - topOffsetPixels, 10));
        textMesh = gameObject.GetComponent<TextMesh>();
    }

    void Update()
    {
        textMesh.text = "Lives: " + lives;
    }

    public void DecrementLives()
    {
        lives -= 1;
    }

    public bool IsGameOver()
    {
        return lives < 0;
    }
}
