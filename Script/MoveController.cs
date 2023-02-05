using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveController : MonoBehaviour
{
    public int maxHP = 5;
    public float maxSpeed = 6.5f;
    public bool isBoss = false;
    public Rigidbody2D rigibody { get; private set; }
    private Vector2 direction = Vector2.down;
    public Text numHP;
    public float speed = 5f;
    public int HP = 1;
    
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public AnimatedSprite spriteRendererUp;
    public AnimatedSprite spriteRendererDown;
    public AnimatedSprite spriteRendererLeft;
    public AnimatedSprite spriteRendererRight;
    public AnimatedSprite spriteRendererRevival;
    public AnimatedSprite spriteRendererDeath;
    private AnimatedSprite activeSpriteRenderer;

    private UI ui;
    private AudioSource audioSource;
    private SoundManage sound;
    private void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>().GetComponent<AudioSource>();
        sound = FindObjectOfType<SoundManage>();
        ui = FindObjectOfType<UI>();
        rigibody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    private void Update()
    {
        numHP.text = HP.ToString();
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
    public void SetDirection(Vector2 nDirection, AnimatedSprite animatedSprite)
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
                numHP.text = HP.ToString();
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
                numHP.text = "0";
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
        audioSource.clip = sound.audioDeath;
        audioSource.Play();
        gameObject.SetActive(false);
        if (isBoss)
        {
            ui.ShowPanel(ui.PanelLose, true);
            Time.timeScale = 0;
        }
        else
        {
            if(FindObjectOfType<GameManager>().CheckWinWithPlayer() == "P1")
            {
                ui.ShowPanel(ui.PanelWin, true);
            }
            else
            {
                ui.ShowPanel(ui.PanelLose, true);
            }
        }
    }
    private void OnRevival()
    {
        gameObject.SetActive(true);
    }
}
