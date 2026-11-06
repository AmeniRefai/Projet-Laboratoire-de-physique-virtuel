using UnityEngine;
using UnityEngine.AI;

public class RobotMixingController : MonoBehaviour
{
    [Header("Robot")]
    public NavMeshAgent agent;

    [Header("Mixing")]
    public Transform mixingPoint;

    [Header("Result")]
    public GameObject resultBeaker;

    // État du mélange
    private string selectedMix = "";
    private bool isMixing = false;

    // Position initiale du robot
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Couleur initiale du liquide
    private Color initialLiquidColor = Color.white;


    // =========================================================
    // START
    // =========================================================

    private void Start()
    {
        // Sauvegarder la position initiale du robot
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Sauvegarder la couleur initiale du liquide
        if (resultBeaker != null)
        {
            Transform liquid = resultBeaker.transform.Find("Liquid_Result");

            if (liquid != null)
            {
                Renderer rend = liquid.GetComponent<Renderer>();

                if (rend != null)
                {
                    initialLiquidColor = rend.material.color;
                }
            }
        }
    }


    // =========================================================
    // BOUTON : BLEU + ROUGE
    // =========================================================

    public void StartBlueRedMix()
    {
        selectedMix = "BlueRed";

        Debug.Log("Mélange Bleu + Rouge sélectionné");

        StartRobot();
    }


    // =========================================================
    // BOUTON : BLEU + JAUNE
    // =========================================================

    public void StartBlueYellowMix()
    {
        selectedMix = "BlueYellow";

        Debug.Log("Mélange Bleu + Jaune sélectionné");

        StartRobot();
    }


    // =========================================================
    // BOUTON : ROUGE + JAUNE
    // =========================================================

    public void StartRedYellowMix()
    {
        selectedMix = "RedYellow";

        Debug.Log("Mélange Rouge + Jaune sélectionné");

        StartRobot();
    }


    // =========================================================
    // DÉMARRER LE ROBOT
    // =========================================================

    private void StartRobot()
    {
        if (agent == null)
        {
            Debug.LogError("❌ NavMeshAgent manquant !");
            return;
        }

        if (mixingPoint == null)
        {
            Debug.LogError("❌ MixingPoint manquant !");
            return;
        }

        // Si le robot était déjà en train de travailler
        isMixing = true;

        // Réactiver le robot
        agent.isStopped = false;

        // Aller vers MixingPoint
        agent.SetDestination(mixingPoint.position);

        Debug.Log("🤖 Robot se dirige vers le point de mélange...");
    }


    // =========================================================
    // UPDATE
    // =========================================================

    private void Update()
    {
        if (!isMixing)
            return;

        if (agent == null)
            return;

        // Vérifier que le robot est arrivé
        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f)
            {
                MakeMix();
            }
        }
    }


    // =========================================================
    // EFFECTUER LE MÉLANGE
    // =========================================================

    private void MakeMix()
    {
        if (resultBeaker == null)
        {
            Debug.LogError("❌ Beaker_Result manquant !");
            isMixing = false;
            return;
        }

        // Chercher Liquid_Result à l'intérieur de Beaker_Result
        Transform liquid = resultBeaker.transform.Find("Liquid_Result");

        if (liquid == null)
        {
            Debug.LogError(
                "❌ Liquid_Result introuvable dans Beaker_Result !"
            );

            isMixing = false;
            return;
        }

        Renderer rend = liquid.GetComponent<Renderer>();

        if (rend == null)
        {
            Debug.LogError(
                "❌ Liquid_Result n'a pas de Renderer !"
            );

            isMixing = false;
            return;
        }


        // =====================================================
        // BLEU + ROUGE = VIOLET
        // =====================================================

        if (selectedMix == "BlueRed")
        {
            rend.material.color = Color.magenta;

            Debug.Log("🟣 Bleu + Rouge = Violet");
        }


        // =====================================================
        // BLEU + JAUNE = VERT
        // =====================================================

        else if (selectedMix == "BlueYellow")
        {
            rend.material.color = Color.green;

            Debug.Log("🟢 Bleu + Jaune = Vert");
        }


        // =====================================================
        // ROUGE + JAUNE = ORANGE
        // =====================================================

        else if (selectedMix == "RedYellow")
        {
            rend.material.color = new Color(1f, 0.5f, 0f);

            Debug.Log("🟠 Rouge + Jaune = Orange");
        }


        // Mélange terminé
        isMixing = false;

        selectedMix = "";

        // Arrêter le robot
        agent.isStopped = true;

        Debug.Log("✅ Mélange terminé !");
    }


    // =========================================================
    // RESET COMPLET
    // =========================================================

    public void ResetRobot()
    {
        Debug.Log("🔄 Reset du robot et du mélange");


        // -----------------------------------------------------
        // 1. Arrêter le mélange
        // -----------------------------------------------------

        isMixing = false;

        selectedMix = "";


        // -----------------------------------------------------
        // 2. Arrêter le NavMeshAgent
        // -----------------------------------------------------

        if (agent != null)
        {
            agent.isStopped = true;

            agent.ResetPath();

            agent.velocity = Vector3.zero;
        }


        // -----------------------------------------------------
        // 3. Remettre le robot à sa position initiale
        // -----------------------------------------------------

        transform.position = initialPosition;

        transform.rotation = initialRotation;


        // -----------------------------------------------------
        // 4. Remettre le robot en état de déplacement
        // -----------------------------------------------------

        if (agent != null)
        {
            agent.Warp(initialPosition);

            agent.isStopped = false;
        }


        // -----------------------------------------------------
        // 5. Remettre le liquide à sa couleur initiale
        // -----------------------------------------------------

        if (resultBeaker != null)
        {
            Transform liquid =
                resultBeaker.transform.Find("Liquid_Result");

            if (liquid != null)
            {
                Renderer rend =
                    liquid.GetComponent<Renderer>();

                if (rend != null)
                {
                    rend.material.color =
                        initialLiquidColor;
                }
            }
        }


        Debug.Log("✅ Reset terminé !");
    }
}