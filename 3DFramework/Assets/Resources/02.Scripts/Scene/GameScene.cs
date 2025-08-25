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

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < 2; i++)
        {
            list.Add(Managers.Resource.Instatiate("Player"));
        }
        foreach (GameObject obj in list)
        {
            Managers.Resource.Destroy(obj);
        }
           
    }

    public override void Clear()
    {
        
    }

    void Start()
    {
        
    }
}
