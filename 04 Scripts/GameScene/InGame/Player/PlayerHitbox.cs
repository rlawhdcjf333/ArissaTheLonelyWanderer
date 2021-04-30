using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    Player m_player;
    GameObject m_gameOver;
    List<HpMoth> m_hpMothGroup = new List<HpMoth>();

    GameObject m_hitter;
    public GameObject hitter { get { return m_hitter; } }

    bool m_hit = false;
    public bool hit { get { return m_hit; } set { m_hit = value; } }

    //=============================================================================
    private void Start()
    {
        m_player = transform.root.GetComponent<Player>();
        m_hpMothGroup.AddRange(m_player.GetComponentsInChildren<HpMoth>());
        m_gameOver = GameObject.Find("Canvas").transform.Find("GameOver").gameObject;
    }

    //=============================================================================
    //충돌 트리거
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("EnemyAttackPoint"))
        {
            if (m_hit == false)
            {
                m_hitter = other.transform.root.gameObject;
                m_hit = true;

                if (m_player.state != Player.State.Guard)
                {
                   m_player.StateConvert(Player.State.Hit);
                }
            }
        }

    }

    //====================================================================
    //hp 감소 함수
    public void LoseMoth()
    {
       foreach (HpMoth elem in m_hpMothGroup)
       {
            if (elem.gameObject.activeInHierarchy)
            {
                elem.TurnOff();
                transform.root.GetComponent<PlayerSfx>().PlayHitSfx();
                return;
            }
       }

        //남은 moth가 없으면 죽음
        EffectManager.instance.CallEffect("Death", transform.position, Quaternion.identity);
        m_gameOver.SetActive(true); 
        Destroy(m_player.gameObject);
    }
    //====================================================================
    //남은 hp 개수 콜
    public int CountMoth()
    {
        int result=0;

        foreach (HpMoth elem in m_hpMothGroup)
        {
            if (elem.gameObject.activeInHierarchy) result++;
        }

        return result;
    }

}
