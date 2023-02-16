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
                if(player.GetComponent<MoveController>().Health < player.GetComponent<MoveController>().MaxHP)
                {
                    player.GetComponent<MoveController>().Health++;
                }   
                break;

            case ItemType.BombItem:
                player.GetComponent<BombController>().AddBomb();
                break;

            case ItemType.FlameItem:
                if (player.GetComponent<BombController>().ExplosionRadius < player.GetComponent<BombController>().MaxRadius)
                {
                    player.GetComponent<BombController>().ExplosionRadius++;
                }
                break;

            case ItemType.SpeedItem:
                if (player.GetComponent<MoveController>().Speed < player.GetComponent<MoveController>().MaxSpeed)
                {
                    player.GetComponent<MoveController>().Speed += 1;
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
