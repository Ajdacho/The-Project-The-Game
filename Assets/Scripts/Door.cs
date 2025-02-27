using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform cube006;
    public Transform cube008;
    public Inventory playerInventory;
    public float rotationAngle = -90f;
    public float rotationSpeed = 1f;
    public float detectionRange = 5f;
    private bool isOpen = false;

    private void Update()
    {
        if (!isOpen && PlayerInRange() && playerInventory.HasItem("Key"))
        {
            StartCoroutine(OpenDoor());
            isOpen = true;
        }
    }

    private bool PlayerInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= detectionRange;
        }
        return false;
    }

    private IEnumerator OpenDoor()
    {
        Quaternion startRotationCube006 = cube006.rotation;
        Quaternion startRotationCube008 = cube008.rotation;
        Quaternion endRotationCube006 = Quaternion.Euler(0, rotationAngle, 0) * startRotationCube006;
        Quaternion endRotationCube008 = Quaternion.Euler(0, rotationAngle, 0) * startRotationCube008;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * rotationSpeed;
            cube006.rotation = Quaternion.Slerp(startRotationCube006, endRotationCube006, time);
            cube008.rotation = Quaternion.Slerp(startRotationCube008, endRotationCube008, time);
            yield return null;
        }
    }
}