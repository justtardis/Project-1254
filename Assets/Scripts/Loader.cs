using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{

    float counter = 0;
    public float timer = 0;
    public float alltime = 3f;
    public DataLoader dl;
    public Text percent;
    
    private void Update()
    {
        if (counter < 100)
        {
            timer = timer + Time.deltaTime;
            counter = (int)((timer / alltime) * 100);
        }
        else 
        {
            gameObject.SetActive(false);
        }
        percent.text = counter.ToString() + "%";
    }
}
