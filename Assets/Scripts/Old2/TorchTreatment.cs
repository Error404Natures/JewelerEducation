using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;

public class TorchTreatment : MonoBehaviour
{
    public StateManager stateManager;
    public event Action OnMeltingComplete;
    public event Action OnSolderingComplete;

    private Animator silverAnimator;
    public AudioSource fire;
    public AudioClip fireClip;
    //public ParticleSystem particleSystem;
    private ParticleSystem silverParcticle;

    public SteamVR_Action_Boolean action;
    public SteamVR_Input_Sources inputSources = SteamVR_Input_Sources.Any;

    public GameObject tigel;
    public GameObject silver;

    public GameObject zoneSoldering;
    public GameObject Ring;
    public GameObject tweezers;
    public GameObject silverSoldering;

    public float timerProcessint = 20f;
    public float timerSoldering = 7.5f;
    public float timerMakingStone = 6.5f;
    public bool isPlay = false;
    public bool wasPlaying = false;
    public bool isMettlingComplete = false;

    public Transform[] makingStonePoint;
    public int counterPoint = 0;

    private void Start()
    {
        //particleSystem.Stop();
        silverAnimator = silver.GetComponent<Animator>();
        fire = GetComponent<AudioSource>();
    }

    private void Update()
    {
        SoundBurnerPlay();
    }

    private void OnTriggerStay(Collider other)
    {
      //  if (other.gameObject == tigel.gameObject 
       //     && action.GetState(inputSources) && !isMettlingComplete
        //   && StateManager.currentState == State.MeltingMetal)
        {
            
            isPlay = true;

            timerProcessint -= Time.deltaTime;

            //particleSystem.Play();
            silverAnimator.SetTrigger("isBurner");

            Debug.Log($"Секунд осталось обработать: {timerProcessint}");

            if (timerProcessint < 0)
            {
                Debug.Log("Обработка завершена");
                OnMeltingComplete?.Invoke();
               // particleSystem.Stop();
                isPlay = false;
                isMettlingComplete = true;
            }
        }
       // else if (other.gameObject == Ring.gameObject == tweezers.gameObject
       //     && action.GetState(inputSources) && isMettlingComplete 
            //&& StateManager.currentState == State.Soldering)
        {
            isPlay = true;

            timerSoldering -= Time.deltaTime;

           // particleSystem.Play();

            if (timerSoldering < 0)
            {
                OnSolderingComplete.Invoke();
                //particleSystem.Stop();
                isPlay = false;
                Destroy(zoneSoldering.gameObject);
                silverSoldering.SetActive(false);
            }
        }
        //else if (other.gameObject == Ring.gameObject == tweezers.gameObject &&
            //action.GetState(inputSources) && StateManager.currentState == State.MakingStoneSetting)
        {
            isPlay = true;
            timerMakingStone -= Time.deltaTime;
            //particleSystem.Play();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == tigel.gameObject)
        {
            //particleSystem.Stop();
            isPlay = false;
        }
        else if (other.gameObject == Ring.gameObject == tweezers.gameObject)
        {
           // particleSystem.Stop();
            isPlay = false;
        }
    }

    public void SoundBurnerPlay()
    {
        // Если нужно играть и звук НЕ играет
        if (isPlay && !fire.isPlaying)
        {
            fire.Play();
            wasPlaying = true;
        }
        // Если НЕ нужно играть и звук ИГРАЕТ
        else if (!isPlay && fire.isPlaying)
        {
            fire.Stop();
            wasPlaying = false;
        }
    }
}
