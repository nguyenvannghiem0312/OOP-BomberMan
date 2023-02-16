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
            writer.WriteLine(grid[i].tileName + ";" + grid[i].pos.x + ";" + grid[i].pos.y + ";" + grid[i].pos.z);
        }
        writer.Close();
    }
    private void SaveEnemy()
    {
        File.WriteAllText(pathEnemy, "");
        StreamWriter writer = new StreamWriter(pathEnemy, true);
        foreach (var enemy in FindObjectsOfType<EnemyController>())
        {
            writer.WriteLine(enemy.name.Replace("(Clone)", "") + ";" + enemy.GetComponent<Transform>().position.x + ";" + enemy.GetComponent<Transform>().position.y
                + ";" + enemy.GetComponent<Transform>().position.z);
        }
        writer.WriteLine("NumEnemy" + ";" + FindObjectOfType<GameManager>().numEnemy);
        writer.Close();
    }
    private void SavePlayer()
    {
        File.WriteAllText(pathPlayer, "");
        StreamWriter writer = new StreamWriter(pathPlayer, true);
        MoveController player = FindObjectOfType<MoveController>();
        
        UI ui = FindObjectOfType<UI>();
        BombController bombController = FindObjectOfType<BombController>();
        writer.WriteLine(player.name + ";" + player.GetComponent<Transform>().position.x + ";" + player.GetComponent<Transform>().position.y
                + ";" + player.GetComponent<Transform>().position.z);
        writer.WriteLine("Score" + ";" + ui.score.text.Remove(0, 7));
        writer.WriteLine("HP" + ";" + ui.numHP.text);
        writer.WriteLine("Bomb" + ";" + ui.numBomb.text);
        writer.WriteLine("Speed" + ";" + player.Speed);
        writer.WriteLine("Radius" + ";" + bombController.ExplosionRadius);
        
        writer.Close();
    }
    public void SavingMap()
    {
        SaveGrid();
        SaveEnemy();
        SavePlayer();
        //UI.isSaveMap = true;
        FindObjectOfType<UI>().ChangeScene(0);
    }
    private void LoadGrid()
    {
        tilemap.ClearAllTiles();
        foreach (string line in File.ReadLines(pathGrid))
        {
            string name = line.Split(";")[0];

            switch (name)
            {
                case "Brick":
                    Vector3Int posLoad = new Vector3Int(int.Parse(line.Split(";")[1]), int.Parse(line.Split(";")[2]), int.Parse(line.Split(";")[3]));
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
            string name = line.Split(";")[0];

            switch (name)
            {
                case "NumEnemy":
                    gameManager.numEnemy = int.Parse(line.Split(";")[1]);
                    break;
                default:
                    Vector3 posEnemyLoad = new Vector3(float.Parse(line.Split(";")[1]), float.Parse(line.Split(";")[2]), float.Parse(line.Split(";")[3]));
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
            string name = line.Split(";")[0];

            switch (name)
            {
                case "HP":
                    moveController.Health = int.Parse(line.Split(";")[1]);
                    ui.SetHP(moveController.Health);
                    break;
                case "Bomb":
                    bombController.BombAmount = int.Parse(line.Split(";")[1]);
                    bombController.SetBombRemaining = int.Parse(line.Split(";")[1]);
                    ui.SetBomb(bombController.BombAmount);
                    break;
                case "Speed":
                    moveController.Speed = int.Parse(line.Split(";")[1]);
                    break;
                case "Radius":
                    bombController.ExplosionRadius = int.Parse(line.Split(";")[1]);
                    break;
                case "Score":
                    ui.SetScore(int.Parse(line.Split(";")[1]));
                    break;
                case "Player":
                    Vector3 posPlayerLoad = new Vector3(float.Parse(line.Split(";")[1]), float.Parse(line.Split(";")[2]), float.Parse(line.Split(";")[3]));
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


