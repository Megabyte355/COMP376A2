using UnityEngine;
using System.Collections;

public class WaterBalloon : MonoBehaviour
{
    [SerializeField]
    AudioSource waterBalloonSplashSound;
    Player player;
    Score score;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dart")
        {
            // Destroy the dart and water balloon
            //TODO: waterBalloonSplashSound.Play();
            score.BalloonPopReward();
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Player")
        {
            //TODO: waterBalloonSplashSound.Play();
            player.Kill();
            Destroy(gameObject);
        }
    }
}
