using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public float silver; // серебро
    public float gold; // золото
    public Case[] cases; //кейсы
    public GameObject casePref; //префаб кейса
    public GameObject itemPref; //префаб товара
    public GameObject caseContainer; //контейнер кейсов
    public GameObject itemContainer; //контейнер товаров
    public GameObject main; // главное окно
    public GameObject preview; // окно предпросмотра
    public GameObject roulett; // окно рулетки
    public ScrollScript scr;

    // Use this for initialization
    void Start () {
        silver = 200;
        gold = 5;
        for (int i = 0; i < cases.Length; i++)
        {
            GameObject A = Instantiate(casePref, casePref.transform.position = new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            A.transform.SetParent(caseContainer.transform, false);
            A.transform.GetChild(0).GetComponent<Image>().sprite = cases[i].picture;
            A.transform.GetChild(1).GetComponent<Text>().text = cases[i].price.ToString();
            A.transform.GetChild(3).GetComponent<Text>().text = cases[i].name;
            int id = cases[i].id;
            A.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { OpenPreview(id); });
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenPreview(int id)
    {
        main.SetActive(false);
        //Отображаем товары кейса
        for (int i = 0; i < cases[id].items.Length; i++)
        {
            GameObject A = itemContainer.transform.GetChild(i).gameObject;
            A.transform.GetChild(0).GetComponent<Text>().text = cases[id].items[i].price.ToString();
            A.transform.GetChild(1).GetComponent<Image>().sprite = cases[id].items[i].picture;
            A.transform.GetChild(2).gameObject.SetActive(cases[id].items[i].group == 4);
            //A.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenPreview);
        }
        //Удаляем ненужные ячейки
        for (int i = cases[id].items.Length; i < 21; i++)
        {
            GameObject A = itemContainer.transform.GetChild(i).gameObject;
            A.SetActive(false);
        }
        preview.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "$" + cases[id].price;
        preview.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "КЕЙС\n\"" + cases[id].name + "\"";
        preview.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate { scr.OpenCase(id); });
        preview.SetActive(true);
    }

    

}

//Класс кейса
[System.Serializable]
public class Case
{
    public int id; 
    public string name; // имя
    public float price; // цена
    public Item[] items;
    public Sprite picture; //картинка
    public int[] groups = new int[4];
}

