using UnityEngine;

public class RingController : MonoBehaviour
{
    [Header("Зазор кольца")]
    public Transform gapStart;
    public Transform gapEnd;

    [Header("Начальная позиция")]
    private Vector3 originalGapPosition;

    private void Start()
    {
        originalGapPosition = gapEnd.position;
    }

    public void CloseGap(float progress)
    {
        gapEnd.position = Vector3.Lerp(
            originalGapPosition,
            gapStart.position,
            progress
        );
    }
}