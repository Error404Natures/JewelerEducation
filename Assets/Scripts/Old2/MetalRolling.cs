using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

////public class MetalRolling : MonoBehaviour
////{
////    private Animator animator;

////    public float speed = 0.2f;
////    private int currentRolling = 0;
////    public int totalRolling = 5;

////    public Transform contactPoint;
////    public Transform endPoint;


////    public GameObject meltedSilver;
////    private Rigidbody silverRb;

////    public bool isTouch = false;

////    public AudioSource soundWork;

////    private void Start()
////    {
////        animator = GetComponent<Animator>();
////    }

////    public void IsTouchedHand()
////    {
////        if (StateManager.rollingSubState == RollingSubState.RollingRolls)
////        {
////            isTouch = true;
////            animator.SetTrigger("isWork");
////            soundWork.Play();
////        }
////    }

////    public void IsLettingHand()
////    {
////        if (StateManager.rollingSubState == RollingSubState.RollingRolls)
////        {
////            isTouch = false;
////            animator.SetTrigger("isIdle");
////            soundWork.Stop();
////        }
////    }

////    private void OnTriggerEnter(Collider other)
////    {
////        if (other.gameObject == meltedSilver.gameObject && currentRolling <= totalRolling
////           && StateManager.rollingSubState == RollingSubState.RollingRolls)
////        {
////            silverRb = meltedSilver.GetComponent<Rigidbody>();
////            silverRb.isKinematic = true;

////            meltedSilver.transform.position = contactPoint.transform.position;
////            meltedSilver.transform.rotation = contactPoint.transform.rotation;
////            Debug.Log("Ну типо сцепиться должен");
////        }
////    }

////    private void Update()
////    {
////        ProcessRolling();
////    }

////    private void ProcessRolling()
////    {
////        if (isTouch && StateManager.rollingSubState == RollingSubState.RollingRolls
////           && endPoint.transform.position.x > meltedSilver.transform.position.x)
////        {
////            meltedSilver.transform.position = Vector3.MoveTowards(meltedSilver.transform.position, endPoint.transform.position, speed * Time.deltaTime);

////            SilverRolled();
////        }
////        else if (!isTouch)
////        {
////            soundWork.Pause();
////            return;
////        }
////    }

////    private void SilverRolled()
////    {
////        if (meltedSilver.transform.position.x == endPoint.transform.position.x
////            && currentRolling <= totalRolling)
////        {
////            meltedSilver.GetComponent<BoxCollider>().enabled = true;
////            silverRb.isKinematic = false;
////            currentRolling++;
////        }
////        else if (currentRolling == totalRolling)
////        {
////            soundWork.Stop();
////            animator.SetTrigger("isIdle");
////            meltedSilver.SetActive(false);

////            //silverForCutting.transform.position = silverIngus.transform.transform.position;
////            //silverForCutting.transform.rotation = silverIngus.transform.rotation;

////            //silverForCutting.SetActive(true);
////        }
////    }
////}
