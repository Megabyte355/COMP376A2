using UnityEngine;
using System.Collections;

public class WaterBalloon : MonoBehaviour
{
    AudioSource waterBalloonSplashSound;
    Player player;
    Score score;
    Progress progress;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<Progress>();
        waterBalloonSplashSound = GameObject.Find("WaterBalloonSplashSound").GetComponent<AudioSource>();

        if (progress.IsSpeedUpActive())
        {
            gameObject.GetComponent<LinearMovement>().IncreaseSpeed(progress.GetSpeedUpAmount());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dart")
        {
            // Destroy the dart and water balloon
            waterBalloonSplashSound.Play();
            score.BalloonPopReward();
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Player")
        {
            waterBalloonSplashSound.Play();
            player.Kill();
            Destroy(gameObject);
        }
    }
}
