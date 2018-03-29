using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

    public string siteName = "http://casesim/data.php";
    public string res;

	// Use this for initialization
	IEnumerator Start () {
        WWW userData = new WWW(siteName);
        yield return userData;
        res = userData.text;
        Debug.Log(res);
    }
	

}
