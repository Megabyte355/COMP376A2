using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudSpawner : MonoBehaviour
{
    public enum WindDirection { LEFT, RIGHT };
    public List<GameObject> cloudPrefabs;
    [SerializeField]
    int numberOfClouds;
    [SerializeField]
    float minSpeed;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    WindDirection windDirection;
    [SerializeField]
    Camera cam;

    void Start()
    {
        for(int i = 0; i < numberOfClouds; i++)
        {
            // Select a random cloud prefab
            int randomCloudIndex = Random.Range(0, cloudPrefabs.Count - 1);

            // Select random coordinate on screen
            float randomX = Random.Range(0, Screen.width);
            float randomY = Random.Range(0, Screen.height);
            Vector3 worldPosition = cam.ScreenToWorldPoint(new Vector3(randomX, randomY, 10));

            // Instantiate cloud
            GameObject cloud = Instantiate(cloudPrefabs[randomCloudIndex], worldPosition, Quaternion.identity) as GameObject;
            LinearMovement cloudMovement = cloud.GetComponent<LinearMovement>();
            
            // Set direction
            if (windDirection == WindDirection.LEFT)
            {
                cloudMovement.RotateDirection(180f);
            }

            // Set random speed
            cloudMovement.SetSpeed(Random.Range(minSpeed, maxSpeed));
        }
    }
}
