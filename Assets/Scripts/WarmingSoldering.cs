using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WarmingSoldering : BaseHeat
{
    public float _timer = 1f; //блять я хуй хзнает почему с базового класса не работает

    Animator animator;

    private void Start()
    {

        if (targetObj == null)
        {
            Debug.Log("Тут не требуется прямая ссылка на объект");
        }
    }

    protected override bool CorrectState() => StateManager.solderingSubState == SolderingSubState.HeatingSolder;

    protected override void SubStateChanged() => StateManager.instance.SolderingSubStateComplete();

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Solder") && BurnerController.isHeating && 
            StateManager.solderingSubState == SolderingSubState.HeatingSolder)
        {
            animator = other.gameObject.GetComponent<Animator>();
            animator.speed = 1;

            if (_timer > 0f)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                StateManager.instance.SolderingSubStateComplete();
            }
            ProcessTrigger(other);
            animator.SetTrigger("Annealing");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Solder"))
        {
            animator = other.gameObject.GetComponent<Animator>();
            animator.speed = 0;
        }
    }
}
