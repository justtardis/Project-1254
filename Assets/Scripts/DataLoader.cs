using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour
{
    private string linkPost;
    public Text[] block1;
    public Text[] block2;
    public string[] news;
    public string[] split_news1;
    public string[] split_news2;
    string GP_score = "market://details?id=com.ForgeGames.cs";

    string siteName = "http://casesim.ru";
    public Game g;
    public GameObject ban;
    public Text comment;
    private string key;
    public bool downloadComplete = false;

    public string[] res;
    //public string[] getData;
    public string[] getData;
    public string[] topData;

    public string[] casesTop;
    public string[] usernameTop = new string[10];
    public string[] temp;
    public string posPlayer;

    private void UserBan()
    {
        string com = "Причина: " + getData[3];
        comment.text = com;
    }

    // Use this for initialization

    public void OpenGP()
    {
        Application.OpenURL(GP_score);
    }

    public void OpenLink()
    {
        Application.OpenURL(linkPost);
    }


    void Start()
    {

        StartCoroutine(getInfo());
        //StartCoroutine(getMainData());
        //StartCoroutine(getDataTop());
    }

    public IEnumerator getInfo()
    {
        while (true)
        {
            string siteName1 = siteName + "/GetInfo.php";
            WWW Data = new WWW(siteName1);
            yield return Data;
            news = Data.text.Split(new[] { "<br/>" }, System.StringSplitOptions.RemoveEmptyEntries);
            split_news1 = news[0].Split('—');
            split_news2 = news[1].Split('—');
            block1[0].text = split_news1[0];
            block1[1].text = split_news1[1];
            block1[2].text = "<color=\"#7C8092FF\">Окончание:  </color>" + split_news1[2];
            linkPost = split_news1[3];
            block2[0].text = split_news2[0];
            block2[1].text = split_news2[1];


            yield return new WaitForSeconds(8f);
        }
    }


    public IEnumerator getMainData()
    {
        while (true)
        {
            string siteName1 = siteName + "/getUserNum.php";
            WWW userData = new WWW(siteName1);
            yield return userData;
            res = userData.text.Split('|');
            g.usersText.text = g.convertMoney(int.Parse(res[0]));
            g.casesText.text = g.convertMoney(int.Parse(res[1]));
            yield return new WaitForSeconds(8f);
        }
    }

    private void setTopList()
    {
        for (int i = 0; i < 10; i++)
        {
            g.groupList.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = usernameTop[i];
            g.groupList.transform.GetChild(i).GetChild(2).GetComponent<Text>().text = g.convertMoney(int.Parse(casesTop[i]));
        }
    }

    public IEnumerator getDataTop()
    {
        while (true)
        {
            string siteName1 = siteName + "/OrderBy.php";
            WWWForm form = new WWWForm();
            form.AddField("user_name", g.nickname);
            WWW userData = new WWW(siteName1, form);
            yield return userData;
            print(userData);
            downloadComplete = true;
            topData = userData.text.Split(new[] { "<br/>" }, System.StringSplitOptions.RemoveEmptyEntries);
            splitTop();
            setTopList();
            g.user.text = g.nickname;
            yield return new WaitForSeconds(8f);
        }
    }

    private void splitTop()
    {
        posPlayer = topData[10];
        g.topLevel.text = posPlayer;
        for (int i = 0; i < 10; i++)
        {
            temp = topData[i].Split(':');
            usernameTop[i] = temp[0];
            casesTop[i] = temp[1];
            //usernameTop[i] = topData[i].Substring(topData[i].IndexOf(topData[i]), topData[i].LastIndexOf(':'));
            //temp = topData[i].Substring(topData[i].LastIndexOf(':'));
            //if (temp.Contains(":")) temp = temp.Remove(temp.IndexOf(":"), 1);
            //casesTop[i] = temp;
        }
    }

    public void Upload(string device_id, int silver, int gold, int cases)
    {
        key = StringCipher.jhewy(Application.companyName.ToString());

        WWWForm form = new WWWForm();
        form.AddField("device_id", device_id);
        form.AddField("cases", cases.ToString());
        form.AddField("silvers", silver.ToString());
        form.AddField("golds", gold.ToString());
        form.AddField("key", key);
        string siteName2 = siteName + "/Upload_new.php";
        WWW www = new WWW(siteName2, form);
        //res = www.text;

    }

    public void updateData(string device_id, int cases)
    {

        key = StringCipher.jhewy(Application.companyName.ToString());
        WWWForm form = new WWWForm();
        form.AddField("device_id", device_id);
        form.AddField("cases", cases.ToString());
        form.AddField("key", key);
        string siteName2 = siteName + "/updateData_new.php";
        WWW www = new WWW(siteName2, form);
        //res = www.text;

    }

    public void updateNickname(string device_id, string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_name", username);
        form.AddField("device_id", device_id);
        string siteName2 = siteName + "/UpdateNickname.php";
        WWW www = new WWW(siteName2, form);

        //res = www.text;
    }

    public void updateBan(string device_id, int ban)
    {

        WWWForm form = new WWWForm();
        form.AddField("ban", ban.ToString());
        form.AddField("device_id", device_id);
        string siteName2 = siteName + "/ban.php";
        WWW www = new WWW(siteName2, form);

        //res = www.text;
    }

    public IEnumerator LoginOrInsertData(string device_id, string username)
    {
        key = StringCipher.jhewy(Application.companyName);
        while (true)
        {

            WWWForm form = new WWWForm();
            form.AddField("device_id", device_id);
            form.AddField("user_name", username);
            form.AddField("key", key);
            string siteName2 = siteName + "/Login_new.php";
            WWW www = new WWW(siteName2, form);
            yield return www;
            string Data = www.text;
            getData = Data.Split('—');
            if (getData[0] == "")
            {
                string n = "LOL";
                updateNickname(g.deviceID, n);
            }
            g.nickname = getData[0];
            g.username_menu.text = getData[0];
            g.user.text = getData[0];
            g.setObj.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>().text = getData[0];
            g.count_cases.text = g.convertMoney(int.Parse(getData[1]));
            if (int.Parse(getData[2]) == 1)
            {
                ban.SetActive(true);
                UserBan();
            }
            //g.silver = int.Parse(getData[0]);
            //g.gold = int.Parse(getData[1]);
            yield return new WaitForSeconds(8f);
        }
    }

    public void SporVK()
    {
        Application.OpenURL("https://vk.com/im?media=&sel=-122700417");
    }
    public void Manual()
    {
        g.silver = 1000;
        g.gold = 40;
        updateBan(g.deviceID, 0);
        Upload(g.deviceID, g.silver, g.gold, g.casesNum);
        ban.SetActive(false);
    }
}
