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
        
        for (int i = 0; i < 8; i++) //�����δ� �κ��丮 ������ �����ؾ�������, �׽�Ʈ�̴ϱ�.
        {
            
        }
    }
}
