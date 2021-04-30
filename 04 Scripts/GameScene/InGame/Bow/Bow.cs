using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    Animator m_animator;
    readonly int m_animHashKeyDrawTrigger = Animator.StringToHash("DrawTrigger");

    //===========================================
    void Start()
    {
        m_animator = transform.GetComponent<Animator>();

        //delegate로 연결, 공격키를 홀딩하고 있을때 활시위가 당겨져 있게끔
        GameObject.Find("Player").GetComponent<Player>().EventBowAction += OnBowAction;
        GameObject.Find("Player").GetComponent<Player>().EventBowActionOff += OnBowActionOff;
    }

    //===========================================

    public void OnBowAction()
    {
        m_animator.SetTrigger(m_animHashKeyDrawTrigger);
        //진행도 0.7 이상에서 정지
        if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f
            && m_animator.GetCurrentAnimatorStateInfo(0).IsName("Drawing")) m_animator.speed = 0f;   
    }


    public void OnBowActionOff()
    {
        //모션 정지 해제
        m_animator.speed = 1f;
        //트리거 리셋
        m_animator.ResetTrigger(m_animHashKeyDrawTrigger);
    }

}
