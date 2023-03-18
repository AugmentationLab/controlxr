using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public GameObject TextDisplay;
    public GameObject SceneParent;
    public GameObject Sound;

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
                    if (i == 10)
                    {
                        Sound.GetComponent<AudioSource>().Play();
                    }
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



