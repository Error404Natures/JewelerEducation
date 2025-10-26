using UnityEngine;

public class SolderingProcess : HeatProcess
{
    [Header("Настройки пайки")]
    public float solderingTime = 8f;
    public GameObject solderPiece; // Кусочек припоя
    public ParticleSystem meltingEffect; // Эффект плавления
    public ParticleSystem sparkEffect; // Эффект завершения

    [Header("Визуальные эффекты")]
    public Material zoneHeatingMaterial;
    private Material originalZoneMaterial;
    private Renderer zoneRenderer;

    [Header("Кольцо")]
    public RingController ringController;

    public override void Initialize()
    {
        base.Initialize();
        zoneRenderer = targetObject.GetComponent<Renderer>();
        originalZoneMaterial = zoneRenderer.material;
    }

    public bool IsSolderInPlace()
    {
        return solderPiece != null;
    }

    public void SetSolderPiece(GameObject solder)
    {
        solderPiece = solder;
    }

    public override void StartProcess()
    {
        if (solderPiece == null)
        {
            Debug.LogError("SolderingProcess: Припой не установлен!");
            return;
        }

        base.StartProcess();
        zoneRenderer.material = zoneHeatingMaterial;
        meltingEffect.Play();
    }

    public override void UpdateProcess(float deltaTime)
    {
        if (solderPiece == null) return;

        base.UpdateProcess(deltaTime);

        progress += deltaTime;

        // Визуальное изменение припоя
        float meltProgress = Mathf.Clamp01(progress / solderingTime);
        UpdateSolderVisual(meltProgress);

        // Закрытие зазора в кольце
        if (ringController != null)
        {
            ringController.CloseGap(meltProgress);
        }

        if (progress >= solderingTime)
        {
            CompleteProcess();
        }
    }

    private void UpdateSolderVisual(float progress)
    {
        // Уменьшаем припой по мере плавления
        solderPiece.transform.localScale = Vector3.Lerp(
            Vector3.one,
            new Vector3(1f, 0.2f, 1f),
            progress
        );
    }

    protected override void CompleteProcess()
    {
        // Финальные эффекты
        meltingEffect.Stop();
        Instantiate(sparkEffect, solderPiece.transform.position, Quaternion.identity);

        // Удаляем припой (он "вплавился")
        Destroy(solderPiece);

        // Восстанавливаем материал зоны
        zoneRenderer.material = originalZoneMaterial;

        base.CompleteProcess();
    }

    public override void StopProcess()
    {
        base.StopProcess();
        meltingEffect.Stop();

        if (zoneRenderer != null)
        {
            zoneRenderer.material = originalZoneMaterial;
        }
    }
}