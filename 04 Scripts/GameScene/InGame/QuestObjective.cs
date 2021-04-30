using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    [SerializeField] int m_questId;
    [SerializeField] GameObject m_target;

    private void Update()
    {
        if (!m_target)
        {

            if (QuestManager.instance.IsThisOnBoardQuest(m_questId))
            {
                QuestManager.instance.CompleteQuest(m_questId);
                Destroy(gameObject);
            }
        }
    }

    
}
