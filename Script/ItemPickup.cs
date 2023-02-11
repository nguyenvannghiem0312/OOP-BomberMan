using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private SoundManage sound;
    private void Awake()
    {
        sound = FindObjectOfType<SoundManage>();
    }
    public enum ItemType
    {
        TymItem,
        BombItem,
        FlameItem,
        SpeedItem,
    }

    public ItemType type;

    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.TymItem:
                if(player.GetComponent<MoveController>().HP < player.GetComponent<MoveController>().maxHP)
                {
                    player.GetComponent<MoveController>().HP++;
                }   
                break;

            case ItemType.BombItem:
                player.GetComponent<BombController>().AddBomb();
                break;

            case ItemType.FlameItem:
                if (player.GetComponent<BombController>().explosionRadius < player.GetComponent<BombController>().maxRadius)
                {
                    player.GetComponent<BombController>().explosionRadius++;
                }
                break;

            case ItemType.SpeedItem:
                if (player.GetComponent<MoveController>().speed < player.GetComponent<MoveController>().maxSpeed)
                {
                    player.GetComponent<MoveController>().speed += 0.5f;
                }
                break;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            sound.PlayAudioClip(sound.audioItem);
            OnItemPickup(other.gameObject);
        }
    }

}
