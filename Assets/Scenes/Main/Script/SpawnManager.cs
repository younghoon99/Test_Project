using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject rockPrefab;         // 생성할 돌 프리팹
    public Transform[] spawnPoints;       // 여러 스폰 위치들
    public int poolSize = 10;             // 미리 생성해둘 오브젝트 수
    public float spawnInterval = 2f;      // 스폰 간격

    private Queue<GameObject> rockPool;   // 오브젝트 풀
    private bool isSpawningActive = false; // 돌이 떨어지기 시작했는지 여부
    private bool isFirstTriggerActive = false;  // 첫 번째 트리거가 활성화되었는지
    private bool isSecondTriggerActive = false; // 두 번째 트리거가 활성화되었는지

    void Start()
    {
        // 오브젝트 풀 초기화
        rockPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject rock = Instantiate(rockPrefab);
            rock.SetActive(false);  // 비활성화 상태로 풀에 저장
            rockPool.Enqueue(rock);
        }

        // 일정 간격으로 돌 생성 (자동 반복 스폰)
        InvokeRepeating("CheckAndSpawnRock", 0.1f, spawnInterval);
    }

    // 모든 위치에서 돌을 스폰하는 메서드
    void CheckAndSpawnRock()
    {
        // 트리거가 활성화되고, 오브젝트 풀에 돌이 남아있는 경우에만 돌 생성
        if (isSpawningActive && rockPool.Count > 0)
        {
            // 모든 스폰 위치에서 돌 생성
            foreach (Transform spawnPoint in spawnPoints)
            {
                // 풀에서 돌 가져오기
                GameObject rock = rockPool.Dequeue();
                rock.transform.position = spawnPoint.position;  // 스폰 위치로 설정
                rock.SetActive(true);  // 활성화

                // 일정 시간 후 돌 반환
                StartCoroutine(ReturnToPool(rock, 5f));
            }
        }
    }

    // 첫 번째 트리거가 활성화되었을 때
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") && this.gameObject.CompareTag("FirstTrigger"))
    //    {
    //        if (!isFirstTriggerActive)
    //        {
    //            Debug.Log("check");
    //            isFirstTriggerActive = true;
    //            isSpawningActive = true; // 돌 생성 시작
    //        }
    //    }

    //    if (other.CompareTag("Player") && this.gameObject.CompareTag("SecondTrigger"))
    //    {
    //        isSecondTriggerActive = true;
    //        isSpawningActive = false; // 돌 생성 중지
    //        Debug.Log("check2");
    //    }
    //}

    public void SpawnRock()
    {
        if (!isFirstTriggerActive)
        {
            Debug.Log("check");
            isFirstTriggerActive = true;
            isSpawningActive = true; // 돌 생성 시작
        }
    }

    public void StageEnd()
    {
        isSecondTriggerActive = true;
        isSpawningActive = false; // 돌 생성 중지
        Debug.Log("check2");
    }

    // 첫 번째 트리거가 비활성화되었을 때
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (isFirstTriggerActive && !isSecondTriggerActive)
    //        {
    //            isSpawningActive = false; // 돌 생성 중지
    //        }
    //    }
    //}

    // 돌을 풀에 반환하는 코루틴
    System.Collections.IEnumerator ReturnToPool(GameObject rock, float delay)
    {
        yield return new WaitForSeconds(delay);
        rock.SetActive(false);  // 돌을 비활성화
        rockPool.Enqueue(rock); // 풀에 다시 넣음
    }
}
