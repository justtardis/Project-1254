using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Text;

public class LangSystem : MonoBehaviour
{
    public Achievment ac;
    public Game g;
    private string json = "";
    public static lang lng = new lang();
    private string[] langArray = { "RU_ru", "EN_en" };
    [Header("Переводы")]
    public Text[] main;
    public Text[] preview;
    public Text[] roulette;
    public Text[] noMoney;
    public Text[] disconnect;
    public Text[] inventory;
    public Text[] store;
    public Text[] settings;
    public Text[] leaderboard;
    //public Text[] achievments;
    public Text[] listL;
    public Text[] game1;
    public Text[] game2;
    public Text[] game3;
    public Text[] lotteryWindow;
    public Text[] achievments;
    public Text[] menu;
    public Image[] LOT1;
    public Sprite[] games;
    public Image[] gamesHolder;
    
    string lang;

    private void LangLoad()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, "Languages/" + PlayerPrefs.GetString("Language") + ".json");
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        json = reader.text;
#else
        json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Language") + ".json");
#endif
        print(json);
        lng = JsonUtility.FromJson<lang>(json);
        Translator();
    }

    private void Awake()
    {

        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
                PlayerPrefs.SetString("Language", "RU_ru");
            else PlayerPrefs.SetString("Language", "EN_en");
        }
        lang = PlayerPrefs.GetString("Language");
        LangLoad();
        print(PlayerPrefs.GetString("Language"));
        
    }



    public void swBtn()
    {
        if (lang == "RU_ru")
            lang = "EN_en";
        else
            lang = "RU_ru";
        PlayerPrefs.SetString("Language", lang);
        LangLoad();
    }

    private void Translator()
    {
        for (int i = 0; i < lng.menu.Length; i++) menu[i].text = lng.menu[i];
        for (int j = 0; j < 6; j++) main[j].text = lng.main[j];
        main[6].text = lng.inventory[3];
        
        preview[0].text = lng.preview[1];
        preview[1].text = lng.preview[2];
        preview[2].text = lng.preview[3];
        preview[3].text = lng.preview[4];
        preview[4].text = lng.preview[5];
        preview[5].text = lng.preview[5];
        preview[6].text = lng.preview[5];
        preview[7].text = lng.preview[6];
        preview[8].text = lng.preview[6];
        preview[9].text = lng.preview[6];
        preview[10].text = lng.preview[7];
        preview[11].text = lng.preview[8];

        for (int i = 0; i < lng.roulette.Length; i++) roulette[i].text = lng.roulette[i];

        noMoney[0].text = lng.noMoneyPanel[0];
        noMoney[1].text = lng.noMoneyPanel[1];

        for (int i = 0; i < lng.disconnect.Length; i++) disconnect[i].text = lng.disconnect[i];

        for (int i = 0; i < lng.inventory.Length; i++) inventory[i].text = lng.inventory[i];

        store[0].text = lng.store[0];
        for (int i = 1; i < 9; i++) store[i].text = lng.store[1];
        for (int i = 9; i < 13; i++) store[i].text = lng.store[2];

        for (int i = 0; i < 11; i++) settings[i].text = lng.settings[i];
        settings[11].text = settings[12].text = lng.settings[11];
        settings[13].text = lng.settings[12];

        for (int i = 0; i < lng.leaderboard.Length; i++) leaderboard[i].text = lng.leaderboard[i];

        listL[0].text = lng.listL[0];
        listL[1].text = lng.listL[1];
        listL[2].text = lng.listL[2];
        listL[3].text = lng.listL[4];
        listL[4].text = lng.listL[5];
        listL[5].text = lng.listL[6];
        listL[6].text = lng.listL[7];
        listL[7].text = lng.listL[8];
        listL[8].text = lng.listL[9];
        listL[9].text = lng.listL[10];
        listL[10].text = lng.listL[8];
        listL[11].text = lng.listL[2];
        listL[12].text = lng.listL[11];
        listL[13].text = lng.listL[4];
        listL[14].text = lng.listL[9];
        listL[15].text = lng.listL[13];

        game1[0].text = lng.game1[0];
        game1[1].text = lng.game1[1];
        game1[2].text = lng.game1[1];
        game1[3].text = lng.game1[1];
        game1[4].text = lng.game1[2];
        game1[5].text = lng.game1[5];
        game1[6].text = lng.game1[6];
        game1[7].text = lng.game1[7];

        game2[0].text = lng.game2[1];
        game2[1].text = lng.game2[2];
        game2[2].text = lng.game2[5];
        game2[3].text = lng.game2[6];

        for (int i = 0; i < lng.game3.Length; i++) game3[i].text = lng.game3[i];

        for(int i = 0; i<6; i++) lotteryWindow[i].text = lng.lotteryWindow[0];
        for (int i = 6; i < 12; i++) lotteryWindow[i].text = lng.lotteryWindow[1];
        lotteryWindow[12].text = lng.lotteryWindow[2];
        lotteryWindow[13].text = lng.lotteryWindow[3];
        lotteryWindow[14].text = lng.lotteryWindow[4];

        if(lang == "RU_ru")
        {
            for (int i = 0; i < 3; i++) gamesHolder[i].sprite = games[i];
            for (int i = 0; i < 4; i++) LOT1[i].sprite = g.text_lot1_ru[i];
        }
        else
        {
           gamesHolder[0].sprite = games[3];
            gamesHolder[1].sprite = games[4];
            gamesHolder[2].sprite = games[5];
            for (int i = 0; i < 4; i++) LOT1[i].sprite = g.text_lot1_en[i];
        }

        achievments[0].text = lng.achievments[0];
        achievments[1].text = lng.achievments[2];
        achievments[2].text = lng.achievments[27];
        //for(int i=1; i<27; i++)
        //{
        //    achievments[i].text = lng.achievments[i+1];
        //}
        ac.achievments[0].header = lng.achievments[3];
        ac.achievments[1].header = lng.achievments[5];
        ac.achievments[2].header = lng.achievments[7];
        ac.achievments[3].header = lng.achievments[9];
        ac.achievments[4].header = lng.achievments[11];
        ac.achievments[5].header = lng.achievments[13];
        ac.achievments[6].header = lng.achievments[15];
        ac.achievments[7].header = lng.achievments[17];
        ac.achievments[8].header = lng.achievments[19];
        ac.achievments[9].header = lng.achievments[21];
        ac.achievments[10].header = lng.achievments[23];
        ac.achievments[11].header = lng.achievments[25];       
        ac.achievments[0].description = lng.achievments[4];
        ac.achievments[1].description = lng.achievments[6];
        ac.achievments[2].description = lng.achievments[8];
        ac.achievments[3].description = lng.achievments[10];
        ac.achievments[4].description = lng.achievments[12];
        ac.achievments[5].description = lng.achievments[14];
        ac.achievments[6].description = lng.achievments[16];
        ac.achievments[7].description = lng.achievments[18];
        ac.achievments[8].description = lng.achievments[20];
        ac.achievments[9].description = lng.achievments[22];
        ac.achievments[10].description = lng.achievments[24];
        ac.achievments[11].description = lng.achievments[26];

        ac.LoadWindow();
        //achievments[1].text = lng.achievments[2];
        //achievments[2].text = lng.achievments[3];
        //achievments[3].text = lng.achievments[4];
        //achievments[4].text = lng.achievments[5];
        //achievments[5].text = lng.achievments[6];
        //achievments[6].text = lng.achievments[7];
        //achievments[7].text = lng.achievments[8];
        //achievments[8].text = lng.achievments[3];
        //achievments[9].text = lng.achievments[3];
        //achievments[10].text = lng.achievments[3];
        //achievments[11].text = lng.achievments[3];
        //achievments[12].text = lng.achievments[3];
        //achievments[13].text = lng.achievments[3];
        //achievments[14].text = lng.achievments[3];
        //achievments[15].text = lng.achievments[3];
        //achievments[16].text = lng.achievments[4];
        //achievments[17].text = lng.achievments[3];
        //achievments[18].text = lng.achievments[3];
        //achievments[19].text = lng.achievments[3];
        //achievments[20].text = lng.achievments[3];
        //achievments[21].text = lng.achievments[3];
        //achievments[22].text = lng.achievments[3];
        //achievments[23].text = lng.achievments[3];
        //achievments[24].text = lng.achievments[3];
        //achievments[25].text = lng.achievments[3];
        //achievments[26].text = lng.achievments[3];       
    }
}


[System.Serializable]
public class lang
{
    //public string[] menu = new string[7];
    public string[] namesCases = new string[12]; // сделано
    public string[] main = new string[6];
    public string[] preview = new string[8];
    public string[] roulette = new string[6];
    public string[] noMoneyPanel = new string[2];
    public string[] disconnect = new string[3];
    public string[] inventory = new string[7];
    public string[] store = new string[3];
    public string[] settings = new string[13];
    public string[] leaderboard = new string[8];
    public string[] achievments = new string[28];
    public string[] listL = new string[16];
    public string[] game1 = new string[8];
    public string[] game2 = new string[7];
    public string[] game3 = new string[4];
    public string[] lotteryWindow = new string[7];
    public string[] menu = new string[10];
    public string[] case1 = new string[12];
    public string[] case4 = new string[13];
    public string[] case9 = new string[15];
    public string[] case10 = new string[14];
    public string[] case11 = new string[13];
    public string[] case12 = new string[18];
}