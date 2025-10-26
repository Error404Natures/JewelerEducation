using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    MeltingMetal,          // ��������� �������
    PouringIntoMold,       // ������� � �����
    RemovingFromMold,      // ���������� �� �����
    RollingMetal,          // �������� �������
    CuttingRod,            // ������� ���������
    Bending,               // ��������
    GrabingSilver,         // ������ ������� ��������
    Soldering,             // �����
    MakingStoneSetting,    // �������� �������
    CleaningSeams,         // �������� ����
    Sanding,               // ��������
    SettingStone,          // �������� �����
    Polishing              // ���������

    #region ����������� ���������� ������ 
    /*
     *  MeltingMetal,          // ��������� �������
        PouringIntoMold,       // ������� � �����
        RemovingFromMold,      // ���������� �� �����
        RollingMetal,          // �������� �������
        ShapingRod,            // ������������ ������
        CuttingRod,            // ������� ���������
        Bending,               // ��������
        Soldering,             // �����
        MakingStoneSetting,    // �������� �������
        FittingStoneShank,     // �������� �����/�����
        CleaningSeams,         // �������� ����
        Sanding,               // ��������
        SettingStone,          // �������� �����
        Polishing              // ���������
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
        Debug.Log("������� �������� ��� ��������� ���������!");
    }

    public void RemovingFormFinished()
    {
        //currentState = State.RemovingFromMold;
        Debug.Log("������ ������� ��� �������� ����� ���������!");
    }
    public void IngotExtractorFinished()
    {
        //currentState = State.RollingMetal;
        Debug.Log("����� ������� ��� ���������� ����� ���������!");
    }
    public void MetallRollingFinished()
    {
        //currentState = State.CuttingRod;
        Debug.Log("������ �������� ��� �������� ��������!");
    }

    public void CuttingRodFinished()
    {
        //currentState = State.Bending;
        Debug.Log("������� �������� ��� ������ ��������!");
    }
    public void BindingRingFinished()
    {
        //currentState = State.GrabingSilver;
        Debug.Log("������ ������� ������� ��� ����� ������!");
    }

    public void GrabingSilverComplete()
    {
        // = State.Soldering;
        Debug.Log("������ ������� ��� �� �������� �����");
    }

    public void SolderingFinished()
    {
        //currentState = State.MakingStoneSetting;
        Debug.Log("������� �������� ��� ����� ���������!");
    }

}
