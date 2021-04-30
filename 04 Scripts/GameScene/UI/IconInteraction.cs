using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool m_isDown;

    //==================================================
    public event System.Action EventButtonDown;
    public event System.Action EventButtonStay;
    public event System.Action EventButtonUp;
    //==================================================

    private void Update()
    {
        //누르고 있는 동안 할거
        if (m_isDown)
        {
            EventButtonStay?.Invoke();
        }
    }

    //==================================================

    public void OnPointerDown(PointerEventData eventData)
    {
        //눌리는 그 순간
        EventButtonDown?.Invoke();
        m_isDown = true;

    }
    //==================================================

    public void OnPointerUp(PointerEventData eventData)
    {
        //떼는 그 순간
        EventButtonUp?.Invoke();
        m_isDown = false;

    }
}
