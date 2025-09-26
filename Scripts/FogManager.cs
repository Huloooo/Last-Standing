using UnityEngine;

public class FogManager : MonoBehaviour
{
    [Header("Fog Settings")]
    public bool enableFog = true;
    public FogMode fogMode = FogMode.Exponential; 
    public Color fogColor = Color.gray;
    [Range(0.0f, 1.0f)] public float fogDensity = 0.02f;
    public float fogStart = 0f;   // Only used in Linear
    public float fogEnd = 50f;    // Only used in Linear

    void Start()
    {
        RenderSettings.fog = enableFog;
        RenderSettings.fogMode = fogMode;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = fogDensity;

        if (fogMode == FogMode.Linear)
        {
            RenderSettings.fogStartDistance = fogStart;
            RenderSettings.fogEndDistance = fogEnd;
        }
    }
}
