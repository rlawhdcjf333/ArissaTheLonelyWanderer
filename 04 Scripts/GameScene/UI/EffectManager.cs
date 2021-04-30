using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    //싱글턴 
    static EffectManager g_instance;
    public static EffectManager instance
    {
        get
        {
            if (!g_instance)
            {
                GameObject effectManagerObject = new GameObject("EffectManager");
                effectManagerObject.AddComponent<EffectManager>();

                g_instance = effectManagerObject.GetComponent<EffectManager>();
            }
            return g_instance;
        }
    }
    //=============================================================================================
    //컨테이너 변수

    Dictionary<string, List<GameObject>> m_objPool = new Dictionary<string, List<GameObject>>();
    //=============================================================================================
    // 풀 초기화
    void Awake()
    {
        RegisterOnPool("TargetMark", 5);
        RegisterOnPool("ArrowEffect", 5);
        RegisterOnPool("ArrowFlash", 5);
        RegisterOnPool("ArrowHit", 5);
        RegisterOnPool("PowerDraw", 5);
        RegisterOnPool("AimLine", 5);
        RegisterOnPool("CounterBlast", 5);
        RegisterOnPool("LosingMoth", 5);
        RegisterOnPool("Death", 1);
        RegisterOnPool("QuestClear", 1);

        DontDestroyOnLoad(gameObject);
    }
    //=============================================================================================
    //이펙트 콜 메서드(리턴 bool)
    public bool CallEffect(string name, Vector3 pos, Quaternion rot)
    {
        List<GameObject> list;

        if(m_objPool.TryGetValue(name, out list))   
        {
            //풀에서 비활성 중인 이펙트 오브젝트 콜
            foreach(GameObject elem in list)
            {
                if(!elem.activeInHierarchy && elem !=null)
                {
                    elem.transform.position = pos;
                    elem.transform.rotation = rot;
                    elem.SetActive(true);
                    return true;
                }
            }

            //풀이 부족할 때 (풀을 한칸 늘려서 맨끝에 들어간 놈 콜)
            GameObject tmp = Instantiate(Resources.Load<GameObject>("FX/" + name));
            list.Add(tmp);
            tmp.transform.position = pos;
            tmp.transform.rotation = rot;
            tmp.SetActive(true);
            return true;
        }
        else
        {
            //애초에 잘못된 이름으로 검색한 경우 false 반환
            return false;
        }
    }

    //================================================
    //이펙트 콜 메서드(리턴 gameobject)
    public GameObject CallEffect(string name)
    {
        List<GameObject> list;

        if (m_objPool.TryGetValue(name, out list))
        {
            //풀에서 비활성 중인 이펙트 오브젝트 리턴
            foreach (GameObject elem in list)
            {
                if(elem)
                {
                    if (!elem.activeInHierarchy)
                    {
                        return elem;
                    }
                }
            }

            //풀이 부족할 때 (풀을 한칸 늘려서 맨끝에 들어간 놈 리턴)
            GameObject tmp = Instantiate(Resources.Load<GameObject>("FX/" + name));
            list.Add(tmp);
            return tmp;
        }
        else
        {
            //애초에 잘못된 이름으로 검색한 경우 null 반환
            return null;
        }
    }
    //================================================
    //오브젝트 풀 등록 메서드
    void RegisterOnPool(string name, int num)
    {
        List<GameObject> tmp = new List<GameObject>();
        for (int i = 0; i < num; i++)
        {
            GameObject elem = Instantiate(Resources.Load<GameObject>("FX/"+name));
            DontDestroyOnLoad(elem);
            elem.SetActive(false);
            tmp.Add(elem);
        }

        m_objPool.Add(name, tmp);
    }

    //================================================
    //특정 풀의 활성 이펙트 모두 비활성

    public bool CutEffect(string name)
    {
        List<GameObject> list;
        if(m_objPool.TryGetValue(name, out list))
        {
            foreach(GameObject elem in list)
            {
                if(elem) elem.SetActive(false);
            }
            return true;
        }
        else
        {
            return false;
        }
    }


}
