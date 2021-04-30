using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    GameObject m_effect;
    [SerializeField] SkillInfo m_SkillInfo;

    //==============================================================
    public event System.Action<SkillInfo> EventSkillButtonDown;
    public event System.Action<SkillInfo> EventSkillButtonStay;
    public event System.Action<SkillInfo> EventSkillButtonUp;

    //==============================================================

    private void Start()
    {
        transform.Find("Icon").GetComponent<IconInteraction>().EventButtonDown += OnSkillButtonDown;
        transform.Find("Icon").GetComponent<IconInteraction>().EventButtonStay += OnSkillButtonStay;
        transform.Find("Icon").GetComponent<IconInteraction>().EventButtonUp += OnSkillButtonUp;


        m_effect = transform.Find("Effect").gameObject;
    }
    //==============================================================

    void OnSkillButtonDown()
    {
        EventSkillButtonDown?.Invoke(m_SkillInfo);
        m_effect?.SetActive(!m_effect.activeInHierarchy);
    }
    //==============================================================

    void OnSkillButtonStay()
    {
        EventSkillButtonStay?.Invoke(m_SkillInfo);
    }

    //==============================================================

    void OnSkillButtonUp()
    {
        EventSkillButtonUp?.Invoke(m_SkillInfo);
    }


}
