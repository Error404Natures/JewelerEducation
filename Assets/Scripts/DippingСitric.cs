using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DippingСitric : BaseProcess
{
    private Animator animator;

    private void Start()
    {
        animator = silver.GetComponent<Animator>();
        useTimer = true;
        isEffects = true;
    }

    protected override void CompleteSubState() =>
        StateManager.instance.RollingSubStateComplete();
    

    protected override bool IsCorrectState() => 
        StateManager.rollingSubState == RollingSubState.DippingСitric;

    private void OnTriggerStay(Collider other)
    {
        ProcessTrigger(other);
        animator.SetTrigger("isCold");
    } 

    //тут второй вариант, я сделал лучше
    //{
    //    if (IsCorrectState()) return;
    //    if (isComplete) return;

    //    if (other.gameObject == silver)
    //    {
    //        HandleAudio();

    //        if (TimerProcess())
    //        {
    //            isComplete = true;
    //            CompleteSubState();
    //        }
    //    }
    //}



    //public GameObject meltedSilver;
    //public float timer = 5f;

    //public AudioSource audioSource;
    //public ParticleSystem particle;

    //private Animator animator;
    //private bool isSound = true;

    //private void Start()
    //{
    //    particle = GetComponent<ParticleSystem>();
    //    audioSource = GetComponent<AudioSource>();
    //    particle.Stop();
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject == meltedSilver.gameObject &&
    //        StateManager.rollingSubState == RollingSubState.DippingСitric)
    //    {
    //        timer -= Time.deltaTime;

    //        animator = meltedSilver.GetComponent<Animator>();
    //        animator.SetTrigger("isCold");

    //        Debug.Log($"До конца остывания осталось: {timer.ToString("F2")}");

    //        if (timer <= 0)
    //        {
    //            StateManager.instance.RollingSubStateComplete();
    //            isSound = false;
    //        }

    //        HandleAudio();
    //    }
    //    else
    //    {
    //        isSound = false;
    //        HandleAudio();
    //    }
    //}
    //private void HandleAudio()
    //{
    //    if (isSound && !audioSource.isPlaying)
    //    {
    //        particle.Play();
    //        audioSource.Play();
    //    }
    //    else if (!isSound && audioSource.isPlaying)
    //    {
    //        particle.Stop();
    //        audioSource.Stop();
    //    }
    //}
}
