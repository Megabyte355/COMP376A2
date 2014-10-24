using UnityEngine;
using System.Collections;

public class WaterBalloon : MonoBehaviour
{
    [SerializeField]
    AudioSource waterBalloonSplashSound;

    void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.tag == "Dart")
        //{
        //    // Destroy the dart and water balloon
        //    waterBalloonSplashSound.Play();
        //    Destroy(col.gameObject);
        //    Destroy(gameObject);
        //}
        //else if(col.gameObject.tag == "Player")
        //{
        //    waterBalloonSplashSound.Play();
        //    Destroy(gameObject);
        //    // TODO: Kill player
        //}
    }
}
