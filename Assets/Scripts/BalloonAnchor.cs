using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalloonAnchor : MonoBehaviour
{
    
    [SerializeField]
    float maxSpeed = 6f;
    public float speed = 1f;
    public Vector3 direction;

    [SerializeField]
    List<Balloon> balloonList;

    void Awake()
    {
        balloonList = new List<Balloon>();
        gameObject.GetComponent<WrapAround>().AddOnWrapAction(WrapAroundBalloons);
    }

    void Start()
    {
        GameObject.FindGameObjectWithTag("BalloonSplitter").GetComponent<BalloonSplitter>().AddAnchor(this);
    }

    void Update()
    {
        gameObject.transform.Translate(direction.normalized * speed * Time.deltaTime);
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

    public void RotateDirection(float degrees)
    {
        direction = Quaternion.AngleAxis(degrees, gameObject.transform.forward) * direction;
    }

    public void IncreaseSpeed()
    {
        if (speed < maxSpeed)
        {
            // Calculates how much speed should be increased based on current size of balloonList
            int iterations = 0;
            float balloons = balloonList.Count;
            while (balloons > 1)
            {
                balloons = balloons / 2;
                iterations++;
            }
            if(iterations > 0)
            {
                float speedDiff = maxSpeed - speed;
                float increase = speedDiff / iterations;
                speed += increase;
            }
        }
    }
}
