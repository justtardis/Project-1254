using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Text;

public class LangSystem : MonoBehaviour
{
    private string json = "";
    public static lang lng = new lang();
    private string[] langArray = { "RU_ru", "EN_en" };

    bool s;
    public Text t;

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
        lng = JsonUtility.FromJson<lang>(json);

    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
                PlayerPrefs.SetString("Language", "RU_ru");
            else PlayerPrefs.SetString("Language", "EN_en");
        }
        LangLoad();

    }



    public void swBtn(int index)
    {
        PlayerPrefs.SetString("Language", langArray[index - 1]);
        LangLoad();
    }

}

[System.Serializable]
public class lang
{
    public string[] menu = new string[7];
   
}