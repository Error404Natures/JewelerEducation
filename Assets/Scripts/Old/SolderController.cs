using UnityEngine;

public class SolderController : MonoBehaviour
{
    public SolderingProcess solderingProcess; // ������ �� ������� �����
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

            // ��������� ������ � ����
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // ������������� ������ ����� � ����
            transform.position = other.transform.position;
        }
    }
}