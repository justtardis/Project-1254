using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievment : MonoBehaviour {

    public Achievment_item[] achievments;
    public Game g;
    public GameObject push;
    public Image medal;


    // Use this for initialization
    void Start () {
        achievments[6].get = checkElite();
        achievments[11].get = checkAll();
        LoadWindow();
        updateMedal();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateMedal()
    {
        if (g.level == 30)
        {
            medal.sprite = g.medals[5];
        }
        else if (g.level > 23)
        {
            medal.sprite = g.medals[4];
        }
        else if (g.level > 17)
        {
            medal.sprite = g.medals[3];
        }
        else if (g.level > 11)
        {
            medal.sprite = g.medals[2];
        }
        else if (g.level > 5)
        {
            medal.sprite = g.medals[1];
        }
        else
        {
            medal.sprite = g.medals[0];
        }
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

    public bool checkAll()
    {
        bool res = true;
        for (int i = 0; i < achievments.Length - 1; i++)
        {
            if (achievments[i].get == false) res = false;
        }
        return res;
    }

    public void LoadWindow()
    {
        GameObject panel = g.Panels[4];
        GameObject group = panel.transform.GetChild(1).GetChild(0).gameObject;
        for (int i = 0; i < 12; i++)
        {
            GameObject A = group.transform.GetChild(i).gameObject;
            //A.transform.GetChild(0).GetComponent<Image>().sprite =  ставим иконку
            A.transform.GetChild(2).GetComponent<Text>().text = achievments[i].header;
            A.transform.GetChild(3).GetComponent<Text>().text = achievments[i].description;
            A.transform.GetChild(1).gameObject.SetActive(!achievments[i].get);
        }
    }

    public void getAch(int id)
    {
        achievments[id].get = true;
        push.transform.GetChild(5).GetComponent<Text>().text = achievments[id].header;
        push.SetActive(true);
        achievments[11].get = checkAll();
        LoadWindow();
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