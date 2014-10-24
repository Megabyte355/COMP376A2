using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BalloonSplitter : MonoBehaviour
{
    [SerializeField]
    float minAngleChange;
    [SerializeField]
    float maxAngleChange;
    [SerializeField]
    GameObject anchorPrefab;
    [SerializeField]
    Score score;
    [SerializeField]
    Progress progress;
    [SerializeField]
    AudioSource balloonPopSound;

    public void SplitBalloons(Vector3 impactDirection, BalloonAnchor anchor, Balloon affectedBalloon)
    {
        List<Balloon> list = anchor.GetBalloons();

        if (list.Count > 1)
        {
            // Remove and destroy the balloon colliding with dart
            anchor.RemoveBalloon(affectedBalloon);
            Destroy(affectedBalloon.gameObject);

            // Increase speed
            anchor.IncreaseSpeed();

            // Initialize new BalloonAnchor with speed of original anchor
            BalloonAnchor newAnchor = (Instantiate(anchorPrefab, anchor.transform.position, Quaternion.identity) as GameObject).GetComponent<BalloonAnchor>();
            newAnchor.SetSpeed(anchor.GetSpeed());

            // Set new directions for both BalloonAnchors
            // NOTE: Writing UnityEngine.Random solves ambiguity between System.Random and UnityEngine.Random
            float rotation = UnityEngine.Random.Range(minAngleChange, maxAngleChange);
            float rotation2 = UnityEngine.Random.Range(minAngleChange, maxAngleChange);
            anchor.SetDirection(Quaternion.AngleAxis(-rotation, gameObject.transform.forward) * impactDirection);
            newAnchor.SetDirection(Quaternion.AngleAxis(rotation2, gameObject.transform.forward) * impactDirection);

            // Split balloons by half
            Color newColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
            int half = list.Count / 2;
            for (int i = 0; i < half; i++)
            {
                list[i].gameObject.GetComponent<SpringJoint2D>().connectedBody = newAnchor.gameObject.GetComponent<Rigidbody2D>();
                list[i].SetAnchor(newAnchor);
                newAnchor.AddBalloon(list[i]);
                anchor.RemoveBalloon(list[i]);
            }

            // Apply different color to balloons on original anchor
            for (int i = 0; i < anchor.GetBalloons().Count; i++)
            {
                list[i].GetComponent<SpriteRenderer>().color = newColor;
            }

            // Award player points (handled by Score.cs)
            score.BalloonSplitReward();
        }
        else
        {
            // Destroy last balloon and anchor
            Destroy(anchor.gameObject);
            Destroy(list[0].gameObject);
            list.Clear();

            // Award player points (handled by Score.cs)
            score.BalloonPopReward();    
        }

        // Update balloon progress
        progress.DecrementBalloonCount();

        // Play balloon burst sound
        balloonPopSound.Play();
    }

    public void DestroyBalloon(BalloonAnchor anchor, Balloon affectedBalloon)
    {
        List<Balloon> list = anchor.GetBalloons();
        if (list.Count > 1)
        {
            // Removed affected balloon
            anchor.RemoveBalloon(affectedBalloon);
            Destroy(affectedBalloon.gameObject);
        }
        else
        {
            // Destroy balloon and anchor
            Destroy(anchor.gameObject);
            Destroy(list[0].gameObject);
            list.Clear();
        }
        progress.DecrementBalloonCount();
        balloonPopSound.Play();
    }
}
