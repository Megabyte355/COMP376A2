﻿using UnityEngine;
using System.Collections;

public class DartGun : MonoBehaviour
{
    [SerializeField]
    GameObject dartPrefab;
    [SerializeField]
    float dartSpawnOffset = 1.0f;
    [SerializeField]
    float dartCooldown = 0.5f;
    float cooldownTimer;
    bool cooldownActive = false;
    Camera cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cooldownTimer = dartCooldown;
    }
    void Update()
    {
        // Rotate Dart Gun
        Vector3 gunToMouse = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        transform.rotation = Quaternion.AngleAxis(Vector3.Angle(gunToMouse.normalized, Vector3.right), gunToMouse.y > 0 ? Vector3.forward : -Vector3.forward);

        // Calculate cooldown
        if(cooldownActive)
        {
            cooldownTimer -= Time.deltaTime;
            if(cooldownTimer < 0f)
            {
                cooldownActive = false;
                cooldownTimer = dartCooldown;
            }
        }
    }

    public void FireDart()
    {
        // Only shoot dart if not in cooldown
        if(!cooldownActive)
        {
            cooldownActive = true;
            Vector3 dartSpawn = transform.position + transform.right * dartSpawnOffset;
            Instantiate(dartPrefab, dartSpawn, transform.rotation);
        }
    }
}