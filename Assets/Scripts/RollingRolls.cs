using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRolls : MonoBehaviour
{
    private Animator animator;

    public float speed = 0.2f;
    private int currentRolling = 0;
    public int totalRolling = 5;

    public Transform contactPoint;
    public Transform endPoint;

    public GameObject meltedSilver;
    public GameObject rolledSilver;

    private Rigidbody silverRb;

    public bool isTouch = false;
    private bool hasCompletedRoll = false; // Флаг для отслеживания завершения прокатки
    private bool isContact = false;

    public AudioSource soundWork;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void IsTouchedHand()
    {
        if (StateManager.rollingSubState == RollingSubState.RollingRolls)
        {
            isTouch = true;
            hasCompletedRoll = false; // Сброс флага при новом касании
            animator.SetTrigger("isWork");
            soundWork.Play();
        }
    }

    public void IsLettingHand()
    {
        if (StateManager.rollingSubState == RollingSubState.RollingRolls)
        {
            isTouch = false;
            animator.SetTrigger("isIdle");
            soundWork.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == meltedSilver.gameObject && currentRolling < totalRolling && !isContact
           && StateManager.rollingSubState == RollingSubState.RollingRolls)
        {
            isContact = true;

            silverRb = meltedSilver.GetComponent<Rigidbody>();
            silverRb.isKinematic = true;

            meltedSilver.transform.SetPositionAndRotation(contactPoint.transform.position, contactPoint.transform.rotation);
            Debug.Log("Ну типо сцепиться должен");
        }
    }

    private void Update()
    {
        ProcessRolling();
    }

    private void ProcessRolling()
    {
        if (isTouch && StateManager.rollingSubState == RollingSubState.RollingRolls && isContact 
           && Vector3.Distance(meltedSilver.transform.position, endPoint.transform.position) > 0.01f)
        {
            meltedSilver.transform.position = Vector3.MoveTowards(meltedSilver.transform.position, endPoint.transform.position, speed * Time.deltaTime);

            // Проверяем достижение конечной точки и завершение прокатки
            if (Vector3.Distance(meltedSilver.transform.position, endPoint.transform.position) <= 0.01f && !hasCompletedRoll)
            {
                CompleteSingleRoll();
            }
        }
        else if (!isTouch)
        {
            soundWork.Pause();
            return;
        }
    }

    private void CompleteSingleRoll()
    {
        hasCompletedRoll = true; // Устанавливаем флаг, что прокатка завершена
        isContact = false;

        meltedSilver.GetComponent<BoxCollider>().enabled = true;
        silverRb.isKinematic = false;

        Rigidbody rb = meltedSilver.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right, ForceMode.Impulse);

        currentRolling++;

        Debug.Log($"Текущее количество прокаток: {currentRolling} осталось: {totalRolling - currentRolling}");

        CompleteRollingProcess();
    }

    private void CompleteRollingProcess()
    {
        if (currentRolling >= totalRolling)
        {
            soundWork.Stop();
            animator.SetTrigger("isIdle");
            meltedSilver.SetActive(false);

            rolledSilver.transform.SetPositionAndRotation(meltedSilver.transform.position, meltedSilver.transform.rotation);
            rolledSilver.SetActive(true);

            StateManager.instance.RollingSubStateComplete();
        }
    }
}