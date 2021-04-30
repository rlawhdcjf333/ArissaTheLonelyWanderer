using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    public enum State : int
    {
        Idle, Move, Attack, Guard, Dodge, Hit
    }

    readonly int m_animHashKeyState = Animator.StringToHash("State");

    //=======================================
    [SerializeField] State m_state;
    public State state { get { return m_state; } set { m_state = value; } }
    [SerializeField] float m_maxSpeed =5f;
    [SerializeField] float m_maxRotationSpeed = 5f;

    Vector3 m_keyInput;
    Rigidbody m_body;
    Animator m_animator;
    FollowCamera m_followCamera;

    

    //=======================================

    private void Start()
    {
        m_body = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_followCamera = Camera.main.GetComponent<FollowCamera>();
        m_arrowEquipPoint = GameObject.FindGameObjectWithTag("ArrowEquipPoint");
        m_aim = GameObject.FindGameObjectWithTag("Aim");
        m_FOV = GetComponent<FOV>();

        GameObject.Find("PanelJoystick").GetComponent<PanelJoystick>().EventStickMove += Move;
        GameObject.Find("PanelJoystick").GetComponent<PanelJoystick>().EventStickMoveEnd += MoveEnd;
        GameObject.Find("PanelSkillButton").GetComponent<PanelSkillButton>().EventPlaySkill += PlaySkill;
        GameObject.Find("PanelSkillButton").GetComponent<PanelSkillButton>().EventHoldSkill += HoldSkill;
        GameObject.Find("PanelSkillButton").GetComponent<PanelSkillButton>().EventExitSkill += ExitSkill;

        GameObject arrowPrefab = Resources.Load("Prefab/Arrow") as GameObject;
        m_arrow = Instantiate(arrowPrefab, m_arrowEquipPoint.transform);
        m_arrow.SetActive(false);

    }
    //=======================================
    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") !=0f) KeyMove();

    }

    //=======================================

    public void StateConvert(State state)
    {
        if(IsThisState(state)) return;

        m_state = state;
        m_animator.SetInteger(m_animHashKeyState, (int)m_state);

    }

    //=======================================

    bool IsThisState(State state)
    {
        return m_state == state;
    }

    //===============================================================

    void PlaySkill(SkillInfo input)
    {
        if (input.m_name == "BasicAttack") AttackStart();
        if (input.m_name == "AimMode") Aiming();
        if (input.m_name == "Dodge") Dodge();
        if (input.m_name == "Guard") Guard();

    }

    //=================================================================

    void HoldSkill(SkillInfo input)
    {
        if (input.m_name == "BasicAttack") AttackHold();
    }


    //=================================================================

    void ExitSkill(SkillInfo input)
    {
        if (input.m_name == "BasicAttack") AttackExit();

    }
    //=================================================================
    void Aiming()
    {
        transform.GetComponent<PlayerSfx>().PlayAimModeSfx();
        m_followCamera.CameraMode = 
            m_followCamera.CameraMode == FollowCamera.Mode.FPS ? FollowCamera.Mode.Follow : FollowCamera.Mode.FPS;
    }
    //=================================================================

    void Dodge()
    {
        StateConvert(State.Dodge);
        if (m_body.velocity == Vector3.zero) m_body.velocity = transform.forward * 15f;
        else m_body.velocity = m_body.velocity.normalized * 15f;
    }
    //=================================================================

    void DodgeEnd()
    {
        StateConvert(State.Idle);
    }

    //=================================================================
    void Guard()
    {
        StateConvert(State.Guard);
    }
    //=================================================================


}
