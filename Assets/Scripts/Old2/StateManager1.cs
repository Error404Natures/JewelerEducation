using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    MeltingMetal,          // Плавление металла
    PouringIntoMold,       // Заливка в форму
    RemovingFromMold,      // Извлечение из формы
    RollingMetal,          // Прокатка металла
    CuttingRod,            // Отрезка заготовки
    Bending,               // Сгибание
    GrabingSilver,         // Захват серебра пинцетом
    Soldering,             // Пайка
    MakingStoneSetting,    // Создание кастика
    CleaningSeams,         // Зачистка швов
    Sanding,               // Шлифовка
    SettingStone,          // Закрепка камня
    Polishing              // Полировка

    #region ИЗНАЧАЛЬНОЕ КОЛИЧЕСТВО ЭТАПОВ 
    /*
     *  MeltingMetal,          // Плавление металла
        PouringIntoMold,       // Заливка в форму
        RemovingFromMold,      // Извлечение из формы
        RollingMetal,          // Прокатка металла
        ShapingRod,            // Формирование прутка
        CuttingRod,            // Отрезка заготовки
        Bending,               // Сгибание
        Soldering,             // Пайка
        MakingStoneSetting,    // Создание кастика
        FittingStoneShank,     // Примерка камня/шинки
        CleaningSeams,         // Зачистка швов
        Sanding,               // Шлифовка
        SettingStone,          // Закрепка камня
        Polishing              // Полировка
     */
    #endregion
}

public class StateManager1 : MonoBehaviour
{
    public static StateManager1 instance;
    public static State currentState1 = State.MeltingMetal;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    //public static event Action<State> OnStateChange;

    public void NextState()
    {
        //currentState++;
        //OnStateChange?.Invoke(currentState);
    }

    private void Start()
    {
        TorchTreatment burner = FindObjectOfType<TorchTreatment>();
        burner.OnMeltingComplete += HandleMeltingFinished;
        burner.OnSolderingComplete += SolderingFinished; 

        CreatingForm creatingForm = FindObjectOfType<CreatingForm>();
        creatingForm.OnRemovingFromMold += RemovingFormFinished;

        IngotExtractor ingotExtractor = FindObjectOfType<IngotExtractor>();
        ingotExtractor.OnIngonExtraction += IngotExtractorFinished;

        //MetalRolling metalRolling = FindObjectOfType<MetalRolling>();
        //metalRolling.OnRollingMetall += MetallRollingFinished;

        CuttingWorkpiece cuttingWorkpiece = FindObjectOfType<CuttingWorkpiece>();
        cuttingWorkpiece.OnCuttingRod += CuttingRodFinished;

        BindingRing bindingRing = FindObjectOfType<BindingRing>();
        bindingRing.OnBindingRingChanged += BindingRingFinished;

        GrabbingSilver grabbingSilver = FindObjectOfType<GrabbingSilver>();
        grabbingSilver.OnGrabingSilver += GrabingSilverComplete;
    }

    public void HandleMeltingFinished()
    {
        //currentState = State.PouringIntoMold;
        Debug.Log("Горелка сообщила что плавление закончено!");
    }

    public void RemovingFormFinished()
    {
        //currentState = State.RemovingFromMold;
        Debug.Log("Тигель сообщил что создание формы закончено!");
    }
    public void IngotExtractorFinished()
    {
        //currentState = State.RollingMetal;
        Debug.Log("Ингус сообщил что извлечение формы закончено!");
    }
    public void MetallRollingFinished()
    {
        //currentState = State.CuttingRod;
        Debug.Log("Вальцы сообщили что прокатка окончена!");
    }

    public void CuttingRodFinished()
    {
        //currentState = State.Bending;
        Debug.Log("Ножницы сообщили что металл разрезан!");
    }
    public void BindingRingFinished()
    {
        //currentState = State.GrabingSilver;
        Debug.Log("Пруток серебра сообщил что метал согнут!");
    }

    public void GrabingSilverComplete()
    {
        // = State.Soldering;
        Debug.Log("Пинцет сообщил что он захватил метал");
    }

    public void SolderingFinished()
    {
        //currentState = State.MakingStoneSetting;
        Debug.Log("Горелка сообщила что пайка завершена!");
    }

}
