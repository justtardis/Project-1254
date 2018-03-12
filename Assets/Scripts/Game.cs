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

    [Header("КНОПКИ И ВСЕ, ЧТО С НИМИ СВЯЗАНО")]
    public bool PriceActive = false;
    public Animator price;
    [Space(5f)]
    [Header("CКРОЛЛ ВСЯКИЙ")]
    public ScrollRect scrollPreview;


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
        print(Base64Encode("Какой-то текст"));
        print(Base64Decode("0JrQsNC60L7QuS3RgtC+INGC0LXQutGB0YI="));
    }

  
    public void OpenPreview(int id)
    {
        main.SetActive(false);
        scrollPreview.verticalNormalizedPosition = 1f;
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
        preview.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = cases[id].price.ToString();
        preview.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "КЕЙС\n\"" + cases[id].name + "\"";
        preview.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate { scr.OpenCase(id); });
        preview.SetActive(true);
    }

    #region СЮДА ПИШЕМ ВЕСЬ КОД НА РАЗНЫЕ КНОПКИ
    //Код кнопкт с ценой в превью, чтобы анимация работала туда-сюда
    public void PriceButton()
    {
        if (!PriceActive)
        {
            PriceActive = true;
            price.SetBool("Active", PriceActive);
        }
        else
        {
            PriceActive = false;
            price.SetBool("Active", PriceActive);
        }
    }
    #endregion

    #region МЕТОДЫ КОДИРОВАНИЯ ДЛЯ СОКРЫТИЯ ИНФОРМАЦИИ
    //Не каждый сообразит, что это base64 и не каждый смекнет, че с ним делать
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
    #endregion

}



//Класс кейса
[System.Serializable]
public class Case
{
    public string Namecase;
    public int id; 
    public string name; // имя
    public float price; // цена
    public Item[] items;
    public Sprite picture; //картинка
    public int[] groups = new int[4];
}

