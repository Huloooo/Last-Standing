using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    [Header("Flash Settings")]
    public Image flashImage;            // drag the DamageFlashPanel's Image here
    public float flashDuration = 0.3f;  // time until it fades away
    public float maxAlpha = 0.6f;       // how red it gets

    private float flashTimer = 0f;
    private bool isFlashing = false;

    void Start()
    {
        if (flashImage != null)
        {
            // Start fully transparent
            flashImage.color = new Color(1f, 0f, 0f, 0f);
        }
    }

    void Update()
    {
        if (isFlashing && flashImage != null)
        {
            flashTimer += Time.unscaledDeltaTime; // unscaled so it works even if Time.timeScale = 0

            float t = flashTimer / flashDuration;
            float alpha = Mathf.Lerp(maxAlpha, 0f, t);

            flashImage.color = new Color(1f, 0f, 0f, alpha);

            if (t >= 1f)
            {
                // end flash
                isFlashing = false;
                flashTimer = 0f;
                flashImage.color = new Color(1f, 0f, 0f, 0f); // transparent
            }
        }
    }

    public void TriggerFlash()
    {
        if (flashImage == null) return;

        flashImage.color = new Color(1f, 0f, 0f, maxAlpha);
        isFlashing = true;
        flashTimer = 0f;
    }
}
