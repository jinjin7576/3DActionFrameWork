using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup
{
    int _score = 0;

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
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //Get<Text>((int)Texts.ScoreText).text = "Test";
        GetText((int)Texts.PointText).text = "Test";

        GameObject go = GetImage((int)Images.Image).gameObject;
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);
    }

    private void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"Á¡¼ö : {_score}";
    }
}

