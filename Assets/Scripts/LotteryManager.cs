﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryManager : MonoBehaviour
{
    
    public int[] unicNames = new int[8];

    public int[] arrIcon = new int[8];
    public Game g;
    public int[] arrTicket = new int[42];
    //int MINIMAL = 0;
    public SaveLottery sv;
    public int ticketNum = 42;
    public Text timeText;

    public int id;

    public int lotType; // тип лотереи: 1- серебро - серебро, 2 - серебро - золото, 3 - золото- золото, 4 - серебро - предмет

    public GameObject winPanel;

    public int price;
    public int rewardMin;
    public int rewardMax;
    public int reward;
    public int[] rewardItem = new int[2];
    public int waitTime; //время между лотереями

    public DateTime start;
    public int lotteryTime = 10; //время в минутах длительности лотереи 
    public BOT[] bot = new BOT[8]; // Максимум ботов за столом
    public int[] cell = new int[42]; // всего ячеек свободных
    public bool[] isBusyCell = new bool[42]; // всего занятых ячеек
    public int countBusy = 42;
    //public bool ALLCELLSBUSY = false;
    public bool isFinished = false;
    [SerializeField]
    private string[] BOT_NAMES = new string[8] { "xxxBOTxxx", "SergeyBot", "AlexBot", "SemenBot", "HenryBot", "ZombyBot", "PencilBot", "KillBot" };
    public int botCount = 8; // число ботов-участников

    public LotteryItem[] it;
    public GameObject Inform_item;
    public Text _InfText;
    public Text countHeader;
    public Image fillAm;
    //int allTickets = 0;
    public int tickets = 0;
    public string lastWinner;
    //Обекты для вывода информации на обложку лотереи
    public Text timer; //время лотереи
    public Text ticketsText; //количество билетов
    public Text costText; //стоимость билетов
    public Text rewardText; //джекпот
    public Image rewardSpr; //джекпот картиночкой
    public GameObject winner; //победитель
    public GameObject block;
    public Text waitOrPlay;



    void Start()
    {
        UnicRandNames();
        LoadLottery();

    }

    public void buyTickets()
    {
        int max = (int)(((ticketNum - tickets) / botCount) * 0.3);
        int count = 0;
        for (int i = 0; i < botCount; i++)
        {
            count = 0;
            int n = UnityEngine.Random.Range(0, max + 1);
            while (count < n && tickets < ticketNum)
            {
                int cellId = UnityEngine.Random.Range(0, ticketNum);
                if (!it[cellId].isBusy)
                {
                    ConfirmLotteryItem(cellId + 1, bot[i].name, bot[i].color, bot[i].icon); // подтверждаем покупку
                    count++;
                }
            }
        }
    }

    public void LoadLottery()
    {
        string name = id.ToString() + "lottery";
        if (PlayerPrefs.HasKey(name))
        {
            sv = JsonUtility.FromJson<SaveLottery>(PlayerPrefs.GetString(name));
            start = new System.DateTime(sv.start[0], sv.start[1], sv.start[2], sv.start[3], sv.start[4], sv.start[5]);
            lastWinner = sv.lastWinner;
            if ((DateTime.Now - start).TotalMinutes > (lotteryTime))
            {
                reward = sv.reward;
                tickets = sv.tickets;
                arrIcon = JsonHelper.FromJson<int>(sv.arrIcon);
                rewardItem[0] = sv.itemCaseId;
                rewardItem[1] = sv.itemItemId;
                botCount = sv.botCount;
                //sv.bots = new string[bot.Length];
                //sv.it = new string[it.Length];
                bot = JsonHelper.FromJson<BOT>(sv.bots);
                for (int i = 0; i < bot.Length; i++)
                {
                    bot[i].icon = g.botIcon[arrIcon[i]];
                    bot[i].color = g.color[i + 3];
                }
                LotteryItemS[] it2 = new LotteryItemS[it.Length];
                it2 = JsonHelper.FromJson<LotteryItemS>(sv.it);
                for (int i = 0; i < it.Length; i++)
                {
                    it[i].id = it2[i].id;
                    it[i].isBusy = it2[i].isBusy;
                    it[i].NameOfBusy = it2[i].NameOfBusy;
                    if (it[i].isBusy)
                    {
                        for (int j = 0; j < bot.Length; j++)
                        {
                            if (bot[j].name == it2[i].NameOfBusy)
                            {
                                //it[i].icon.sprite = bot[j].icon;
                                it[i].transform.GetChild(0).gameObject.SetActive(false);
                                it[i].transform.GetChild(1).gameObject.SetActive(true);
                                it[i].transform.GetChild(1).GetComponent<Image>().sprite = bot[j].icon;
                                it[i].GetComponent<Image>().color = bot[j].color;
                            }
                        }
                        if (g.deviceID == it2[i].NameOfBusy)
                        {
                            it[i].GetComponent<Image>().color = g.color[1];
                        }
                    }

                }
                isFinished = sv.isFinished;
                //showWinner();
                winner.transform.GetChild(0).GetComponent<Text>().text = lastWinner;
                winner.SetActive(true);
                timer.transform.parent.gameObject.GetComponent<Button>().interactable = false;
                block.SetActive(true);
                if ((DateTime.Now - start).TotalMinutes > (lotteryTime + waitTime))
                {
                    double delta = (DateTime.Now - start).TotalMinutes - (lotteryTime + waitTime);
                    timer.transform.parent.gameObject.GetComponent<Button>().interactable = true;
                    block.SetActive(false);
                    startLottery();
                    int seconds = UnityEngine.Random.Range(0, (int)(delta * 60));
                    start.Subtract(new DateTime(0, 0, 0, seconds / 3600, seconds / 60, seconds % 60));
                    buyTickets();
                }
            }
            else
            {
                reward = sv.reward;
                tickets = sv.tickets;
                arrIcon = JsonHelper.FromJson<int>(sv.arrIcon);
                rewardItem[0] = sv.itemCaseId;
                rewardItem[1] = sv.itemItemId;
                botCount = sv.botCount;
                //sv.bots = new string[bot.Length];
                //sv.it = new string[it.Length];
                isFinished = sv.isFinished;
                bot = JsonHelper.FromJson<BOT>(sv.bots);
                for (int i = 0; i < bot.Length; i++)
                {
                    bot[i].icon = g.botIcon[arrIcon[i]];
                    bot[i].color = g.color[i + 3];
                }
                LotteryItemS[] it2 = new LotteryItemS[it.Length];
                it2 = JsonHelper.FromJson<LotteryItemS>(sv.it);
                for (int i = 0; i < it.Length; i++)
                {
                    it[i].id = it2[i].id;
                    it[i].isBusy = it2[i].isBusy;
                    it[i].NameOfBusy = it2[i].NameOfBusy;
                    if (it[i].isBusy)
                    {
                        for (int j = 0; j < bot.Length; j++)
                        {
                            if (bot[j].name == it2[i].NameOfBusy)
                            {
                                //it[i].icon.sprite = bot[j].icon;
                                it[i].transform.GetChild(0).gameObject.SetActive(false);
                                it[i].transform.GetChild(1).gameObject.SetActive(true);
                                it[i].transform.GetChild(1).GetComponent<Image>().sprite = bot[j].icon;
                                it[i].GetComponent<Image>().color = bot[j].color;
                            }
                        }
                        if (g.deviceID == it2[i].NameOfBusy)
                        {
                            it[i].GetComponent<Image>().color = g.color[1];
                        }
                    }

                }
                tickets = countTickets();
                buyTickets();
                for (int i = 0; i < botCount; i++)
                {
                    StartCoroutine(BotActive(bot[i]));
                }
                winner.SetActive(false);
            }
        }
        else
        {
            startLottery();
        }
        updateCover();
    }

    public void updateCover()
    {
        ticketsText.text = (ticketNum - tickets).ToString() + " / " + ticketNum.ToString();
        //fillAm.fillAmount = (float)(tickets / ticketNum);
        fillAm.fillAmount = (float)tickets / ticketNum;
        countHeader.text = tickets.ToString() + " / " + ticketNum.ToString();
        costText.text = g.convertMoney(price);
        if (rewardSpr != null)
        {
            int rand = (reward - 4000) / 500;
            rewardSpr.sprite = g.spr_lot1[rand];
        }
        else if (lotType != 4)
        {
            rewardText.text = g.convertMoney(reward);
        }
    }

    public int countTickets()
    {
        int res = 0;
        for (int i = 0; i < it.Length; i++)
        {
            if (it[i].isBusy) res++;
        }
        return res;
    }

    public void startLottery()
    {
        ticketsText.text = ticketNum.ToString() + " / " + ticketNum.ToString();
        winner.SetActive(false);
        UnicRand();
        if (waitOrPlay != null)
        {
            waitOrPlay.text = LangSystem.lng.listL[2]; //"До конца лотереи";
        }
        tickets = 0;
        fillAm.fillAmount = (float)(tickets / ticketNum);
        countHeader.text = tickets.ToString() + " / " + ticketNum.ToString();
        costText.text = g.convertMoney(price);
        isFinished = false;
        for (int i = 0; i < ticketNum; i++)
        {
            it[i].isBusy = false;
            it[i].transform.GetChild(0).gameObject.SetActive(true);
            it[i].transform.GetChild(1).gameObject.SetActive(false);
            it[i].GetComponent<Image>().color = g.color[2];
        }
        if (rewardSpr != null)
        {
            int rand = UnityEngine.Random.Range(0, 13);
            reward = 4000 + rand * 500;
            rewardSpr.sprite = g.spr_lot1[rand];
        }
        else if (lotType != 4)
        {
            reward = UnityEngine.Random.Range(rewardMin, rewardMin);
            reward = reward / 1000 * 1000;
            rewardText.text = g.convertMoney(reward);
        }
        else
        {
            int caseId = UnityEngine.Random.Range(0, g.cases.Length);
            int i = 0;
            while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group != 4) i++;
            int itemId = UnityEngine.Random.Range(i, g.cases[caseId].items.Length);
            rewardItem[0] = caseId;
            rewardItem[1] = itemId;
        }
        if (lotType == 2 || lotType == 3)
        {
            reward = UnityEngine.Random.Range(rewardMin, rewardMin);
            rewardText.text = g.convertMoney(reward);
        }
        start = DateTime.Now;
        //lotteryTime = 2;
        countBusy = ticketNum;
        int countCell = (ticketNum - 3) / botCount + 1;
        botCount = UnityEngine.Random.Range(3, 9);
        for (int i = 0; i < botCount; i++)
        {
            //bot[i].name = BOT_NAMES[UnityEngine.Random.Range(0, BOT_NAMES.Length)];
            bot[i].name = g.botNames[unicNames[i]];
            bot[i].countCell = UnityEngine.Random.Range(1, countCell + 1);
            //bot[i].startTime = UnityEngine.Random.Range(1, lotteryTime * 60 / bot[i].countCell);
            bot[i].waitTime = UnityEngine.Random.Range(1, lotteryTime * 60 / bot[i].countCell);
            bot[i].icon = g.botIcon[arrIcon[i]];
            bot[i].color = g.color[i + 3];
            StartCoroutine(BotActive(bot[i]));
        }
    }

    void Update()
    {
        if (!isFinished)
        {
            isFinished = (DateTime.Now - start).TotalMinutes > (lotteryTime);
            if (!isFinished)
            {
                DateTime end = start.AddMinutes(lotteryTime);
                TimeSpan left = end - DateTime.Now;
                timeText.text = (left.Hours).ToString("0#") + ":" + (left.Minutes).ToString("0#") + ":" + (left.Seconds).ToString("0#");
                timer.text = (left.Hours).ToString("0#") + ":" + (left.Minutes).ToString("0#") + ":" + (left.Seconds).ToString("0#");
            }
            else showWinner();
        }
        else
        {
            if ((DateTime.Now - start).TotalMinutes > (lotteryTime + waitTime))
            {
                StopCoroutine("BotActive");
                timer.transform.parent.gameObject.GetComponent<Button>().interactable = true;
                block.SetActive(false);
                startLottery();
            }
            else
            {
                timer.text = "00:" + (waitTime + lotteryTime - 1 - (DateTime.Now - start).Minutes).ToString("0#") + ":" + (59 - (DateTime.Now - start).Seconds).ToString("0#");
            }
        }
    }

    public void showWinner()
    {
        block.SetActive(true);
        timer.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        if (waitOrPlay != null)
        {
            waitOrPlay.text = LangSystem.lng.listL[3];// "Ожидание";
        }
        int winner1 = UnityEngine.Random.Range(1, ticketNum);
        if (it[winner1 - 1].isBusy && timeText.transform.parent.gameObject.activeSelf)
        {
            winPanel.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = LangSystem.lng.listL[14]/* "БИЛЕТ №"*/ + winner;
            if (it[winner1 - 1].NameOfBusy == g.deviceID)
            {
                winPanel.transform.GetChild(4).GetChild(2).GetComponent<Text>().text = g.nickname;
            }
            else
            {
                winPanel.transform.GetChild(4).GetChild(2).GetComponent<Text>().text = it[winner1 - 1].NameOfBusy;
            }
            timeText.transform.parent.gameObject.SetActive(false);
            if (lotType != 4)
            {
                winPanel.transform.GetChild(9).GetComponent<Text>().text = g.convertMoney(reward);
                winPanel.transform.GetChild(8).gameObject.SetActive(true);
            }
            else
            {
                winPanel.transform.GetChild(9).GetComponent<Text>().text = g.cases[rewardItem[0]].items[rewardItem[1]].name;
                winPanel.transform.GetChild(8).gameObject.SetActive(false);
            }
            winPanel.SetActive(true);
        }
        lastWinner = it[winner1 - 1].NameOfBusy;
        if (it[winner1 - 1].NameOfBusy == g.deviceID)
        {
            lastWinner = g.nickname;
            if (lotType == 1)
            {
                g.silver = g.silver + reward;
            }
            else if (lotType != 4)
            {
                g.gold = g.gold + reward;
            }
            else
            {
                Inventory inv = g.gameObject.transform.GetComponent<Inventory>();
                inv.items[inv.invSize][1] = rewardItem[1];
                inv.items[inv.invSize][0] = rewardItem[0];
                inv.invSize++;
                inv.LoadInventory();
            }
            if (!g.ach.achievments[8].get) g.ach.getAch(8);
        }
        if (it[winner1 - 1].isBusy)
        {
            winner.SetActive(true);
            if (it[winner1 - 1].NameOfBusy == g.deviceID)
            {
                winner.transform.GetChild(0).GetComponent<Text>().text = g.nickname;
            }
            else
            {
                winner.transform.GetChild(0).GetComponent<Text>().text = it[winner1 - 1].NameOfBusy;
            }
        }
        else
        {
            winner.SetActive(false);
        }
        
        for (int i = 0; i < ticketNum; i++)
        {
            it[i].isBusy = false;
            it[i].transform.GetChild(0).gameObject.SetActive(true);
            it[i].transform.GetChild(1).gameObject.SetActive(false);
            it[i].GetComponent<Image>().color = g.color[2];
        }
        countHeader.text = 0 + " / " + ticketNum.ToString();
        fillAm.fillAmount = 0;
        Debug.Log("Победил билет № " + winner + " с пользователем " + it[winner1 - 1].NameOfBusy);

    }

    public void FlagRefresh()
    {
        for (int i = 0; i < isBusyCell.Length; i++)
        {
            if (!isBusyCell[i])
            {
                cell[i] = it[i].id;
            }
            else
            {
                cell[i] = 0;
            }
        }
    }

    void UnicRand() // Для генерации рандомных позиций целей
    {
        bool alreadyThere;
        for (int i = 0; i < 8;)
        {
            alreadyThere = false;
            int newRandomValue = UnityEngine.Random.Range(0, 14);
            for (int j = 0; j < i; j++)
            {
                if (arrIcon[j] == newRandomValue)
                {
                    alreadyThere = true;
                    break;
                }
            }
            if (!alreadyThere)
            {
                arrIcon[i] = newRandomValue;
                i++;
            }
        }
    }
    void UnicRandNames() // Для генерации рандомных позиций целей
    {
        bool already;
        for (int i = 0; i < 8;)
        {
            already = false;
            int newRandomValue = UnityEngine.Random.Range(0, 72);
            for (int j = 0; j < i; j++)
            {
                if (arrIcon[j] == newRandomValue)
                {
                    already = true;
                    break;
                }
            }
            if (!already)
            {
                unicNames[i] = newRandomValue;
                i++;
            }
        }
    }
   

    // Каждому боту известно число занимаемых ячеек
    // Бегаем по массиву всех ячеек это количество раз и выкупаем свободные билеты рандомно.
    IEnumerator BotActive(BOT bot)
    {
        yield return null; // заглушка

        //Цикл ограничен числом билетов у бота
        while (!isFinished)
        {
            // 
            for (int i = 0; i < bot.countCell; i++)
            {
                // если билет уже куплен, двигаемся к ближайшему незанятому
                // после повторяем операцию
                yield return new WaitForSeconds(bot.waitTime); // Задержка перед покупкой

                bool isBought = false;
                int cellId = UnityEngine.Random.Range(0, ticketNum);
                bot.waitTime = UnityEngine.Random.Range(1, lotteryTime * 60 / bot.countCell);
                while (!isBought && !isFinished && tickets < ticketNum)
                {
                    if (!it[cellId].isBusy)
                    {
                        ConfirmLotteryItem(cellId + 1, bot.name, bot.color, bot.icon); // подтверждаем покупку
                        bot.leftCell--;
                        isBought = true;
                    }
                    else
                    {
                        cellId = UnityEngine.Random.Range(0, ticketNum);
                    }
                }
            }
        }

    }

    public void ConfirmLotteryItem(int id, string name, Color color1, Sprite spr)
    {
        if (!it[id - 1].isBusy)
        {
            color1.a = 1f;
            it[id - 1].GetComponent<Image>().color = color1;
            if (color1 != g.color[1])
            {
                it[id - 1].transform.GetChild(0).gameObject.SetActive(false);
                it[id - 1].transform.GetChild(1).gameObject.SetActive(true);
                it[id - 1].transform.GetChild(1).GetComponent<Image>().sprite = spr;
            }
            else
            {
                if (lotType != 3) g.silver = g.silver - price;
                else g.gold = g.gold - price;
                if (!g.ach.achievments[7].get) g.ach.getAch(7);
            }
            it[id - 1].isBusy = true;
            it[id - 1].NameOfBusy = name;
            isBusyCell[id - 1] = true;
            countBusy -= 1;
            tickets = tickets + 1;
            countHeader.text = tickets.ToString() + " / " + ticketNum.ToString();
            ticketsText.text = (ticketNum - tickets).ToString() + " / " + ticketNum.ToString();
            fillAm.fillAmount = (float)tickets / ticketNum;
            //FlagRefresh(); // нужно для обновления ячеекы
        }
        else
        {
            Inform_item.SetActive(true);
            //_InfText.text = string.Format("Билет {0} занят игроком {1}", it[id - 1].id, it[id - 1].NameOfBusy);
            _InfText.text = string.Format(LangSystem.lng.listL[15], it[id - 1].id, it[id - 1].NameOfBusy);
        }
    }

    public void LotteryClickItem(LotteryItem item)
    {
        if (item.isBusy && item.NameOfBusy != g.deviceID)
        {
            Inform_item.SetActive(true);
            _InfText.text = string.Format(LangSystem.lng.listL[15], item.id, item.NameOfBusy);
            //_InfText.text = string.Format("Билет {0} занят игроком {1}", item.id, item.NameOfBusy);
            // print(string.Format("ячейка {0} занята игроком {1}", item.id, item.NameOfBusy));
        }
        else if (!item.isBusy)
        {
            int money;
            if (lotType != 3) money = g.silver;
            else money = g.gold;
            if (money >= price)
            {
                g.LotteryConfirm.SetActive(true);
                g.LotteryConfirm.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>().text = item.id.ToString(); // выводим номер id на табло
                g.LotteryConfirm.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
                g.LotteryConfirm.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { ConfirmLotteryItem(item.id, g.deviceID, g.color[1], g.botIcon[1]); }); // подтверждаем выбор
            }
            else
            {
                g.noMoney.SetActive(true);
            }
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        sv.reward = reward;
        sv.itemCaseId = rewardItem[0];
        sv.itemItemId = rewardItem[1];
        //sv.bots = new string[bot.Length];
        //sv.it = new string[it.Length];
        sv.start[0] = start.Year;
        sv.start[1] = start.Month;
        sv.start[2] = start.Day;
        sv.start[3] = start.Hour;
        sv.start[4] = start.Minute;
        sv.start[5] = start.Second;
        sv.arrIcon = JsonHelper.ToJson<int>(arrIcon);
        sv.lastWinner = lastWinner;
        sv.isFinished = isFinished;
        sv.botCount = botCount;
        sv.tickets = tickets;
        sv.bots = JsonHelper.ToJson<BOT>(bot);
        LotteryItemS[] it2 = new LotteryItemS[it.Length];
        for (int i = 0; i < it.Length; i++)
        {
            it2[i] = new LotteryItemS();
            it2[i].id = it[i].id;
            it2[i].isBusy = it[i].isBusy;
            it2[i].NameOfBusy = it[i].NameOfBusy;
            it2[i].icon = it[i].icon;
        }
        sv.it = JsonHelper.ToJson<LotteryItemS>(it2);
        string json = JsonUtility.ToJson(sv);
        string name = id.ToString() + "lottery";
        PlayerPrefs.SetString(name, JsonUtility.ToJson(sv));
    }
}



[System.Serializable]
public class BOT
{
    public string name = string.Empty;
    public int countCell;
    public int leftCell;
    public int[] arrayTicket;
    public Color color;
    public Sprite icon;
    //public int startTime;
    public int waitTime;
}

[System.Serializable]
public class LotteryItemS
{
    public int id;
    public bool isBusy = false;
    public string NameOfBusy = string.Empty;
    public Image icon;
    public Text number;
}

[System.Serializable]
public class SaveLottery
{
    public int reward;
    public int itemCaseId;
    public int itemItemId;
    public int tickets;
    public int botCount;
    public string lastWinner;
    public string bots;
    public string it;
    public string arrIcon;
    public int[] start = new int[6];
    public bool isFinished;
}
