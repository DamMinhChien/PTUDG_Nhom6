using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bar : MonoBehaviour
{
    public Image HpBar;
    public float smoothSpeed = 2f; // T?c ?? gi?m máu

    public void UpdateHpBar(int currentHP, int maxHP)
    {
        float targetFill = (float)currentHP / maxHP;
        StopAllCoroutines(); // D?ng coroutine tr??c ?ó n?u có
        StartCoroutine(SmoothFill(targetFill));
    }

    IEnumerator SmoothFill(float target)
    {
        float start = HpBar.fillAmount;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * smoothSpeed;
            HpBar.fillAmount = Mathf.Lerp(start, target, t);
            yield return null;
        }

        HpBar.fillAmount = target; // ??m b?o chính xác 100%
    }
}
