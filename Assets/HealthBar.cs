using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private float timeToDrain = 0.25f;
    [SerializeField] private Gradient healthBarGradient;
    private Image image;
    private Color newHealthBarColor;
    private float targetFillAmount = 1f;
    private Coroutine drainHealthBarCoroutine;
    void Start()
    {
        image = GetComponent<Image>();
        image.color = healthBarGradient.Evaluate(targetFillAmount);
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        targetFillAmount = currentHealth / maxHealth;
        drainHealthBarCoroutine = StartCoroutine(DrainHealthBar());
        CheckHealthBarGradient();
    }

    private IEnumerator DrainHealthBar()
    {
        float fillAmountStart = image.fillAmount;
        Color currentColor = image.color;
        
        float elapsedTime = 0f;
        while (elapsedTime < timeToDrain)
        {
            elapsedTime += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(fillAmountStart, targetFillAmount, elapsedTime / timeToDrain);
            image.color = Color.Lerp(currentColor, newHealthBarColor, elapsedTime / timeToDrain);
            yield return null;
        }
    }

    private void CheckHealthBarGradient()
    {
        newHealthBarColor = healthBarGradient.Evaluate(targetFillAmount);
    }
}
