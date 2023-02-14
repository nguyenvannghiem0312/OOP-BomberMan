using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataScore : MonoBehaviour
{
    public void WriteScore()
    {
        string path = Application.dataPath + "/Resources/Score.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(FindObjectOfType<UI>().GetName());
        writer.Close();
        FindObjectOfType<UI>().ChangeScene(0);
    }
    
}
