using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCController : MonoBehaviour
{
    Collider m_playerHitbox;

    //==================================================

    //NPC 대화
    string[] m_dialogues;
    int m_dialogueIndex;
    PanelDialogue m_panelDialogue;
    bool m_touch;
    
    
    //==================================================
    //NPC 애니메이션
    Animator m_animator;
    readonly int m_animHashKeyWake = Animator.StringToHash("wake");

    //==================================================

    private void Awake()
    {
        m_touch = false;

        m_dialogues = new string[8];
        m_dialogues.SetValue("경비병 : 안녕하십니까", 0);


        m_dialogues.SetValue("경비병 : 아, 공고를 보고 오셨군요", 1);


        m_dialogues.SetValue("경비병 : 사냥은 할 줄 아십니까? 이건 정말 위험한 일이 될 수도 있습니다만...", 2);
        m_dialogues.SetValue("경비병 : 경력 2년 이상이시라면 화살을 시위에서 놓는 순간에 목표를 쳐다봐야 한다는 건 알고 계실 테죠...", 3);
        m_dialogues.SetValue("경비병 : 가드 카운터는 가능하십니까? 적당한 타이밍에 방어를 하시면... 아 충분하시다구요..", 4);
        m_dialogues.SetValue("경비병 : 좋습니다. 준비가 되시면 다시 말씀해주십시오.", 5);

        m_dialogues.SetValue("경비병 : 준비가 되신 모양이군요. 저를 따라오십시오.", 6);

        m_dialogues.SetValue("경비병 : 성공하셨군요! 정말 감사드립니다.", 7);

    }

    //==================================================

    private void Start()
    {
        m_playerHitbox = GameObject.Find("Player/hitbox").GetComponent<CapsuleCollider>();
        m_panelDialogue = GameObject.Find("Canvas").transform.Find("PanelDialogue").GetComponent<PanelDialogue>();
        m_animator = GetComponent<Animator>();
    }

    //==================================================
    private void OnTriggerStay(Collider other)
    {
        //콜라이더와 닿아있는 상태에서 터치까지 해주면 활성
        if (Input.touchCount > 0 && m_touch == false)
        {
            Vector2 touch = Input.GetTouch(0).position;
            Vector3 touchWorld = new Vector3(touch.x, touch.y);

            Ray ray = Camera.main.ScreenPointToRay(touchWorld);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    if (other == m_playerHitbox)
                    {
                        m_touch = true;
                        DialogueStart();
                    }
                }
            }
        }

        //마우스 클릭도 인정해주자
        if(Input.GetMouseButtonUp(0) && m_touch == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    if (other == m_playerHitbox)
                    {
                        m_touch = true;
                        DialogueStart();
                    }
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == m_playerHitbox)
        {
            ExitDialogue();
        }
    }
    //==================================================

    void ShowDialogue(int index)
    {
        m_dialogueIndex = index;
        if(m_dialogues.GetValue(index) != null)
        {
            m_panelDialogue.ShowDialogue(m_dialogues[index]);
        }
    }

    //==================================================


    public void Wake()
    {
        m_animator.SetBool(m_animHashKeyWake, true);
    }

    //==================================================


    void DialogueStart()
    {
        //일단 기상
        Wake();

        //0번 퀘스트 진행 중일 때
        if (QuestManager.instance.IsThisOnBoardQuest(0))
        {
            ShowDialogue(1);
        }

        //0번 퀘스트 완료했고 1번 퀘스트 수주 이전
        else if (QuestManager.instance.IsThisCompletedQuest(0) && !QuestManager.instance.IsThisOnBoardQuest(1) && !QuestManager.instance.IsThisCompletedQuest(1))
        {
            ShowDialogue(2);
        }

        //1번 퀘스트 진행중
        else if (QuestManager.instance.IsThisOnBoardQuest(1))
        {
            ShowDialogue(6);
        }

        //1번 퀘스트 완료
        else if (QuestManager.instance.IsThisCompletedQuest(1))
        {
            ShowDialogue(7);
        }

        //해당사항 없음
        else
        {
            ShowDialogue(0);
        }

        //대화를 시작할 때마다 상호작용을 바인딩한다
        m_panelDialogue.buttonAction += DialogueInteraction;
    }


    //==================================================

    public void DialogueInteraction()
    {
        //만약에 0번 퀘스트가 진행중인 상태면
        if(QuestManager.instance.IsThisOnBoardQuest(0))
        {
            QuestManager.instance.CompleteQuest(0);
            ShowDialogue(2);
        }

        //만약에 0번 퀘스트가 클리어 되었고 아직 1번 퀘스트를 받지 않았을 때
        else if(QuestManager.instance.IsThisCompletedQuest(0) && !QuestManager.instance.IsThisOnBoardQuest(1) && !QuestManager.instance.IsThisCompletedQuest(1))
        {
            if(m_dialogueIndex == 5)
            {
                transform.Find("QuestMark").gameObject.SetActive(true);
                transform.Find("QuestMark").GetComponent<QuestMark>().ShowQuestInfo();
                ExitDialogue();
                return;
            }
            m_dialogueIndex++;
            ShowDialogue(m_dialogueIndex);
        }

        //1번 퀘스트 진행중
        else if(QuestManager.instance.IsThisOnBoardQuest(1))
        {
            ExitDialogue();
            SceneManager.LoadScene("GameScene");
        }

        //1번 퀘스트 완료
        else if(QuestManager.instance.IsThisCompletedQuest(1))
        {
            ExitDialogue();
            GameObject theEnd = GameObject.Find("Canvas").transform.Find("TheEnd").gameObject;
            theEnd.SetActive(true);

        }

        //해당사항 없음
        else
        {
            ExitDialogue();
        }
    }

    void ExitDialogue()
    {
        m_touch = false;
        //대화가 끝나면 상호작용 바인딩 해제
        m_panelDialogue.buttonAction -= DialogueInteraction;
        m_panelDialogue.ExitDialogue();
    }
}
