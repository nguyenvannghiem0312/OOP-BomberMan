using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Players;

    public string CheckWinWithPlayer()
    {
        int countPlayer = 0;
        for(int i = 0; i < Players.Length; i++)
        {
            if (Players[i].activeSelf)
            {
                countPlayer++;
            }
        }
        if(countPlayer <= 1 && Players[0].activeSelf)
        {
            return "P1";
        }
        else if(countPlayer <= 1 && Players[1].activeSelf)
        {
            return "P2";
        }
        return "";
    }
    
    public bool CheckWinWithBoss()
    {
        if(FindObjectsOfType<EnemyController>().Length == 0)
        {
            return true;
        }
        return false;
    }
}
