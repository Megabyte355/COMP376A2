using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    float dyingDuration;
    [SerializeField]
    float respawnDuration;
    [SerializeField]
    Animator animator;
    LifeCount lifeCount;
    float timer = 0f;

    WrapAround wrapper;
    FallingDeath fall;
    PlayerControls controls;
    Camera cam;
    enum PlayerState { FLYING, DYING, RESPAWNING }
    PlayerState state;
    
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        lifeCount = GameObject.FindGameObjectWithTag("LifeCount").GetComponent<LifeCount>();
        wrapper = GetComponent<WrapAround>();
        fall = GetComponent<FallingDeath>();
        controls = GetComponent<PlayerControls>();
        state = PlayerState.FLYING;
    }

    public void Kill()
    {
        lifeCount.DecrementLives();
        if(lifeCount.IsGameOver())
        {
            Application.LoadLevel("GameOver");
        }
        ChangeState(PlayerState.DYING);
    }

    void Update()
    {
        if(timer >= 0f)
        {
            // Handle respawn timing
            timer -= Time.deltaTime;
            if (timer < 0f && state == PlayerState.RESPAWNING)
            {
                ChangeState(PlayerState.FLYING);
            }
            if(timer < 0f && state == PlayerState.DYING)
            {
                ChangeState(PlayerState.RESPAWNING);
            }
        }
    }

    void ChangeState(PlayerState newState)
    {
        state = newState;
        switch (newState)
        {
            case PlayerState.FLYING:
                animator.SetBool("Respawning", false);
                animator.SetBool("Flying", true);
                
                foreach (BoxCollider2D c in GetComponents<BoxCollider2D>())
                {
                    c.enabled = true;
                }
                break;
            case PlayerState.DYING:
                animator.SetBool("Flying", false);
                animator.SetBool("Dying", true);
                wrapper.enabled = false;
                fall.enabled = true;
                timer = dyingDuration;
                controls.enabled = false;
                foreach(BoxCollider2D c in GetComponents<BoxCollider2D>())
                {
                    c.enabled = false;
                }
                break;
            case PlayerState.RESPAWNING:
                animator.SetBool("Dying", false);
                animator.SetBool("Respawning", true);
                wrapper.enabled = true;
                fall.enabled = false;
                controls.enabled = true;
                timer = respawnDuration;
                transform.position = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10));
                break;
        }
    }


}
