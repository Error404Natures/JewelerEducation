using UnityEngine;
using Valve.VR;

public class TorchController1 : MonoBehaviour
{
    [Header("Общие настройки")]
    public AudioSource fireAudio;
    //public ParticleSystem flameParticles;
    public SteamVR_Action_Boolean action;
    public SteamVR_Input_Sources inputSources = SteamVR_Input_Sources.Any;

    [Header("Процессы нагрева")]
    public HeatProcess[] heatProcesses;

    private HeatProcess activeProcess;
    private bool isHeating;

    private void Start()
    {
        //flameParticles.Stop();
        foreach (var process in heatProcesses)
        {
            process.Initialize();
        }
    }

    private void Update()
    {
        HandleAudio();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!action.GetState(inputSources)) return;

        foreach (var process in heatProcesses)
        {
              //if (process.CanProcess(other.gameObject) &&
              // StateManager.currentState == process.requiredState)
            {
                // Дополнительная проверка для пайки
                if (process is SolderingProcess)
                {
                    var sp = process as SolderingProcess;
                    if (!sp.IsSolderInPlace()) continue;
                }

                if (activeProcess == null || activeProcess != process)
                {
                    StartProcess(process);
                }

                process.UpdateProcess(Time.deltaTime);
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (activeProcess != null && activeProcess.targetObject == other.gameObject)
        {
            StopProcess();
        }
    }

    private void StartProcess(HeatProcess process)
    {
        StopProcess();
        activeProcess = process;
        isHeating = true;

        //flameParticles.Play();
        fireAudio.Play();
        activeProcess.StartProcess();
    }

    private void StopProcess()
    {
        if (activeProcess == null) return;

        isHeating = false;
       // flameParticles.Stop();
        fireAudio.Stop();
        activeProcess.StopProcess();
        activeProcess = null;
    }

    private void HandleAudio()
    {
        if (isHeating && !fireAudio.isPlaying)
        {
            fireAudio.Play();
        }
        else if (!isHeating && fireAudio.isPlaying)
        {
            fireAudio.Stop();
        }
    }
}