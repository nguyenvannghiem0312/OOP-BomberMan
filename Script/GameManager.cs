using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Maybe not need
    [Header("Players")]
    public GameObject[] Players;

    [Header("Enemys")]
    public GameObject[] EnemysPrefabs;
    public int numEnemy;
    public int scoreHard = 200;
    
    private UI ui;
    private void Start()
    {
        numEnemy = FindObjectsOfType<EnemyController>().Length;
        ui = FindObjectOfType<UI>();
    }
    private void Update()
    {
        SummonEnemy();
        IncreaseEnemy();
    }
    private void IncreaseEnemy()
    {
        if(ui.GetScore() / scoreHard > 0)
        {
            numEnemy += 2 * ui.GetScore() / scoreHard;
            scoreHard *= 2;
        }
    }
    public bool CheckLose()
    {
        int countPlayer = 0;
        for(int i = 0; i < Players.Length; i++)
        {
            if (Players[i].activeSelf)
            {
                countPlayer++;
            }
        }
        if(countPlayer == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void SummonEnemy()
    {
        while (FindObjectsOfType<EnemyController>().Length < numEnemy)
        {
            int x, y;
            do
            {
                x = Random.Range(-12, 12);
                y = Random.Range(-5, 5);
            }
            while (x % 2 != 0 || y % 2 == 0);
            int index = Random.Range(0, EnemysPrefabs.Length);
            Instantiate(EnemysPrefabs[index], new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}
