using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    [SerializeField] private int maxBomb;
    [SerializeField] private float bombFuseTime = 3f;
    [SerializeField] private int bombAmount = 1;
    [SerializeField] private int bombsRemaining;
    [SerializeField] private GameObject bombPrefabs;

    [Header("Place Bomb")]
    [SerializeField] private KeyCode inputKey = KeyCode.Space;

    [Header("Explosion Radius")]
    [SerializeField] private int maxRadius;
    [SerializeField] private int explosionRadius = 2;
    [SerializeField] private float explosionDuration = 1f;
    [SerializeField] private Explosion explosionPrefabs;
    [SerializeField] private LayerMask explosionLayerMask;

    [Header("Map")]
    [SerializeField] private Tilemap destructibleTiles;
    [SerializeField] private Destructible destructiblePrefabs;

    private SoundManage sound;
    private UI ui;
    public int MaxBomb
    {
        set { maxBomb = value; }
        get { return maxBomb; }
    }
    public int MaxRadius
    {
        set { maxRadius = value; }
        get { return maxRadius; }
    }
    public int BombAmount
    {
        get { return bombAmount; }
        set { bombAmount = value; }
    }
    public int SetBombRemaining
    {
        set { bombsRemaining = value; }
    }
    public int ExplosionRadius
    {
        get { return explosionRadius ; }
        set { explosionRadius = value; }
    }
    private void OnEnable()
    {
        ui = FindObjectOfType<UI>();
        UpdateInformation();
        sound = FindObjectOfType<SoundManage>();
        bombsRemaining = bombAmount;
    }
    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());    
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
                case "MaxBomb":
                    MaxBomb = int.Parse(line.Split(";")[1]);
                    break;
                case "MaxRadius":
                    MaxRadius = int.Parse(line.Split(";")[1]);
                    break;
                case "Bomb":
                    BombAmount = int.Parse(line.Split(";")[1]);
                    SetBombRemaining = BombAmount;
                    ui.SetBomb(BombAmount);
                    break;
                case "Radius":
                    ExplosionRadius = int.Parse(line.Split(";")[1]);
                    break;
                default:
                    break;
            }
        }
    }
    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefabs, position, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        sound.PlayAudioClip(sound.audioBomb);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(explosionPrefabs, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsRemaining++;
    }
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;

        if(Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            DestroyBrick(position);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefabs, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }

    private void DestroyBrick(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);
        if(tile != null)
        {
            Instantiate(destructiblePrefabs, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }

    public void AddBomb()
    {
        if(bombAmount < maxBomb)
        {
            bombAmount++;
            bombsRemaining++;
            ui.SetBomb(bombAmount);
        }
    }
}
