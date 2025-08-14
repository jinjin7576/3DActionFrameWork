using UnityEngine;

public class UI_Inven : UI_Scene
{

    enum GameObjects
    {
        GridPanel,
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        //내부 초기화
        foreach (Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }
        
        for (int i = 0; i < 12; i++) //실제로는 인벤토리 정보를 참고해야하지만, 테스트이니까.
        {
            UI_Inven_Item ivenitem = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform);
            ivenitem.SetInfo($"테스트 {i}번");
        }
    }
}
