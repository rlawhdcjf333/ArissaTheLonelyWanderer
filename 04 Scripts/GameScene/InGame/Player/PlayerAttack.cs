using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    //=======================================
    //타겟팅 조작 변수

    FOV m_FOV;
    GameObject m_target;

    //============================================
    //활과 화살 오브젝트 모션 조작 변수

    GameObject m_aim;
    GameObject m_arrowEquipPoint;
    GameObject m_arrow;

    public event System.Action EventBowAction;
    public event System.Action EventBowActionOff;


    //============================================
    //애니메이션 조작변수

    readonly int m_animHashKeyIsReloading = Animator.StringToHash("IsReloading");
    readonly int m_animHashKeyRecoilTrigger = Animator.StringToHash("RecoilTrigger");
    readonly int m_animHashKeyOverdrawSpeed = Animator.StringToHash("OverdrawSpeed");


    float m_holdingTime;
    public float holdingTime { get { return m_holdingTime; } }

    //============================================
    void AttackStart()
    {
        //리코일 트리거 초기화
        m_animator.ResetTrigger(m_animHashKeyRecoilTrigger);
        //장전 모션 시작
        m_animator.SetBool(m_animHashKeyIsReloading, true);
    }

    //===========================================

    void AttackHold()
    {
        //드로잉 모션 실행중일때 홀딩 시간 계산
        if (m_animator.GetCurrentAnimatorStateInfo(1).IsName("overdraw"))
        {
            //드로잉 시간 계산
            m_holdingTime = m_animator.GetCurrentAnimatorStateInfo(1).normalizedTime;

            // 최대 드로우 이후 고정, 1이 넘어가면 0으로 초기화되니까 그냥 0.99
            if (m_holdingTime >= 0.99f)
            {
                m_animator.SetFloat(m_animHashKeyOverdrawSpeed, 0f);
            }
        }
    }

    //===========================================

    void AttackExit()
    {
        //떼는 순간 장전 불리언 초기화 및 슈팅 트리거 온
        m_animator.SetBool(m_animHashKeyIsReloading, false);
        m_animator.SetTrigger(m_animHashKeyRecoilTrigger);

    }
    //===========================================

    //오른손 화살 오브젝트 활성화
    public void DrawArrow()
    {
        //오토 타겟팅
        Targeting();

        //화살 오브젝트 온
        m_arrow.SetActive(true);
        //이동에 따른 화살 위치 재조정
        m_arrow.transform.position = m_arrowEquipPoint.transform.position;
        //활 끝을 향해서 화살 방향 정조준
        Vector3 targetAim = (m_aim.transform.position - m_arrow.transform.position).normalized;
        m_arrow.transform.up = targetAim;
        //활시위 모션 활성
        EventBowAction?.Invoke();
    }


    //=============================================

    //오른손 화살 오브젝트 비활성화
    public void ReleaseArrow()
    {
        //화살 비활성
        m_arrow.SetActive(false);
        //조준선 비활성
        EffectManager.instance.CutEffect("AimLine");
        //히트 판정
        if(m_target)
        {
            // 방향 벡터
            Vector3 dirToTarget = (m_target.transform.position - m_aim.transform.position).normalized;
            //타겟을 향해 날아가는 이펙트 활성
            EffectManager.instance.CallEffect("ArrowEffect", m_aim.transform.position, Quaternion.LookRotation(dirToTarget));
        }
        else 
        {
            //에임의 정면으로 날아가는 이펙트 활성
            EffectManager.instance.CallEffect("ArrowEffect", m_aim.transform.position, Quaternion.LookRotation(m_aim.transform.forward));
        }
        //활시위 초기화
        EventBowActionOff?.Invoke();
        //타겟 초기화
        m_target = null;
    }

    //==============================================
    //타게팅 로직
    public void Targeting()
    {
        if (m_FOV.TargetsInView.Count != 0)
        {
            if (!m_target)
            {
                //타겟팅이 없으면 가장 가까운 타겟 설정
                m_target = m_FOV.TargetsInView[0];
                //활성된 조준선이 있다면 모두 제거
                EffectManager.instance.CutEffect("AimLine");
                // 조준선 활성
                EffectManager.instance.CallEffect("AimLine", m_aim.transform.position, Quaternion.identity);
            }
            else if (m_target != m_FOV.TargetsInView[0])
            {
                //타겟이 있으나 가장 가깝지 아니한 경우 타겟 초기화
                m_target = null;
            }
        }
 
    }


    //================================================
    // 타겟을 바라보는 역운동학
    private void OnAnimatorIK()
    {
        if (m_target)
        {
            //시선
            m_animator.SetLookAtWeight(1);
            m_animator.SetLookAtPosition(m_target.transform.position);
            
            //활을 든 왼손
            m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            m_animator.SetIKPosition(AvatarIKGoal.LeftHand, m_target.transform.position);

            //몸 방향
            Vector3 tmp = (m_target.transform.position - transform.position).normalized;
            tmp.y = 0;   
            transform.LookAt(tmp);

        }
        else
        {
            //타겟 없으면 정면
            m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            m_animator.SetLookAtWeight(0);
        }
    }

    //=================================================
    //캔슬되었을 때 실행할 초기화 함수
    public void NoneAttack()
    {
        // 모션 변수 전체 초기화
        m_holdingTime = 0f;
        m_target = null;
        m_animator.SetBool(m_animHashKeyIsReloading, false);
        m_animator.SetFloat(m_animHashKeyOverdrawSpeed, 1f);
        m_arrow.SetActive(false);
        EventBowActionOff?.Invoke();

        //이펙트 초기화... 투사체는 제외
        EffectManager.instance.CutEffect("PowerDraw");
        EffectManager.instance.CutEffect("AimLine");
    }

}
