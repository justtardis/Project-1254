using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rev : MonoBehaviour
{


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
        GameObject[] Object = new GameObject[4];

        Object[1].SetActive(true);
        Object[2].SetActive(true);
        Object[3].SetActive(true);

        Object[1] = GameObject.Find("/Canvas/Game1/Group2/Jackpot1/Image");
        Object[2] = GameObject.Find("/Canvas/Game1/Group2/Jackpot2/Image");
        Object[3] = GameObject.Find("/Canvas/Game1/Group2/Jackpot3/Image");

        

    }
}
