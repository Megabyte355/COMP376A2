using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    int topOffSetPixels;
    [SerializeField]
    int leftOffSetPixels;
    [SerializeField]
    int pointsPerBallonPop;
    [SerializeField]
    int pointsPerBalloonSplit;
    [SerializeField]
    int pointsPerHotAirBalloon;

    int score = 0;
    TextMesh textMesh;
    
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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

    public void BalloonPopReward()
    {
        AddPoints(pointsPerBallonPop);
    }

    public void BalloonSplitReward()
    {
        AddPoints(pointsPerBalloonSplit);
    }

    public void HotAirBalloonReward()
    {
        AddPoints(pointsPerHotAirBalloon);
    }
}
