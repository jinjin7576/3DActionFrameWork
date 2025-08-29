using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{

    [SerializeField]
    float _scanRange = 10;

    [SerializeField]
    float _attackRange = 2;

    public override void Init()
    {
        _stat = gameObject.GetComponent<Stat>();
        WorldObjectType = Define.WorldObject.Monster;
        if (gameObject.GetComponentInChildren<UI_HpBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HpBar>(transform);
        }
    }

    protected override void UpdateIdle()
    {
        Debug.Log("Monster Idle State");

        GameObject player = Managers.Game.GetPlayer();

        if (player == null) return;
        float distance = (player.transform.position - transform.position).magnitude;
        if (distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
        }
    }

    protected override void UpdateMoving()
    {
        Debug.Log("Monster Moving State");
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= _attackRange) //공격으로 넘어가야 하는가?
            {
                NavMeshAgent nma = gameObject.GetorAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
        }
        Vector3 dir = _destPos - transform.position; //타겟은 있지만, 공격 거리 밖에 있음 -> 이동
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetorAddComponent<NavMeshAgent>();
            nma.SetDestination(_destPos);
            nma.speed = _stat.MoveSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
    {
        Debug.Log("Monster Skill State");

        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    protected override void OnHitEvent()
    {
        base.OnHitEvent();

            Stat targetStat = _lockTarget.GetComponent<Stat>();

            //연속공격
            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                {
                    State = Define.State.Skill;
                }
                else
                {
                    State = Define.State.Moving;
                }
            }
            else
            {
                State = Define.State.Idle;
            }
        
    }

    protected override void UpdateDie()
    {
        Debug.Log("Monster Die State");
    }
}
