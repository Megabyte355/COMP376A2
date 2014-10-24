using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalloonAnchor : MonoBehaviour
{

    [SerializeField]
    List<Balloon> balloonList;
    BalloonSplitter splitter;
    LinearMovement movement;

    void Awake()
    {
        balloonList = new List<Balloon>();
        gameObject.GetComponent<WrapAround>().AddOnWrapAction(WrapAroundBalloons);
        movement = gameObject.GetComponent<LinearMovement>();
    }

    void Start()
    {
        splitter = GameObject.FindGameObjectWithTag("BalloonSplitter").GetComponent<BalloonSplitter>();
    }

    public void WrapAroundBalloons(Vector3 translation)
    {
        foreach (Balloon balloon in balloonList)
        {
            balloon.transform.Translate(translation, Space.World);
        }
    }

    public void AddBalloon(Balloon balloon)
    {
        balloonList.Add(balloon);
    }

    public void RemoveBalloon(Balloon balloon)
    {
        balloonList.Remove(balloon);
    }

    public List<Balloon> GetBalloons()
    {
        return balloonList;
    }

    //public void IncreaseSpeed()
    //{
    //    if (speed < maxSpeed)
    //    {
    //        // Calculates how much speed should be increased based on current size of balloonList
    //        int iterations = 0;
    //        float balloons = balloonList.Count;
    //        while (balloons > 1)
    //        {
    //            balloons = balloons / 2;
    //            iterations++;
    //        }
    //        if(iterations > 0)
    //        {
    //            float speedDiff = maxSpeed - speed;
    //            float increase = speedDiff / iterations;
    //            speed += increase;
    //        }
    //    }
    //}

    // ------------------------ Delegates to BalloonSplitter ------------------------
    public void SplitBalloons(Vector3 impactDirection, Balloon affectedBalloon)
    {
        splitter.SplitBalloons(impactDirection, this, affectedBalloon);
    }


    // ------------------------ Delegates to LinearMovement ------------------------
    public float GetSpeed()
    {
        return movement.speed;
    }

    public void SetSpeed(float s)
    {
        movement.speed = s;
    }

    public void IncreaseSpeed()
    {
        movement.IncreaseSpeed();
    }

    public Vector3 GetDirection()
    {
        return movement.direction;
    }

    public void SetDirection(Vector3 d)
    {
        movement.direction = d;
    }

    public void RotateDirection(float degrees)
    {
        movement.RotateDirection(degrees);
    }
}
