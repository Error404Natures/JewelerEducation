using UnityEngine;

public class SolderController : MonoBehaviour
{
    public SolderingProcess solderingProcess; // Ссылка на процесс пайки
    public bool isInSolderingZone;

    private Rigidbody rb;
    private Vector3 originalScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == solderingProcess.targetObject)
        {
            isInSolderingZone = true;
            solderingProcess.SetSolderPiece(gameObject);

            // Фиксируем припой в зоне
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Позиционируем припой точно в зоне
            transform.position = other.transform.position;
        }
    }
}