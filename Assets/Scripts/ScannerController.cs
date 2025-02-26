using System.Collections;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class ScannerController
{
    public GameObject scanner;
    public float targetHeight = 0.65f;
    public float liftSpeed = 2.4f;

    public ScannerController(GameObject scanner)
    {
        this.scanner = scanner;
    }

    public void ActivateScanner()
    {
        scanner = FindInactiveObject("ScannerHold");

        if (scanner == null)
        {
            Debug.LogError("ScannerHold not found in the scene (even if inactive)!");
            return;
        }

        scanner.SetActive(true);
        CoroutineRunner.instance.StartCoroutine(LiftScanner());
    }

    private GameObject FindInactiveObject(string objectName)
    {
        return Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(obj => obj.name == objectName);
    }

    private IEnumerator LiftScanner()
    {
        bool hasParent = scanner.transform.parent != null;

        Vector3 startPosition = hasParent ? scanner.transform.localPosition : scanner.transform.position;

        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y + targetHeight, startPosition.z);

        Debug.Log($"Start Position: {startPosition}");
        Debug.Log($"Target Position: {targetPosition}");

        float elapsedTime = 0;
        while (elapsedTime < targetHeight / liftSpeed)
        {
            float t = elapsedTime / (targetHeight / liftSpeed);

            if (hasParent)
                scanner.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            else
                scanner.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (hasParent)
            scanner.transform.localPosition = targetPosition;
        else
            scanner.transform.position = targetPosition;

        Debug.Log($"Final Position: {scanner.transform.position}");
    }

}
