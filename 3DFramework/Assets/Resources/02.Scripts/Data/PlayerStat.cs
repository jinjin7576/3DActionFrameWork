using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    public int Exp { get { return _exp; } set { _exp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;
        _hp = 200;
        _maxHp = 200;
        _attack = 30;
        _defense = 5;
        _moveSpeed = 5.0f;

        _exp = 0;
        _gold = 0;
    }
    protected override void OnDead(Stat attacker)
    {
        base.OnDead(attacker);
        Debug.Log("Player Dead");
    }
}
