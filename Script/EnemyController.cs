using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigibody { get; private set; }
    public Vector2 directionStart = Vector2.down;
    public Vector2 directionEnd = Vector2.up;
    public float speed = 2f;
    
    public AnimatedSprite spriteRendererStart;
    public AnimatedSprite spriteRendererEnd;
    public AnimatedSprite spriteRendererDeath;
    private AnimatedSprite activeSpriteRenderer;

    private AudioSource audioSource;
    private SoundManage sound;
    private void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>().GetComponent<AudioSource>();
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
    }
    public void SetDirection(Vector2 nDirection, AnimatedSprite animatedSprite)
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
        audioSource.clip = sound.audioDeath;
        audioSource.Play();
        gameObject.SetActive(false);
        if (FindObjectOfType<GameManager>().CheckWinWithBoss() == true)
        {
            audioSource.clip = sound.audioWin;
            audioSource.Play();

            FindObjectOfType<UI>().ShowPanel(FindObjectOfType<UI>().PanelWin, true);
            Time.timeScale = 0;
        }
    }
}
