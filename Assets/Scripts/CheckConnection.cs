using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConnection : MonoBehaviour {

    public GameObject panelDisconnect;
    public DataLoader dl;
    public bool active = true;
    void Start()
    {
        active = true;
        TryConnection();
    }
    
    IEnumerator Wait()
    {
        while (active)
        {
            StartCoroutine(Check());
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator Check()
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            active = false;
            dl.downloadComplete = false;
            panelDisconnect.SetActive(true);
            StopCoroutine(Wait());
            print("Offline");
        }
        else
            panelDisconnect.SetActive(false);

    }

    public void TryConnection()
    {
        active = true;
        StartCoroutine(Wait());
    }
}
