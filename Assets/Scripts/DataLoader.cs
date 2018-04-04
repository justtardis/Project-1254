using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

    public string siteName = "http://casesim/";
    public Game g;

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
            siteName = "http://localhost/casesim/getUserNum.php";
            WWW userData = new WWW(siteName);
            yield return userData;
            res = userData.text.Split('|');
            g.usersText.text = res[0];
            g.casesText.text = res[1];
        }
    }

    public void updateData(int id, int cases)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id.ToString());
        form.AddField("cases", cases.ToString());
        siteName = "http://localhost/casesim/updateData.php";
        WWW www = new WWW(siteName, form);
        //res = www.text;
    }


}
