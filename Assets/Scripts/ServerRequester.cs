// REQUEST.CS handles networking with remote servers

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;

public class ServerRequester : MonoBehaviour
{
    public Text responseText;
    public Text transcriptionText;

    // FUNCTION RequestCommand takes in user input, sends it with current object references to the server, and displays the responses
    public void RequestCommand() {

        string inputText = transcriptionText.text;
        debug("inputText");
        debug(inputText);

        StartCoroutine(GetResponse(new Dictionary<string, string> {
            {"rawString", inputText},
            {"objectReferences", ""}
        },
        "https://atomxr.ngrok.io/getResponse",
        ExecuteCommand));
    }

    public void EmptyCallback(Dictionary<string, string> response) {
        debug("EmptyCallback");
    }

    public void ExecuteCommand(Dictionary<string, string> response) {

        string responseCommand = response["responseCommand"];

        debug("SERVERCALL - responseCommand: " + responseCommand);

        responseText.text = responseCommand;

    }

    IEnumerator GetResponse(Dictionary<string, string> inputData, string requestURL, System.Action<Dictionary<string, string>> callback)
    {
        WWWForm requestData = new WWWForm();
        foreach(var item in inputData)
        {
            requestData.AddField(item.Key, item.Value);
        }

        using (UnityWebRequest www = UnityWebRequest.Post(requestURL, requestData))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                debug("NETWORKING - Request error: " + www.error);
                callback(new Dictionary<string, string>());
            }
            else
            {
                debug("NETWORKING - Request Complete");
                string serverResponse = www.downloadHandler.text;
                debug("NETWORKING - Response: " + serverResponse);
            }
        }
    }

    // FUNCTION debug takes in a debug message and prints it to the console, togglable on and off
    private void debug(string message) {
        Debug.Log(message);
    }


}