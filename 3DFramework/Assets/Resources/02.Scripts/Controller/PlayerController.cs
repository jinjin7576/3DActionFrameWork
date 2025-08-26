using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Vector3 _destPos;
    
    public PlayerState _state = PlayerState.Idle;
   
    UI_Button uipopup;

    PlayerStat _stat;
    int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster);

    GameObject _lockTarget;

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }
    void Start()
    {
        //Managers.input.KeyAction -= Onkeyboard;
        //Managers.input.KeyAction += Onkeyboard;
        Managers.input.MouseAction -= OnMouseEvent;
        Managers.input.MouseAction += OnMouseEvent;

        _stat = GetComponent<PlayerStat>();
    }

   

    void Update()
    {
        switch (_state)
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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit) //��Ʈ�� ���� �����̵� �ϴ� �̵��ϰ���
                    {
                        _destPos = hit.point;
                        _state = PlayerState.Moving;
                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster) //�װ� ���� ���Ͷ��
                            _lockTarget = hit.collider.gameObject; //��Ÿ�ٿ� ���
                        else
                            _lockTarget = null; //�ƴ϶�� null�� ��
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget != null) //������ �ִµ� Ÿ���� �ִٸ� Ÿ�ٿ��� �̵�
                    {
                        _destPos = _lockTarget.transform.position;
                    }
                    else if (raycastHit) //�׷��� ������ �浹�� ������ �浹�� ������
                    {
                        _destPos = hit.point;
                    }
                }
                break;
            case Define.MouseEvent.PointerUp:
                {
                    //_lockTarget = null;
                }
                break;
                
        }
    }
    private void UpdateSkill()
    {
        Debug.Log("UpdateSkill");
        Animator ani = GetComponent<Animator>();
        ani.SetBool("attack", true);
    }

    void OnHitEvent()
    {
        Debug.Log("OnHitEvent");
        Animator ani = GetComponent<Animator>();
        ani.SetBool("attack", false);
        _state = PlayerState.Moving;
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
                _state = PlayerState.Skill;
                return;
            }
        }

        Vector3 dir = _destPos - transform.position;

        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetorAddComponent<NavMeshAgent>();

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position, dir.normalized, Color.green);

            if (Physics.Raycast(transform.position, dir, 1.0f, _mask))
            {
                _state = PlayerState.Idle;
                return;
            }
            if (Physics.Raycast(transform.position, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                {
                    _state = PlayerState.Idle;
                }
                return;
            }
            //transform.position += dir.normalized * moveDist;
            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        Animator ani = GetComponent<Animator>();
        ani.SetFloat("speed", _stat.MoveSpeed);
    }
    void UpdateIdle()
    {
        Animator ani = GetComponent<Animator>();
        ani.SetFloat("speed", 0);
    }
}
