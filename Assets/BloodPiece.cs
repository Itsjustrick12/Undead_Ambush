using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPiece : MonoBehaviour
{
    private BloodTrail trail;
    private float destroyDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOutBlood(this.gameObject));
    }

    public void SetTrail(BloodTrail newTrail)
    {
        trail = newTrail;
    }

    public void SetDelay (float delay)
    {
        destroyDelay = delay;
    }

    private IEnumerator FadeOutBlood(GameObject bloodInstance)
    {
        SpriteRenderer spriteRenderer = bloodInstance.GetComponent<SpriteRenderer>();

        Color startColor = spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsedTime = 0f;
        float fadeDuration = destroyDelay;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / fadeDuration;
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }

        trail.bloodInstances.Remove(this.gameObject);
        Destroy(bloodInstance);
    }
}
