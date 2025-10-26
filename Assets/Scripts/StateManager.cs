using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region �������� ���������
public enum MainState
{
    MeltingMetal,               //��������� �������
    RollingMetal,               //�������� �������
    CuttingOffWorkpiece,        //������� ���������
    BendingTheWorkpiece,        //C������� ��������� �� ������
    Soldering,                  //�����
    Grinding,                   //��������
    Polishing                   //���������
}
#endregion

#region ������������
public enum MeltingSubState
{
    WarmingIngotMould,          //������ ���������
    HeatingCrucible,            //������� ������
    AddingBorax,                //��������� ����
    AddingMetal,                //���������� ������� (������ ������� + ����)
    WarmingAlloy,               //������� ������ �� N t
    CrucibeIntoTheMold,         //�������� �� ����� � ���������
    WaitingForCooling,          //���� ��������� � �������
    ThrowWater,                 //������� � ����
    Complete                    //����������
}

public enum RollingSubState
{
    DippingFlux,                //�������� �� ����
    AnnealingMetal,             //����� ������� �� ������
    Dipping�itric,              //�������� � �������� �������
    RinsingWater,               //�������� � ����
    RollingRolls,               //�������� ������� � �������
    Complete                    //����������
}

public enum CuttingSubState 
{
    CuttingWorkpice,             //������� ���������
    DippingFlux,                 //�������� �� ����
    AnnealingWorkpiece,          //����� ������� �� ������
    Dipping�itric,               //�������� ��������� � �������
    RinsingWater,                //�������� � ����
    Complete                     //����������
}

public enum BendingSubstate
{
    installTheWorkpiece,         //��������� ��������� �� ������
    hummerBending,               //�������� ������� �������
    Complete
}
public enum SolderingSubState
{
    DippingFlux,                  //�������� �� ����
    PutBoardRing,                 //�������� �� ����������� ������� 
    HeatingRing,                  //������ ������� �� n t
    PutBoardSolder,               //�������� ������ �� ����������� �������
    HeatingSolder,                //������� ������
    GrabSolder,                   //��������� ������ ������
    HeatingWorkpieceSolder,       //������� ���� ��������� �� ������� ���������� ������ � ������ �����
    DippingCitric,                //�������� � �������
    HeatingWorkpieceCitricAcid,   //������� ��������� � �������
    FlushingWater,                //������� � ����
    Complete
}

public enum GrindingSubState
{
    CleaningOfSeams,               //�������� ���� ��������
    Sanding,                       //������� ���������
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

    #region ������ ���������
    private void MainStateComplete()
    {
        currentMainState++;
        Debug.Log($"������� ������� ��������� ���������� ��: {currentMainState}");
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
        Debug.Log($"������� ������������: {meltingSubState}");
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
        Debug.Log($"������� ������������: {rollingSubState}");
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
        Debug.Log($"������� ������������: {cuttingSubState}");
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
        Debug.Log($"������� ������������: {bendingSubstate}");
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
        Debug.Log($"������� ������������: {solderingSubState}");
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
        Debug.Log($"������� ������������: {solderingSubState}");
        OnGrindingSubStateChanged.Invoke(grindingSubState);
    }
    #endregion
}
    