using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class MoveController : MonoBehaviour
{
    [Header("Speed and HP")]
    [SerializeField] private int maxHP;
    [SerializeField] private int maxSpeed;
    [SerializeField] private int speed = 5;
    [SerializeField] private int HP = 1;

    [Header("Input")]
    [SerializeField] private KeyCode inputUp = KeyCode.W;
    [SerializeField] private KeyCode inputDown = KeyCode.S;
    [SerializeField] private KeyCode inputLeft = KeyCode.A;
    [SerializeField] private KeyCode inputRight = KeyCode.D;

    [Header("Animation")]
    [SerializeField] private AnimatedSprite spriteRendererUp;
    [SerializeField] private AnimatedSprite spriteRendererDown;
    [SerializeField] private AnimatedSprite spriteRendererLeft;
    [SerializeField] private AnimatedSprite spriteRendererRight;
    [SerializeField] private AnimatedSprite spriteRendererRevival;
    [SerializeField] private AnimatedSprite spriteRendererDeath;
    private AnimatedSprite activeSpriteRenderer;

    public Rigidbody2D rigibody { get; private set; }
    private Vector2 direction = Vector2.down;

    private UI ui;
    private SoundManage sound;
    private GameManager gameManager;
    public int MaxHP
    {
        set { maxHP = value; }
        get { return maxHP; }
    }
    public int Health
    {
        set { HP = value; }
        get { return HP; }
    }
    public int MaxSpeed
    {
        set { maxSpeed = value; }
        get { return maxSpeed; }
    }
    public int Speed
    {
        set { speed = value; }
        get { return speed; }
    }
    private void OnEnable()
    {
        UpdateInformation();
        sound = FindObjectOfType<SoundManage>();
        ui = FindObjectOfType<UI>();
        gameManager = FindObjectOfType<GameManager>();
        rigibody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }
    private void Update()
    {
        ui.SetHP(HP);
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRendererDown);
        } 
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRendererRight);
        } 
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteRendererLeft );
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }
    private void FixedUpdate()
    {
        Vector2 position = rigibody.position;
        Vector2 translation = direction * speed * Time.deltaTime;
        rigibody.MovePosition(position + translation); 
    }
    private void UpdateInformation()
    {
        string pathInformationPlayer = Application.dataPath + "/Resources/InformationPlayer.txt";
        foreach (string line in File.ReadLines(pathInformationPlayer))
        {
            string name = line.Split(";")[0];

            switch (name)
            {
                case "MaxHP":
                    MaxHP = int.Parse(line.Split(";")[1]);
                    break;
                case "MaxSpeed":
                    MaxSpeed = int.Parse(line.Split(";")[1]);
                    break;
                case "HP":
                    Health = int.Parse(line.Split(";")[1]);
                    break;
                case "Speed":
                    Speed = int.Parse(line.Split(";")[1]);
                    break;
                default:
                    break;
            }
        }
    }
    private void SetDirection(Vector2 nDirection, AnimatedSprite animatedSprite)
    {
        direction = nDirection;

        spriteRendererUp.enabled = animatedSprite == spriteRendererUp;
        spriteRendererDown.enabled = animatedSprite == spriteRendererDown;
        spriteRendererLeft.enabled = animatedSprite == spriteRendererLeft;
        spriteRendererRight.enabled = animatedSprite == spriteRendererRight;
        spriteRendererRevival.enabled = animatedSprite == spriteRendererRevival;

        activeSpriteRenderer = animatedSprite;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            HP--;
            if(HP <= 0)
            {
                ui.SetHP(HP);
                Death();
            }
            else
            {
                SetDirection(Vector2.zero, spriteRendererRevival);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HP--;
            if (HP <= 0)
            {
                ui.SetHP(0);
                Death();
            }
            else
            {
                SetDirection(Vector2.zero, spriteRendererRevival);
            }
        }
    }
    private void Death()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeath), 1.25f);
    }
    private void OnDeath()
    {
        sound.PlayAudioClip(sound.audioDeath);
        gameObject.SetActive(false);
        if(gameManager.CheckLose() == true)
        {
            Time.timeScale = 0;
            ui.ShowPanel(ui.PanelEndGame, true);
        }
    }
    private void OnRevival()
    {
        gameObject.SetActive(true);
    }
}
