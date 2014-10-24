using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    [SerializeField]
    int topOffSetPixels;
    [SerializeField]
    int leftOffSetPixels;

    int score = 0;
    TextMesh textMesh;
    
    void Start()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(leftOffSetPixels, Screen.height - topOffSetPixels, 10));
        textMesh = gameObject.GetComponent<TextMesh>();
    }

    void Update()
    {
        textMesh.text = "Score: " + score;
    }

    public void AddPoints(int pts)
    {
        score += pts;
    }
}
