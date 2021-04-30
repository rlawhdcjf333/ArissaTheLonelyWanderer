using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    [SerializeField] float m_viewAngle = 120.0f;
    [SerializeField] float m_viewRadius = 60.0f;
    [SerializeField] LayerMask m_targetMask;
    [SerializeField] LayerMask m_obstacleMask;

    readonly List<GameObject> m_visibleTargets = new List<GameObject>();
    public List<GameObject> TargetsInView { get {return m_visibleTargets;} }

   private void Start()
   {
       //0.3초 마다 시야 내 목표 갱신
       StartCoroutine(TraceEnemyWithDelay(0.3f));
   }
   IEnumerator TraceEnemyWithDelay(float delay)
   {
       while(true)
       {
           yield return new WaitForSeconds(delay);
           TraceEnemy();
       }
   }

    public void TraceEnemy()
    {
        m_visibleTargets.Clear();
        EffectManager.instance.CutEffect("TargetMark");

        //나를 기준으로하는 반경 viewRadius 크기의 구와 충돌하는 모든 Enemy의 콜라이더
        Collider[] resultsInViewRadius = Physics.OverlapSphere(transform.position, m_viewRadius, m_targetMask);
        
        foreach (Collider elem in resultsInViewRadius)
        {
            //루트 콜라이더는 필요없음
            if (elem.transform == elem.transform.root) continue;
            //Collider to Transform
            Transform target = elem.transform;
            // 방향 벡터 to Enemy
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            // 시야각 이내 판정
            if(Vector3.Angle(transform.forward, dirToTarget) <= m_viewAngle/2)
            {
                //레이캐스트 장애물 판정
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, m_obstacleMask))
                {
                    //타겟 리스트에 등록
                    m_visibleTargets.Add(target.gameObject);

                    //타게팅 이펙트 활성
                    Marking(target.gameObject);
                }
            }
        }
           
        //가까운 순으로 정렬
        m_visibleTargets.Sort((GameObject x, GameObject y) => 
        Vector3.Distance(x.transform.position, transform.position).CompareTo(Vector3.Distance(y.transform.position, transform.position)));
    }

    //==========================================================================
    //타게팅 이펙트
    void Marking(GameObject target)
    {
        GameObject tmp = EffectManager.instance.CallEffect("TargetMark");
        tmp.GetComponent<TargetMark>().SetTarget(target);
        tmp.SetActive(true);
    }

    //==========================================================================

    private void OnDrawGizmos()
    {
        if(m_visibleTargets.Count !=0)
        {
            //최단 거리 타겟
            if(m_visibleTargets[0] != null) Debug.DrawLine(transform.position, m_visibleTargets[0].transform.position, Color.blue);
            //시야각 이내 최단 이외 모든 타겟
            foreach(GameObject elem in m_visibleTargets)
            {
                if(elem != m_visibleTargets[0] && elem !=null) Debug.DrawLine(transform.position, elem.transform.position, Color.red);
            }
        }
    }
}
