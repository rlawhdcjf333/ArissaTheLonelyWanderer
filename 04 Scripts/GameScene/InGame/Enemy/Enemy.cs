using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject m_target;
    public GameObject target { get { return m_target; } }

    [SerializeField] GameObject m_attackPoint;
    [SerializeField] GameObject m_hitPointHead;
    [SerializeField] GameObject m_hitPointSpine;
    [SerializeField] GameObject m_hitPointButt;
    [SerializeField] Material m_bloody;
    [SerializeField] Material m_red;

    [SerializeField] bool m_isMother;
    public bool isMother { get { return m_isMother; } }
    bool m_isStun =false;
    NavMeshAgent m_navAgent;
    Rigidbody m_body;
    //==============================================================
    //애니메이터 컨트롤 필드

    Animator m_animator;
    

    readonly int m_animHashKeyHit = Animator.StringToHash("hit");
    readonly int m_animHashKeySkill = Animator.StringToHash("skill");
    readonly int m_animHashKeyStun = Animator.StringToHash("stun");
    readonly int m_animHashKeyAttack = Animator.StringToHash("attack");
    readonly int m_animHashKeyRun = Animator.StringToHash("run");
    readonly int m_animHashKeyDie = Animator.StringToHash("die");

    int m_stunCount = 0;
    bool m_isDead = false;

    //===========================================================
    void Start()
    {
        m_target = GameObject.Find("Player");
        m_animator = GetComponent<Animator>();
        m_body = GetComponent<Rigidbody>();
        m_navAgent = GetComponent<NavMeshAgent>();

    }

    //===========================================================
    void Update()
    {
        //새끼인 경우 어미가 죽으면 사망
        if(!m_isMother)
        {
            if(!GameObject.Find("Wolf"))
            {
                Die();
            }
        }

        if (m_target == null) return;
        if (m_isDead) return;

        m_navAgent.SetDestination(m_target.transform.position);

        if(m_navAgent.remainingDistance<=m_navAgent.stoppingDistance )
        {
            m_animator.ResetTrigger(m_animHashKeyRun);
            m_animator.SetTrigger(m_animHashKeyAttack);
        }

        else
        {
            m_animator.ResetTrigger(m_animHashKeyAttack);
            m_animator.SetTrigger(m_animHashKeyRun);
        }

        //세 곳이 모두 피격되어 비활성되면 스턴 트리거 호출
        if(m_hitPointHead.activeInHierarchy == false && m_hitPointSpine.activeInHierarchy == false && m_hitPointButt.activeInHierarchy == false && m_isStun == false)
        {
            m_animator.SetTrigger(m_animHashKeyStun);
            m_stunCount++;
            m_isStun = true;
        }

        //3번 스턴되면 사망처리
        if (m_stunCount > 2)
        {
            Die();
        }

        //치트키
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Die();
        }
    }
    //===========================================================
    //돌진 
    public void Thrust()
    {
        m_body.velocity = transform.forward * 15f;
    }
    //===========================================================
    //공격 트리거 오브젝트 온오프
    public void TurnAttckTrigger(bool val)
    {
        if (val) m_attackPoint.SetActive(true);
        else m_attackPoint.SetActive(false);
    }
    //===========================================================
    //모션 트리거 전체 오프
    public void ResetMotionAll()
    {
       foreach(AnimatorControllerParameter elem in m_animator.parameters)
        {
            m_animator.ResetTrigger(elem.name);
        }

    }
    //===========================================================
    public void RestoreStun()
    {
        m_hitPointHead.SetActive(true);
        m_hitPointSpine.SetActive(true);
        m_hitPointButt.SetActive(true);
        m_isStun = false;
    }
    //===========================================================
    public void Skill()
    {
        switch (m_stunCount)
        {
            case 1:
                Instantiate(Resources.Load<GameObject>("Prefab/WolfCub"), transform.position, Quaternion.identity);
                transform.GetComponentInChildren<SkinnedMeshRenderer>().material = m_bloody;

                break;
            case 2:
                Instantiate(Resources.Load<GameObject>("Prefab/WolfCub"), transform.position, Quaternion.identity);
                Instantiate(Resources.Load<GameObject>("Prefab/WolfCub"), transform.position, Quaternion.identity);
                transform.GetComponentInChildren<SkinnedMeshRenderer>().material = m_red;

                break;
            default:
                break;
        }
    }
    //===========================================================
    public void Die()
    {
        //업데이트 막고
        m_isDead = true;

        //어미이면 퀘스트 클리어
        if(m_isMother)
        {
            ClearQuest();
        }

        //죽기 모션 활성 및 삭제 5초 딜레이
        ResetMotionAll();
        m_animator.SetTrigger(m_animHashKeyDie);
        Destroy(gameObject, 5f);
    }

    //===========================================================
    //클리어 퀘스트

    void ClearQuest()
    {
        //퀘스트 중이었으면 퀘완
        if (QuestManager.instance.IsThisOnBoardQuest(1))
        {
            QuestManager.instance.CompleteQuest(1);
        }

        //마을로 복귀
        PanelDialogue panelDialogue = GameObject.Find("Canvas").transform.Find("PanelDialogue").GetComponent<PanelDialogue>();
        panelDialogue.ShowDialogue("...거대 늑대 사냥에 성공했다");

        panelDialogue.buttonAction += () =>
        {
            panelDialogue.ExitDialogue();
            UnityEngine.SceneManagement.SceneManager.LoadScene("PreGameScene");
        };

    }
}
