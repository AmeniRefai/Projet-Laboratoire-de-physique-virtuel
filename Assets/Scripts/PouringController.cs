using UnityEngine;

public class PouringController : MonoBehaviour
{
    public GameObject dropPrefab; 
    public Transform dropPosition;

    public void CreateDrop()
    {
        Instantiate(
            dropPrefab, // prefab to instantiate
            dropPosition.position, // position to instantiate at 
            dropPosition.rotation // rotation to instantiate with
        );
    }
}