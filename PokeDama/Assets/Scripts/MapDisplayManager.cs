using UnityEngine;
using System.Collections;

public class MapDisplayManager : MonoBehaviour, GameManager {
    private static AndroidJavaClass bridge = null;
    NetworkManager network;
    string imei;
    int number = 0;
    int limit = 200;

    // Use this for initialization
    void Start()
    {
        network = FindObjectOfType<NetworkManager>();

        Debug.Log("Unity before blue shareText callShareApp");
        //Get Android context
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject app = activity.Call<AndroidJavaObject>("getApplicationContext");

        imei = SystemInfo.deviceUniqueIdentifier;

        //Install 
        bridge = new AndroidJavaClass("com.madcamp.pokedamalib.BridgeActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            bridge.CallStatic("callMap", app, imei);
        }));

        Debug.Log("Unity What is the name of gameobjects? " + gameObject.name);
        Debug.Log("Unity after blue callShareApp");

    }

    // Update is called once per frame
    void Update()
    {
        if (number > limit)
        {
            //server의 db에 query를 날리고 scene 정보를 받아오고 넘기던지 말던지 결정
            network.RequestScene(imei);

            number = 0;
        }
        number++;
    }

    IEnumerator RequestWithDelay(string data)
    {

        Debug.Log("Unity go to battle scene" + data);

        return null;
    }

    void GameManager.handleResponse(string data)
    {
        JSONObject json = new JSONObject(data);

        string sc = json.GetField("scene").str;

        Debug.Log("Scene : " + json.GetField("scene").str);
        Debug.Log("Scene : " + json.GetField("scene").str.Equals("BATTLE"));

        if (json.GetField("scene").str.Equals("BATTLE"))
        {
            Application.LoadLevel(4);
        }
        else if (json.GetField("scene").str.Equals("DAMA"))
        {
            Application.LoadLevel(2);
        }
    }
}
