using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BurnerController : MonoBehaviour
{
    [Header("Общие настройки")]
    public AudioSource fireAudio;
    public ParticleSystem fireParticles;
    public SteamVR_Action_Boolean action;
    public SteamVR_Input_Sources input_Sources = SteamVR_Input_Sources.Any;

    [Header("Скрипты процессов нагрева")]
    public WarmingIngotmold warmingIngotmold;       //Скрипт прогрева изложницы
    public WarmingTigel warmingTigel;               //Скрипт прогрева тигеля
    public WarmingAlloy warmingAlloy;               //Скрипт плавления металла
    public AnnealingMetal annealingMetal;           //Скрипт отжигания металла
    public AnnealingWorkpiece annealingWorkpiece;   //Скрипт отжига заготовки
    public WarmingSoldering warmingSoldering;       //Скрипт прогрева припоя до шарика
    public AnnealingRing annealingRing;             //Скрипт нагревания кольцы
    public SolderingRing solderingRing;             //Скрипт пайки
    public HeatingCitric heatingCitric;             //Скрипт прогрева лимонной кислоты

    public static bool isHeating;

    private Interactable interactable;
    private Hand attachedHand;

    private void Start()
    {
        fireParticles.Stop();
        interactable = GetComponent<Interactable>();
    }
    private void Update()
    {
        attachedHand = interactable.attachedToHand;
        bool isHeld = attachedHand != null;

        if (isHeld && action.GetState(input_Sources))
        {
            isHeating = true;
        }
        else
        {
            isHeating = false;
        }
        HandleAudio();
    }

    private void HandleAudio()
    {
        if (isHeating && !fireAudio.isPlaying)
        {
            fireAudio.Play();
            fireParticles.Play();
        }
        else if (!isHeating && fireAudio.isPlaying)
        {
            fireAudio.Stop();
            fireParticles.Stop();
        }
    }
}
