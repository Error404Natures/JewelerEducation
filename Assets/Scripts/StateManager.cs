using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Основные состояния
public enum MainState
{
    MeltingMetal,               //Плавление металла
    RollingMetal,               //Прокатка металла
    CuttingOffWorkpiece,        //Отрезка заготовки
    BendingTheWorkpiece,        //Cгибание заготовки на ригеле
    Soldering,                  //Пайка
    Grinding,                   //Шлифовка
    Polishing                   //Полировка
}
#endregion

#region Подсостояния
public enum MeltingSubState
{
    WarmingIngotMould,          //Прогев изложницы
    HeatingCrucible,            //Прогрев тигеля
    AddingBorax,                //Добавляем буру
    AddingMetal,                //Добовление металла (обычно серебро + медь)
    WarmingAlloy,               //Прогрев сплава до N t
    CrucibeIntoTheMold,         //Выливаем из тигля в изложницу
    WaitingForCooling,          //Ждем остывания и достаем
    ThrowWater,                 //Бросаем в воду
    Complete                    //Завершение
}

public enum RollingSubState
{
    DippingFlux,                //Окунание во флюс
    AnnealingMetal,             //Отжиг металла до красна
    DippingСitric,              //Окунание в лимонную кислоту
    RinsingWater,               //Промывка в воде
    RollingRolls,               //Прокатка металла в вальцах
    Complete                    //Завершение
}

public enum CuttingSubState 
{
    CuttingWorkpice,             //Отрезка заготовки
    DippingFlux,                 //Окунание во флюс
    AnnealingWorkpiece,          //Отжиг металла до красна
    DippingСitric,               //Окунание заготовки в лимонку
    RinsingWater,                //Промывка в воде
    Complete                     //Завершение
}

public enum BendingSubstate
{
    installTheWorkpiece,         //Установка заготовки на ригель
    hummerBending,               //Сгибание ударами молотка
    Complete
}
public enum SolderingSubState
{
    DippingFlux,                  //Окунание во флюс
    PutBoardRing,                 //Положить на огнеупорную дошечку 
    HeatingRing,                  //Нагрев металла до n t
    PutBoardSolder,               //Положить припой на огнеупорную дошечку
    HeatingSolder,                //Прогрев припоя
    GrabSolder,                   //Захватить припой палкой
    HeatingWorkpieceSolder,       //Прогрев всей заготвоки до полного распекания припоя в местах стыка
    DippingCitric,                //Окунание в лимонку
    HeatingWorkpieceCitricAcid,   //Прогрев заготовки в лимонке
    FlushingWater,                //Промыть в воде
    Complete
}

public enum GrindingSubState
{
    CleaningOfSeams,               //Зачистка швов надфилем
    Sanding,                       //Шлфовка наждачкой
    Complete
}

#endregion

public class StateManager : MonoBehaviour
{
    public static StateManager instance;

    public static MainState currentMainState = MainState.MeltingMetal;
    public static MeltingSubState meltingSubState = MeltingSubState.WarmingIngotMould;
    public static RollingSubState rollingSubState = RollingSubState.DippingFlux;
    public static CuttingSubState cuttingSubState = CuttingSubState.CuttingWorkpice;
    public static BendingSubstate bendingSubstate = BendingSubstate.installTheWorkpiece;
    public static SolderingSubState solderingSubState = SolderingSubState.DippingFlux;
    public static GrindingSubState grindingSubState = GrindingSubState.CleaningOfSeams;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static event Action<MainState> OnMainStateChanged;
    public static event Action<MeltingSubState> OnMeltingSubStateChanged;
    public static event Action<RollingSubState> OnRollingSubStateChanged;
    public static event Action<CuttingSubState> OnCuttingSubStateChanged;
    public static event Action<BendingSubstate> OnBendingSubStateChanged;
    public static event Action<SolderingSubState> OnSolderingSubStateChanged;
    public static event Action<GrindingSubState> OnGrindingSubStateChanged;

    #region Методы состояний
    private void MainStateComplete()
    {
        currentMainState++;
        Debug.Log($"Текущее главное состояние изменилось на: {currentMainState}");
        OnMainStateChanged.Invoke(currentMainState);
    }

    public void MeltingSubStateComplete()
    {
        if (currentMainState != MainState.MeltingMetal) return;
        if (meltingSubState == MeltingSubState.ThrowWater)
        {
            MainStateComplete();
        }

        meltingSubState++;
        Debug.Log($"Текущее подсостояние: {meltingSubState}");
        OnMeltingSubStateChanged.Invoke(meltingSubState);
    }
    public void RollingSubStateComplete()
    {
        if (currentMainState != MainState.RollingMetal) return;
        if (rollingSubState == RollingSubState.RollingRolls)
        {
            MainStateComplete();
        }

        rollingSubState++;
        Debug.Log($"Текущее подсостояние: {rollingSubState}");
        OnRollingSubStateChanged.Invoke(rollingSubState);
    }
    public void CuttingSubStateComplete()
    {
        if (currentMainState != MainState.CuttingOffWorkpiece) return;
        if (cuttingSubState == CuttingSubState.RinsingWater)
        {
            MainStateComplete();
        }
        cuttingSubState++;
        Debug.Log($"Текущее подсостояние: {cuttingSubState}");
        OnCuttingSubStateChanged.Invoke(cuttingSubState);
    }
    public void BendingSubStateComplete()
    {
        if (currentMainState != MainState.BendingTheWorkpiece) return;
        if (bendingSubstate == BendingSubstate.hummerBending)
        {
            MainStateComplete();
        }
        bendingSubstate++;
        Debug.Log($"Текущее подсостояние: {bendingSubstate}");
        OnBendingSubStateChanged.Invoke(bendingSubstate);
    }
    public void SolderingSubStateComplete() 
    {
        if (currentMainState != MainState.Soldering) return;
        if (solderingSubState == SolderingSubState.FlushingWater)
        {
            MainStateComplete();
        }
        solderingSubState++;
        Debug.Log($"Текущее подсостояние: {solderingSubState}");
        OnSolderingSubStateChanged.Invoke(solderingSubState);
    }

    public void GrindingSubStateComplete()
    {
        if (currentMainState != MainState.Grinding) return;
        if (grindingSubState == GrindingSubState.Sanding)
        {
            MainStateComplete();
        }
        grindingSubState++;
        Debug.Log($"Текущее подсостояние: {solderingSubState}");
        OnGrindingSubStateChanged.Invoke(grindingSubState);
    }
    #endregion
}
    