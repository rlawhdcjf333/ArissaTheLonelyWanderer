using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSkillButton : MonoBehaviour
{
    SkillButton[] m_buttons;
    public event System.Action<SkillInfo> EventPlaySkill;
    public event System.Action<SkillInfo> EventHoldSkill;
    public event System.Action<SkillInfo> EventExitSkill;

    //==========================================================
    private void Start()
    {
        m_buttons = GetComponentsInChildren<SkillButton>();

        foreach(SkillButton elem in m_buttons)
        {
            elem.EventSkillButtonDown += PlaySkill;
            elem.EventSkillButtonStay += HoldSkill;
            elem.EventSkillButtonUp += ExitSkill;
        }
    }
    //==========================================================


    void PlaySkill(SkillInfo input)
    {
        EventPlaySkill?.Invoke(input);
    }
    //==========================================================

    void HoldSkill(SkillInfo input)
    {
        EventHoldSkill?.Invoke(input);
    }


    //==========================================================

    void ExitSkill(SkillInfo input)
    {
        EventExitSkill?.Invoke(input);
    }



}
