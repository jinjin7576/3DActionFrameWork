using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    public int Exp { get { return _exp; } 
        set 
        {
            _exp = value;

            int level = 1;
            while (true) //�ѹ��� �뷮�� ����ġ�� ���� ��� ��� ������ �ǰ�
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false) //���� ������ �����ϴ°�?
                    break;
                if (_exp < stat.totalExp) //_exp�� totalExp�� �����ߴ°�?
                    break;
                level++; //�� ������ ��� �������Դٸ� ������
            }
            if (level != Level)
            {
                Debug.Log("Level Up!");
                Level = level;
                SetStat(Level);
            }
        }
    }
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;
        _defense = 5;
        _moveSpeed = 5.0f;
        _exp = 0;
        _gold = 0;

        SetStat(_level);
    }

    private void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[1];
        _hp = stat.maxhp;
        _maxHp = stat.maxhp;
        _attack = stat.attack;
    }

    protected override void OnDead(Stat attacker)
    {
        base.OnDead(attacker);
        Debug.Log("Player Dead");
    }
}
