using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rev : MonoBehaviour
{

    public GameObject[] Object;
    public int rev = 0;
    // 1 - вкл, 2 - выкл
    void AnimTrigger()
    {
        if (rev == 1)
        {
            gameObject.SetActive(true);
        }
        else if (rev == 2)
        {
            gameObject.SetActive(false);
        }
    }
    void Trigger()
    {
        if (rev == 2)
        {
            for(int i=0; i<4; i++)
            {
                if (!Object[i].activeSelf)
                {
                    Object[i].SetActive(true);
                }
            }
        }
    }
}
