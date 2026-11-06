
using UnityEngine;

public class BeakerController : MonoBehaviour
{
    public Renderer liquidRenderer;
    private Color originalColor;

    private void Start()
    {
        if (liquidRenderer != null)
        {
            originalColor = liquidRenderer.material.color;
        }
    }

    public void ChangeLiquidColor(Color newColor)
    {
        if (liquidRenderer != null)
        {
            liquidRenderer.material.color = newColor;
        }
    }

    public void ResetLiquid()
    {
        if (liquidRenderer != null)
        {
            liquidRenderer.material.color = originalColor;
        }
    }
}
