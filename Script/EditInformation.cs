using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EditInformation : MonoBehaviour
{
    public InputField RatioItem;
    public InputField MaxHP;
    public InputField MaxSpeed;
    public InputField MaxBomb;
    public InputField MaxRadius;
    public Destructible Brick;
    public GameObject moveController;
    public GameObject bombController;

    private string pathInformationPlayer;
    static bool isGetInforPlayer = false;
    private void Awake() {
    
        if(isGetInforPlayer == false){
            GetInforPlayer();
            isGetInforPlayer = true;
        }
        pathInformationPlayer = Application.dataPath + "/Resources/InformationPlayer.txt";
        foreach (string line in File.ReadLines(pathInformationPlayer))
        {
            string name = line.Split("\t")[0];

            switch (name)
            {
                case "MaxHP":
                    MaxHP.text = line.Split("\t")[1];
                    break;
                case "MaxSpeed":
                    MaxSpeed.text = line.Split("\t")[1];
                    break;
                case "MaxBomb":
                    MaxBomb.text = line.Split("\t")[1];
                    break;
                case "MaxRadius":
                    MaxRadius.text = line.Split("\t")[1];
                    break;
                case "RatioItem":
                    RatioItem.text = line.Split("\t")[1];
                    break;
            }
        }
    }
    public void GetInforPlayer()
    {
        pathInformationPlayer = Application.dataPath + "/Resources/InformationPlayer.txt";
        File.WriteAllText(pathInformationPlayer, "");
        StreamWriter writer = new StreamWriter(pathInformationPlayer, true);
        writer.WriteLine("MaxHP" + "\t" + moveController.GetComponent<MoveController>().MaxHP.ToString());
        writer.WriteLine("MaxSpeed" + "\t" + moveController.GetComponent<MoveController>().MaxSpeed.ToString());
        writer.WriteLine("MaxBomb" + "\t" + bombController.GetComponent<BombController>().MaxBomb.ToString());
        writer.WriteLine("MaxRadius" + "\t" + bombController.GetComponent<BombController>().MaxRadius.ToString());
        writer.WriteLine("RatioItem" + "\t" + Brick.itemSpawnChance.ToString());
        writer.Close();
    }
    public void LimitItem()
    {
        if(float.Parse(RatioItem.text) < 0f)
        {
            RatioItem.text = "0";
        }
        else if(float.Parse(RatioItem.text) > 1f)
        {
            RatioItem.text = "1";
        }
    }
    public void SaveChange()
    {
        pathInformationPlayer = Application.dataPath + "/Resources/InformationPlayer.txt";
        File.WriteAllText(pathInformationPlayer, "");
        StreamWriter writer = new StreamWriter(pathInformationPlayer, true);
        writer.WriteLine("MaxHP" + "\t" + int.Parse(MaxHP.text));
        writer.WriteLine("MaxSpeed" + "\t" + float.Parse(MaxSpeed.text));
        writer.WriteLine("MaxBomb" + "\t" + int.Parse(MaxBomb.text));
        writer.WriteLine("MaxRadius" + "\t" + int.Parse(MaxRadius.text));
        writer.WriteLine("RatioItem" + "\t" + float.Parse(RatioItem.text));
        writer.Close();
    }
}
