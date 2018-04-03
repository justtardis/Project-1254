using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryItem : MonoBehaviour
{
    public int id;
    public bool isBusy = false;
    public string NameOfBusy = string.Empty;
    public Text number;

    private void Start()
    {
       number.text = id.ToString();
    }

}
