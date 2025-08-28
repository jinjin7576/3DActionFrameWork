using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected GameObject _lockTarget;

    [SerializeField]
    protected Vector3 _destPos;

    [SerializeField]
    protected Define.State _state = Define.State.Idle;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    public Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            Animator ani = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Die:

                    break;
                case Define.State.Idle:
                    ani.CrossFade("WAIT", 0.1f);
                    break;
                case Define.State.Moving:
                    ani.CrossFade("RUN", 0.1f);
                    break;
                case Define.State.Skill:
                    ani.CrossFade("Attack", 0.1f, -1, 0);
                    break;
            }
        }
    }
    private void Start()
    {
        Init();
    }
    public abstract void Init();

    protected void Update()
    {
        switch (State)
        {
            case Define.State.Die:
                UpdateDie();
                break;

            case Define.State.Moving:
                UpdateMoving();
                break;

            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
        }
    }
    protected virtual void UpdateSkill() { }

    protected virtual void UpdateIdle() { }

    protected virtual void UpdateMoving() { }

    protected virtual void UpdateDie() { }
}
