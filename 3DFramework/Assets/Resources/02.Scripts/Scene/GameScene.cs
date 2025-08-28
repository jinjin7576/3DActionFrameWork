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
        Dictionary<int, Data.Stat> Dict = Managers.Data.StatDict;
        gameObject.GetorAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        Managers.Game.Spawn(Define.WorldObject.Monster, "Monster");

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
