using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour
{

    public Game g;
    public Inventory inv;
    public GameObject scrollCont;
    public bool isOpened;
    public float speed = -20f;
    public float velocity = 3f;
    public GameObject resPanel;
    public GameObject progressPanel;
    public int resItem;
    public int caseID;
    public float allTime;
    public float timeLeft;
    public Text timer;
    public Text progressCounter;
    public Image progressFill;
    private bool wasPlayed = false;
    private bool wasPlayedDrop = false;
    private string index;
    // Use this for initialization
    void Start()
    {
        g._as = gameObject.GetComponent<AudioSource>();
        //speed = -14.2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOpened)
        {
            speed = Mathf.MoveTowards(speed, 0, velocity * Time.deltaTime);
            scrollCont.transform.Translate(new Vector2(speed, 0) * Time.deltaTime); //явно указал контейнер с объектами, а не gameobject
            timeLeft = speed * (-1) / velocity;
            timer.text = "00:" + timeLeft.ToString("00.00#");
            progressCounter.text = (100 - (int)((timeLeft / allTime) * 100)) + "%";
            progressFill.fillAmount = 1 - timeLeft / allTime;
            RaycastHit2D hit = Physics2D.Raycast(Vector2.down, Vector2.up);
            Debug.DrawLine(Vector2.down, Vector2.up);
            if (hit.collider != null)
            {
                if (speed == 0 && progressPanel.activeSelf)
                {
                    resItem = hit.collider.gameObject.GetComponent<Item_ID>().id;
                    ShowResult();
                    progressPanel.SetActive(false);
                    resPanel.SetActive(true);
                    isOpened = false;
                    g._as.PlayOneShot(g.ac[1]);

                }
                else if (!wasPlayed)
                {
                    g._as.PlayOneShot(g.ac[0]);
                    index = hit.collider.gameObject.name;
                    wasPlayed = true;
                }
                if (index != hit.collider.gameObject.name)
                {
                    wasPlayed = false;
                }
            }
            else if (speed == 0)
            {
                speed = Mathf.MoveTowards(speed, -5f, velocity * Time.deltaTime);
            }
        }

    }

    public void ShowResult()
    {
        resPanel.transform.GetChild(3).GetComponent<Text>().text = g.cases[caseID].items[resItem].name.ToUpper(); //Добавил toUpper для верхнего регистра
        resPanel.transform.GetChild(6).GetChild(0).GetComponent<Text>().text = g.convertMoneyFloat(g.cases[caseID].items[resItem].price);
        resPanel.transform.GetChild(2).GetComponent<Image>().sprite = g.cases[caseID].items[resItem].picture;
        resPanel.transform.GetChild(7).gameObject.SetActive(g.cases[caseID].items[resItem].group == 4);
    }

    public void addToInventory()
    {
        inv.items[inv.invSize][1] = resItem;
        inv.items[inv.invSize][0] = caseID;
        inv.invSize++;
        inv.LoadInventory();
        g.roulett.SetActive(false);
        g.Get = true;
        g.Preloader(g.Panels[0]); // Переход на главную
        g.menu.interactable = true;
        //inv.invPanel.transform.parent.parent.gameObject.SetActive(true);
    }

    public void sellItem()
    {
        g.menu.interactable = true;
        g.silver = g.silver + (int)g.cases[caseID].items[resItem].price;
        g.roulett.SetActive(false);
        g.Get = false;
        g.OpenPreview(caseID);
       
        //
        //inv.invPanel.transform.parent.parent.gameObject.SetActive(true);
    }

    public void OpenCase(int id)
    {

        //Я зарандомил значения скорости и торможения, чтобы игра была чуть интересней.
        int rnd = Random.Range(1, 3);
        if (rnd == 1)
        {
            speed = -14.2f;
            velocity = Random.Range(1.98f, 2.02f);
        }
        else
        {
            speed = -20f;
            velocity = Random.Range(3.94f, 3.96f);
        }
        scrollCont.transform.localPosition = new Vector2(4799f, 0f);
        caseID = id;
        progressPanel.SetActive(true);
        resPanel.SetActive(false);
        g.preview.SetActive(false);

        int count1 = 0;
        int count2 = 0;
        int count3 = 0;
        int count4 = 0;
        //Считаем сколько товаров каждой группы
        for (int j = 0; j < g.cases[id].items.Length; j++)
        {
            switch (g.cases[id].items[j].group)
            {
                case 1:
                    count1++;
                    break;
                case 2:
                    count2++;
                    break;
                case 3:
                    count3++;
                    break;
                case 4:
                    count4++;
                    break;
                default:
                    break;
            }
        }
        //Добавление шанса, если куплен
        int chance = 0;
        int[] c = { 0, 0, 0 }; // если будет изменение шанса, то это изменение для первого и т д
        switch (g.id_toggle)
        {
            case 1:
                chance = 50;
                break;
            case 2:
                chance = 100;
                break;
            case 3:
                chance = 150;
                break;
            default:
                break;
        }
        if (g.id_toggle != -1)
        {
            int temp = chance / 3;
            c[2] = chance - temp * 2;
            c[1] = temp;
            c[0] = temp;
            for (int j = 0; j < 3; j++)
            {
                if (g.cases[id].groups[j] == 0)
                {
                    int k = 2;
                    bool t = false; // скинули ли кому-то вероятность
                    while (k > -1 && !t)
                    {
                        if (k != j && g.cases[id].groups[k] != 0)
                        {
                            c[k] = c[k] + c[j];
                            c[j] = 0;
                            t = true;
                        }
                        else
                        {
                            k--;
                        }
                    }
                }
            }
        }
        //Отображаем товары
        for (int i = 0; i < 30; i++)
        {
            int rand = Random.Range(0, 1000);
            int itemID = 0;
            if (rand < g.cases[id].groups[0] - c[1])
            {
                itemID = Random.Range(0, count1);
            }
            else if (rand < (g.cases[id].groups[0] + g.cases[id].groups[1]) - (c[1] + c[2]))
            {
                itemID = Random.Range(count1, count1 + count2);
            }
            else if (rand < (g.cases[id].groups[0] + g.cases[id].groups[1] + g.cases[id].groups[2] - chance))
            {
                itemID = Random.Range(count1 + count2, count1 + count2 + count3);
            }
            else
            {
                itemID = Random.Range(count1 + count2 + count3, count1 + count2 + count3 + count4);
            }
            GameObject A = scrollCont.transform.GetChild(i).gameObject;
            A.transform.GetComponent<Image>().sprite = g.cases[id].items[itemID].picture;
            A.transform.GetComponent<Item_ID>().id = g.cases[id].items[itemID].id;
            //A.transform.GetChild(1).GetComponent<Image>().sprite = g.cases[id].items[i].picture;
            A.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = LangSystem.lng.roulette[2];
            A.transform.GetChild(0).gameObject.SetActive(g.cases[id].items[itemID].group == 4);
            A.name = i.ToString();
            //A.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenPreview);
        }
        /*g.preview.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "$" + cases[id].price;
        g.preview.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "КЕЙС\n\"" + cases[id].name + "\"";*/

        timeLeft = speed * (-1) / velocity;
        allTime = timeLeft;
        isOpened = true;
        g.roulett.SetActive(true);
    }
}
