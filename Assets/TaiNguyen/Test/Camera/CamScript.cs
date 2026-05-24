using UnityEngine;

public class CamScript : MonoBehaviour
{
    public float duration = 0.1f;   // thời gian rung
    public float magnitude = 0.1f;  // độ mạnh rung

    private Vector3 originalPos;

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine());
    }

    System.Collections.IEnumerator ShakeCoroutine()
    {
        originalPos = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
