using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public int[][] items; // первое число - id кейса, второе - id товара
    public int invSize = 0; // количество предметов в инвентаре
    public Save sv;
    public Game g;
    public GameObject invPanel; // инвентарь
    public GameObject itemPref; // префаб товара в инвентаре
    public bool isMult; // включена ли множественная продажа
    public Sprite itemPrev;
    public Sprite emptyPrev;
    public GameObject prevPanel; // окно предпросмотра перед продажей
    public GameObject sellPanel; // кнопка множественной продажи
    public GameObject multSell; // подтверждение множественной продажи
    public GameObject moneyPanel; // деньхи
    public int multSum = 0; // сколько денех стоят отмеченные товары 

    private void Awake()
    {
        string mydocpath = Directory.GetCurrentDirectory();
        try
        {
            StreamReader sr = new StreamReader(mydocpath + @"\Result.txt");
            string line = sr.ReadLine();
            sv = JsonUtility.FromJson<Save>(line);
            invSize = sv.invSize;
            items = new int[1000][];
            for (int i = 0; i < 1000; i++)
            {
                items[i] = new int[2];
            }
            if (invSize != 0)
            {
                for (int j = 0; j < invSize; j++)
                {
                    items[j] = JsonHelper.FromJson<int>(sv.items[j]);
                }
                
            }
            g.gold = sv.gold;
            g.silver = sv.silver;
        }
        catch (System.Exception e)
        {
            Debug.Log("Exception: " + e.Message);
            items = new int[1000][];
            for (int i = 0; i < 1000; i++)
            {
                items[i] = new int[2];
            }
        }
        LoadInventory();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moneyPanel.transform.GetChild(1).GetComponent<Text>().text = g.convertMoney(g.silver);
    }

    public void LoadInventory()
    {
        for (int i = 0; i < invSize; i++)
        {
            if (i < 100)
            {
                GameObject A = invPanel.transform.GetChild(i).gameObject;
                A.transform.GetChild(0).GetComponent<Text>().text = g.cases[items[i][0]].items[items[i][1]].price.ToString();
                A.transform.GetChild(1).GetComponent<Image>().sprite = g.cases[items[i][0]].items[items[i][1]].picture;
                A.transform.GetComponent<Image>().sprite = itemPrev;
                A.transform.GetChild(2).gameObject.SetActive(g.cases[items[i][0]].items[items[i][1]].group == 4);
                A.transform.GetChild(3).gameObject.SetActive(false);
                A.GetComponent<Item_ID>().id = i;
                A.transform.GetChild(4).gameObject.SetActive(true);
                A.transform.GetChild(0).gameObject.SetActive(true);
                A.transform.GetChild(1).gameObject.SetActive(true);
                //A.transform.GetChild(2).gameObject.SetActive(true);
                A.SetActive(true);
            }
            else
            {
                GameObject A = Instantiate(itemPref, itemPref.transform.position = new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                A.transform.SetParent(invPanel.transform, false);
                A.transform.GetChild(0).GetComponent<Text>().text = g.cases[items[i][0]].items[items[i][1]].price.ToString();
                A.transform.GetChild(1).GetComponent<Image>().sprite = g.cases[items[i][0]].items[items[i][1]].picture;
                A.transform.GetComponent<Image>().sprite = itemPrev;
                A.transform.GetChild(2).gameObject.SetActive(g.cases[items[i][0]].items[items[i][1]].group == 4);
                A.GetComponent<Item_ID>().id = i;
                A.transform.GetChild(4).gameObject.SetActive(true);
                A.transform.GetChild(3).gameObject.SetActive(false);
                A.transform.GetChild(0).gameObject.SetActive(true);
                A.transform.GetChild(1).gameObject.SetActive(true);
                //A.transform.GetChild(2).gameObject.SetActive(true);
                A.SetActive(true);
            }
            //A.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenPreview);
        }
        addEmpty();

    }

    //функция добавления пустых ячеек
    public void addEmpty()
    {
        int addSize = 3;
        if (invSize < 9)
        {
            addSize = 12 - invSize;
        }
        else
        {
            if (invSize % 3 != 0)
            {
                addSize = 6 - invSize % 3;
            }
        }
        for (int i = invSize; i < invSize + addSize; i++)
        {
            if (i < 100)
            {
                GameObject A = invPanel.transform.GetChild(i).gameObject;
                A.transform.GetComponent<Image>().sprite = emptyPrev;
                A.transform.GetChild(0).gameObject.SetActive(false);
                A.transform.GetChild(1).gameObject.SetActive(false);
                A.transform.GetChild(2).gameObject.SetActive(false);
                A.transform.GetChild(3).gameObject.SetActive(false);
                A.transform.GetChild(4).gameObject.SetActive(false);
                A.GetComponent<Item_ID>().id = -1;
                A.SetActive(true);
            }
            else
            {
                GameObject A = Instantiate(itemPref, itemPref.transform.position = new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                A.transform.GetChild(0).gameObject.SetActive(false);
                A.transform.GetChild(1).gameObject.SetActive(false);
                A.transform.GetComponent<Image>().sprite = emptyPrev;
                A.transform.GetChild(2).gameObject.SetActive(false);
                A.transform.GetChild(3).gameObject.SetActive(false);
                A.transform.GetChild(4).gameObject.SetActive(false);
                A.GetComponent<Item_ID>().id = -1;
                A.SetActive(true);
            }
            //A.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenPreview);
        }
        //Удаляем ненужные ячейки
        for (int i = invSize + addSize; i < 100; i++)
        {
            GameObject A = invPanel.transform.GetChild(i).gameObject;
            A.SetActive(false);
        }
    }

    public void sellItem(int id)
    {
        prevPanel.SetActive(false);
        int caseID = items[id][0];
        int itemID = items[id][1];
        g.silver = g.silver + (int)(g.cases[caseID].items[itemID].price);
        for (int i = id; i < invSize; i++)
        {
            if (i == invSize - 1)
            {
                GameObject A = invPanel.transform.GetChild(i).gameObject;
                A.SetActive(false);
            }
            else { 
                items[i][0] = items[i + 1][0];
                items[i][1] = items[i + 1][1];
                GameObject A = invPanel.transform.GetChild(i).gameObject;
                A.transform.GetChild(0).GetComponent<Text>().text = g.cases[items[i][0]].items[items[i][1]].price.ToString();
                A.transform.GetChild(1).GetComponent<Image>().sprite = g.cases[items[i][0]].items[items[i][1]].picture;
                A.transform.GetComponent<Image>().sprite = itemPrev;
                A.transform.GetChild(2).gameObject.SetActive(g.cases[items[i][0]].items[items[i][1]].group == 4);
                A.transform.GetChild(3).gameObject.SetActive(invPanel.transform.GetChild(i+1).GetChild(3).gameObject.activeSelf);
                //A.transform.GetChild(3).gameObject.SetActive(false);
                A.GetComponent<Item_ID>().id = i;
            }
        }
        invSize--;
        addEmpty();
    }

    public void sellMult()
    {
        for (int i = 0; i < invSize; i++)
        {
            if (invPanel.transform.GetChild(i).transform.GetChild(3).gameObject.activeSelf)
            {
                sellItem(i);
                i--;
            }
        }
        multSum = 0;
        changeMult();
    }

    public void changeMult()
    {
        isMult = !isMult;
        sellPanel.transform.GetChild(0).gameObject.SetActive(!isMult);
        sellPanel.transform.GetChild(1).gameObject.SetActive(isMult);
        multSell.SetActive(isMult);
        moneyPanel.SetActive(!isMult);
        if (!isMult)
        {
            for (int i = 0; i < invSize; i++)
            {
                invPanel.transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
            }
            multSum = 0;
        }
        else
        {
            
        }
        //вдруг если чего ещё дописать
    }

    public void clickOnItem(GameObject item)
    {
        if (isMult)
        {
            item.transform.GetChild(3).gameObject.SetActive(!item.transform.GetChild(3).gameObject.activeSelf);
            int i = item.GetComponent<Item_ID>().id;
            if (item.transform.GetChild(3).gameObject.activeSelf)
            {
                multSum = multSum + (int)g.cases[items[i][0]].items[items[i][1]].price; 
            }
            else
            {
                multSum = multSum - (int)g.cases[items[i][0]].items[items[i][1]].price;
            }
            multSell.transform.GetChild(2).GetComponent<Text>().text = multSum.ToString();
        }
        else
        {
            int id = item.GetComponent<Item_ID>().id;
            int caseID = items[id][0];
            int itemID = items[id][1];
            prevPanel.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = g.cases[caseID].items[itemID].name.ToUpper();
            prevPanel.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = g.cases[caseID].items[itemID].picture;
            prevPanel.transform.GetChild(0).GetChild(3).gameObject.SetActive(g.cases[caseID].items[itemID].group == 4);
            prevPanel.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = g.cases[caseID].items[itemID].price.ToString();
            int temp = id;
            prevPanel.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
            prevPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { sellItem(temp); });
            prevPanel.SetActive(true);
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void restartGame()
    {
        PlayerPrefs.DeleteAll();
        Application.LoadLevel(Application.loadedLevel);        
    }

    public void SaveGame()
    {
        sv.gold = g.gold;
        sv.silver = g.silver;
        sv.invSize = invSize;
        sv.items = new string[invSize];
        for (int j = 0; j < invSize; j++)
        {
            sv.items[j] = JsonHelper.ToJson<int>(items[j]);
        }
        string json = JsonUtility.ToJson(sv);
        string mydocpath = Directory.GetCurrentDirectory();
        File.WriteAllText(mydocpath + @"\Result.txt", json);
    }
}

[System.Serializable]
public class Save
{
    public string[] items; // первое число - id кейса, второе - id товара
    public int invSize;
    public int silver; 
    public int gold; 
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}