using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestInfo : MonoBehaviour
{
    QuestData m_nowQuest;

    public void SetQuestLog(QuestData data)
    {
        m_nowQuest = data;

        if(data !=null)
        {
            transform.Find("Background/Info/Viewport/Content/Name").GetComponent<TextMeshProUGUI>().text = data.name;
            transform.Find("Background/Info/Viewport/Content/Description").GetComponent<TextMeshProUGUI>().text = data.description;
            transform.Find("Background/Info/Viewport/Content/Objective").GetComponent<TextMeshProUGUI>().text = data.objective;
            transform.Find("Background/Info/Viewport/Content/IsCompleted").GetComponent<TextMeshProUGUI>().text = data.isCompleted ? "완료" : "미완료";
        }
        else
        {
            transform.Find("Background/Info/Viewport/Content/Name").GetComponent<TextMeshProUGUI>().text = "현재 진행 중인 퀘스트가 없습니다";
            transform.Find("Background/Info/Viewport/Content/Description").GetComponent<TextMeshProUGUI>().text = "";
            transform.Find("Background/Info/Viewport/Content/Objective").GetComponent<TextMeshProUGUI>().text = ""; ;
            transform.Find("Background/Info/Viewport/Content/IsCompleted").GetComponent<TextMeshProUGUI>().text = "";
        }

    }

}
