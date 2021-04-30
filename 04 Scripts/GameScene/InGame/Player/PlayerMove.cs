using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    readonly int m_animHashKeyMoveBlend = Animator.StringToHash("MoveBlend");
    bool m_canMove = true;
    public bool canMove {get {return m_canMove;} set { m_canMove = value;}}

    //======================================
    // 기본 이동 로직
    public void Move(Vector3 dir, float maxRange)
    {
        if (m_state != State.Idle && m_state != State.Move) return;
        if (m_canMove == false) return;

        //유저 조작의 최대 조작 가능량에 대한 비(MoveBlendParam)
        float MoveBlendParam = (maxRange - dir.magnitude) / maxRange;
        MoveBlendParam = 1 - MoveBlendParam;

        //들어온 스틱 이동 벡터를 정규화해서 단위(방향) 벡터화
        dir = dir.normalized;

        Vector3 corrected = Vector3.zero;
        Quaternion targetRotation;

        //1인칭 시점
        if (m_followCamera.CameraMode == FollowCamera.Mode.FPS)
        {
            //2차원 스틱의 입력 벡터를 캐릭터 위치 벡터를 기준으로 하는 로컬 3차원 방향 벡터로 재조정
            corrected.x = dir.x;
            corrected.z = dir.y;
            corrected = transform.TransformDirection(corrected);
            corrected.y = 0f;

            //1인칭 모드에서는 뒤로 향하는 입력 벡터가 오면 전방을 바라보도록 회전
            targetRotation = dir.y < 0 ?
                Quaternion.LookRotation(transform.forward) : Quaternion.LookRotation(corrected);
        }
        //그외 == 3인칭 시점
        else
        {
            //2차원 스틱 방향 벡터를 메인 카메라 위치 벡터를 기준으로 하는 로컬 3차원 방향 벡터로 재조정
            corrected = Camera.main.transform.TransformDirection(dir);
            corrected.y = 0f;

            //목표 회전값
            targetRotation = Quaternion.LookRotation(corrected);
        }
        //리지드바디에 MoveBlendParam에 비례하는 이동속도 부여
        m_body.velocity = corrected * m_maxSpeed * MoveBlendParam;

        //캐릭터가 최종적으로 바라봐야하는 방향 쿼터니언
        //Quaternion targetRotation = Quaternion.LookRotation(corrected);

        //프레임 당 회전 속도(MoveBlendParam에 비례) 값으로 회전량 선형보간 -->smooth turning
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_maxRotationSpeed * MoveBlendParam * Time.deltaTime);

        //이동 모션 블렌드 트리 조작 변수 대입(걷기 >>> 달리기)
        m_animator.SetFloat(m_animHashKeyMoveBlend, MoveBlendParam);
        StateConvert(State.Move);
    }
    //====================================

    public void MoveEnd()
    {
        if (m_state != State.Move) return;

        //리지드 바디 속도 초기화
        m_body.velocity = Vector3.zero;

        //모션 블렌드 변수 초기화
        m_animator.SetFloat(m_animHashKeyMoveBlend, 0f);
        StateConvert(State.Idle);
    }


    //==================================================
    //키보드 이동

    private void KeyMove()
    {
        if (m_state != State.Idle && m_state != State.Move) return;
        if (m_canMove == false) return;

        m_keyInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

        if (m_keyInput != Vector3.zero)
        {
            if (m_followCamera.CameraMode == FollowCamera.Mode.Follow)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_keyInput), m_maxRotationSpeed * Time.deltaTime);
            else
            {
                if(m_keyInput.z>0f)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.TransformDirection(m_keyInput)), m_maxRotationSpeed * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.LookRotation(transform.forward);
                }
                m_keyInput = transform.TransformDirection(m_keyInput);
            }

            m_body.velocity = m_keyInput * m_maxSpeed;

            m_animator.SetFloat(m_animHashKeyMoveBlend, m_keyInput.magnitude);
            StateConvert(State.Move);
        }

    }
}
