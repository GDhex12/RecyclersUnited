using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    private float shrinkTime = 0.5f;
    //private float currentTime = 0.0f;
    private Vector3 originalScale;


    private void Start()
    {
        originalScale = transform.localScale;
    }

	private void Update()
	{
		
	}

	public void PickUp()
	{
        StartCoroutine(AnimatePickup());
	}

    private IEnumerator AnimatePickup()
    {

        float currentTime = 0.0f;
        Vector3 initialScale = transform.localScale;

        while (currentTime < shrinkTime)
        {
            currentTime += Time.deltaTime;

            // Calculate the new scale of the object based on the current time
            float t = currentTime / shrinkTime;
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);

            yield return null;
        }

        // Set the final scale to zero
        transform.localScale = Vector3.zero;

        // Destroy the object
        
        gameObject.transform.parent.gameObject.SetActive(false);
        transform.localScale = originalScale;
        yield return null;
    }
}
