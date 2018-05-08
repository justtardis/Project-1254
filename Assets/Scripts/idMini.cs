using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class idMini : MonoBehaviour
{
    public string control = string.Empty;
    public MiniGames mini;
    public int id;
    public Text text;
    public bool opened = false;
    // Use this for initialization
    void triggerAnim()
    {
        if (control == "first") text.text = mini.firstString[id].ToString();
        else if(control == "second") text.text = mini.secondString[id].ToString();
    }

}
