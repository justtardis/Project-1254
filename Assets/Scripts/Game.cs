using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public int silver; // серебро | заменил на целые числа
    public int gold; // золото
    public Text silverText;
    public Text goldText;
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
    [Header("----------------------------------------------------------")]
    public Color whiteEnabled;
    public Color whiteDisabled;
    [Space(5f)]
    [Header("CКРОЛЛ ВСЯКИЙ")]
    public ScrollRect scrollPreview;
    [Space(5f)]
    [Header("Меню и его компоненты")]
    public GameObject Menu_panel; // сама менюшка
    public bool MenuActive = false; // флаг на вызов меню (кликнули на бургер или нет)
    public bool inMove = false; // флаг для движения меню (движется или нет). Нужна на Туда-обратно
    [Space(5f)]
    [Header("Прелоадер")]
    public Text progressLoader;
    public GameObject preLoader;
    public bool preLoaderActive = false;
    public int progress = 0;
    //Перенес в Awake, потому что нужно задавать положения плюсика у баланса
    private void Awake()
    {
        //silver = 99; 
        //gold = 5;
    }

    // Use this for initialization
    void Start () {

        silverText.text = silver.ToString(); //отображаем серебро в панели на главной
        goldText.text = gold.ToString(); //отображаем золото в панели на главной
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

    public void ClickMenu()
    {
        // блок, если меню нужно
        if (!MenuActive)
        {
            MenuActive = true;
            inMove = true;          
        }
        // блок, если меню не нужно
        else
        {
            MenuActive = false;
            inMove = true;
        } 
    }

    public void Preloader()
    {
        preLoaderActive = true;
        preLoader.SetActive(true);
        ClickMenu();
    }

    // Кнопки в меню
    // Это главная кнопка
    public void ClickMain()
    {
        Menu_panel.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().color = whiteEnabled; // Иконка главной 
        Menu_panel.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>().color = whiteEnabled; // Заголовок главной 
        for(int i = 1; i<6; i++)
        {
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().color = whiteDisabled; // Иконки всех меняем на светло-белый 
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().color = whiteDisabled; // Заголовки всех меняем на светло-белый
        }
        ClickMenu(); // Чтобы меню уезжало
    }

    // Это кнопка инвентаря
    public void ClickInventory()
    {
        Menu_panel.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().color = whiteDisabled; // Иконку главной меняем на светло-белый 
        Menu_panel.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>().color = whiteDisabled; // Заголовок главной меняем на светло-белый 

        Menu_panel.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Image>().color = whiteEnabled; // Иконка инвентаря 
        Menu_panel.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().color = whiteEnabled; // Заголовок инвентаря 
        for (int i = 2; i < 6; i++)
        {
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().color = whiteDisabled; // Иконки всех меняем на светло-белый 
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().color = whiteDisabled; // Заголовки всех меняем на светло-белый
        }
        ClickMenu(); // Чтобы меню уезжало
    }

    // Это кнопка магазина
    public void ClickShop()
    {
        for (int i = 0; i < 2; i++)
        {
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().color = whiteDisabled; // Иконки всех меняем на светло-белый 
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().color = whiteDisabled; // Заголовки всех меняем на светло-белый
        }
        Menu_panel.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Image>().color = whiteEnabled; // Иконка магазина 
        Menu_panel.transform.GetChild(2).GetChild(2).GetChild(1).GetComponent<Text>().color = whiteEnabled; // Заголовок магазина 
        for (int i = 3; i < 6; i++)
        {
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().color = whiteDisabled; // Иконки всех меняем на светло-белый 
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().color = whiteDisabled; // Заголовки всех меняем на светло-белый
        }
        ClickMenu(); // Чтобы меню уезжало
    }

    // Это кнопка казино
    public void ClickCasino()
    {
        for (int i = 0; i < 3; i++)
        {
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().color = whiteDisabled; // Иконки всех меняем на светло-белый 
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().color = whiteDisabled; // Заголовки всех меняем на светло-белый
        }
        Menu_panel.transform.GetChild(2).GetChild(3).GetChild(0).GetComponent<Image>().color = whiteEnabled; // Иконка казино 
        Menu_panel.transform.GetChild(2).GetChild(3).GetChild(1).GetComponent<Text>().color = whiteEnabled; // Заголовок казино 
        for (int i = 4; i < 6; i++)
        {
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().color = whiteDisabled; // Иконки всех меняем на светло-белый 
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().color = whiteDisabled; // Заголовки всех меняем на светло-белый
        }
        ClickMenu(); // Чтобы меню уезжало
    }

    // Это кнопка магазина
    public void ClickAchievement()
    {
        for (int i = 0; i < 4; i++)
        {
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().color = whiteDisabled; // Иконки всех меняем на светло-белый 
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().color = whiteDisabled; // Заголовки всех меняем на светло-белый
        }
        Menu_panel.transform.GetChild(2).GetChild(4).GetChild(0).GetComponent<Image>().color = whiteEnabled; // Иконка Наград 
        Menu_panel.transform.GetChild(2).GetChild(4).GetChild(1).GetComponent<Text>().color = whiteEnabled; // Заголовок наград 

        Menu_panel.transform.GetChild(2).GetChild(5).GetChild(0).GetComponent<Image>().color = whiteDisabled; // Иконку настроек меняем на светло-белый 
        Menu_panel.transform.GetChild(2).GetChild(5).GetChild(1).GetComponent<Text>().color = whiteDisabled; // Заголовок настроек меняем на светло-белый
        ClickMenu(); // Чтобы меню уезжало
    }

    // Это кнопка настроек
    public void ClickSettings()
    {
        Menu_panel.transform.GetChild(2).GetChild(5).GetChild(0).GetComponent<Image>().color = whiteEnabled; // Иконка настроек
        Menu_panel.transform.GetChild(2).GetChild(5).GetChild(1).GetComponent<Text>().color = whiteEnabled; // Заголовок настроек 
        for (int i = 0; i < 5; i++)
        {
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().color = whiteDisabled; // Иконки всех меняем на светло-белый 
            Menu_panel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().color = whiteDisabled; // Заголовоки всех меняем на светло-белый
        }
        ClickMenu(); // Чтобы меню уезжало
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

    private void FixedUpdate()
    {
        if (preLoaderActive)
        {
            progressLoader.text = progress.ToString() + "%";
            if (progress == 100)
            {
                preLoaderActive = false;
                preLoader.SetActive(false);
                progress = 0;
            }
            else
            {
                progress += 1;
            }
        }
        // Если меню должно двигаться, 
        if (inMove)
        {
            // смотрим, нужно ли оно
            // Если да, то
            if (MenuActive)
            {
                Menu_panel.SetActive(MenuActive);
                // Везем его к нужной точке. Позже заменим -300 на переменную, значение которое присваиваем в зависимости от разрешения экрана
                Menu_panel.transform.localPosition = new Vector2(Mathf.Lerp(Menu_panel.transform.localPosition.x, -300, 6 * Time.deltaTime), Menu_panel.transform.localPosition.y);
                if (Menu_panel.transform.localPosition.x >= -302)
                {
                    inMove = false;
                }
            }
            // Если нет, то
            else if(!MenuActive)
            {
                Menu_panel.transform.localPosition = new Vector2(Mathf.Lerp(Menu_panel.transform.localPosition.x, -1000, 6 * Time.deltaTime), Menu_panel.transform.localPosition.y);
                if (Menu_panel.transform.localPosition.x <= -995)
                {
                    inMove = false;
                    Menu_panel.SetActive(MenuActive);
                }
            }
        }
    }
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

