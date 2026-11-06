using UnityEngine;
using TMPro;

public class ExperimentUI : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public void ShowBlueRed()
    {
        messageText.text = "🧪 Expérience réussie !\nBleu + Rouge = Violet";
    }

    public void ShowBlueYellow()
    {
        messageText.text = "🧪 Expérience réussie !\nBleu + Jaune = Vert";
    }

    public void ShowRedYellow()
    {
        messageText.text = "🧪 Expérience réussie !\nRouge + Jaune = Orange";
    }

    public void ResetMessage()
    {
        messageText.text = "Choisissez deux liquides pour commencer l'expérience.";
    }
}