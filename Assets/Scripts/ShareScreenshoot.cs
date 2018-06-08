using System.Collections;
using System.IO;
using UnityEngine;

public class ShareScreenshoot : MonoBehaviour
{
    public GameObject CanvasShareObj;

    private bool isProcessing = false;
    private bool isFocus = false;
    public Achievment ach;
    string text1;

    public void ShareBtnPress(int text)
    {
        if (!isProcessing)
        {
            CanvasShareObj.SetActive(true);
            if (text == 1)
            {
               text1 = "Смотри, что выбил из кейса!\nКейс Симулятор Реальных Вещей\n" + "https:" + "//" + "play.google.com/store/apps/details?id=com.ForgeGames.cs";
                //text1 = "Смотри, что выбил из кейса!\nКейс Симулятор Реальных Вещей\n" + "http:"+"////"+ "play.google.com//apps//publish//?hl=ru&account=8491254212155528099#AppDashboardPlace:p=com.forgegames.miningclicker&appid=4972617207827160832";
            }
            else if (text == 2)
            {
                text1 = "Кейс Симулятор Реальных Вещей\n" + "https:" + "//" + "play.google.com/store/apps/details?id=com.ForgeGames.cs";
            }
            StartCoroutine(ShareScreenshot());
        }
    }

    IEnumerator ShareScreenshot()
    {
        isProcessing = true;

        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot("screenshot.png", 2);
        string destination = Path.Combine(Application.persistentDataPath, "screenshot.png");

        yield return new WaitForSecondsRealtime(0.3f);

        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
                uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
               text1);// );
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
                intentObject, "Выберете приложение");
            currentActivity.Call("startActivity", chooser);

            yield return new WaitForSecondsRealtime(1);
        }

        yield return new WaitUntil(() => isFocus);
        //CanvasShareObj.SetActive(false);
        isProcessing = false;
        if (!ach.achievments[9].get) ach.getAch(9);
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }
}