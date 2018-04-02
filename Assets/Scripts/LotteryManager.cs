using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryManager : MonoBehaviour
{

    public Game g;
    public int[] arrTicket = new int[42];
    int MINIMAL = 0;

    public int ticketNum = 42;
    public Text timeText;

    public DateTime start;
    public int lotteryTime = 10; //время в минутах длительности лотереи 
    public BOT[] bot = new BOT[8]; // Максимум ботов за столом
    public int[] cell = new int[42]; // всего ячеек свободных
    public bool[] isBusyCell = new bool[42]; // всего занятых ячеек
    public int countBusy = 42;
    public bool ALLCELLSBUSY = false;
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
        for (int i = 0; i < botCount; i++)
        {
            bot[i].name = BOT_NAMES[UnityEngine.Random.Range(0, BOT_NAMES.Length)];
            bot[i].countCell = UnityEngine.Random.Range(1, countCell + 1);
            bot[i].startTime = UnityEngine.Random.Range(1, lotteryTime * 60 / bot[i].countCell);
            bot[i].waitTime = UnityEngine.Random.Range(1, lotteryTime * 60 / bot[i].countCell);
            bot[i].color = new Vector4(UnityEngine.Random.Range(0f, 0.5f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);

            StartCoroutine(BotActive(bot[i]));

        }

        //FlagRefresh();
        //botCount =  Random.Range(3, bot.Length); // Определим число ботов-участников
        //for (int i = 0; i < botCount; i++)
        //{
        //    bot[i].name = BOT_NAMES[Random.Range(0, BOT_NAMES.Length)]; // Зададим рандомное имя боту.
        //    bot[i].countCell =  Random.Range(1, 20);
        //    bot[i].color = new Vector4(Random.Range(0f, 0.5f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        //    bot[i].leftCell = bot[i].countCell;
        //    bot[i].arrayTicket = new int[bot[i].countCell];
        //    UnicRand(bot[i]); // cгенерируем список билетов, которые бот купит

        //    StartCoroutine(BotActive(bot[i]));
        //}

    }

    void Update()
    {
        if (!isFinished)
        {
            isFinished = (DateTime.Now - start).Minutes > lotteryTime;
            timeText.text = "Оставшееся время: 00:" + (lotteryTime - (DateTime.Now - start).Minutes).ToString("0#") + ":" + (59 - (DateTime.Now - start).Seconds).ToString("0#");

        }
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
        for (int i = 0; i < 39;)
        {
            alreadyThere = false;
            int newRandomValue = UnityEngine.Random.Range(1, 43);
            for (int j = 0; j < i; j++)
            {
                if (arrTicket[j] == newRandomValue)
                {
                    alreadyThere = true;
                    break;
                }
            }
            if (!alreadyThere)
            {
                arrTicket[i] = newRandomValue;
                i++;
            }
        }
    }

    void Razdacha(BOT bot)
    {
        int sum = MINIMAL + bot.countCell;
        for (int i = MINIMAL; i < MINIMAL + bot.countCell; i++)
        {
            bot.arrayTicket[i] = arrTicket[i];
        }
        MINIMAL += bot.countCell;
        print(MINIMAL);
    }


    // Каждому боту известно число занимаемых ячеек
    // Бегаем по массиву всех ячеек это количество раз и выкупаем свободные билеты рандомно.
    IEnumerator BotActive(BOT bot)
    {
        // Цикл ограничен числом билетов у бота
        for (int i = 0; i < bot.countCell; i++)
        {
            // если билет уже куплен, двигаемся к ближайшему незанятому
            // после повторяем операцию
            yield return new WaitForSeconds(bot.waitTime); // Задержка перед покупкой

            bool isBought = false;
            int cellId = UnityEngine.Random.Range(0, ticketNum);

            while (!isBought && cellId < ticketNum)
            {
                if (!g.it[cellId].isBusy)
                {
                    g.ConfirmLotteryItem(cellId+1, bot.name, bot.color); // подтверждаем покупку
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

[System.Serializable]
public class BOT
{
    public string name = string.Empty;
    public int countCell;
    public int leftCell;
    public int[] arrayTicket;
    public Color color;
    public int startTime;
    public int waitTime;
}
