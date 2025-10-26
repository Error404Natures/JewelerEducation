using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHeat : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] protected GameObject targetObj;
    [SerializeField] float timer;

    protected bool isComplete = false;

    protected void ProcessTrigger(Collider collider)
    {
        if (!CorrectState() || isComplete || !BurnerController.isHeating || collider.gameObject != targetObj.gameObject) 
            return;

        if (TimerProcess())
        {
            isComplete = true;
            SubStateChanged();
        }
    }

    protected bool TimerProcess()
    {        
        timer -= Time.deltaTime;
       
        if (timer < 0)
        {
            return true;
        }
        Debug.Log($"До конца осталось: {timer.ToString("F1")}");
        return false;
        
    }

    protected abstract  bool CorrectState();

    protected abstract void SubStateChanged();
    

}
