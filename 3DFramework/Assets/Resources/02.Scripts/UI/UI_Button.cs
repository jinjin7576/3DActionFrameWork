using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : UI_Base
{
    enum Buttons
    {
        PointButton,
    }
    enum Texts
    {
        PointText,
        ScoreText,
    }
    enum GameObjects
    {
        TestObject,
    }
    enum Images
    {
        Image,
    }
    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //Get<Text>((int)Texts.ScoreText).text = "Test";
        GetText((int)Texts.PointText).text = "Test";

        GameObject go = GetImage((int)Images.Image).gameObject;
        //�̺κ��� UI_Base���� �Լ� ���·� �Ѵٸ�...?
        UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        evt.OnDragHandler +=
            ((PointerEventData data) => { go.transform.position = data.position; });
    }
}

