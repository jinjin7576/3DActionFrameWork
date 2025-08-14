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
        //���� �ʱ�ȭ
        foreach (Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }
        
        for (int i = 0; i < 12; i++) //�����δ� �κ��丮 ������ �����ؾ�������, �׽�Ʈ�̴ϱ�.
        {
            UI_Inven_Item ivenitem = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform);
            ivenitem.SetInfo($"�׽�Ʈ {i}��");
        }
    }
}
