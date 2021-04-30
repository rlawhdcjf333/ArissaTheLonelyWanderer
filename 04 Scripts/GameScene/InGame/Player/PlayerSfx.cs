using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSfx : MonoBehaviour
{
    AudioSource m_stepSfx;
    AudioSource m_bowShootSfx;
    AudioSource m_bowDrawSfx;
    AudioSource m_dodgeSfx;
    AudioSource m_guardSfx;
    AudioSource m_hitSfx;
    AudioSource m_aimModeSfx;
    AudioSource m_counterSfx;


    //============================================================
    void Start()
    {
        m_stepSfx = transform.Find("Sfx/StepSfx").GetComponent<AudioSource>();
        m_bowShootSfx = transform.Find("Sfx/BowShootSfx").GetComponent<AudioSource>();
        m_bowDrawSfx = transform.Find("Sfx/BowDrawSfx").GetComponent<AudioSource>();
        m_dodgeSfx = transform.Find("Sfx/DodgeSfx").GetComponent<AudioSource>();
        m_guardSfx = transform.Find("Sfx/GuardSfx").GetComponent<AudioSource>();
        m_hitSfx = transform.Find("Sfx/HitSfx").GetComponent<AudioSource>();
        m_aimModeSfx = transform.Find("Sfx/AimModeSfx").GetComponent<AudioSource>();
        m_counterSfx = transform.Find("Sfx/CounterSfx").GetComponent<AudioSource>();
    }

    //============================================================


    public void PlayStepSfx()
    {
        m_stepSfx.Play();
    }

    public void PlayBowShootSfx()
    {
        m_bowShootSfx.Play();
    }

    public void PlayBowDrawSfx()
    {
        m_bowDrawSfx.Play();
    }

    public void PlayDodgeSfx()
    {
        m_dodgeSfx.Play();
    }

    public void PlayGuardSfx()
    {
        m_guardSfx.Play();
    }

    public void PlayHitSfx()
    {
        m_hitSfx.Play();
    }

    public void PlayAimModeSfx()
    {
        m_aimModeSfx.Play();
    }

    public void PlayCounterSfx()
    {
        m_counterSfx.Play();
    }

}
