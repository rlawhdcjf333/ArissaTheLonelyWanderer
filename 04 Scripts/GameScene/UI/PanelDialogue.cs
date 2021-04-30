using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelDialogue : MonoBehaviour
{
    TextMeshProUGUI m_textPro;
    Button m_button;
    public event System.Action buttonAction = null;
   

    void Start()
    {
        m_textPro = transform.Find("Background/Text").GetComponent<TextMeshProUGUI>();
        m_button = transform.Find("Background").GetComponent<Button>();
        m_button.onClick.AddListener(ButtonAction);

        gameObject.SetActive(false);
    }

    public void ShowDialogue(string input)
    {
        m_textPro.text = input;
        gameObject.SetActive(true);
    }

    public void ExitDialogue()
    {
        m_textPro.text = "";
        
        gameObject.SetActive(false);
    }

    public void ButtonAction()
    {
        if(buttonAction != null)
        {
            buttonAction();
        }
        else
        {
            ExitDialogue();
        }

    }
}
