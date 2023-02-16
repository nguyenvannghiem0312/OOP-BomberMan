using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EditInformation : MonoBehaviour
{
    public InputField RatioItem;
    public InputField MaxHP, HP;
    public InputField MaxSpeed, Speed;
    public InputField MaxBomb, Bomb;
    public InputField MaxRadius, Radius;
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
            string name = line.Split(";")[0];

            switch (name)
            {
                case "MaxHP":
                    MaxHP.text = line.Split(";")[1];
                    break;
                case "MaxSpeed":
                    MaxSpeed.text = line.Split(";")[1];
                    break;
                case "MaxBomb":
                    MaxBomb.text = line.Split(";")[1];
                    break;
                case "MaxRadius":
                    MaxRadius.text = line.Split(";")[1];
                    break;
                case "RatioItem":
                    RatioItem.text = line.Split(";")[1];
                    break;
                case "HP":
                    HP.text = line.Split(";")[1];
                    break;
                case "Speed":
                    Speed.text = line.Split(";")[1];
                    break;
                case "Bomb":
                    Bomb.text = line.Split(";")[1];
                    break;
                case "Radius":
                    Radius.text = line.Split(";")[1];
                    break;
                default:
                    break;
            }
        }
    }
    public void GetInforPlayer()
    {
        pathInformationPlayer = Application.dataPath + "/Resources/InformationPlayer.txt";
        File.WriteAllText(pathInformationPlayer, "");
        StreamWriter writer = new StreamWriter(pathInformationPlayer, true);
        writer.WriteLine("MaxHP" + ";" + moveController.GetComponent<MoveController>().MaxHP.ToString());
        writer.WriteLine("MaxSpeed" + ";" + moveController.GetComponent<MoveController>().MaxSpeed.ToString());
        writer.WriteLine("MaxBomb" + ";" + bombController.GetComponent<BombController>().MaxBomb.ToString());
        writer.WriteLine("MaxRadius" + ";" + bombController.GetComponent<BombController>().MaxRadius.ToString());
        writer.WriteLine("RatioItem" + ";" + Brick.itemSpawnChance.ToString());

        writer.WriteLine("HP" + ";" + moveController.GetComponent<MoveController>().Health.ToString());
        writer.WriteLine("Speed" + ";" + moveController.GetComponent<MoveController>().Speed.ToString());
        writer.WriteLine("Bomb" + ";" + bombController.GetComponent<BombController>().BombAmount.ToString());
        writer.WriteLine("Radius" + ";" + bombController.GetComponent<BombController>().ExplosionRadius.ToString());
        writer.Close();
    }
    public void LimitItem()
    {
        if (float.Parse(RatioItem.text) < 0f)
        {
            RatioItem.text = "0";
        }
        else if (float.Parse(RatioItem.text) > 1f)
        {
            RatioItem.text = "1";
        }
    }
    public void LimitHP()
    {
        if (int.Parse(MaxHP.text) < int.Parse(HP.text))
        {
            HP.text = MaxHP.text;
        }
        else if (int.Parse(MaxHP.text) <= 0 || int.Parse(HP.text) <= 0)
        {
            HP.text = MaxHP.text = "1";
        }
    }
    public void LimitSpeed()
    {
        if (int.Parse(MaxSpeed.text) < int.Parse(Speed.text))
        {
            Speed.text = MaxSpeed.text;
        }
        else if (int.Parse(MaxSpeed.text) <= 0f || int.Parse(Speed.text) <= 0f)
        {
            Speed.text = MaxSpeed.text = "1";
        }
    }
    public void LimitBomb()
    {
        if (int.Parse(MaxBomb.text) < int.Parse(Bomb.text))
        {
            Bomb.text = MaxBomb.text;
        }
        else if (int.Parse(MaxBomb.text) <= 0 || int.Parse(Bomb.text) <= 0)
        {
            Bomb.text = MaxBomb.text = "1";
        }
    }
    public void LimitRadius()
    {
        if (int.Parse(MaxRadius.text) < int.Parse(Radius.text))
        {
            Radius.text = MaxRadius.text;
        }
        else if (int.Parse(MaxRadius.text) <= 0 || int.Parse(Radius.text) <= 0)
        {
            Radius.text = MaxRadius.text = "1";
        }
    }
    public void SaveChange()
    {
        pathInformationPlayer = Application.dataPath + "/Resources/InformationPlayer.txt";
        File.WriteAllText(pathInformationPlayer, "");
        StreamWriter writer = new StreamWriter(pathInformationPlayer, true);
        writer.WriteLine("MaxHP" + ";" + int.Parse(MaxHP.text));
        writer.WriteLine("MaxSpeed" + ";" + float.Parse(MaxSpeed.text));
        writer.WriteLine("MaxBomb" + ";" + int.Parse(MaxBomb.text));
        writer.WriteLine("MaxRadius" + ";" + int.Parse(MaxRadius.text));
        writer.WriteLine("RatioItem" + ";" + float.Parse(RatioItem.text));
        writer.WriteLine("HP" + ";" + int.Parse(HP.text));
        writer.WriteLine("Speed" + ";" + float.Parse(Speed.text));
        writer.WriteLine("Bomb" + ";" + int.Parse(Bomb.text));
        writer.WriteLine("Radius" + ";" + int.Parse(Radius.text));
        writer.Close();
    }
}
