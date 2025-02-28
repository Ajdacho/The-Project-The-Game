using System.Collections;
using UnityEngine;
using TMPro;

public class Stone : MonoBehaviour
{
    public Inventory playerInventory;
    public GameObject explosionEffect;
    public Transform player;
    public float triggerDistance = 5.0f;
    private bool hasExploded = false;
    AudioManager audioManager;
    public TextMeshPro messageText;

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

                StartCoroutine(DelayedExplosion());

                hasExploded = true;
                Debug.Log("Stone destroyed");
            }
            else
            {
                Debug.Log("You need match and dynamite to blow up the stone");

                if (messageText != null)
                {
                    messageText.gameObject.SetActive(true);
                    messageText.text = "You need match and dynamite to blow up the stone!";
                }
            }
        }
        else
        {
            if (messageText != null)
            {
                messageText.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator DelayedExplosion()
    {
        yield return new WaitForSeconds(1f);

        playerInventory.RemoveItem("Matches");
        playerInventory.RemoveItem("Dynamite");

        if (explosionEffect)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 3f);
        }

        Destroy(gameObject);
    }
}
