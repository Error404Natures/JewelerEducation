using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BurnerController : MonoBehaviour
{
    [Header("����� ���������")]
    public AudioSource fireAudio;
    public ParticleSystem fireParticles;
    public SteamVR_Action_Boolean action;
    public SteamVR_Input_Sources input_Sources = SteamVR_Input_Sources.Any;

    [Header("������� ��������� �������")]
    public WarmingIngotmold warmingIngotmold;       //������ �������� ���������
    public WarmingTigel warmingTigel;               //������ �������� ������
    public WarmingAlloy warmingAlloy;               //������ ��������� �������
    public AnnealingMetal annealingMetal;           //������ ��������� �������
    public AnnealingWorkpiece annealingWorkpiece;   //������ ������ ���������
    public WarmingSoldering warmingSoldering;       //������ �������� ������ �� ������
    public AnnealingRing annealingRing;             //������ ���������� ������
    public SolderingRing solderingRing;             //������ �����
    public HeatingCitric heatingCitric;             //������ �������� �������� �������

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
