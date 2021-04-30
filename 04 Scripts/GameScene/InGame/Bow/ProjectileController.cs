using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //==============================================
    //필드
    [SerializeField] string m_flash;
    [SerializeField] string m_hitEffect;

    float m_holding =0f;
    GameObject m_player;
    [SerializeField] float m_speed =20f;

    Rigidbody m_body;

    //=============================================
    //최초 초기화
    private void Awake()
    {
        m_body = GetComponent<Rigidbody>();
    }

    //==============================================
    //날아가랏
    void FixedUpdate()
    {
        //홀딩여하에 따라 최대 두배까지 가속가능
        m_body.velocity = transform.forward * (m_speed + m_holding*m_speed);
    }
    //==============================================
    //충돌 트리거
    private void OnTriggerEnter(Collider other)
    {
        //충돌한 콜라이더가 타겟 포인트 컴포넌트를 지니고 있다면, 혹은 그게 맵이면
        if (other.transform.GetComponent<TargetPoint>() !=null || other.transform.root.CompareTag("Map"))
        {
            //충격량 연산, 홀딩여하에 따라 최대 3배 화력, 물론 리지드 바디가 있는 친구만
            if(other.transform.root.GetComponent<Rigidbody>() !=null)
            other.transform.root.GetComponent<Rigidbody>().AddForce(transform.forward*(1+2*m_holding), ForceMode.Impulse);
            //투사체 비활성
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        //홀딩 시간 갱신
        m_holding = GameObject.Find("Player").GetComponent<Player>().holdingTime;
        //생겨날때 펑
        EffectManager.instance.CallEffect(m_flash, transform.position, transform.rotation);
    }

    private void OnDisable()
    {
        //사라질때 펑
        EffectManager.instance.CallEffect(m_hitEffect, transform.position, transform.rotation);
        
    }
}
