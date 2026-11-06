using UnityEngine;

public class BeakerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Table"))
        {
            Debug.Log("Les béchers touchent la table !");
        }
    }
}