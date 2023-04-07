using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    public float scaleMultiplier = 2.0f; // The amount to multiply the original scale by when the power-up is picked up
    public float animationDuration = 0.5f; // The duration of the animation in seconds
    public float alphaSpeed = 2f;
    private Vector3 originalScale; // The original scale of the power-up

    

    private void Start()
    {
        originalScale = transform.localScale;
    }

   public void PickUp()
	{
        StartCoroutine(AnimatePickup());
	}

    private IEnumerator AnimatePickup()
    {
        float elapsedTime = 0.0f;
        Vector3 targetScale = originalScale * scaleMultiplier;
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        while (color.a > 0f)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / animationDuration);
            color.a -= alphaSpeed * Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
        gameObject.transform.parent.gameObject.SetActive(false);
        color.a = 1;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
