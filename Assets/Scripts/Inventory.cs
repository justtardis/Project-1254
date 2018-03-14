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
    public Sprite itemPrev;
    public Sprite emptyPrev;

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
                A.transform.GetChild(3).gameObject.SetActive(false);
                A.SetActive(true);
            }
            //A.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenPreview);
        }
        //Удаляем ненужные ячейки
        for (int i = invSize; i < 100; i++)
        {
            GameObject A = invPanel.transform.GetChild(i).gameObject;
            A.SetActive(false);
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