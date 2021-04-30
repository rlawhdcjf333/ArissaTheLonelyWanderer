using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    Button m_button;
    void Start()
    {
        m_button = GetComponent<Button>();

        m_button.onClick.AddListener(LoadPreGameScene);
    }


    public void LoadPreGameScene()
    {
        SceneManager.LoadScene("PreGameScene");
    }
}
