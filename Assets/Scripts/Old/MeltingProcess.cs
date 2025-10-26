using UnityEngine;

public class MeltingProcess : HeatProcess
{
    [Header("Настройки плавления")]
    public float meltingTime = 20f;
    public Animator silverAnimator;

    public override void StartProcess()
    {
        base.StartProcess();
        silverAnimator.SetTrigger("isBurner");
    }

    public override void UpdateProcess(float deltaTime)
    {
        base.UpdateProcess(deltaTime);

        progress += deltaTime;

        if (progress >= meltingTime)
        {
            CompleteProcess();
        }
    }

    public override void StopProcess()
    {
        base.StopProcess();
        silverAnimator.ResetTrigger("isBurner");
    }
}