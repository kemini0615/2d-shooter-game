using System.Collections;
using UnityEngine;

public static class Helper
{
    public static IEnumerator Fade(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float timer = 0f;
        float initialAlpha = canvasGroup.alpha;

        while (timer < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(initialAlpha, targetAlpha, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
