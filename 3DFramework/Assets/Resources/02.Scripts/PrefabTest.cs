using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    string cubePath = "prefabs/Cube";
    void Start()
    {
        GameObject cube = Managers.Resource.Load<GameObject>(cubePath);
        Instantiate(cube);
        Destroy(cube, 3f);

        GameObject newCube = Managers.Resource.Instatiate("Cube");
        Managers.Destroy(newCube, 3f);
    }
}
