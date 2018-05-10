using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGames : MonoBehaviour
{
    public Sprite trueGet;
    public Sprite falseGet;
    public GameObject[] JacpotBlock;
    public Sprite[] money;
    public GameObject[] group;
    public Game g;

    public GameObject[] button;
    public Sprite defIc;
    private int price = 2000;
    public Text priceText;

    public int winOrLose = 0;  // 1 - игрок НЕ выигрывает, 2 - выигрывает
    public int countWin = 0; // 1 - выигрывает 1 поле, 2 - 2 поля, 3 - все 3.
    public int typeOfPrise = 0;  // 1 - серебро, 2 - золото, 3 - предмет топ
    public int[] firstString = new int[3] { 9, 0, 0 };
    public int[] secondString = new int[3] { 0, 0, 0 };
    int stolb;
    int[] arr = new int[2];
    public bool[] check = new bool[6];
    public GameObject[] Lep;
    int index = 0;
    int count = 0;

    public void BuyGame()
    {
        g.silver = g.silver - price;
        group[2].SetActive(false);
        for (int i = 0; i < 6; i++)
        {
            button[i].GetComponent<Button>().interactable = true;
            button[i].GetComponent<Animator>().SetBool("Click", false);
            check[i] = false;
        }
    }

    public void RestartGame()
    {
        count = 0;
        for (int i = 0; i < 6; i++)
        {
            //button[i].GetComponent<Button>().interactable = fa;
            button[i].GetComponent<Animator>().SetBool("Click", false);
            check[i] = false;
        }
        group[0].GetComponent<Animator>().SetBool("AnimTrigger", true);
        group[0].GetComponent<Rev>().rev = 2;
        for (int j = 0; j < 3; j++)
        {
            JacpotBlock[j].transform.GetChild(2).GetComponent<Animator>().SetBool("AnimTrigger", true);
            JacpotBlock[j].transform.GetChild(2).GetComponent<Rev>().rev = 2;
            JacpotBlock[j].transform.GetChild(3).GetComponent<Animator>().SetBool("AnimTrigger", true);
            JacpotBlock[j].transform.GetChild(3).GetComponent<Rev>().rev = 2;
            JacpotBlock[j].transform.GetChild(4).GetComponent<Animator>().SetBool("AnimTrigger", true);
            JacpotBlock[j].transform.GetChild(4).GetComponent<Rev>().rev = 2;
            JacpotBlock[j].transform.GetChild(5).GetComponent<Animator>().SetBool("AnimTrigger", true);
            JacpotBlock[j].transform.GetChild(5).GetComponent<Rev>().rev = 2;
        }
    }

    void Prise(int id, Sprite spr, bool isWin)
    {
        JacpotBlock[id].transform.GetChild(3).gameObject.SetActive(true);
        JacpotBlock[id].transform.GetChild(3).GetComponent<Rev>().rev = 1;
        JacpotBlock[id].transform.GetChild(3).GetComponent<Image>().sprite = spr;
       // JacpotBlock[id].transform.GetChild(3).GetComponent<Animator>().SetBool("isFalse", false);
        typeOfPrise = Random.Range(1, 4);
        switch (typeOfPrise)
        {
            case 1:
                int silver = Random.Range(50, 40000);

                JacpotBlock[id].transform.GetChild(1).gameObject.SetActive(false);
                JacpotBlock[id].transform.GetChild(4).gameObject.SetActive(true);
                JacpotBlock[id].transform.GetChild(5).gameObject.SetActive(true);
                JacpotBlock[id].transform.GetChild(5).GetComponent<Rev>().rev = 1;
                JacpotBlock[id].transform.GetChild(4).GetComponent<Rev>().rev = 1;
                JacpotBlock[id].transform.GetChild(4).GetComponent<Image>().sprite = money[0];
                JacpotBlock[id].transform.GetChild(5).GetComponent<Text>().text = silver.ToString();
                if (isWin)
                {
                    g.silver = g.silver + silver;
                }
                break;
            case 2:
                int gold = Random.Range(5, 250);
                JacpotBlock[id].transform.GetChild(1).gameObject.SetActive(false);
                JacpotBlock[id].transform.GetChild(4).gameObject.SetActive(true);
                JacpotBlock[id].transform.GetChild(5).gameObject.SetActive(true);
                JacpotBlock[id].transform.GetChild(5).GetComponent<Rev>().rev = 1;
                JacpotBlock[id].transform.GetChild(4).GetComponent<Rev>().rev = 1;
                JacpotBlock[id].transform.GetChild(4).GetComponent<Image>().sprite = money[1];
                JacpotBlock[id].transform.GetChild(5).GetComponent<Text>().text = gold.ToString();
                if (isWin)
                {
                    g.gold = g.gold + gold;
                }
                break;
            case 3:
                int caseId = Random.Range(0, g.cases.Length);
                int i = 0;
                while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group != 4)
                {
                    i++;
                }
                int itemId = Random.Range(i, g.cases[caseId].items.Length);
                JacpotBlock[id].transform.GetChild(1).gameObject.SetActive(false);
                JacpotBlock[id].transform.GetChild(2).gameObject.SetActive(true);
                JacpotBlock[id].transform.GetChild(2).GetComponent<Rev>().rev = 1;
                JacpotBlock[id].transform.GetChild(2).GetComponent<Image>().sprite = g.cases[caseId].items[itemId].picture;
                if (isWin)
                {
                    Inventory inv = g.gameObject.transform.GetComponent<Inventory>();
                    inv.items[inv.invSize][1] = itemId;
                    inv.items[inv.invSize][0] = caseId;
                    inv.invSize++;
                    inv.LoadInventory();
                }
                break;
        }
    }


    public void clickCircle(GameObject thisObj)
    {
        thisObj.GetComponent<idMini>().opened = true;
        int id = thisObj.GetComponent<idMini>().id;
        thisObj.GetComponent<Animator>().SetBool("Click", true);
        thisObj.GetComponent<Button>().interactable = false;
        if (check[id])
        {
            count += 1;
            if (count >= 3)
            {
                group[0].SetActive(true);
                group[1].SetActive(false);
                group[0].GetComponent<Rev>().rev = 1;
            }
            if (secondString[id] > firstString[id])
            {
                //Lep[id].SetActive(true);
                //Lep[id + 3].SetActive(true);
                Prise(id, trueGet, true);
                group[0].transform.GetChild(0).GetComponent<Text>().text = "Поздравляем!";
               
            }
            else Prise(id, falseGet, false);
        }
        else
        {
            check[id] = thisObj.GetComponent<idMini>().opened;
        }
    }

    private void Start()
    {
        Raund();
    }

    void UnicRand() // Для генерации рандомных позиций целей
    {
        bool alreadyThere;
        for (int i = 0; i < 2;)
        {
            alreadyThere = false;
            int newRandomValue = Random.Range(0, 3);
            for (int j = 0; j < i; j++)
            {
                if (arr[j] == newRandomValue)
                {
                    alreadyThere = true;
                    break;
                }
            }
            if (!alreadyThere)
            {
                arr[i] = newRandomValue;
                i++;
            }
        }
    }

    public void Raund()
    {
        for (int i = 0; i < 6; i++)
        {
            button[i].GetComponent<Button>().interactable = false;
        }
        winOrLose = Random.Range(1, 3);
        if (winOrLose == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                firstString[i] = Random.Range(2, 9);
                secondString[i] = Random.Range(1, firstString[i]);
                group[0].transform.GetChild(0).GetComponent<Text>().text = "К сожалению, вы ничего не выиграли";
            }
        }
        // победа
        if (winOrLose == 2)
        {
            // число выигрышных столбцов
            countWin = Random.Range(1, 4);
            countWin = 2;
            switch (countWin)
            {
                case 1:
                    stolb = Random.Range(0, 3);
                    for (int i = 0; i < 3; i++)
                    {
                        firstString[i] = Random.Range(2, 10);
                        secondString[i] = Random.Range(1, firstString[i]);
                    }
                    // Выигрышный столбец
                    firstString[stolb] = Random.Range(1, 8);
                    secondString[stolb] = Random.Range(firstString[stolb] + 1, 10);
                    print("win  1");
                    break;
                case 2:

                    for (int i = 0; i < 3; i++)
                    {
                        firstString[i] = Random.Range(2, 10);
                        secondString[i] = Random.Range(1, firstString[i]);
                    }
                    UnicRand();

                    for (int j = 0; j < 2; j++)
                    {
                        firstString[arr[j]] = Random.Range(1, 8);
                        secondString[arr[j]] = Random.Range(firstString[arr[j]] + 1, 10);
                        print("win  2");
                    }
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        firstString[i] = Random.Range(1, 10);
                        secondString[i] = Random.Range(firstString[i] + 1, 10);
                        print("win 3");
                    }
                    break;
            }
        }
    }
}
