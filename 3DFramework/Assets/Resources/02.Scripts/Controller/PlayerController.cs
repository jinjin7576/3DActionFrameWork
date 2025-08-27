using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Vector3 _destPos;

    PlayerStat _stat;

    int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster);

    GameObject _lockTarget;

    bool _skillStop = false;
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

    private PlayerState _state = PlayerState.Idle;

    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;
            Animator ani = GetComponent<Animator>();
            switch (_state)
            {
                case PlayerState.Die:

                    break;
                case PlayerState.Idle:
                    ani.CrossFade("WAIT", 0.1f);
                    break;
                case PlayerState.Moving:
                    ani.CrossFade("RUN", 0.1f);
                    break;
                case PlayerState.Skill:
                    ani.CrossFade("Attack", 0.1f, -1, 0); //루프시키는 코드 0부터 다시시작, -1 의미없음
                    break;
            }
        }
    }


    void Start()
    {
        Managers.input.MouseAction -= OnMouseEvent;
        Managers.input.MouseAction += OnMouseEvent;

        _stat = GetComponent<PlayerStat>();

        Managers.UI.MakeWorldSpaceUI<UI_HpBar>(gameObject.transform, "UI_HpBar");
    }

    void Update()
    {
        switch (State)
        {
            case PlayerState.Die:
                UpdateDie();
                break;

            case PlayerState.Moving:
                UpdateMoving();
                break;

            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Skill:
                UpdateSkill();
                break;
        }
    }

    private void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
                if (evt == Define.MouseEvent.PointerUp)
                {
                    _skillStop = true;
                }
                break;
        }
    }
    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = PlayerState.Moving;
                        _skillStop = false; //이때는 Idle일지, 재공격할지 정해지지 않음
                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        {
                            _lockTarget = hit.collider.gameObject;
                        }

                        else
                        {
                            _lockTarget = null;
                        }

                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                    {
                        _destPos = hit.point;
                    }
                }
                break;
            case Define.MouseEvent.PointerUp:
                _skillStop = true;
                break;
        }
    }

    private void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }
    void OnHitEvent()
    {
        Debug.Log("OnHitEvent");
        if (_skillStop)
        {
            State = PlayerState.Idle;
        }
        else
        {
            State = PlayerState.Skill;
        }
    }
    void UpdateDie()
    {

    }
    void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= 1.5f)
            {
                Debug.Log("Skill");
                State = PlayerState.Skill;
                return;
            }
        }

        Vector3 dir = _destPos - transform.position;

        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetorAddComponent<NavMeshAgent>();

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position, dir.normalized, Color.green);

            if (Physics.Raycast(transform.position, dir, 1.0f, _mask))
            {
                State = PlayerState.Idle;
                return;
            }
            if (Physics.Raycast(transform.position, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                {
                    State = PlayerState.Idle;
                }
                return;
            }
            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }
    void UpdateIdle()
    {

    }
}
