using UnityEngine;

public class Test : MonoBehaviour
{
    string path = "UI/Popup/UI_Button";
    [SerializeField]
    GameObject UI_Button;
    void Start()
    {
       UI_Button = Managers.Resource.Instatiate(path);
    }
}
