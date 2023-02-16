using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Destructible : MonoBehaviour
{
    public float destructibleTime = 1f;
    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;
    void Start()
    {
        UpdateInformation();
        Destroy(gameObject, destructibleTime);
    }

    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
    private void UpdateInformation()
    {
        string pathInformationPlayer = Application.dataPath + "/Resources/InformationPlayer.txt";
        foreach (string line in File.ReadLines(pathInformationPlayer))
        {
            string name = line.Split(";")[0];

            switch (name)
            {
                case "RatioItem":
                    itemSpawnChance = float.Parse(line.Split(";")[1]);
                    break;
                default:
                    break;
            }
        }
    }
}
