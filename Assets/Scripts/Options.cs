using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Application.LoadLevel("NormalGame");
        }
        else if(Input.GetButtonDown("Fire2"))
        {
            Application.LoadLevel("SpecialGame");
        }
    }
}
