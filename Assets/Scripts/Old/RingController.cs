using UnityEngine;

public class RingController : MonoBehaviour
{
    [Header("����� ������")]
    public Transform gapStart;
    public Transform gapEnd;

    [Header("��������� �������")]
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