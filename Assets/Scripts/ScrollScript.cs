using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour {

    public Game g;
    public GameObject scrollCont;
    public bool isOpened;
    public float speed = -20;
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
    // Use this for initialization
    void Start () {
        speed = -10;
	}
	
	// Update is called once per frame
	void Update () {
		if (isOpened)
        {
            speed = Mathf.MoveTowards(speed, 0, velocity * Time.deltaTime);
            gameObject.transform.Translate(new Vector2(speed, 0) * Time.deltaTime);
            timeLeft = speed * (-1) / velocity;
            timer.text = "00:" + timeLeft.ToString("00.00#");
            progressCounter.text = (100-(int)((timeLeft / allTime) * 100)) + "%";
            progressFill.fillAmount = 1 - timeLeft / allTime;
            RaycastHit2D hit = Physics2D.Raycast(Vector2.down, Vector2.up);
            if (hit.collider != null)
            {
                if (speed == 0 && progressPanel.activeSelf)
                {
                    resItem = hit.collider.gameObject.GetComponent<Item_ID>().id;
                    ShowResult();
                    progressPanel.SetActive(false);
                    resPanel.SetActive(true);
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
        resPanel.transform.GetChild(3).GetComponent<Text>().text = g.cases[caseID].items[resItem].name;
        resPanel.transform.GetChild(6).GetChild(0).GetComponent<Text>().text = g.cases[caseID].items[resItem].price.ToString();
        resPanel.transform.GetChild(2).GetComponent<Image>().sprite = g.cases[caseID].items[resItem].picture;
        resPanel.transform.GetChild(7).gameObject.SetActive(g.cases[caseID].items[resItem].group == 4);
    }

    public void OpenCase(int id)
    {
        caseID = id;
        speed = -10;
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
        //Отображаем товары
        for (int i = 0; i < 30; i++)
        {
            int rand = Random.Range(0, 1000);
            int itemID = 0;
            if (rand < g.cases[id].groups[0])
            {
                itemID = Random.Range(0, count1);
            }
            else if (rand < (g.cases[id].groups[0] + g.cases[id].groups[1]))
            {
                itemID = Random.Range(count1, count1 + count2);
            }
            else if (rand < (g.cases[id].groups[0] + g.cases[id].groups[1] + g.cases[id].groups[2]))
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
            A.transform.GetChild(0).gameObject.SetActive(g.cases[id].items[itemID].group == 4);
            //A.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenPreview);
        }
        /*g.preview.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "$" + cases[id].price;
        g.preview.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "КЕЙС\n\"" + cases[id].name + "\"";*/
        velocity = Random.Range(1.1f, 1.5f);
        timeLeft = speed * (-1) / velocity;
        allTime = timeLeft;
        isOpened = true;
        g.roulett.SetActive(true);
    }
}
