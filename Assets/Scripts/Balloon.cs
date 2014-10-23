using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour
{
    BalloonAnchor anchor;

    void Start()
    {
        // Connect self to the BalloonSplitter in the connected RigidBody's GameObject
        anchor = gameObject.GetComponent<SpringJoint2D>().connectedBody.GetComponent<BalloonAnchor>();
        anchor.AddBalloon(this);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.tag == "Dart")
        {
            // Trigger balloon splitting
            anchor.SplitBalloons(obj.GetComponent<LinearMovement>().GetWorldDirection());

            // Destroy the dart
            Destroy(obj);
        }

    }

    public void SetAnchor(BalloonAnchor a)
    {
        anchor = a;
    }
}
