using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.IO;
using System;
[SerializeField]
public class SaveMap : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase brick;
    private List<Grid> grid = new List<Grid>();
    private string pathGrid, pathEnemy, pathPlayer;

    // public MoveController playerLoad;
    public EnemyController[] enemyesLoad;

    private void Awake()
    {
        pathGrid = Application.dataPath + "/Resources/SaveGrid.txt";
        pathEnemy = Application.dataPath + "/Resources/SaveEnemy.txt";
        pathPlayer = Application.dataPath + "/Resources/SavePlayer.txt";
        if (UI.isLoadMap == true)
        {
            LoadMap();
            UI.isLoadMap = false;
        }
    }
    private void SaveGrid()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int gridPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemap.HasTile(gridPlace))
            {
                TileBase tile = tilemap.GetTile(gridPlace);
                grid.Add(new Grid { tileName = tile.name, pos = gridPlace} );
            }
        }

        File.WriteAllText(pathGrid, "");
        StreamWriter writer = new StreamWriter(pathGrid, true);
        
        for (int i = 0; i < grid.Count; i++)
        {
            writer.WriteLine(grid[i].tileName + "\t" + grid[i].pos.x + "\t" + grid[i].pos.y + "\t" + grid[i].pos.z);
        }
        writer.Close();
    }
    private void SaveEnemy()
    {
        File.WriteAllText(pathEnemy, "");
        StreamWriter writer = new StreamWriter(pathEnemy, true);
        foreach (var enemy in FindObjectsOfType<EnemyController>())
        {
            writer.WriteLine(enemy.name.Replace("(Clone)", "") + "\t" + enemy.GetComponent<Transform>().position.x + "\t" + enemy.GetComponent<Transform>().position.y
                + "\t" + enemy.GetComponent<Transform>().position.z);
        }
        writer.WriteLine("NumEnemy" + "\t" + FindObjectOfType<GameManager>().numEnemy);
        writer.Close();
    }
    private void SavePlayer()
    {
        File.WriteAllText(pathPlayer, "");
        StreamWriter writer = new StreamWriter(pathPlayer, true);
        MoveController player = FindObjectOfType<MoveController>();
        
        UI ui = FindObjectOfType<UI>();
        BombController bombController = FindObjectOfType<BombController>();
        writer.WriteLine(player.name + "\t" + player.GetComponent<Transform>().position.x + "\t" + player.GetComponent<Transform>().position.y
                + "\t" + player.GetComponent<Transform>().position.z);
        writer.WriteLine("Score" + "\t" + ui.score.text.Remove(0, 7));
        writer.WriteLine("HP" + "\t" + ui.numHP.text);
        writer.WriteLine("Bomb" + "\t" + ui.numBomb.text);
        writer.WriteLine("Speed" + "\t" + player.speed);
        writer.WriteLine("Radius" + "\t" + bombController.explosionRadius);
        
        writer.Close();
    }
    public void SavingMap()
    {
        SaveGrid();
        SaveEnemy();
        SavePlayer();
        FindObjectOfType<UI>().ChangeScene(0);
    }
    private void LoadGrid()
    {
        tilemap.ClearAllTiles();
        foreach (string line in File.ReadLines(pathGrid))
        {
            string name = line.Split("\t")[0];

            switch (name)
            {
                case "Brick":
                    Vector3Int posLoad = new Vector3Int(int.Parse(line.Split("\t")[1]), int.Parse(line.Split("\t")[2]), int.Parse(line.Split("\t")[3]));
                    tilemap.SetTile(posLoad, brick);
                    break;
                default:
                    break;
            }
        }
    }
    private void LoadEnemy()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        foreach (string line in File.ReadLines(pathEnemy))
        {
            string name = line.Split("\t")[0];

            switch (name)
            {
                case "NumEnemy":
                    gameManager.numEnemy = int.Parse(line.Split("\t")[1]);
                    break;
                default:
                    Vector3 posEnemyLoad = new Vector3(float.Parse(line.Split("\t")[1]), float.Parse(line.Split("\t")[2]), float.Parse(line.Split("\t")[3]));
                    Instantiate(Array.Find(enemyesLoad, x => x.name == name), posEnemyLoad, Quaternion.identity);
                    break;
            }
        }
    }
    private void LoadPlayer()
    {
        UI ui = FindObjectOfType<UI>();
        BombController bombController = FindObjectOfType<BombController>();
        MoveController moveController = FindObjectOfType<MoveController>();
        foreach (string line in File.ReadLines(pathPlayer))
        {
            string name = line.Split("\t")[0];

            switch (name)
            {
                case "HP":
                    moveController.HP = int.Parse(line.Split("\t")[1]);
                    ui.SetHP(moveController.HP);
                    break;
                case "Bomb":
                    bombController.bombAmount = int.Parse(line.Split("\t")[1]);
                    bombController.SetBombRemaining = int.Parse(line.Split("\t")[1]);
                    ui.SetBomb(bombController.bombAmount);
                    break;
                case "Speed":
                    moveController.speed = float.Parse(line.Split("\t")[1]);
                    break;
                case "Radius":
                    bombController.explosionRadius = int.Parse(line.Split("\t")[1]);
                    break;
                case "Score":
                    ui.SetScore(int.Parse(line.Split("\t")[1]));
                    break;
                case "Player":
                    Vector3 posPlayerLoad = new Vector3(float.Parse(line.Split("\t")[1]), float.Parse(line.Split("\t")[2]), float.Parse(line.Split("\t")[3]));
                    moveController.GetComponent<Transform>().position = posPlayerLoad;
                    break;
                default:
                    break;
            }
        }
    }
    public void LoadMap()
    {
        LoadGrid();
        LoadPlayer();
        LoadEnemy();
    }
    
}


