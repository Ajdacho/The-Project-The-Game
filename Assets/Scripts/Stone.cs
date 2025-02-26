using UnityEngine;

public class Stone : MonoBehaviour
{
    public Inventory playerInventory; 
    //public GameObject explosionEffect;
    public Transform player; 
    public float triggerDistance = 5.0f; 

    private void Update()
    {
        if (player == null)
        {
            Debug.LogError("Brak przypisanego gracza w Stone.cs!");
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
                Debug.Log("Stone destroyed");
                //if (explosionEffect)
                //    Instantiate(explosionEffect, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("You need match and dynamite to blow up the stone");
            }
        }
    }
}
