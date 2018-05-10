using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

    string siteName = "https://elminaross.000webhostapp.com";
    public Game g;
    private string GoogleID = "123jklffskj4nmsdfm53";
    private string UserName = "Administrator";

    public string[] res;

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


}
