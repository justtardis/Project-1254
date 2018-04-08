using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryManager : MonoBehaviour
{
    public Color[] color;
    public Sprite[] botIcon;
    public int[] arrIcon = new int[8];
    public Game g;
    public int[] arrTicket = new int[42];
    //int MINIMAL = 0;

    public int ticketNum = 42;
    public Text timeText;

    public GameObject winPanel;

    public int price;
    public int reward;

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


    void Start()
    {
        UnicRand();
        isFinished = false;
        start = DateTime.Now;
        botCount = 8;
        lotteryTime = 2;
        int countCell = (ticketNum - 3) / botCount + 1;
        botCount = UnityEngine.Random.Range(3, 9);
        botCount = 8;
        for (int i = 0; i < botCount; i++)
        {
            bot[i].name = BOT_NAMES[UnityEngine.Random.Range(0, BOT_NAMES.Length)];
            bot[i].countCell = UnityEngine.Random.Range(1, countCell + 1);
            //bot[i].startTime = UnityEngine.Random.Range(1, lotteryTime * 60 / bot[i].countCell);
            bot[i].waitTime = UnityEngine.Random.Range(1, lotteryTime * 60 / bot[i].countCell);
            bot[i].icon = botIcon[arrIcon[i]];
            bot[i].color = color[i];
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
                timeText.text = "00:" + (lotteryTime - 1 - (DateTime.Now - start).Minutes).ToString("0#") + ":" + (59 - (DateTime.Now - start).Seconds).ToString("0#");
            }
            else showWinner();
        }
    }

    public void showWinner()
    {
        int winner = UnityEngine.Random.Range(1, ticketNum);
        if (g.it[winner - 1].isBusy && timeText.transform.parent.gameObject.activeSelf)
        {
            winPanel.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "БИЛЕТ №" + winner;
            winPanel.transform.GetChild(4).GetChild(2).GetComponent<Text>().text = g.it[winner - 1].NameOfBusy;
            winPanel.SetActive(true);
        }
        Debug.Log("Победил билет № " + winner + " с пользователем " + g.it[winner - 1].NameOfBusy);
    }

    public void FlagRefresh()
    {
        for (int i = 0; i < isBusyCell.Length; i++)
        {
            if (!isBusyCell[i])
            {
                cell[i] = g.it[i].id;
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

    /*void Razdacha(BOT bot)
    {
        int sum = MINIMAL + bot.countCell;
        for (int i = MINIMAL; i < MINIMAL + bot.countCell; i++)
        {
            bot.arrayTicket[i] = arrTicket[i];
        }
        MINIMAL += bot.countCell;
        print(MINIMAL);
    }*/


    // Каждому боту известно число занимаемых ячеек
    // Бегаем по массиву всех ячеек это количество раз и выкупаем свободные билеты рандомно.
    IEnumerator BotActive(BOT bot)
    {
        // Цикл ограничен числом билетов у бота
        while (!isFinished)
        {
            for (int i = 0; i < bot.countCell; i++)
            {
                // если билет уже куплен, двигаемся к ближайшему незанятому
                // после повторяем операцию
                yield return new WaitForSeconds(bot.waitTime); // Задержка перед покупкой

                bool isBought = false;
                int cellId = UnityEngine.Random.Range(0, ticketNum);
                bot.waitTime = UnityEngine.Random.Range(1, lotteryTime * 60 / bot.countCell);
                while (!isBought && !isFinished && countBusy != 0)
                {
                    if (!g.it[cellId].isBusy)
                    {
                        g.ConfirmLotteryItem(cellId + 1, bot.name, bot.color, bot.icon); // подтверждаем покупку
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
