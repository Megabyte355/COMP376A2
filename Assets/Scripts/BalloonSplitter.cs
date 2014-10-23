using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalloonSplitter : MonoBehaviour
{

    [SerializeField]
    float maxAngleSpread;
    [SerializeField]
    GameObject anchorPrefab;
    List<BalloonAnchor> anchors;

    public int pointsForSplitting = 1;
    public int pointsForPopping = 2;

    void Awake()
    {
        anchors = new List<BalloonAnchor>();
    }

    void Update()
    {

        //// Test
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    //SplitBalloonsTest();
        //    SplitBalloons(new Vector3(0, 1, 0), anchors[0]);
        //}
    }

    public void SplitBalloons(Vector3 impactDirection, BalloonAnchor anchor)
    {
        List<Balloon> list = anchor.GetBalloons();

        if (list.Count > 1)
        {
            // Initialize new BalloonAnchor
            BalloonAnchor newAnchor = (Instantiate(anchorPrefab, anchor.transform.position, Quaternion.identity) as GameObject).GetComponent<BalloonAnchor>();
            newAnchor.SetSpeed(anchor.GetSpeed());

            // Set new directions for both BalloonAnchors
            float rotation = Random.Range(-maxAngleSpread, maxAngleSpread);
            anchor.RotateDirection(rotation);
            newAnchor.SetDirection(Quaternion.AngleAxis(rotation, gameObject.transform.forward) * impactDirection);

            // Split balloons by half
            Color newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            Debug.Log(newColor);
            int half = list.Count / 2;
            for (int i = 0; i < half; i++)
            {
                list[i].gameObject.GetComponent<SpringJoint2D>().connectedBody = newAnchor.gameObject.GetComponent<Rigidbody2D>();
                newAnchor.AddBalloon(list[i]);
                anchor.RemoveBalloon(list[i]);
            }

            // Apply different color to balloons on original anchor
            for (int i = 0; i < anchor.GetBalloons().Count; i++)
            {
                list[i].GetComponent<SpriteRenderer>().color = newColor;
            }

            // Award player 1 point
        }
        else
        {
            // Destroy balloon
            Debug.Log("Not enough Balloons");

            // Award player 2 points
        }
    }



    // TODO: DELETE THIS ONCE DONE
    public void SplitBalloonsTest()
    {
        // ==================== Test balloon splitting ====================

        foreach (BalloonAnchor anchor in anchors)
        {
            List<Balloon> list = anchor.GetBalloons();

            if(list.Count > 1)
            {

                //// Increase speed for all balloons on this anchor before splitting
                //anchor.IncreaseSpeed();
                anchor.SetSpeed(anchor.GetSpeed() + 1f);

                // Initialize new BalloonAnchor
                BalloonAnchor newAnchor = (Instantiate(anchorPrefab, anchor.transform.position, Quaternion.identity) as GameObject).GetComponent<BalloonAnchor>();
                newAnchor.SetSpeed(anchor.GetSpeed());

                //newAnchor.speed = anchor.speed;
                //newAnchor.direction = anchor.direction;
                
                // Set new directions for both BalloonAnchors
                anchor.RotateDirection(30);
                newAnchor.RotateDirection(-30);

                // Split balloons by half
                int half = list.Count / 2;
                for (int i = 0; i < half; i++)
                {
                    list[i].gameObject.GetComponent<SpringJoint2D>().connectedBody = newAnchor.gameObject.GetComponent<Rigidbody2D>();
                    newAnchor.AddBalloon(list[i]);
                    anchor.RemoveBalloon(list[i]);
                }
            }
            else
            {
                // Destroy balloon
                Debug.Log("Not enough Balloons");
            }
        }
    }

    public void AddAnchor(BalloonAnchor anchor)
    {
        anchors.Add(anchor);
    }
}
