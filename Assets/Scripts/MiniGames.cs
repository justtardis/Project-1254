using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGames : MonoBehaviour
{
    public AdManager ad;
    public bool one_two = false;
    // Крэш, игра №2
    public float time = 0;
    public float counter = 1f;
    public float stopCounter = 0;
    public float TimerRestart = 15f;
    public Text coef;
    public Text _timer;
    public Text cashSilver;
    public bool startCrash = false;
    public bool restart = false;
    public bool startGame = false;
    public int betCount = 0;
    public int autoCash = 0;
    public InputField betInp;
    public InputField AutoCashOutInp;
    public GameObject buttonStart;
    public float TotalCash = 0;
    public Color[] c = new Color[2];
    public Sprite[] but = new Sprite[2];
    public float[] history = new float[4];
    public GameObject groupHist;

    //Рулетка, игра №3
    public Color[] mainColor; // 0 - серый, 1 - красный, 2 - голубой и 3 - желтый
    public bool isRotate = false;
    public bool isRestart = false;
    public float speed = -90f;
    public float velocity = 3f;
    public GameObject roulette;
    public Image arrow;
    public int idElement = 0;
    public float timeRound = 0f;
    public Text TimerRound;
    public Text moneyT;
    public InputField betRoulette;
    public GameObject groupBut;
    int bet = 0;
    public int[] bets = new int[4];
    public int[] multiplay = new int[4] { 2, 3, 5, 50 };
    float[] alpha = new float[2] { 0.5f, 1f };
    Color col;
    public GameObject panel;
    public int[] historyColor = new int[16];
    public GameObject historyGroup;
    private int com = 0;

    public Sprite trueGet;
    public Sprite falseGet;
    public GameObject[] JacpotBlock;
    public Sprite[] money;
    public GameObject[] group;
    public Game g;
    public GameObject list;


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

    int index = 0;
    int count = 0;
    int silver = 0;
    int gold = 0;
    int rareCoins = 0;
    int caseId = 0;
    int itemId = 0;
    int rareTOP = 0;
    #region
    public int count_win = 0;

    public void CheckOpen(GameObject mini)
    {
        if (g._time == 4.2f)
        {
            mini.SetActive(true);
            list.SetActive(false);
        }
        else
        {

        }
    }

    public void BetInp()
    {
        if (betRoulette.text != "")
        {
            bet = int.Parse(betRoulette.text);
            if (bet > 500000 && g.silver > 500000) bet = 500000;
            else if (bet > g.silver) bet = g.silver;
            betRoulette.text = bet.ToString();
            if (bet == 0) betRoulette.text = string.Empty;
        }
    }

    public void ClickButton(int id)
    {
        if (timeRound <= 10f)
        {
            if (betRoulette.text != "" && g.silver >= bet)
            {

                bets[id - 1] += bet;
                col = groupBut.transform.GetChild(id - 1).GetChild(0).GetComponent<Text>().color;
                col.a = 1f;
                groupBut.transform.GetChild(id - 1).GetChild(1).GetComponent<Text>().color = col;
                groupBut.transform.GetChild(id - 1).GetChild(1).GetComponent<Text>().text = bets[id - 1].ToString();
                g.silver -= bet;
            }
        }
    }

    private void RouletteRules()
    {
        speed = Random.Range(-30f, -80f);
        timeRound = 15f;
        if (isRotate)
        {
            betRoulette.interactable = !isRotate;
            for (int i = 0; i < 4; i++)
            {
                groupBut.transform.GetChild(i).GetComponent<Button>().interactable = !isRotate;
                col = groupBut.transform.GetChild(i).GetChild(0).GetComponent<Text>().color;
                col.a = 0.5f;
                groupBut.transform.GetChild(i).GetChild(0).GetComponent<Text>().color = col;
                groupBut.transform.GetChild(i).GetChild(1).GetComponent<Text>().color = col;
            }
        }
    }

    void ShiftElementsColor(int element0)
    {
        int[] temp1 = new int[16];
        for (int i = 0; i < 16; i++) temp1[i] = historyColor[i];
        for (int i = 0; i < 15; i++) historyColor[i + 1] = temp1[i];
        historyColor[0] = element0;
        showColor();
    }

    private void showColor()
    {
        for (int i = 0; i < 16; i++)
        {
            historyGroup.transform.GetChild(i).GetComponent<Image>().color = mainColor[historyColor[i] - 1];
        }
    }

    void ShiftElements(float element0)
    {
        float[] temp = new float[4];
        for (int i = 0; i < 4; i++) temp[i] = history[i];
        for (int i = 0; i < 3; i++) history[i + 1] = temp[i];
        history[0] = element0;
        showHistory();
    }

    void showHistory()
    {
        for (int i = 0; i < 4; i++)
        {
            groupHist.transform.GetChild(i).GetComponent<Text>().text = "x" + history[i].ToString("0.00");
            if (history[i] < 2.00f) groupHist.transform.GetChild(i).GetComponent<Text>().color = new Color(212f, 0f, 0f);
            else if (history[i] > 2.00f && history[i] < 5.00f) groupHist.transform.GetChild(i).GetComponent<Text>().color = new Color(255f, 255f, 255f);
            else if (history[i] > 5.00f) groupHist.transform.GetChild(i).GetComponent<Text>().color = c[2];
        }
    }

    void CrashRules()
    {
        int rand = Random.Range(0, 1000);
        if (rand >= 0 && rand < 480) stopCounter = Random.Range(1f, 2f);
        if (rand >= 480 && rand < 630) stopCounter = Random.Range(1f, 2.5f);
        if (rand >= 630 && rand < 730) stopCounter = Random.Range(2.5f, 6f);
        if (rand >= 730 && rand < 820) stopCounter = Random.Range(6f, 15f);
        if (rand >= 820 && rand < 900) stopCounter = Random.Range(15f, 40f);
        if (rand >= 900 && rand < 950) stopCounter = Random.Range(40f, 80f);
        if (rand >= 950 && rand < 980) stopCounter = Random.Range(80f, 150f);
        if (rand >= 980 && rand < 995) stopCounter = Random.Range(150f, 230f);
        if (rand >= 995 && rand < 1000) stopCounter = 0f;
        coef.GetComponent<Text>().color = new Color(255f, 255f, 255f);
        TimerRestart = 15f;
        counter = 1f;
        time = 0f;
        _timer.text = string.Empty;
        cashSilver.text = g.convertMoney(g.silver);
        buttonStart.GetComponent<Image>().sprite = but[0]; // зеленый
        buttonStart.transform.GetChild(0).GetComponent<Text>().color = c[0];// зеленый
        buttonStart.transform.GetChild(0).GetComponent<Text>().text = LangSystem.lng.game2[4];// "Играть";
        buttonStart.GetComponent<Button>().interactable = !startGame;
        betInp.interactable = false; // выключаем все инпуты
        AutoCashOutInp.interactable = false;
    }

    public void AutoCash()
    {
        if (AutoCashOutInp.text != "")
        {
            autoCash = int.Parse(AutoCashOutInp.text);
            if (autoCash == 0 || autoCash < betCount) AutoCashOutInp.text = string.Empty;
        }
    }

    public void BetCorrect()
    {
        if (betInp.text != "")
        {
            betCount = int.Parse(betInp.text);
            if (betCount > 500000 && g.silver > 500000)
            {
                betCount = 500000;
            }
            else if (betCount > g.silver)
            {
                betCount = g.silver;
            }    
            betInp.text = betCount.ToString();
            if (betCount == 0) betInp.text = string.Empty;
            AutoCash();
        }
    }

    public void StartGame()
    {
        if (startCrash && !startGame)
        {
            buttonStart.GetComponent<Button>().interactable = false;
            buttonStart.GetComponent<Image>().sprite = but[1]; // серый
            buttonStart.transform.GetChild(0).GetComponent<Text>().color = c[1];
            betInp.interactable = true; // выключаем все инпуты
            AutoCashOutInp.interactable = true;
            buttonStart.transform.GetChild(0).GetComponent<Text>().text = LangSystem.lng.game2[3]; //"Ожидание нового раунда";
        }
        if (betCount != 0) // если ставка не нулевая
        {
            
                if (!startCrash) // и игра еще не началась
                {
                if (betCount <= g.silver)
                {
                    startGame = true;
                    g.silver -= betCount; // снимаем деньги сразу  
                    buttonStart.GetComponent<Image>().sprite = but[1];
                    buttonStart.transform.GetChild(0).GetComponent<Text>().color = c[1];
                    buttonStart.GetComponent<Button>().interactable = false; // выключаем кнопку
                    betInp.interactable = false; // выключаем все инпуты
                    AutoCashOutInp.interactable = false;
                }
                }
                else if (startGame)
                {
                    startGame = false;
                    buttonStart.GetComponent<Button>().interactable = false;
                    buttonStart.GetComponent<Image>().sprite = but[1];
                    buttonStart.transform.GetChild(0).GetComponent<Text>().color = c[1];
                    g.silver += (int)TotalCash;
                    if (ad.counter == 6)
                    {
                        if (!one_two)
                        {
                            ad.showInterstital();
                            ad.ReqInter();
                            ad.counter = 1;
                            one_two = true;
                        }
                        else
                        {
                            ad.VideoAds();
                            ad.counter = 1;
                            one_two = false;
                        }
                    }
                    else
                    {
                        ad.counter += 1;
                    }
                    startGame = false;
                }
        }
        else
        {
            g.noMoney.SetActive(true);
        }

    }

    public void ClickAd()
    {
        if (com != 1)
        {
            com += 1;
        }
    }

    public void Default()
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


    private void Update()
    {
        if (isRotate)
        {
            speed = Mathf.MoveTowards(speed, 0, velocity * Time.deltaTime);
            roulette.transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
            RaycastHit2D hit = Physics2D.Raycast(arrow.transform.position, Vector3.down);
            Debug.DrawRay(arrow.transform.position, Vector3.down);
            if (hit.collider != null)
            {
                idElement = hit.collider.gameObject.GetComponent<ColorElement>().Type;
                arrow.color = mainColor[idElement - 1];
                if (speed == 0)
                {
                    if (ad.counter == 6)
                    {
                        if (!one_two)
                        {
                            ad.showInterstital();
                            ad.ReqInter();
                            ad.counter = 1;
                            one_two = true;
                        }
                        else
                        {
                            ad.VideoAds();
                            ad.counter = 1;
                            one_two = false;
                        }
                    }
                    else
                    {
                        ad.counter += 1;
                    }
                    ShiftElementsColor(idElement);
                    isRotate = false;
                    isRestart = true;
                    groupBut.transform.GetChild(idElement - 1).GetComponent<Button>().interactable = !isRotate;
                    col = groupBut.transform.GetChild(idElement - 1).GetChild(0).GetComponent<Text>().color;
                    col.a = 1f;
                    groupBut.transform.GetChild(idElement - 1).GetChild(0).GetComponent<Text>().color = col;
                    if (bets[idElement - 1] != 0)
                    {
                        bets[idElement - 1] *= multiplay[idElement - 1];
                        col = groupBut.transform.GetChild(idElement - 1).GetChild(0).GetComponent<Text>().color;
                        col.a = 1f;
                        groupBut.transform.GetChild(idElement - 1).GetChild(0).GetComponent<Text>().color = col;
                        groupBut.transform.GetChild(idElement - 1).GetChild(1).GetComponent<Text>().text = bets[idElement - 1].ToString();
                        groupBut.transform.GetChild(idElement - 1).GetChild(1).GetComponent<Text>().color = col;
                        panel.SetActive(true);
                        panel.GetComponent<Image>().color = mainColor[idElement - 1];
                        panel.transform.GetChild(0).GetComponent<Text>().text = g.convertMoney(bets[idElement - 1]);
                        g.silver += bets[idElement - 1];
                    }
                }
            }
        }
        if (isRestart)
        {
            timeRound -= Time.deltaTime;
            if (timeRound < 10f)
            {
                betRoulette.interactable = !isRotate;
                for (int i = 0; i < 4; i++)
                {
                    groupBut.transform.GetChild(i).GetComponent<Button>().interactable = !isRotate;
                    col = groupBut.transform.GetChild(i).GetChild(0).GetComponent<Text>().color;
                    col.a = 1f;
                    groupBut.transform.GetChild(i).GetChild(0).GetComponent<Text>().color = col;
                }
                panel.SetActive(false);
                TimerRound.text = timeRound.ToString("0.0") + "с";
            }
            if (timeRound < 10f && timeRound > 9.8f)
            {
                for (int i = 0; i < 4; i++)
                {
                    groupBut.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = string.Empty;
                    bets[i] = 0;
                }
            }
            if (timeRound <= 0f)
            {
                TimerRound.text = string.Empty;
                isRestart = false;
                isRotate = true;
                RouletteRules();

            }
        }

        if (startCrash)
        {
            if (!startGame)
            {
                buttonStart.GetComponent<Button>().interactable = false;
                buttonStart.GetComponent<Image>().sprite = but[1]; // серый
                buttonStart.transform.GetChild(0).GetComponent<Text>().color = c[1];
                buttonStart.transform.GetChild(0).GetComponent<Text>().text = LangSystem.lng.game2[3]; //"Ожидание нового раунда";
            }
            if (counter < stopCounter)
            {
                counter = (0.09f * time) * (0.09f * time) + 1;
                time += Time.deltaTime;
                if (startGame)
                {
                    TotalCash = betCount * counter;
                    buttonStart.GetComponent<Image>().sprite = but[0]; // зеленый
                    buttonStart.GetComponent<Button>().interactable = true;
                    buttonStart.transform.GetChild(0).GetComponent<Text>().color = c[0];// зеленый
                    buttonStart.transform.GetChild(0).GetComponent<Text>().text = g.convertMoneyFloat(TotalCash);
                }
                if (autoCash != 0)
                {
                    if (TotalCash >= autoCash && startGame)
                    {
                        startGame = false;
                        buttonStart.GetComponent<Button>().interactable = false;
                        buttonStart.GetComponent<Image>().sprite = but[1];
                        buttonStart.transform.GetChild(0).GetComponent<Text>().color = c[1];
                        buttonStart.transform.GetChild(0).GetComponent<Text>().text = g.convertMoneyFloat(autoCash);
                        g.silver += autoCash;
                    }
                }
            }
            else
            {
                ShiftElements(counter);
                startCrash = false;
                coef.GetComponent<Text>().color = new Color(212f, 0f, 0f);
                startGame = false;
                restart = true;
                buttonStart.GetComponent<Button>().interactable = true;
                buttonStart.GetComponent<Image>().sprite = but[0];
                buttonStart.transform.GetChild(0).GetComponent<Text>().color = c[0];// зеленый
                buttonStart.transform.GetChild(0).GetComponent<Text>().text = LangSystem.lng.game2[4];// "Играть";
            }
            coef.text = "x" + (counter).ToString("#.#0");
        }
        if (restart)
        {

            betInp.interactable = true; // выключаем все инпуты
            AutoCashOutInp.interactable = true;
            TimerRestart -= Time.deltaTime;
            if (TimerRestart < 10f)
            {
                coef.text = string.Empty;
                _timer.text = LangSystem.lng.game2[0] + /* "Следующий раунд через "*/ TimerRestart.ToString("0.0") + LangSystem.lng.game2[7];
                //buttonStart.GetComponent<Button>().interactable = true;
                //buttonStart.GetComponent<Image>().sprite = but[0];
                //buttonStart.transform.GetChild(0).GetComponent<Text>().color = c[0];// зеленый
                buttonStart.transform.GetChild(0).GetComponent<Text>().text = LangSystem.lng.game2[4];// "Играть";
            }
            if (TimerRestart <= 0f)
            {
                _timer.text = string.Empty;
                restart = false;
                CrashRules();
                startCrash = true;
            }
        }
        cashSilver.text = g.convertMoney(g.silver);
        moneyT.text = g.convertMoney(g.silver);
    }


    private void NonPlayable(int count_w, string word)
    {
        switch (word)
        {
            case "coins":
                if (count_w == 0)
                {
                    rareCoins = Random.Range(0, 100);
                    if (rareCoins <= 3)
                    {
                        silver = Random.Range(12, 1500);
                        gold = Random.Range(2, 10);
                    }
                    else if (rareCoins > 3 && rareCoins <= 100)
                    {
                        silver = Random.Range(30000, 50000);
                        gold = Random.Range(25, 50);
                    }
                }
                if (count_w == 1)
                {
                    rareCoins = Random.Range(0, 100);
                    if (rareCoins <= 25)
                    {
                        silver = Random.Range(50, 2000);
                        gold = Random.Range(5, 15);
                    }
                    else if (rareCoins > 25 && rareCoins <= 100)
                    {
                        silver = Random.Range(24000, 28000);
                        gold = Random.Range(40, 70);
                    }
                }
                if (count_w == 2)
                {
                    rareCoins = Random.Range(0, 100);
                    if (rareCoins <= 60)
                    {
                        silver = Random.Range(50, 2000);
                        gold = Random.Range(5, 15);
                    }
                    else if (rareCoins > 60 && rareCoins <= 100)
                    {
                        silver = Random.Range(5800, 7000);
                        gold = Random.Range(50, 150);
                    }
                }
                if (count_w == 3)
                {
                    rareCoins = Random.Range(0, 100);
                    if (rareCoins <= 50)
                    {
                        silver = Random.Range(50, 2000);
                        gold = Random.Range(5, 15);
                    }
                    else if (rareCoins > 50 && rareCoins <= 100)
                    {
                        silver = Random.Range(500, 2000);
                        gold = Random.Range(70, 150);
                    }
                }
                if (count_w == 4)
                {
                    rareCoins = Random.Range(0, 100);
                    if (rareCoins <= 3)
                    {
                        silver = Random.Range(50, 2000);
                        gold = Random.Range(5, 15);
                    }
                    else if (rareCoins > 3 && rareCoins <= 100)
                    {
                        silver = Random.Range(17000, 25000);
                        gold = Random.Range(1500, 5000);
                    }
                }
                if (count_w == 5)
                {
                    rareCoins = Random.Range(0, 100);
                    if (rareCoins <= 90)
                    {
                        silver = Random.Range(12, 1500);
                        gold = Random.Range(2, 10);
                    }
                    else if (rareCoins > 90 && rareCoins <= 100)
                    {
                        silver = Random.Range(10000, 40000);
                        gold = Random.Range(20, 50);
                    }
                }
                if (count_w > 5)
                {
                    //countWin = Random.Range(1, 4);
                    rareCoins = Random.Range(0, 100);
                    if (rareCoins <= 90)
                    {
                        silver = Random.Range(12, 1500);
                        gold = Random.Range(2, 10);
                    }
                    else if (rareCoins > 90 && rareCoins <= 100)
                    {
                        silver = Random.Range(10000, 40000);
                        gold = Random.Range(20, 50);
                    }
                }
                break;
            case "item":
                if (count_w == 0)
                {
                    caseId = Random.Range(0, g.cases.Length);
                    rareTOP = Random.Range(0, 100);
                    if (rareTOP >= 0 && rareTOP <= 3)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group < 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                    else if (rareTOP > 3 && rareTOP < 100)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group != 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                }
                if (count_w == 1)
                {

                    caseId = Random.Range(0, g.cases.Length);
                    rareTOP = Random.Range(0, 100);
                    if (rareTOP >= 0 && rareTOP <= 25)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group < 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                    else if (rareTOP > 25 && rareTOP < 100)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group != 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                }
                if (count_w == 2)
                {
                    caseId = Random.Range(0, g.cases.Length);
                    rareTOP = Random.Range(0, 100);
                    if (rareTOP >= 0 && rareTOP <= 60)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group < 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                    else if (rareTOP > 60 && rareTOP < 100)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group != 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                }
                if (count_w == 3)
                {
                    caseId = Random.Range(0, g.cases.Length);
                    rareTOP = Random.Range(0, 100);
                    if (rareTOP >= 0 && rareTOP <= 50)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group < 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                    else if (rareTOP > 50 && rareTOP < 100)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group != 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                }
                if (count_w == 4)
                {
                    caseId = Random.Range(0, g.cases.Length);
                    rareTOP = Random.Range(0, 100);
                    if (rareTOP >= 0 && rareTOP <= 3)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group < 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                    else if (rareTOP > 3 && rareTOP < 100)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group != 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                }
                if (count_w == 5)
                {
                    caseId = Random.Range(0, g.cases.Length);
                    rareTOP = Random.Range(0, 100);
                    if (rareTOP >= 0 && rareTOP <= 80)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group < 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                    else if (rareTOP > 80 && rareTOP < 100)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group != 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                }
                if (count_w > 5)
                {
                    caseId = Random.Range(0, g.cases.Length);
                    rareTOP = Random.Range(0, 100);
                    if (rareTOP >= 0 && rareTOP <= 70)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group < 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }
                    else if (rareTOP > 70 && rareTOP < 100)
                    {
                        int i = 0;
                        while (i < g.cases[caseId].items.Length && g.cases[caseId].items[i].group != 4)
                        {
                            i++;
                        }
                        itemId = Random.Range(i, g.cases[caseId].items.Length);
                    }

                }
                break;
            case "STOLB":
                if (count_w == 0)
                {
                    countWin = 3;
                    winOrLose = 2;
                }
                if (count_w == 1)
                {
                    countWin = 2;
                    winOrLose = 2;
                }
                if (count_w == 2)
                {
                    countWin = 1;
                    winOrLose = 2;
                }
                if (count_w == 3)
                {
                    countWin = 1;
                    winOrLose = 2;
                }
                if (count_w == 4)
                {
                    countWin = 0;
                    winOrLose = 1;
                }
                if (count_w == 5)
                {
                    countWin = 1;
                    winOrLose = 2;
                }
                if (count_w > 5)
                {

                    winOrLose = Random.Range(1, 3);
                    if (winOrLose == 2)
                    {
                        countWin = Random.Range(1, 4);
                    }
                    else
                        countWin = 0;
                }
                break;
        }
    }
    #endregion

    private string conversionFunction(int number)
    {
        string converted = number.ToString("#,###,##0");
        converted = converted.Replace(",", " ");
        return converted;
    }

    public void BuyGame()
    {
        if (price < g.silver)
        {
            g.silver = g.silver - price;
            group[2].SetActive(false);
            NonPlayable(count_win, "STOLB");
            Raund();
            for (int i = 0; i < 6; i++)
            {
                button[i].GetComponent<Button>().interactable = true;
                button[i].GetComponent<Animator>().SetBool("Click", false);
                check[i] = false;
            }
        }
        else
            g.noMoney.SetActive(true);
    }

    public void RestartGame()
    {
        count_win += 1;
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
                NonPlayable(count_win, "coins");
                //silver = Random.Range(50, 40000);
                JacpotBlock[id].transform.GetChild(1).gameObject.SetActive(false);
                JacpotBlock[id].transform.GetChild(4).gameObject.SetActive(true);
                JacpotBlock[id].transform.GetChild(5).gameObject.SetActive(true);
                JacpotBlock[id].transform.GetChild(5).GetComponent<Rev>().rev = 1;
                JacpotBlock[id].transform.GetChild(4).GetComponent<Rev>().rev = 1;
                JacpotBlock[id].transform.GetChild(4).GetComponent<Image>().sprite = money[0];
                JacpotBlock[id].transform.GetChild(5).GetComponent<Text>().text = conversionFunction(silver);
                if (isWin)
                {
                    g.silver = g.silver + silver;
                }
                break;
            case 2:
                NonPlayable(count_win, "coins");
                //gold = Random.Range(5, 250);
                JacpotBlock[id].transform.GetChild(1).gameObject.SetActive(false);
                JacpotBlock[id].transform.GetChild(4).gameObject.SetActive(true);
                JacpotBlock[id].transform.GetChild(5).gameObject.SetActive(true);
                JacpotBlock[id].transform.GetChild(5).GetComponent<Rev>().rev = 1;
                JacpotBlock[id].transform.GetChild(4).GetComponent<Rev>().rev = 1;
                JacpotBlock[id].transform.GetChild(4).GetComponent<Image>().sprite = money[1];
                JacpotBlock[id].transform.GetChild(5).GetComponent<Text>().text = conversionFunction(gold);
                if (isWin)
                {
                    g.gold = g.gold + gold;
                }
                break;
            case 3:
                NonPlayable(count_win, "item");
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
                group[0].transform.GetChild(0).GetComponent<Text>().text = LangSystem.lng.game1[4];// "Поздравляем!";

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
        showHistory();
        CrashRules();
        startCrash = true;
        Raund();
        showColor();
        isRotate = true;
        RouletteRules();


        priceText.text = conversionFunction(price);
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
        //winOrLose = Random.Range(1, 3);

        if (winOrLose == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                firstString[i] = Random.Range(2, 9);
                secondString[i] = Random.Range(1, firstString[i]);
                group[0].transform.GetChild(0).GetComponent<Text>().text = LangSystem.lng.game1[3];//"К сожалению, вы ничего не выиграли";
            }
        }
        // победа
        if (winOrLose == 2)
        {
            // число выигрышных столбцов
            //countWin = Random.Range(1, 4);

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
        if (ad.counter == 6)
        {
            if (!one_two)
            {
                ad.showInterstital();
                ad.ReqInter();
                ad.counter = 1;
                one_two = true;
            }
            else
            {
                ad.VideoAds();
                ad.counter = 1;
                one_two = false;
            }
        }
        else
        {
            ad.counter += 1;
        }
    }
}
