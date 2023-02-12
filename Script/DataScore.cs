using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataScore : MonoBehaviour
{
    public void WriteScore()
    {
        string path = Application.dataPath + "/Resources/Data.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(FindObjectOfType<UI>().GetName());
        writer.Close();
        StreamReader reader = new StreamReader(path);
        reader.Close();
        FindObjectOfType<UI>().ChangeScene(0);
    }
    
}
