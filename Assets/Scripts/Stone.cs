using UnityEngine;
using UnityEngine.Rendering;

public class Stone : MonoBehaviour
{
    public Inventory playerInventory;
    public GameObject explosionEffect;
    public Transform player;
    public float triggerDistance = 5.0f;
    private bool hasExploded = false;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (player == null || hasExploded)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= triggerDistance)
        {
            if (playerInventory == null)
            {
                return;
            }

            if (playerInventory.HasItem("Matches") && playerInventory.HasItem("Dynamite"))
            {
                audioManager.PlaySFX(audioManager.Audio_Explosion);
                hasExploded = true;
                Debug.Log("Stone destroyed");

                playerInventory.RemoveItem("Matches");
                playerInventory.RemoveItem("Dynamite");

                if (explosionEffect)
                {
                    GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    Destroy(explosion, 3f);
                }

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("You need match and dynamite to blow up the stone");
            }
        }
    }
}
