using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{

    string siteName = "https://elminaross.000webhostapp.com";
    public Game g;

    public string[] res;
    //public string[] getData;
    public string getData;

    // Use this for initialization

    void Start()
    {
        StartCoroutine(getMainData());
    }

    /*IEnumerator Start () {
        siteName = "http://casesim/getUserNum.php";
        WWW userData = new WWW(siteName);
        yield return userData;
        res = userData.text;
        Debug.Log(res);
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) StartCoroutine(LoginOrInsertData("6984412st50933dc", "LemonS"));
    }

    IEnumerator getMainData()
    {
        while (true)
        {
            string siteName1 = siteName + "/getUserNum.php";
            WWW userData = new WWW(siteName1);
            yield return userData;
            res = userData.text.Split('|');
            g.usersText.text = res[0];
            g.casesText.text = res[1];
            yield return new WaitForSeconds(10f);
        }
    }

    public void updateData(int id, int cases)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id.ToString());
        form.AddField("cases", cases.ToString());
        string siteName2 = siteName + "/updateData.php";
        WWW www = new WWW(siteName2, form);
        //res = www.text;
    }

    IEnumerator LoginOrInsertData(string google_ID, string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("google_id", google_ID);
        form.AddField("user_name", username);
        string siteName2 = siteName + "/Login.php";
        WWW www = new WWW(siteName2, form);
        yield return www;
    //    getData = www.text;
    //    public string[] r = new string[5];
    //r= getData.Split('—');
       // print(GetDataValue(getData[0], "silver:"));
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        value = value.Remove(value.IndexOf("—"));
        return value;
    }


}
