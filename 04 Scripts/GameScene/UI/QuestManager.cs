using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestManager : MonoBehaviour
{
    static QuestManager g_instance;

    public static QuestManager instance
    {
        get
        {
            if(!g_instance)
            {
                GameObject questManager = new GameObject("QuestManager");
                questManager.AddComponent<QuestManager>();

                g_instance = questManager.GetComponent<QuestManager>();
            }
            return g_instance;
        }
    }

    public Dictionary<int, QuestData> m_questDictionary = new Dictionary<int, QuestData>(); 

    private void Awake()
    {
        LoadQuests();
        DontDestroyOnLoad(gameObject);
    }

    void LoadQuests()
    {
        for (int i = 0; i < 2; i++)
        {

            string data = File.ReadAllText(Application.streamingAssetsPath + "/Resources/JsonFiles/QuestLog" + i.ToString() + ".json");
            QuestData newQuest = JsonUtility.FromJson<QuestData>(data);
            m_questDictionary.Add(newQuest.id, newQuest);

        }
    }

    //===========================================================================================================
    //퀘스트 수주
    public void RegisterQuest(int questId)
    {
        m_questDictionary[questId].isOnBoard = true;
        File.WriteAllText(Application.streamingAssetsPath + "/Resources/JsonFiles/QuestLog" + questId.ToString()+".json", JsonUtility.ToJson(m_questDictionary[questId]));


    }

    //===========================================================================================================
    //퀘스트 완료
    public void CompleteQuest(int questId)
    {
        m_questDictionary[questId].isCompleted = true;
        m_questDictionary[questId].isOnBoard = false;
        File.WriteAllText(Application.streamingAssetsPath + "/Resources/JsonFiles/QuestLog" + questId.ToString() +".json", JsonUtility.ToJson(m_questDictionary[questId]));

        //이펙트 하나 넣읍시다
        EffectManager.instance.CallEffect("QuestClear", GameObject.Find("Player").transform.position, Quaternion.identity);
    }

    //===========================================================================================================
    //현재 진행중인 퀘스트 찾기
    public QuestData NowQuest()
    {
        //현재 진행중인 퀘스트 반환
        foreach(QuestData elem in m_questDictionary.Values)
        {
            if (elem.isOnBoard == true) return elem;
        }

        //그런거 없으면 null 반환
        return null;
    }


    //======================================================================================================
    //완료된 퀘스트인지 여부
    public bool IsThisCompletedQuest(int questId)
    {
        //맨 처음 퀘스트의 요건 퀘스트(퀘스트 아이디 -1)는 항상 완료 상태
        if (questId == -1) return true;

        return m_questDictionary[questId].isCompleted;
    }


    //======================================================================================================
    //이미 수주한 퀘스트인지 여부

    public bool IsThisOnBoardQuest(int questId)
    {
        return m_questDictionary[questId].isOnBoard;
    }



    //======================================================================================================
    //리셋 퀘스트 로그

    public void ResetAllQuest()
    {
        //딕셔너리 상의 모든 퀘스트 수주 여부 및 완료여부 초기화
        foreach(QuestData elem in m_questDictionary.Values)
        {
            elem.isOnBoard = false;
            elem.isCompleted = false;
        }
        //파일 출력
        foreach (int elem in m_questDictionary.Keys)
        {
            File.WriteAllText(Application.streamingAssetsPath + "/Resources/JsonFiles/QuestLog" + elem.ToString() + ".json", JsonUtility.ToJson(m_questDictionary[elem]));
        }
    }
}
