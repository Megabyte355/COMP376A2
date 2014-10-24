using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour
{
    BalloonAnchor anchor;
    Player player;

    void Start()
    {
        // Connect self to the BalloonSplitter in the connected RigidBody's GameObject
        anchor = gameObject.GetComponent<SpringJoint2D>().connectedBody.GetComponent<BalloonAnchor>();
        anchor.AddBalloon(this);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.tag == "Dart")
        {
            // Destroy the dart
            Destroy(obj);

            // Trigger balloon splitting
            // NOTE: Destruction of balloon is handled in anchor
            anchor.SplitBalloons(obj.GetComponent<LinearMovement>().GetWorldDirection(), this);
        }
        else if (obj.tag == "Player")
        {
            player.Kill();
            anchor.DestroyBalloon(this);
        }
    }

    public void SetAnchor(BalloonAnchor a)
    {
        anchor = a;
    }
}
