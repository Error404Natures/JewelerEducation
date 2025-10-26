using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningSeams : MonoBehaviour
{
    public GameObject ring;
    public Transform pointFirstX;
    public Transform pointTwoX;

    public GameObject processingArea;

    public int counterProcessing = 0;
    public int requiredQuantity = 5;

    public float delay = 2f;

    private Rigidbody rb;
    private bool isProcessing = false;
    private bool canProcessAgain = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StateManager.OnMainStateChanged += OnMainStateChanged;
    }

    private void OnMainStateChanged(MainState mainState)
    {
        if (StateManager.currentMainState == MainState.Grinding)
        {
            processingArea.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == ring && CurrentState() && canProcessAgain && !isProcessing)
        {
            float totalDistance = Vector3.Distance(pointFirstX.position, pointTwoX.position);

            float currentDistance = Vector3.Distance(pointFirstX.position, ring.transform.position);

            float progress = currentDistance / totalDistance;

            Debug.Log($"Общая длина: {totalDistance}, Пройдено: {currentDistance}, Прогресс: {progress:F2}");

            if (progress >= 0.75f)
            {
                StartCoroutine(ProcessSeam());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ring)
        {
            isProcessing = false;
        }
    }

    private IEnumerator ProcessSeam()
    {
        isProcessing = true;
        canProcessAgain = false;

        counterProcessing++;
        Debug.Log($"Обработано швов: {counterProcessing}/{requiredQuantity}");

        EndCheck();

        yield return new WaitForSeconds(delay);

        canProcessAgain = true;
        isProcessing = false;
    }

    private void EndCheck()
    {
        if (counterProcessing >= requiredQuantity)
        {
            Debug.Log("швы обработаны");
            StateManager.instance.GrindingSubStateComplete();
        }
    }

    private bool CurrentState() => StateManager.grindingSubState == GrindingSubState.CleaningOfSeams;
}