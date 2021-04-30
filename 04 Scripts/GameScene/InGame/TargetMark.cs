using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMark : MonoBehaviour
{

    GameObject m_target;

    private void Awake()
    {
        m_target = null;
    }
    public void SetTarget(GameObject target)
    {
        m_target = target;
    }


    private void FixedUpdate()
    {
        // 타겟이 잡혀있다면 그 위치 상부를 따라다니게끔 함
        if(m_target)
            transform.position = m_target.transform.position + Vector3.up * m_target.transform.localScale.magnitude;
    }
}
