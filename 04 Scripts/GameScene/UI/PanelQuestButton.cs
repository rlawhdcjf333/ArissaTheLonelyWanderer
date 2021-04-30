using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelQuestButton : MonoBehaviour
{

    GameObject m_effect;
    Button m_button;

    QuestInfo m_questInfo;
    AudioSource m_questInfoSfx;

    private void Start()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(ShowQuestInfo);
        m_questInfo = transform.Find("QuestInfo").GetComponent<QuestInfo>();
        m_questInfoSfx = transform.root.Find("Sfx/QuestInfoSfx").GetComponent<AudioSource>();
    }

    public void ShowQuestInfo()
    {
        QuestData nowQuest = QuestManager.instance.NowQuest();

        m_questInfo.SetQuestLog(nowQuest);
        m_questInfoSfx.Play();
        m_questInfo.gameObject.SetActive(!m_questInfo.gameObject.activeInHierarchy);
    }

    public void QuestRegister()
    {
        StartCoroutine(CoroutineQuestRegisterEffect());
    }

    IEnumerator CoroutineQuestRegisterEffect()
    {
        //사운드 온
        GameObject.Find("Canvas/Sfx/QuestAcceptSfx").GetComponent<AudioSource>().Play();

        //이펙트 하나 만든다
        m_effect = Instantiate(Resources.Load<GameObject>("FX/QuestionMark"));
        m_effect.layer = LayerMask.NameToLayer("UI");

        //플레이어 위치를 찾는다
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        //화면 좌표로 바꿔준다
        Vector2 screenPos = Camera.main.WorldToScreenPoint(playerPos);

        //바꿔준 화면좌표를 캔버스 상의 월드 좌표로 바꿔준다
        RectTransform canvasRect = transform.root.GetComponent<RectTransform>();
        Camera canvasCam = GameObject.Find("Camera4UI").GetComponent<Camera>();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, screenPos, canvasCam, out Vector3 worldPos);


        //거기다 옮긴다
        m_effect.transform.position = worldPos;

        while (true)
        {
            //이동
            m_effect.transform.position = Vector3.Slerp(m_effect.transform.position, transform.position, Time.deltaTime*2f);

            float dist = (transform.position - m_effect.transform.position).magnitude;

            if (dist < 1)
            {
                Destroy(m_effect);
                break;
            }

            yield return null;
        }

    }

}
