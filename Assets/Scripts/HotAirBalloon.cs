using UnityEngine;
using System.Collections;

public class HotAirBalloon : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject waterBalloonPrefab;
    [SerializeField]
    float balloonCooldown = 2.0f;
    [SerializeField]
    float offScreenLimit = 50f;

    float cooldownTimer;
    bool cooldownActive = false;
    Camera cam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cooldownTimer = balloonCooldown;
    }

    void Update()
    {
        // Destroy hot air balloon if it exits the screen
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        if (screenPos.x < -offScreenLimit || screenPos.x > Screen.width + offScreenLimit)
        {
            Destroy(gameObject);
        }

        // Manage water balloon cooldown
        if (cooldownActive)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0f)
            {
                cooldownActive = false;
                cooldownTimer = balloonCooldown;
            }
        }
        else
        {
            // Aim and throw water balloon towards player
            Vector3 directionToPlayer = player.transform.position - transform.position;
            GameObject wb = Instantiate(waterBalloonPrefab, transform.position, Quaternion.identity) as GameObject;
            float angle = Vector3.Angle(directionToPlayer.normalized, Vector3.right);
            wb.transform.rotation = Quaternion.AngleAxis(angle, directionToPlayer.y > 0 ? Vector3.forward : -Vector3.forward);
            cooldownActive = true;
        }
        
    }
}
