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
        Drag, //���߿� �ʿ��ϴٸ� Drag ���� �̺�Ʈ�� �߰��ص� ��(DragEnd, DragBegin ��)
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
