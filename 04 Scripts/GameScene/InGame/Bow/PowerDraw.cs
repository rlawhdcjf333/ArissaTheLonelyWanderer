using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDraw : MonoBehaviour
{
    [SerializeField] GameObject m_parent;
    void Start()
    {
        m_parent = GameObject.FindGameObjectWithTag("Aim");
        transform.SetParent(m_parent.transform);
    }

    private void Update()
    {
        transform.position = transform.parent.position;
    }

}
