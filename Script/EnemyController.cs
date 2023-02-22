using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigibody { get; private set; }

    [Header("Direction")]
    [SerializeField] private Vector2 directionStart = Vector2.down;
    [SerializeField] private Vector2 directionEnd = Vector2.up;

    [Header("Speed")]
    [SerializeField] private float speed = 2f;
    
    [Header("Animation")]
    [SerializeField] private AnimatedSprite spriteRendererStart;
    [SerializeField] private AnimatedSprite spriteRendererEnd;
    [SerializeField] private AnimatedSprite spriteRendererDeath;
    private AnimatedSprite activeSpriteRenderer;

    private bool isEnemy;
    private SoundManage sound;
    private UI ui;
    private void Awake()
    {
        ui = FindObjectOfType<UI>();
        sound = FindObjectOfType<SoundManage>();

        rigibody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererStart;
        SetDirection(directionStart, spriteRendererEnd); 
    }
    private void FixedUpdate()
    {
        SetDirection(directionStart, spriteRendererStart);
        Vector2 position = rigibody.position;
        Vector2 translation = directionStart * speed * Time.deltaTime;
        rigibody.MovePosition(position + translation);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 directionChange = directionStart;
        directionStart = directionEnd;
        directionEnd = directionChange;

        AnimatedSprite spriteRendererChange = spriteRendererStart;
        spriteRendererStart = spriteRendererEnd;
        spriteRendererEnd = spriteRendererChange;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            isEnemy = true;
            Death();
        }
    }
    private void SetDirection(Vector2 nDirection, AnimatedSprite animatedSprite)
    {
        directionStart = nDirection;

        spriteRendererStart.enabled = animatedSprite == spriteRendererStart;
        spriteRendererEnd.enabled = animatedSprite == spriteRendererEnd;
        
        
        activeSpriteRenderer = animatedSprite;
        activeSpriteRenderer.idle = directionStart == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Death();  
        }
    }
    private void Death()
    {
        enabled = false;

        spriteRendererStart.enabled = false;
        spriteRendererEnd.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeath), 1.25f);
    }
    private void OnDeath()
    {
        if (!isEnemy)
        {
            IncreScore();
        }
        else
        {
            isEnemy = false;
        }

        gameObject.SetActive(false);
    }
    private void IncreScore()
    {
        if (gameObject.GetComponent<EnemyController>().speed == 2)
        {
            ui.SetScore(10);
        }
        else if(gameObject.GetComponent<EnemyController>().speed == 5)
        {
            ui.SetScore(20);
        }
        else if(gameObject.GetComponent<EnemyController>().speed == 7)
        {
            ui.SetScore(30);
        }
        else if (gameObject.GetComponent<EnemyController>().speed == 12)
        {
            ui.SetScore(40);
        }
        else if(gameObject.GetComponent<EnemyController>().speed == 15)
        {
            ui.SetScore(50);
        }
    }
}
