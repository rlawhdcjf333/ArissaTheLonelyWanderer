using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMark : MonoBehaviour
{
    QuestData m_questData;
    [SerializeField] int m_questId;
    public int questid { get; }

    [SerializeField] int m_requiredQuestId;
    

    private void Start()
    {
        // 선행퀘스트가 있으면서 완료 안되어 있으면 꺼둠
        if (QuestManager.instance.IsThisCompletedQuest(m_requiredQuestId) == false) gameObject.SetActive(false);
        // 이미 이 퀘스트가 완료된거면 꺼둠
        if (QuestManager.instance.IsThisCompletedQuest(m_questId)) gameObject.SetActive(false);
        // 이미 이 퀘스트가 진행중이면 꺼둠
        if (QuestManager.instance.IsThisOnBoardQuest(m_questId)) gameObject.SetActive(false);       

        //이건 혹시 몰라 나중에 다시 켜질 수 도 있으니까 일단 로드는 함
        m_questData = QuestManager.instance.m_questDictionary[m_questId];
    }

    private void Update()
    {
        //수주되거나 완료된 퀘스트는 더이상 알림 마크를 활성할 필요 없음
        if(QuestManager.instance.IsThisOnBoardQuest(m_questId) || QuestManager.instance.IsThisCompletedQuest(m_questId))
        {
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.CompareTag("Player")) ShowQuestInfo();
    }

    public void ShowQuestInfo()
    {
        GameObject canvas = GameObject.Find("Canvas");

        GameObject panelQuestLog = canvas.transform.Find("PanelQuestLog").gameObject;

        panelQuestLog.GetComponent<PanelQuestLog>().SetQuestLog(m_questData);
        panelQuestLog.SetActive(true);
    }

}
