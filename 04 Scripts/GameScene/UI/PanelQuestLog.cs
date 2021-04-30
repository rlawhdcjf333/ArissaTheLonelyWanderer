using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelQuestLog : MonoBehaviour
{

    QuestData m_nowShowing;

    Button m_accept;
    Button m_reject;

    PanelQuestButton m_panelQuestButton;

    //======================================================================================================

    private void Start()
    {
        m_panelQuestButton = transform.root.Find("PanelQuestButton").GetComponent<PanelQuestButton>();

        m_accept = transform.Find("Background/Buttons/Accept").GetComponent<Button>();
        m_reject= transform.Find("Background/Buttons/Reject").GetComponent<Button>();

        m_accept.onClick.AddListener(Accept);
        m_reject.onClick.AddListener(Reject);
    }


    //======================================================================================================
    //퀘스트 로그 보여주기 용
    public void SetQuestLog(QuestData data)
    {
        m_nowShowing = data;

        transform.Find("Background/Info/Viewport/Content/Name").GetComponent<TextMeshProUGUI>().text = data.name;
        transform.Find("Background/Info/Viewport/Content/Description").GetComponent<TextMeshProUGUI>().text = data.description;
        transform.Find("Background/Info/Viewport/Content/Objective").GetComponent<TextMeshProUGUI>().text = data.objective;
        transform.Find("Background/Info/Viewport/Content/IsCompleted").GetComponent<TextMeshProUGUI>().text = data.isCompleted ? "완료" : "미완료";
    }

    //======================================================================================================
    //수락하기
    void Accept()
    {
        m_panelQuestButton.QuestRegister();
        QuestManager.instance.RegisterQuest(m_nowShowing.id);

        gameObject.SetActive(false);
    }

    //======================================================================================================
    //거절하기
    void Reject()
    {
        transform.root.Find("Sfx/QuestRejectSfx").GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
    }



  
}
