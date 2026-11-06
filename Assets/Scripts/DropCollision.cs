using UnityEngine;

public class DropCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Beaker"))
        {
            Destroy(gameObject);
        }
    }
}