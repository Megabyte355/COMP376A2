using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalloonSplitter : MonoBehaviour
{
    [SerializeField]
    GameObject anchorPrefab;
    List<BalloonAnchor> anchors;

    void Awake()
    {
        anchors = new List<BalloonAnchor>();
    }

    void Update()
    {

        // Test
        if (Input.GetButtonDown("Fire1"))
        {
            SplitBalloonsTest();
        }
    }

    public void SplitBalloonsTest()
    {
        // ==================== Test balloon splitting ====================

        foreach (BalloonAnchor anchor in anchors)
        {
            List<Balloon> list = anchor.GetBalloons();

            if(list.Count > 1)
            {

                // Increase speed for all balloons on this anchor before splitting
                anchor.IncreaseSpeed();

                // Initialize new BalloonAnchor
                BalloonAnchor newAnchor = (Instantiate(anchorPrefab, anchor.transform.position, Quaternion.identity) as GameObject).GetComponent<BalloonAnchor>();
                newAnchor.speed = anchor.speed;
                newAnchor.direction = anchor.direction;
                
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
