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
        Managers.UI.ShowScenenUI<UI_Inven>();
        Dictionary<int, Data.Stat> Dict = Managers.Data.StatDict;
    }

    public override void Clear()
    {
        
    }

    void Start()
    {
        
    }
}
