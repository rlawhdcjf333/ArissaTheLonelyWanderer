using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    RectTransform m_touchArea;
    Image m_stick;
    Image m_origin;

    [SerializeField] float m_stickRange =100f;
    bool m_isDown = false;
    public event System.Action<Vector3, float> EventStickMove;
    public event System.Action EventStickMoveEnd;

    //======================================================

    private void Start()
    {
        m_stick = transform.Find("Stick").GetComponent<Image>();
        m_origin = transform.Find("Origin").GetComponent<Image>();
        m_touchArea = m_origin.transform.GetComponent<RectTransform>();
    }

    //======================================================
    private void Update()
    {
        if(m_isDown)
        {
            Vector3 dir = m_stick.rectTransform.anchoredPosition - m_origin.rectTransform.anchoredPosition;
            EventStickMove?.Invoke(dir, m_stickRange);
        }
        else
        {
            EventStickMoveEnd?.Invoke();
        }
    }
    
    //======================================================
    public void OnPointerDown(PointerEventData eventData)
    {
        StickUpdate(eventData);
        m_isDown = true;
    }
    
    //======================================================
    public void OnPointerUp(PointerEventData eventData)
    {
        m_stick.transform.position = m_origin.transform.position;
        m_isDown = false;
    }
    //======================================================

    public void OnDrag(PointerEventData eventData)
    {
        StickUpdate(eventData);
    }


    //======================================================

    void StickUpdate(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_touchArea, eventData.position, eventData.pressEventCamera, out Vector2 localVector);

        m_stick.rectTransform.anchoredPosition = localVector;

        if(localVector.magnitude > m_stickRange)
        {
            m_stick.rectTransform.anchoredPosition = localVector.normalized * m_stickRange;
        }
    }

    //======================================================
   
}
