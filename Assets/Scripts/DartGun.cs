using UnityEngine;
using System.Collections;

public class DartGun : MonoBehaviour
{
    [SerializeField]
    AudioSource blowSound;
    [SerializeField]
    GameObject dartPrefab;
    [SerializeField]
    float dartSpawnOffset = 1.0f;
    [SerializeField]
    float dartCooldown = 0.5f;
    [SerializeField]
    float specialCooldown = 3f;
    [SerializeField]
    bool specialEnabled = false;

    float cooldownTimer;
    float specialCooldownTimer;
    float angle;
    bool cooldownActive = false;
    bool specialCooldownActive = false;
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
        angle = Vector3.Angle(gunToMouse.normalized, Vector3.right);
        transform.rotation = Quaternion.AngleAxis(angle, gunToMouse.y > 0 ? Vector3.forward : -Vector3.forward);

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
        
        // Calculate special cooldown
        if(specialCooldownActive && specialEnabled)
        {
            specialCooldownTimer -= Time.deltaTime;
            if(specialCooldownTimer < 0f)
            {
                specialCooldownActive = false;
                specialCooldownTimer = specialCooldown;
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
            blowSound.Play();
        }
    }

    public void FireSpecial()
    {
        if (!specialCooldownActive && specialEnabled)
        {
            specialCooldownActive = true;
            Vector3 dartSpawn = transform.position + transform.right * dartSpawnOffset;
            Vector3 specialSpawn = transform.position + transform.right;
            Instantiate(dartPrefab, dartSpawn, transform.rotation);
            GameObject special = Instantiate(dartPrefab, specialSpawn, transform.rotation) as GameObject;
            special.GetComponent<LinearMovement>().RotateDirection(180);
            blowSound.Play();
        }
    }

    public float GetAngle()
    {
        return angle;
    }
}
