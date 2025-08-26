using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Vector3 _destPos;
    
    public PlayerState _state = PlayerState.Idle;
   
    UI_Button uipopup;

    PlayerStat _stat;

    Texture2D _attackIcon;
    Texture2D _basicIcon;

    int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster);

    CursorType _cursorType = CursorType.None;
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }
    public enum CursorType
    {
        None,
        Attack,
        Hand,
    }
    void Start()
    {
        //Managers.input.KeyAction -= Onkeyboard;
        //Managers.input.KeyAction += Onkeyboard;
        Managers.input.MouseAction -= OnMouseCliked;
        Managers.input.MouseAction += OnMouseCliked;

        _stat = GetComponent<PlayerStat>();

        _attackIcon = Managers.Resource.Load<Texture2D>("06.Res/Attack");//공격일때의 커서
        _basicIcon = Managers.Resource.Load<Texture2D>("06.Res/Basic");//공격일때의 커서
    }

   

    void Update()
    {
        UpdateMouseCursor();
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
        }
    }

    /*
     * void Onkeyboard()
    {
        //입력을 받아 전후좌우 이동
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            //transform.Translate(Vector3.back * Time.deltaTime * _speed);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            //transform.Translate(Vector3.left * Time.deltaTime * _speed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            //transform.Translate(Vector3.right * Time.deltaTime * _speed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.UI.ClosePopupUI(uipopup);
        }
    }
    */

    private void OnMouseCliked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click) return;
        if (_state == PlayerState.Die) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
        }

        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
        {
            Debug.Log("Monster Click!");
        }
        else
        {
            Debug.Log("Ground Click!");
        }
    }
    
    void UpdateDie()
    {

    }
    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;

        if (dir.magnitude < 0.01f)
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

    void UpdateMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackIcon,new  Vector2(_attackIcon.width/ 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_basicIcon, new Vector2(_attackIcon.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }
}
