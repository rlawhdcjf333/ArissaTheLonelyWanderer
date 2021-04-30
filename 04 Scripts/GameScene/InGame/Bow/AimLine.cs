using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLine : MonoBehaviour
{
    GameObject m_aim;

    private void Start()
    {
        m_aim = GameObject.FindGameObjectWithTag("Aim");
    }
    void FixedUpdate()
    {
        if (m_aim)
        {
            transform.position = m_aim.transform.position;
            transform.forward = m_aim.transform.forward;
        }
    }

}
