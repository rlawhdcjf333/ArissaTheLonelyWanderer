using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSfx : MonoBehaviour
{
    AudioSource m_moveSfx;
    AudioSource m_attackSfx;
    AudioSource m_skillSfx;
    AudioSource m_hitSfx;


    //============================================================
    private void Start()
    {
        m_moveSfx = transform.Find("Sfx/MoveSfx").GetComponent<AudioSource>();
        m_attackSfx = transform.Find("Sfx/AttackSfx").GetComponent<AudioSource>();
        m_skillSfx = transform.Find("Sfx/SkillSfx").GetComponent<AudioSource>();
        m_hitSfx = transform.Find("Sfx/HitSfx").GetComponent<AudioSource>();
    }

    //============================================================

    public void PlayMoveSfx()
    {
        m_moveSfx.Play();
    }
    public void PlayAttackSfx()
    {
        m_attackSfx.Play();
    }
    public void PlaySkillSfx()
    {
        m_skillSfx.Play();
    }
    public void PlayHitSfx()
    {
        m_hitSfx.Play();
    }

}
