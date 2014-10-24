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
    TextMesh progressTextMesh;
    //TextMesh victoryTextMesh;

    int currentBalloonCount;
    int totalBalloonCount;

    void Start()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(leftOffSetPixels, Screen.height - topOffSetPixels, 10));
        progressTextMesh = gameObject.GetComponent<TextMesh>();

        totalBalloonCount = GameObject.FindGameObjectsWithTag("Balloon").Length;
        currentBalloonCount = totalBalloonCount;
    }

    void Update()
    {
        progress = (int)(((float)currentBalloonCount / (float)totalBalloonCount) * 100f);
        progressTextMesh.text = "Progress: " + (100 - progress) + "%";


        // 80 -> speed up
        // 30, 60, 90 -> hot air balloon
    }

    public void DecrementBalloonCount()
    {
        currentBalloonCount -= 1;
        if(currentBalloonCount == 0)
        {
            // YOU WIN!!!
        }
    }
}
