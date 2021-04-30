using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheEnd : MonoBehaviour
{
    TextMeshProUGUI m_text;
    Material m_textMaterial;
    AudioSource m_endingSfx;
    readonly string[] m_textContents = new string[5];
    float m_dilateValue;

    private void Awake()
    {
        m_endingSfx = GetComponent<AudioSource>();
        m_text = GetComponent<TextMeshProUGUI>();
        m_textMaterial = Resources.Load<Material>("Fonts/GodoB SDF");

        m_textContents.SetValue("The End", 0);
        m_textContents.SetValue("제작 : 김종철", 1);
        m_textContents.SetValue("Noncommercial Portfolio", 2);
        m_textContents.SetValue("Made with Unity/C#", 3);
        m_textContents.SetValue("Thank you for Playing!", 4);


        //텍스트 머테리얼 쉐이더 dilate 시작값을 -1로 초기화
        SetTextMaterialDilate(-1);
    }


    private void OnEnable()
    {
        //퀘스트 리셋
        QuestManager.instance.ResetAllQuest();


        StartCoroutine(CoroutineEnding());
    }

    IEnumerator CoroutineEnding()
    {
        //커튼 콜
        GameObject curtain = Instantiate(Resources.Load<GameObject>("Prefab/Curtain"), transform);
        Destroy(curtain, 2.1f);

        //브금 끄고
        GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
        //엔딩 브금 온
        m_endingSfx.Play();

        yield return new WaitForSeconds(1f);

        //==============================================================================
        for(int i=0; i< 5; i++) 
        {
            //텍스트 설정
            m_text.text = m_textContents[i];
            //텍스트 키고
            m_text.enabled = true;
            //서서히 나타나게
            m_dilateValue = -1;
            while(m_dilateValue <= 0)
            {
                m_dilateValue += Time.deltaTime/3;
                SetTextMaterialDilate(m_dilateValue);
                yield return null;
            }
            //다나오고 1초 대기
            yield return new WaitForSeconds(1f);
            //서서히 지워지게
            while(m_dilateValue >= -1)
            {
                m_dilateValue -= Time.deltaTime/3;
                SetTextMaterialDilate(m_dilateValue);

                yield return null;
            }
            m_text.enabled = false;
            yield return new WaitForSeconds(1f);
        }
        //==============================================================================
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    }


    void SetTextMaterialDilate(float val)
    {
        m_textMaterial.SetFloat("_FaceDilate", val);
    }


}
