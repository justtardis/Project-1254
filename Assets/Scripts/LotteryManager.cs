using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotteryManager : MonoBehaviour
{
    
    public Game g;
    public bool alreadyThere;
    public BOT[] bot = new BOT[8]; // Максимум ботов за столом
    public int[] cell = new int[42]; // всего ячеек свободных
    public bool[] isBusyCell = new bool[42]; // всего занятых ячеек
    public int countBusy = 42;
    public bool ALLCELLSBUSY = false;
    [SerializeField]
    private string[] BOT_NAMES = new string[8] { "xxxBOTxxx", "SergeyBot", "AlexBot", "SemenBot", "HenryBot", "ZombyBot", "PencilBot", "KillBot" };
    public int botCount; // число ботов-участников
    void Start()
    {
        FlagRefresh();
        botCount =  Random.Range(3, bot.Length); // Определим число ботов-участников
        for (int i = 0; i < botCount; i++)
        {
            bot[i].name = BOT_NAMES[Random.Range(0, BOT_NAMES.Length)]; // Зададим рандомное имя боту.
            bot[i].countCell =  Random.Range(1, 20);
            bot[i].color = new Vector4(Random.Range(0f, 0.5f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            bot[i].leftCell = bot[i].countCell;
            bot[i].arrayTicket = new int[bot[i].countCell];
            UnicRand(bot[i]); // cгенерируем список билетов, которые бот купит
            
            StartCoroutine(BotActive(bot[i]));
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

    void UnicRand(BOT bot) // Для генерации рандомных позиций целей
    {
        
        //int[] arr = new int[bot.countCell];
        for (int i = 0; i < bot.countCell;)
        {
            alreadyThere = false;
            int newRandomValue = Random.Range(1, 43);
            for (int j = 0; j < i; j++)
            {
                if (bot.arrayTicket[j] == newRandomValue)
                {
                    alreadyThere = true;
                    break;
                }
            }
            if (!alreadyThere)
            {
                bot.arrayTicket[i] = newRandomValue;
                i++;
            }
        }
        print(bot.arrayTicket[0] + " " + bot.arrayTicket[1] + " " + bot.arrayTicket[2] + " " + bot.arrayTicket[3] + " " + bot.arrayTicket[4] + " " + bot.arrayTicket[5] + " " + bot.arrayTicket[6] + " " + bot.arrayTicket[7] + " " + bot.arrayTicket[8] + " " + bot.arrayTicket[9]);
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
            yield return new WaitForSeconds(2f); // Задержка перед покупкой

            if (!isBusyCell[bot.arrayTicket[i]-1])
            {
                g.ConfirmLotteryItem(bot.arrayTicket[i], bot.name, bot.color); // подтверждаем покупку
                bot.leftCell--;
            }
            else
            {
                continue;
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
}
