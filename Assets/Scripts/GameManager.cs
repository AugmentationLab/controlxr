using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.IO.Ports;

public class GameManager : MonoBehaviour
{

    public SerialPort stream;
    public string command = "010000";

    public GameObject TextDisplay;
    public GameObject SceneParent;
    public GameObject Sound;

    void Start() {
        stream = new SerialPort("COM4", 9600);
        stream.ReadTimeout = 50;
        stream.Open();
    }

    // return a dictionary of joint indices with corresponding game objects that overlap with the positions of game objects in SceneObjects
    public Dictionary<int, GameObject> CheckOverlap(List<Vector3> points)
    {
        Dictionary<int, GameObject> overlaps = new Dictionary<int, GameObject>();
        foreach (Transform child in SceneParent.transform)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (child.gameObject.GetComponent<Collider>().bounds.Contains(points[i]))
                {
                    overlaps.Add(i, child.gameObject);
                    // test code for rapid prototyping
                    // if (i == 3 || i == 5 || i == 10) {
                    Debug.Log("Overlap detected!");
                    stream.WriteLine(command);
                    stream.BaseStream.Flush();
                    // }
                }
            }
        }
        return overlaps;
    }

    // display the dictionary of overlaps in TextDisplay
    public void DisplayText(Dictionary<int, GameObject> overlaps){

        TMP_Text displayText = TextDisplay.GetComponent<TMP_Text>();
        displayText.text = "Overlap dictionary: \n" + string.Join("\n", overlaps.Select(kvp => kvp.Key + " = " + kvp.Value.name).ToArray());

    }

}



