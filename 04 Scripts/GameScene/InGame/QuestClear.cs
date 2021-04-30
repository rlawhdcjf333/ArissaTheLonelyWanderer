using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestClear : MonoBehaviour
{

    AudioSource m_sfx;
    void Awake()
    {
        m_sfx = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        m_sfx.Play();
    }


}
