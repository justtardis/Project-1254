using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievment : MonoBehaviour {

    public Achievment_item[] achievments;
    public Game g;


    // Use this for initialization
    void Start () {
        achievments[6].get = checkElite();
        LoadWindow();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //функция для проверки, получена ли ачивка Элита
    public bool checkElite()
    {
        bool res = true;
        for (int i = 0; i < g.cases.Length; i++)
        {
            for (int j = 0; j < g.cases[i].items.Length; j++){
                if (!g.cases[i].items[j].received && (g.cases[i].items[j].group == 4)) res = false;
            }
        }
        return res;
    }

    public void LoadWindow()
    {
        GameObject panel = g.Panels[5];
        GameObject group = panel.transform.GetChild(1).GetChild(0).gameObject;
        for (int i = 0; i < 12; i++)
        {
            GameObject A = group.transform.GetChild(i).gameObject;
            //A.transform.GetChild(0).GetComponent<Image>().sprite =  ставим иконку
            A.transform.GetChild(1).GetComponent<Text>().text = achievments[i].header;
            A.transform.GetChild(2).GetComponent<Text>().text = achievments[i].description;
        }
    }

}

[System.Serializable]
public class Achievment_item
{
    public Sprite icon;
    public string header;
    public string description;
    //public int award;
    public bool get;
}