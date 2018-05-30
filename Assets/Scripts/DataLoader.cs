using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour
{

    string siteName = "https://elminaross.000webhostapp.com";
    public Game g;

    public bool downloadComplete = false;

    public string[] res;
    //public string[] getData;
    public string[] getData;
    public string[] topData;

    public string[] casesTop;
    public string[] usernameTop = new string[10];
    public string[] temp;
    public string posPlayer;


    // Use this for initialization

    void Start()
    {
        //StartCoroutine(getMainData());
        //StartCoroutine(getDataTop());
    }

    /*IEnumerator Start () {
        siteName = "http://casesim/getUserNum.php";
        WWW userData = new WWW(siteName);
        yield return userData;
        res = userData.text;
        Debug.Log(res);
    }*/

    
    public IEnumerator getMainData()
    {
        while (true)
        {
            string siteName1 = siteName + "/getUserNum.php";
            WWW userData = new WWW(siteName1);
            yield return userData;
            res = userData.text.Split('|');
            g.usersText.text = res[0];
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
        WWWForm form = new WWWForm();
        form.AddField("device_id", device_id);
        form.AddField("cases", cases.ToString());
        form.AddField("silvers", silver.ToString());
        form.AddField("golds", gold.ToString());
        string siteName2 = siteName + "/Upload.php";
        WWW www = new WWW(siteName2, form);
        //res = www.text;
    }

    public void updateData(string device_id, int cases)
    {
        WWWForm form = new WWWForm();
        form.AddField("device_id", device_id);
        form.AddField("cases", cases.ToString());
        string siteName2 = siteName + "/updateData.php";
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

    public IEnumerator LoginOrInsertData(string device_id, string username)
    {
        while (true)
        {
            WWWForm form = new WWWForm();
            form.AddField("device_id", device_id);
            form.AddField("user_name", username);
            string siteName2 = siteName + "/Login.php";
            WWW www = new WWW(siteName2, form);
            yield return www;
            string Data = www.text;     
            getData = Data.Split('—');
            g.nickname = getData[0];
            g.username_menu.text = getData[0];
            g.user.text = getData[0];
            g.setObj.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>().text = getData[0];
            g.count_cases.text = g.convertMoney(int.Parse(getData[1]));
            //g.silver = int.Parse(getData[0]);
            //g.gold = int.Parse(getData[1]);
            yield return new WaitForSeconds(8f);
        }        
    }
}
