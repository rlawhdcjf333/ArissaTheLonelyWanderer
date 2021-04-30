using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public enum Mode : int
    {
        Follow,
        FPS
    }

    [SerializeField] Mode m_mode = Mode.Follow;
    [SerializeField] GameObject m_lens;
    public Mode CameraMode { get { return m_mode; } set { m_mode = value; } }

    //=========================================================================
    GameObject m_target;
    Vector3 m_targetPos;
    [SerializeField] Vector3 m_cameraDistanceVector;
    //=========================================================================
    private void Start()
    {
        //HeadTop은 플레이어 캐릭터 rig 중 대략 눈에 해당하는 위치
        m_target = GameObject.Find("HeadTop");
    }
    //=================================================================

    private void LateUpdate()
    {
        //플레이어가 살아있어서 여기저기 돌아다닌다는 가정하에
        if (m_target != null)
        {
            //카메라가 바라보는 목표점 == 캐릭터의 HeadTop
            m_targetPos = m_target.transform.position;

            switch(m_mode)
            {
                // 카메라 모드 Follow
                case Mode.Follow:
                    //어안 렌즈 오프
                    m_lens.SetActive(false);
                    //시야각 60
                    transform.GetComponent<Camera>().fieldOfView = 60;
                    //카메라 위치 == 목표점 위치 + 지정된 거리
                    transform.position = m_targetPos + m_cameraDistanceVector;
                    //목표점을 바라보도록 회전
                    transform.LookAt(m_targetPos);
                    break;

             //===================================================================
                
                // 카메라 모드 FPS
                case Mode.FPS:
                    //어안 렌즈 온
                    m_lens.SetActive(true);
                    //시야각 120
                    transform.GetComponent<Camera>().fieldOfView = 120;
                    //카메라 위치 == 목표점 위치
                    transform.position = m_targetPos;
                    //목표의 전방을 바라보도록 회전
                    transform.rotation = Quaternion.LookRotation(m_target.transform.forward);
                    break;

            }
        }
    }
    //=================================================================

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, m_targetPos, Color.red);
    }
    //=================================================================




}
