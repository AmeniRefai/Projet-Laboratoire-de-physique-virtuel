using UnityEngine;

public class LabManager : MonoBehaviour
{
    public GameObject blueBeaker;
    public GameObject redBeaker;
    public GameObject yellowBeaker;
    public GameObject resultLiquid;

    private Renderer resultRenderer;
    private Color initialResultColor;
    private bool hasInitialResultColor;

    private void Awake()
    {
        if (resultLiquid == null)
            return;

        resultRenderer = resultLiquid.GetComponent<Renderer>();
        if (resultRenderer != null)
        {
            initialResultColor = resultRenderer.material.color;
            hasInitialResultColor = true;
        }
    }

    public void MixBlueRed()
    {
        SetResultColor(Color.magenta);
    }

    public void MixBlueYellow()
    {
        SetResultColor(Color.green);
    }

    public void MixRedYellow()
    {
        SetResultColor(new Color(1f, 0.5f, 0f));
    }

    public void ResetExperiment()
    {
        if (!TryGetResultRenderer(out Renderer renderer))
            return;

        if (hasInitialResultColor)
            renderer.material.color = initialResultColor;
    }

    private void SetResultColor(Color color)
    {
        if (!TryGetResultRenderer(out Renderer renderer))
            return;

        renderer.material.color = color;
    }

    private bool TryGetResultRenderer(out Renderer renderer)
    {
        renderer = resultRenderer;

        if (resultLiquid == null)
        {
            Debug.LogError("Result Liquid manquant !");
            return false;
        }

        if (renderer == null)
        {
            renderer = resultLiquid.GetComponent<Renderer>();
            resultRenderer = renderer;
        }

        if (renderer == null)
        {
            Debug.LogError("Result Liquid n'a pas de Renderer !");
            return false;
        }

        if (!hasInitialResultColor)
        {
            initialResultColor = renderer.material.color;
            hasInitialResultColor = true;
        }

        return true;
    }
}
