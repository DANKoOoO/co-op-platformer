using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private int brojac = 0;
    private bool desno = true;

    // Update is called once per frame
    void Update()
    {
        if (desno)
        {
            transform.position += new Vector3(0.005f, 0.0f, 0.0f);
            brojac++;
            if (brojac == 300)
            {
                desno = false;
            }
        }
        else
        {
            transform.position += new Vector3(-0.005f, 0.0f, 0.0f);
            brojac--;
            if (brojac == -300)
            {
                desno = true;
            }
        }

    }
}
