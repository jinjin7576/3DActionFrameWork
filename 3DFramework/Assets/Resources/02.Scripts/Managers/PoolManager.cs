using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }
        Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name} _Root";

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        private Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name; //(clone)이 나오지 않도록 처리
            return go.GetorAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null) return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;
            if(_poolStack.Count > 0)
            {
                poolable = _poolStack.Pop();
            }
            else
            {
                poolable = Create();
            }
            poolable.gameObject.SetActive(true);

            //DontDestroyOnLoad 해체 용도
            if (parent == null)
                poolable.transform.parent = Managers.Scnen.CurrentScene.transform;

            poolable.transform.parent = parent;
            poolable.IsUsing = true;
            return poolable;
        }
    }
    #endregion

    //각각 풀들은 키를 이용해 이름을 이용해서 관리하기
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();

    Transform _root;
    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }

    }

    public void Push (Poolable poolable)
    {
        string name = poolable.gameObject.name;

        //만약 생성을 한번도 안하고 풀이 없는 상태에서 Push를 한 상태
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
        {
            CreatPool(original);
        }
        return _pool[original.name].Pop(parent);
    }

    private void CreatPool(GameObject original , int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false) return null;
        return _pool[name].Original;
    }
    
    public void Clear()
    {
        foreach(Transform child in _root)
        {
            GameObject.Destroy(child.gameObject);
        }
        _pool.Clear();
    }
}
