using UnityEngine;
using System.Collections;

public class Progress : MonoBehaviour
{
    [SerializeField]
    int topOffSetPixels;
    [SerializeField]
    int leftOffSetPixels;
    [SerializeField]
    int hotAirBalloonSpawnMargins;
    [SerializeField]
    GameObject hotAirBalloonPrefab;

    int progress = 0;
    Camera cam;
    TextMesh progressTextMesh;

    //TextMesh victoryTextMesh;

    int currentBalloonCount;
    int totalBalloonCount;

    // Checkpoints
    [SerializeField]
    int balloonSpeedUp = 80;
    [SerializeField]
    int hotAirBalloon1 = 30;
    [SerializeField]
    int hotAirBalloon2 = 60;
    [SerializeField]
    int hotAirBalloon3 = 90;

    // Checkpoint booleans (to make them one-time only)
    bool activatedSpeedUp = false;
    bool activatedHotAir1 = false;
    bool activatedHotAir2 = false;
    bool activatedHotAir3 = false;

    [SerializeField]
    float speedUpAmount;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        transform.position = cam.ScreenToWorldPoint(new Vector3(leftOffSetPixels, Screen.height - topOffSetPixels, 10));
        progressTextMesh = gameObject.GetComponent<TextMesh>();

        totalBalloonCount = GameObject.FindGameObjectsWithTag("Balloon").Length;
        currentBalloonCount = totalBalloonCount;
    }

    void Update()
    {
        progress = 100 - (int)(((float)currentBalloonCount / (float)totalBalloonCount) * 100f);
        progressTextMesh.text = "Progress: " + progress + "%";
        VerifyCheckpoints();
    }

    public void DecrementBalloonCount()
    {
        currentBalloonCount -= 1;
        if (currentBalloonCount == 0)
        {
            // TODO: YOU WIN!!!
        }
    }

    void VerifyCheckpoints()
    {
        if (!activatedSpeedUp && progress >= balloonSpeedUp)
        {
            activatedSpeedUp = true;
            // Speed up ballons
            GameObject[] anchors = GameObject.FindGameObjectsWithTag("BalloonAnchor");
            foreach (GameObject anchor in anchors)
            {
                anchor.GetComponent<LinearMovement>().IncreaseSpeed(speedUpAmount);
            }
        }
        if (!activatedHotAir1 && progress >= hotAirBalloon1)
        {
            activatedHotAir1 = true;
            SpawnHotAirBalloon(CalculateSpawnPoint());
        }
        if (!activatedHotAir2 && progress >= hotAirBalloon2)
        {
            activatedHotAir2 = true;
            SpawnHotAirBalloon(CalculateSpawnPoint());
        }
        if (!activatedHotAir3 && progress >= hotAirBalloon3)
        {
            activatedHotAir3 = true;
            SpawnHotAirBalloon(CalculateSpawnPoint());
        }
    }

    Vector3 CalculateSpawnPoint()
    {
        // Hot air balloon spawns on the left or the right
        int spawnY = Random.Range(hotAirBalloonSpawnMargins, Screen.height - hotAirBalloonSpawnMargins);

        // 50-50 chance of spawning both sides
        bool fromLeft = Random.value < 0.5f;
        int spawnX = fromLeft ? 0 : Screen.width;

        return cam.ScreenToWorldPoint(new Vector3(spawnX, spawnY, 10));
    }

    void SpawnHotAirBalloon(Vector3 spawnPoint)
    {
        GameObject hotAirBalloon = Instantiate(hotAirBalloonPrefab, spawnPoint, Quaternion.identity) as GameObject;
        if (cam.WorldToScreenPoint(spawnPoint).x >= Screen.width)
        {
            hotAirBalloon.GetComponent<LinearMovement>().direction = new Vector3(-1, 0, 0);
        }
    }
}
