using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour
{
    void Start()
    {
        // Connect self to the BalloonSplitter in the connected RigidBody's GameObject
        gameObject.GetComponent<SpringJoint2D>().connectedBody.GetComponent<BalloonAnchor>().AddBalloon(this);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // destroy dart
        // tell splitter to split
    }
}
