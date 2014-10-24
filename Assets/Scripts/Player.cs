using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    LifeCount lifeCount;

    void Start()
    {
        lifeCount = GameObject.FindGameObjectWithTag("LifeCount").GetComponent<LifeCount>();
    }

    public void Kill()
    {
        lifeCount.DecrementLives();
    }
}
