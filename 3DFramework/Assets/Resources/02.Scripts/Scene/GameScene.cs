using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        _sceneType = Define.Scene.Game;
        //Managers.UI.ShowScenenUI<UI_Inven>();
        //Dictionary<int, Data.Stat> Dict = Managers.Data.StatDict;
        gameObject.GetorAddComponent<CursorController>();

        //플레이어 생성
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        //몬스터 생성
        //Managers.Game.Spawn(Define.WorldObject.Monster, "Monster");

        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetorAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);

        //_camera._player = player;
        _camera.SetPlayer(player);
    }

    public override void Clear()
    {
        
    }

    void Start()
    {
        
    }
}
