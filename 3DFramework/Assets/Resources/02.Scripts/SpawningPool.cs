using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0; //현재 몬스터 수
    int _reserveCount = 0; //생성할 몬스터 수 (생성지연된, 생성 예약된)

    [SerializeField]
    int _keepMonsterCount = 0; //유지시켜야 하는 몬스터의 수

    [SerializeField]
    Vector3 _spawnPos; //스폰 중심점
    [SerializeField]
    float _spawnRadius = 15.0f; //스폰 반경
    [SerializeField]
    float _spawnTimer = 5; //리젠 타임

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

            //갈 수 있는 곳인지 아닌지 판단하기 위해서
            NavMeshPath path = new NavMeshPath();
            //네비메쉬로 이동가능 여부를 반환한다. 갈수 있으면 네비메쉬 위이므로 루프 종료
            if (nma.CalculatePath(randPos, path))
                break;
        }
        obj.transform.position = randPos;
        _reserveCount--; //예약된 것이 생성 됐으니 예약은 다시 초기화
    }
}
