﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    #region Переменные
    public int silver; // серебро | заменил на целые числа
    public int gold; // золото
    public Text silverText;
    public Text goldText;
    public Text usersText;
    public Text casesText;
    public Case[] cases; //кейсы
    public DataLoader dl; // класс для получения и обновления данных
    public GameObject casePref; //префаб кейса
    public GameObject itemPref; //префаб товара
    public GameObject caseContainer; //контейнер кейсов
    public GameObject itemContainer; //контейнер товаров
    public GameObject main; // главное окно
    public GameObject preview; // окно предпросмотра
    public GameObject roulett; // окно рулетки
    public GameObject noMoney;
    public ScrollScript scr;
    public Achievment ach;
    //public int userId = 2;

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
    public float progress = 0f;
    public bool Get = false;
    [Space(5f)]
    [Header("Счётчики")]
    public int casesNum; //сколько открыто кейсов
    public int itemsSold; //сколько продано товаров
    public int bonuses; //получено на ежедневных бонусах
    public int level = 0;
    [Space(5f)]
    [Header("Тогглы")]
    public int id_toggle;
    public int[] priceToggle;
    public bool[] toggleActive;
    public bool[] moveToggle;
    public GameObject[] togglePreview;
    public Image[] bgTogglePreview;
    public float[] timesColor;
    public bool[] changes;
    [Space(5f)]
    [Header("Лидерборд")]
    public GameObject groupList;
    private GameObject tempList;
    public Sprite FilledBtn;
    public Sprite LinearBtn;
    public GameObject[] L;
    public Image[] Btn;
    public Text Header;

    public Text user;
    public Text count_cases;
    public Text topLevel;
    Texture2D userPicture;

    [Space(5f)]
    [Header("Настройки")]
    public Color[] color;
    public float[] t;
    public bool[] change;
    public Image[] bgToggle;
    public GameObject[] touchOne;
    public GameObject[] touchSecond;
    public bool[] SettingsBool;
    public bool[] move;

    [Space(5f)]
    [Header("Достижения")]
    public Text levelText; // Номер уровня
    public Text levelLead; // Уровень в лидерборде
    public Text levelPerc; // Процент открытых кейсов
    public Image caseFill; // Полоса заполнения кейсов
    public Sprite[] medals;

    [Space(5f)]
    [Header("Лотерея")]
    public Sprite[] spr_lot1;
    public Sprite[] text_lot1_en;
    public Sprite[] text_lot1_ru;
    public string nickname = "Jack";
    public string google_id = string.Empty;
    public LotteryManager Lm;
    public GameObject LotteryConfirm;
    /*public LotteryItem[] it;
    public GameObject Inform_item;
    public Text _InfText;
    public Text countHeader;
    public Image fillAm;
    int allTickets = 0;
    int tickets = 0;*/
    public Sprite[] botIcon;
    [Space(5f)]
    [Header("Все панели и окна")]
    public GameObject[] Panels;
    public GameObject PanelAct;
    public int curPanelId;
    public Image avatar;
    public Image avatar_menu;
    // 0 - Main
    // 1 - Inventory
    // 2 - Shop
    // 3 - Casino
    // 4 - Achievement
    // 5 - Settings
    // 6 - HeaderCounter
    // 7 - Lottery
    // 8 - Preview
    #endregion
    public AudioSource audioSource;
    public AudioClip audioClip;
    public Text username_menu;
   
    public int[] Cases_Level;
    public AudioClip[] ac;
    public AudioSource _as;

    public Image fillSlider;
    public Slider sld;
    public Text percent;
    //Перенес в Awake, потому что нужно задавать положения плюсика у баланса
    public void playClip()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    
    public void CustomSlider()
    {
        fillSlider.fillAmount = sld.value;
        percent.text = (sld.value * 100 ).ToString("#0")+"%";
    }
    
    public void auth()
    {
        //PlayGamesPlatform.Activate();
        //Social.localUser.Authenticate((bool success) =>
        //{
        //    if (success)
        //    {
        //        nickname = Social.localUser.userName;
        //        google_id = Social.localUser.id;
        //        username_menu.text = nickname;
        //        //suc = true;
        ////        StartCoroutine(dl.LoginOrInsertData(google_id, nickname));
        //       // avatar.sprite = Sprite.Create(Social.localUser.image, new Rect(0, 0, Social.localUser.image.width, Social.localUser.image.height), new Vector2(0.5f, 0.5f),50f);
        //    }
        //    else
        //    {
        //        nickname = "LemonS";
        //        google_id = "6984412st50933dc";
        //        username_menu.text = nickname;
        //        StartCoroutine(dl.LoginOrInsertData(google_id, nickname));
        //    }
        //    StartCoroutine(LoadImage());
        //    StartCoroutine(dl.getMainData());
        //    StartCoroutine(dl.getDataTop());           
        //});
    }

    public void W()
    {
        //PlayGamesPlatform.Activate();
        //Social.localUser.Authenticate((bool success) =>
        //{
        //    if (success)
        //    {
        //        nickname = Social.localUser.userName;
        //        google_id = Social.localUser.id;
        //    }
        //});
    }

    IEnumerator LoadImage()
    {
        while (Social.localUser.image == null)
        {
            yield return null;
        }
        avatar.sprite = Sprite.Create(Social.localUser.image, new Rect(0, 0, Social.localUser.image.width, Social.localUser.image.height), new Vector2(0.5f, 0.5f), 50f);
        avatar_menu.sprite = Sprite.Create(Social.localUser.image, new Rect(0, 0, Social.localUser.image.width, Social.localUser.image.height), new Vector2(0.5f, 0.5f), 50f);
        //Social.localUser.image is not null any more
    }
    // Use this for initialization
    void Start()
    {
        W();
            sld.value = 0.59f;
      //  auth();
        silverText.text = convertMoney(silver); //отображаем серебро в панели на главной
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
        #region Тут альфа-значения для тогглов в настройках. Для теста задаем булку в инспекторе. Не цикл, потому что touchOne и touchSecond
        if (SettingsBool[0])
        {
            bgToggle[0].color = color[1];
            touchOne[0].transform.localPosition = new Vector2(77.5f, touchOne[0].transform.localPosition.y);
            touchOne[1].transform.localPosition = new Vector2(221f, touchOne[1].transform.localPosition.y);
            touchOne[2].transform.localPosition = new Vector2(-31.665f, touchOne[2].transform.localPosition.y);
            gameObject.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            bgToggle[0].color = color[0];
            touchOne[0].transform.localPosition = new Vector2(-77.5f, touchOne[0].transform.localPosition.y);
            touchOne[1].transform.localPosition = new Vector2(31.665f, touchOne[1].transform.localPosition.y);
            touchOne[2].transform.localPosition = new Vector2(-221f, touchOne[2].transform.localPosition.y);
            gameObject.GetComponent<AudioSource>().mute = true;
        }
        if (SettingsBool[1])
        {
            bgToggle[1].color = color[1];
            touchSecond[0].transform.localPosition = new Vector2(77.5f, touchSecond[0].transform.localPosition.y);
            touchSecond[1].transform.localPosition = new Vector2(221f, touchSecond[1].transform.localPosition.y);
            touchSecond[2].transform.localPosition = new Vector2(-31.665f, touchSecond[2].transform.localPosition.y);
        }
        else
        {
            bgToggle[1].color = color[0];
            touchSecond[0].transform.localPosition = new Vector2(-77.5f, touchSecond[0].transform.localPosition.y);
            touchSecond[1].transform.localPosition = new Vector2(31.665f, touchSecond[1].transform.localPosition.y);
            touchSecond[2].transform.localPosition = new Vector2(-221f, touchSecond[2].transform.localPosition.y);
        }
        #endregion

        //Тут альфа-значения для тогглов в превью. 
        DefaultUPDToggle();

        print(Base64Encode("Какой-то текст"));
        print(Base64Decode("0JrQsNC60L7QuS3RgtC+INGC0LXQutGB0YI="));
        if (!ach.achievments[0].get)
        {
            ach.getAch(0);
        }
        int perc = ((int)(((float)(casesNum - Cases_Level[level - 1]) / (Cases_Level[level] - Cases_Level[level - 1])) * 100));
        percent.text = perc + "%";
        fillSlider.fillAmount = (float)perc / 100;
        sld.value = (float)perc / 100;
    }

    public void CheckCase(int id)
    {
        if (cases[id].price <= silver)
        {
            silver = silver - cases[id].price;
            switch (id_toggle)
            {
                case 1:
                    gold -= 3;
                    break;
                case 2:
                    gold -= 7;
                    break;
                case 3:
                    gold -= 10;
                    break;
                default:
                    break;
            }
            casesNum++;
            dl.updateData(google_id, casesNum);
            if (casesNum == 100) ach.getAch(1);
            else if (casesNum == 1000) ach.getAch(2);
            else if (casesNum == 5000) ach.getAch(3);
            else if (casesNum == 10000) ach.getAch(4);
            if (casesNum == Cases_Level[level])
            {
                level++;
                levelText.text = "Уровень " + level.ToString(); 
            }
            int perc = ((int)(((float)(casesNum - Cases_Level[level - 1]) / (Cases_Level[level] - Cases_Level[level - 1])) * 100));
            percent.text = perc + "%";
            fillSlider.fillAmount = (float)perc / 100;
            sld.value = (float)perc / 100;
            caseFill.fillAmount = (float)perc / 100;
            levelLead.text = level.ToString();
            ach.updateMedal();
            scr.OpenCase(id);
        }
        else
        {
            noMoney.SetActive(true);
        }
    }

    public void OpenPreview(int id)
    {
        main.SetActive(false);
        playClip();
        id_toggle = -1; //чтобы можно было узнать, что никто не запущен
        DefaultUPDToggle(); //дефолтные значения тогглов наверху
        scrollPreview.verticalNormalizedPosition = 1f;
        //Отображаем товары кейса
        for (int i = 0; i < cases[id].items.Length; i++)
        {
            GameObject A = itemContainer.transform.GetChild(i).gameObject;
            A.transform.GetChild(0).GetComponent<Text>().text = convertMoneyFloat(cases[id].items[i].price);
            A.transform.GetChild(1).GetComponent<Image>().sprite = cases[id].items[i].picture;
            A.transform.GetChild(2).gameObject.SetActive(cases[id].items[i].group == 4);
            A.SetActive(true);
            //A.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenPreview);
        }
        //Удаляем ненужные ячейки
        for (int i = cases[id].items.Length; i < 21; i++)
        {
            GameObject A = itemContainer.transform.GetChild(i).gameObject;
            A.SetActive(false);
        }
        preview.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = cases[id].price.ToString();
        preview.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "КЕЙС\n\"" + cases[id].name + "\"";
        preview.transform.GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
        preview.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate { CheckCase(id); });
        preview.SetActive(true);
    }

    #region СЮДА ПИШЕМ ВЕСЬ КОД НА РАЗНЫЕ КНОПКИ

    private void DefaultUPDToggle()
    {
        for (int i = 0; i < 3; i++)
        {
            bgTogglePreview[i].color = color[0];
            toggleActive[i] = false;
        }
        togglePreview[1].SetActive(true);
        togglePreview[4].SetActive(true);
        togglePreview[7].SetActive(true);
        togglePreview[2].SetActive(false);
        togglePreview[5].SetActive(false);
        togglePreview[8].SetActive(false);
        togglePreview[0].transform.localPosition = new Vector2(-77.5f, togglePreview[0].transform.localPosition.y);
        togglePreview[3].transform.localPosition = new Vector2(-77.5f, togglePreview[3].transform.localPosition.y);
        togglePreview[6].transform.localPosition = new Vector2(-77.5f, togglePreview[6].transform.localPosition.y);
        togglePreview[1].transform.localPosition = new Vector2(31.665f, togglePreview[1].transform.localPosition.y);
        togglePreview[4].transform.localPosition = new Vector2(31.665f, togglePreview[4].transform.localPosition.y);
        togglePreview[7].transform.localPosition = new Vector2(31.665f, togglePreview[7].transform.localPosition.y);
        togglePreview[2].transform.localPosition = new Vector2(-221f, togglePreview[2].transform.localPosition.y);
        togglePreview[5].transform.localPosition = new Vector2(-221f, togglePreview[5].transform.localPosition.y);
        togglePreview[8].transform.localPosition = new Vector2(-221f, togglePreview[8].transform.localPosition.y);

    }

    public void Individual()
    {
        if (!L[0].activeSelf)
        {
            L[0].SetActive(true);
            L[1].SetActive(false);
            Btn[0].sprite = FilledBtn;
            Btn[1].sprite = LinearBtn;
            Header.text = "ЛИЧНЫЙ ПРОГРЕСС";
        }
    }

    public void TopGamers()
    {
        if (!L[1].activeSelf)
        {
            L[0].SetActive(false);
            L[1].SetActive(true);
            Btn[0].sprite = LinearBtn;
            Btn[1].sprite = FilledBtn;
            Header.text = "ТОП-10 ЛУЧШИХ";
        }

    }

    public void ClickMenu()
    {
       // avatar.sprite = Sprite.Create(Social.localUser.image, new Rect(0, 0, Social.localUser.image.width, Social.localUser.image.height), new Vector2(0.5f, 0.5f), 20f);
        // блок, если меню нужно
        if (!MenuActive && !Get)
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

    // Переключение тогглов. id необходим для запоминания того, что мы выбрали и списывания бабла согласно цене priceToggle
    // Списывать деньги можно ТОЛЬКО, если игрок открыл кейс

    public void ClickToggle(int id)
    {
        if (!toggleActive[id - 1])
        {
            if (gold >= priceToggle[id - 1])
            {
                switch (id)
                {
                    case 1:
                        toggleActive[0] = true;
                        toggleActive[1] = false;
                        toggleActive[2] = false;
                        break;
                    case 2:
                        toggleActive[0] = false;
                        toggleActive[1] = true;
                        toggleActive[2] = false;
                        break;
                    case 3:
                        toggleActive[0] = false;
                        toggleActive[1] = false;
                        toggleActive[2] = true;
                        break;
                }
                id_toggle = id;
                for (int i = 0; i < 3; i++)
                {
                    moveToggle[i] = true;
                }
            }
        }
        else
        {
            toggleActive[id - 1] = false;
            moveToggle[id - 1] = true;
        }
    }

    public void Preloader(GameObject panelActive)
    {
        if (!panelActive.activeSelf)
        {
            for (int i = 0; i < Panels.Length; i++)
            {
                if (Panels[i] == panelActive)
                {
                    PanelAct = panelActive;
                    curPanelId = i;
                }
                else
                {
                    Panels[i].SetActive(false);
                }
            }

            for (int i = 0; i < 7; i++)
            {
                Menu_panel.transform.GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().color = whiteDisabled; // Иконки всех меняем на светло-белый 
                Menu_panel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().color = whiteDisabled; // Заголовки всех меняем на светло-белый
            }
            Menu_panel.transform.GetChild(2).GetChild(curPanelId).GetChild(0).GetComponent<Image>().color = whiteEnabled; // Иконка главной 
            Menu_panel.transform.GetChild(2).GetChild(curPanelId).GetChild(1).GetComponent<Text>().color = whiteEnabled; // Заголовок главной            
            preLoaderActive = true;
            preLoader.SetActive(true);
        }
        else { }
        ClickMenu();
    }

    // Тогглы настроек
    public void Toggle(int id)
    {
        if (!SettingsBool[id - 1])
        {
            SettingsBool[id - 1] = true;
            move[id - 1] = true;
        }
        else
        {
            SettingsBool[id - 1] = false;
            move[id - 1] = true;
        }
    }

    /*public void ConfirmLotteryItem(int id, string name, Color color1, Sprite spr)
    {
        if (!it[id - 1].isBusy)
        {

                it[id - 1].GetComponent<Image>().color = color1;
                if (color1 != color[1])
                {
                    it[id - 1].transform.GetChild(0).gameObject.SetActive(false);
                    it[id - 1].transform.GetChild(1).gameObject.SetActive(true);
                    it[id - 1].transform.GetChild(1).GetComponent<Image>().sprite = spr;
                }
                else
                {
                    silver = silver - Lm.price;
                }
                it[id - 1].isBusy = true;
                it[id - 1].NameOfBusy = name;
                Lm.isBusyCell[id - 1] = true;
                Lm.countBusy -= 1;
                tickets += 1;
                countHeader.text = tickets.ToString() + " / " + allTickets.ToString();
                fillAm.fillAmount = (float)tickets / allTickets;
                Lm.FlagRefresh(); // нужно для обновления ячеекы
        }
        else
        {
            Inform_item.SetActive(true);
            _InfText.text = string.Format("Билет {0} занят игроком {1}", it[id - 1].id, it[id - 1].NameOfBusy);
        }
    }

    public void LotteryClickItem(LotteryItem item)
    {
        if (item.isBusy && item.NameOfBusy != nickname)
        {
            Inform_item.SetActive(true);
            _InfText.text = string.Format("Билет {0} занят игроком {1}", item.id, item.NameOfBusy);
           // print(string.Format("ячейка {0} занята игроком {1}", item.id, item.NameOfBusy));
        }
        else if (!item.isBusy)
        {
            if (silver >= Lm.price)
            {
                LotteryConfirm.SetActive(true);
                LotteryConfirm.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>().text = item.id.ToString(); // выводим номер id на табло
                LotteryConfirm.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
                LotteryConfirm.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { ConfirmLotteryItem(item.id, nickname, color[1], Lm.botIcon[1]); }); // подтверждаем выбор
            }
            else
            {
                noMoney.SetActive(true);
            }
        }
    }*/
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
        #region ВОТ ЭТОТ КОД ОТВЕЧАЕТ ЗА ПРЕЛОАДЕР И АКТИВАЦИЮ НУЖНОЙ ПАНЕЛИ ПОСЛЕ
        if (preLoaderActive)
        {
            progressLoader.text = progress.ToString("#") + "%";
            if (progress >= 99f)
            {
                Get = false;
                preLoaderActive = false;
                preLoader.SetActive(false);
                progress = 0;
                if (PanelAct != Panels[1]) // Если панель главная, то покажем еще и шапку
                {
                    Panels[7].SetActive(true);
                }
                PanelAct.SetActive(true);
            }
            else
            {
                progress += 4.5f;
            }
        }
        #endregion

        //Никогда не открывать :)
        #region 1й Тоггл в настройках (Звуки)
        if (move[0])
        {
            if (SettingsBool[0])
            {
                gameObject.GetComponent<AudioSource>().mute = false;
                touchOne[2].SetActive(true);
                touchOne[0].transform.localPosition = new Vector2(Mathf.Lerp(touchOne[0].transform.localPosition.x, 77.5f, 4 * Time.deltaTime), touchOne[0].transform.localPosition.y);
                touchOne[1].transform.localPosition = new Vector2(Mathf.Lerp(touchOne[1].transform.localPosition.x, 221f, 4 * Time.deltaTime), touchOne[1].transform.localPosition.y);
                touchOne[2].transform.localPosition = new Vector2(Mathf.Lerp(touchOne[2].transform.localPosition.x, -31.665f, 4 * Time.deltaTime), touchOne[2].transform.localPosition.y);
                bgToggle[0].color = Color.Lerp(color[0], color[1], t[0]);
                if (!change[0])
                {
                    t[0] += 4 * Time.deltaTime;
                }
                if (t[0] >= 1f)
                {
                    change[0] = true;
                }
                if (touchOne[0].transform.localPosition.x >= 77.49f)
                {
                    if (touchOne[1].transform.localPosition.x >= 219f)
                    {
                        touchOne[1].SetActive(false);
                    }
                    move[0] = false;
                    
                }
            }
            else if (!SettingsBool[0])
            {
                gameObject.GetComponent<AudioSource>().mute = true;
                touchOne[1].SetActive(true);
                touchOne[0].transform.localPosition = new Vector2(Mathf.Lerp(touchOne[0].transform.localPosition.x, -77.5f, 4 * Time.deltaTime), touchOne[0].transform.localPosition.y);
                touchOne[1].transform.localPosition = new Vector2(Mathf.Lerp(touchOne[1].transform.localPosition.x, 31.665f, 4 * Time.deltaTime), touchOne[1].transform.localPosition.y);
                touchOne[2].transform.localPosition = new Vector2(Mathf.Lerp(touchOne[2].transform.localPosition.x, -221f, 4 * Time.deltaTime), touchOne[2].transform.localPosition.y);
                bgToggle[0].color = Color.Lerp(color[0], color[1], t[0]);
                if (change[0])
                {
                    t[0] -= 4 * Time.deltaTime;
                }
                if (t[0] <= 0f)
                {
                    change[0] = false;
                }
                if (touchOne[0].transform.localPosition.x <= -77.49f)
                {
                    if (touchOne[2].transform.localPosition.x <= -219f)
                    {
                        touchOne[2].SetActive(false);
                    }
                    move[0] = false;
                   
                }
            }
        }
        #endregion
        //Никогда не открывать :)
        #region 2й Тоггл в настройках (Музыка)
        if (move[1])
        {
            if (SettingsBool[1])
            {
                touchSecond[2].SetActive(true);
                touchSecond[0].transform.localPosition = new Vector2(Mathf.Lerp(touchSecond[0].transform.localPosition.x, 77.5f, 4 * Time.deltaTime), touchSecond[0].transform.localPosition.y);
                touchSecond[1].transform.localPosition = new Vector2(Mathf.Lerp(touchSecond[1].transform.localPosition.x, 221f, 4 * Time.deltaTime), touchSecond[1].transform.localPosition.y);
                touchSecond[2].transform.localPosition = new Vector2(Mathf.Lerp(touchSecond[2].transform.localPosition.x, -31.665f, 4 * Time.deltaTime), touchSecond[2].transform.localPosition.y);
                bgToggle[1].color = Color.Lerp(color[0], color[1], t[1]);
                if (!change[1])
                {
                    t[1] += 4 * Time.deltaTime;
                }
                if (t[1] >= 1f)
                {
                    change[1] = true;
                }
                if (touchSecond[0].transform.localPosition.x >= 77.49f)
                {
                    if (touchSecond[1].transform.localPosition.x >= 219f)
                    {
                        touchSecond[1].SetActive(false);
                    }
                    move[1] = false;
                }
            }
            else if (!SettingsBool[1])
            {
                touchSecond[1].SetActive(true);
                touchSecond[0].transform.localPosition = new Vector2(Mathf.Lerp(touchSecond[0].transform.localPosition.x, -77.5f, 4 * Time.deltaTime), touchSecond[0].transform.localPosition.y);
                touchSecond[1].transform.localPosition = new Vector2(Mathf.Lerp(touchSecond[1].transform.localPosition.x, 31.665f, 4 * Time.deltaTime), touchSecond[1].transform.localPosition.y);
                touchSecond[2].transform.localPosition = new Vector2(Mathf.Lerp(touchSecond[2].transform.localPosition.x, -221f, 4 * Time.deltaTime), touchSecond[2].transform.localPosition.y);
                bgToggle[1].color = Color.Lerp(color[0], color[1], t[1]);
                if (change[1])
                {
                    t[1] -= 4 * Time.deltaTime;
                }
                if (t[1] <= 0f)
                {
                    change[1] = false;
                }
                if (touchSecond[0].transform.localPosition.x <= -77.49f)
                {
                    if (touchSecond[2].transform.localPosition.x <= -219f)
                    {
                        touchSecond[2].SetActive(false);
                    }
                    move[1] = false;
                }
            }
        }
        #endregion

        #region 1й Тоггл превью
        if (moveToggle[0])
        {
            if (toggleActive[0])
            {
                togglePreview[2].SetActive(true);
                togglePreview[0].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[0].transform.localPosition.x, 77.5f, 4 * Time.deltaTime), togglePreview[0].transform.localPosition.y);
                togglePreview[1].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[1].transform.localPosition.x, 221f, 4 * Time.deltaTime), togglePreview[1].transform.localPosition.y);
                togglePreview[2].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[2].transform.localPosition.x, -31.665f, 4 * Time.deltaTime), togglePreview[2].transform.localPosition.y);
                bgTogglePreview[0].color = Color.Lerp(color[0], color[1], timesColor[0]);
                if (!changes[0])
                {
                    timesColor[0] += 4 * Time.deltaTime;
                }
                if (timesColor[0] >= 1f)
                {
                    changes[0] = true;
                }
                if (togglePreview[0].transform.localPosition.x >= 77.49f)
                {
                    if (togglePreview[1].transform.localPosition.x >= 219f)
                    {
                        togglePreview[1].SetActive(false);
                    }
                    moveToggle[0] = false;
                    timesColor[0] = 0f;
                }
            }
            else if (!toggleActive[0])
            {
                togglePreview[1].SetActive(true);
                togglePreview[0].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[0].transform.localPosition.x, -77.5f, 4 * Time.deltaTime), togglePreview[0].transform.localPosition.y);
                togglePreview[1].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[1].transform.localPosition.x, 31.665f, 4 * Time.deltaTime), togglePreview[1].transform.localPosition.y);
                togglePreview[2].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[2].transform.localPosition.x, -221f, 4 * Time.deltaTime), togglePreview[2].transform.localPosition.y);
                bgTogglePreview[0].color = Color.Lerp(color[0], color[1], timesColor[0]);
                if (changes[0])
                {
                    timesColor[0] -= 4 * Time.deltaTime;
                }
                if (timesColor[0] <= 0f)
                {
                    changes[0] = false;
                }
                if (togglePreview[0].transform.localPosition.x <= -77.49f)
                {
                    if (togglePreview[2].transform.localPosition.x <= -219f)
                    {
                        togglePreview[2].SetActive(false);
                    }
                    moveToggle[0] = false;
                    timesColor[0] = 0f;
                }
            }
        }
        #endregion

        #region 2й Тоггл превью
        if (moveToggle[1])
        {
            if (toggleActive[1])
            {
                togglePreview[5].SetActive(true);
                togglePreview[3].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[3].transform.localPosition.x, 77.5f, 4 * Time.deltaTime), togglePreview[3].transform.localPosition.y);
                togglePreview[4].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[4].transform.localPosition.x, 221f, 4 * Time.deltaTime), togglePreview[4].transform.localPosition.y);
                togglePreview[5].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[5].transform.localPosition.x, -31.665f, 4 * Time.deltaTime), togglePreview[5].transform.localPosition.y);
                bgTogglePreview[1].color = Color.Lerp(color[0], color[1], timesColor[1]);
                if (!changes[1])
                {
                    timesColor[1] += 4 * Time.deltaTime;
                }
                if (timesColor[1] >= 1f)
                {
                    changes[1] = true;
                }
                if (togglePreview[3].transform.localPosition.x >= 77.49f)
                {
                    if (togglePreview[4].transform.localPosition.x >= 219f)
                    {
                        togglePreview[4].SetActive(false);
                    }
                    moveToggle[1] = false;
                    timesColor[1] = 0f;
                }
            }
            else if (!toggleActive[1])
            {
                togglePreview[4].SetActive(true);
                togglePreview[3].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[3].transform.localPosition.x, -77.5f, 4 * Time.deltaTime), togglePreview[3].transform.localPosition.y);
                togglePreview[4].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[4].transform.localPosition.x, 31.665f, 4 * Time.deltaTime), togglePreview[4].transform.localPosition.y);
                togglePreview[5].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[5].transform.localPosition.x, -221f, 4 * Time.deltaTime), togglePreview[5].transform.localPosition.y);
                bgTogglePreview[1].color = Color.Lerp(color[0], color[1], timesColor[1]);
                if (changes[1])
                {
                    timesColor[1] -= 4 * Time.deltaTime;
                }
                if (timesColor[1] <= 0f)
                {
                    changes[1] = false;
                }
                if (togglePreview[3].transform.localPosition.x <= -77.49f)
                {
                    if (togglePreview[5].transform.localPosition.x <= -219f)
                    {
                        togglePreview[5].SetActive(false);
                    }
                    moveToggle[1] = false;
                    timesColor[1] = 0f;
                }
            }
        }
        #endregion

        #region 3й Тоггл превью
        if (moveToggle[2])
        {
            if (toggleActive[2])
            {
                togglePreview[8].SetActive(true);
                togglePreview[6].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[6].transform.localPosition.x, 77.5f, 4 * Time.deltaTime), togglePreview[6].transform.localPosition.y);
                togglePreview[7].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[7].transform.localPosition.x, 221f, 4 * Time.deltaTime), togglePreview[7].transform.localPosition.y);
                togglePreview[8].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[8].transform.localPosition.x, -31.665f, 4 * Time.deltaTime), togglePreview[8].transform.localPosition.y);
                bgTogglePreview[2].color = Color.Lerp(color[0], color[1], timesColor[2]);
                if (!changes[2])
                {
                    timesColor[2] += 4 * Time.deltaTime;
                }
                if (timesColor[2] >= 1f)
                {
                    changes[2] = true;
                }
                if (togglePreview[6].transform.localPosition.x >= 77.49f)
                {
                    if (togglePreview[7].transform.localPosition.x >= 219f)
                    {
                        togglePreview[7].SetActive(false);
                    }
                    moveToggle[2] = false;
                    timesColor[2] = 0f;
                }
            }
            else if (!toggleActive[2])
            {
                togglePreview[7].SetActive(true);
                togglePreview[6].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[6].transform.localPosition.x, -77.5f, 4 * Time.deltaTime), togglePreview[6].transform.localPosition.y);
                togglePreview[7].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[7].transform.localPosition.x, 31.665f, 4 * Time.deltaTime), togglePreview[7].transform.localPosition.y);
                togglePreview[8].transform.localPosition = new Vector2(Mathf.Lerp(togglePreview[8].transform.localPosition.x, -221f, 4 * Time.deltaTime), togglePreview[8].transform.localPosition.y);
                bgTogglePreview[2].color = Color.Lerp(color[0], color[1], timesColor[2]);
                if (changes[2])
                {
                    timesColor[2] -= 4 * Time.deltaTime;
                }
                if (timesColor[2] <= 0f)
                {
                    changes[2] = false;
                }
                if (togglePreview[6].transform.localPosition.x <= -77.49f)
                {
                    if (togglePreview[8].transform.localPosition.x <= -219f)
                    {
                        togglePreview[8].SetActive(false);
                    }
                    moveToggle[2] = false;
                    timesColor[2] = 0f;
                }
            }
        }
        #endregion

        #region Меню
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
            else if (!MenuActive)
            {
                Menu_panel.transform.localPosition = new Vector2(Mathf.Lerp(Menu_panel.transform.localPosition.x, -1000, 6 * Time.deltaTime), Menu_panel.transform.localPosition.y);
                if (Menu_panel.transform.localPosition.x <= -995)
                {
                    inMove = false;
                    Menu_panel.SetActive(MenuActive);
                }
            }
        }
        #endregion
        silverText.text = convertMoney(silver);
        goldText.text = convertMoney(gold);
    }

    public string convertMoney(int money)
    {
        string res = "";
        if (money < 1000000) // млрд
        {
            res = money.ToString("#,##0");
            res = res.Replace(",", " ");
        }
        else if (money < 1000000000)// млн
        {
            res = money.ToString("#,###,##0");
            res = res.Replace(",", " ");
        }
        else
        {
            res = money.ToString("#,###,###,##0");
            res = res.Replace(",", " ");
        }
        return res;
    }
    public string convertMoneyFloat(float money)
    {
        string res = "";
        if (money < 1000000) 
        {
            res = money.ToString("#,##0");
            res = res.Replace(",", " ");
        }
        return res;
    }
}



//Класс кейса
[System.Serializable]
public class Case
{
    public string Namecase;
    public int id;
    public string name; // имя
    public int price; // цена
    public Item[] items;
    public Sprite picture; //картинка
    public int[] groups = new int[4];
}

