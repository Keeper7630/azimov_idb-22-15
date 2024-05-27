using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.EventSystems;

public class zadanie2 : MonoBehaviour
{

    public Text field;
    public string jsonURL;
    public Jsonclass jsnData;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator getData()
    {
        var uwr = new UnityWebRequest(jsonURL);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var resultFile = Path.Combine(Application.persistentDataPath, "result.json");
        var dh = new DownloadHandlerFile(resultFile);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("ERROR");
        }

        else
        {

            Debug.Log("Downolad saved to: " + resultFile);
            jsnData = JsonUtility.FromJson<Jsonclass>(File.ReadAllText(Application.persistentDataPath + "/result.json"));
            field.text = $"Название станка: {jsnData.Stanokname.ToString()}";
            yield return StartCoroutine(getData());

        }

    }
    [System.Serializable]

    public class Jsonclass
    {

        public string Stanokname;
    }

}
