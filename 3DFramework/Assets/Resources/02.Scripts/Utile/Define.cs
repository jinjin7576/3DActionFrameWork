using UnityEngine;

public class Define
{
    public enum CameraMode
    {
        QuarterView,
    }
    public enum MouseEvent
    {
        Press,
        Click,
    }
    public enum UIEvent
    {
        Click,
        Drag, //나중에 필요하다면 Drag 관련 이벤트를 추가해도 됨(DragEnd, DragBegin 등)
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }
    public enum Layer
    {
        Ground = 9,
        Block = 12,
        Monster = 13,
    }
}
