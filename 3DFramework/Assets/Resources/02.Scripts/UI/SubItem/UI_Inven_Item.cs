using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inven_Item : UI_Base
{
    string _name;
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        Get<GameObject>((int)GameObjects.ItemIcon).BindUIEvent((PointerEventData) => { Debug.Log($"아이템 클릭, {_name} "); } , Define.UIEvent.Click);
    }
    public void SetInfo(string name)
    {
        _name = name;
    }
}
