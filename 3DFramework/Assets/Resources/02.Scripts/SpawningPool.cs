using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0; //���� ���� ��
    int _reserveCount = 0; //������ ���� �� (����������, ���� �����)

    [SerializeField]
    int _keepMonsterCount = 0; //�������Ѿ� �ϴ� ������ ��

    [SerializeField]
    Vector3 _spawnPos; //���� �߽���
    [SerializeField]
    float _spawnRadius = 15.0f; //���� �ݰ�
    [SerializeField]
    float _spawnTimer = 5; //���� Ÿ��

    private void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }
    public void AddMonsterCount(int value) { _monsterCount += value; }

    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }

    private void Update()
    {
        while (_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }
    
    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, _spawnTimer));
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Monster");

        NavMeshAgent nma = obj.GetorAddComponent<NavMeshAgent>();
        Vector3 randPos;
        while (true)
        {
            Vector3 randDir = UnityEngine.Random.insideUnitSphere * UnityEngine.Random.Range(0, _spawnRadius);
            randDir.y = 0;
            randPos = _spawnPos + randDir;

            //�� �� �ִ� ������ �ƴ��� �Ǵ��ϱ� ���ؼ�
            NavMeshPath path = new NavMeshPath();
            //�׺�޽��� �̵����� ���θ� ��ȯ�Ѵ�. ���� ������ �׺�޽� ���̹Ƿ� ���� ����
            if (nma.CalculatePath(randPos, path))
                break;
        }
        obj.transform.position = randPos;
        _reserveCount--; //����� ���� ���� ������ ������ �ٽ� �ʱ�ȭ
    }
}
