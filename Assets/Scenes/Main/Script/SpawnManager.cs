using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject rockPrefab;         // ������ �� ������
    public Transform[] spawnPoints;       // ���� ���� ��ġ��
    public int poolSize = 10;             // �̸� �����ص� ������Ʈ ��
    public float spawnInterval = 2f;      // ���� ����

    private Queue<GameObject> rockPool;   // ������Ʈ Ǯ
    private bool isSpawningActive = false; // ���� �������� �����ߴ��� ����
    private bool isFirstTriggerActive = false;  // ù ��° Ʈ���Ű� Ȱ��ȭ�Ǿ�����
    private bool isSecondTriggerActive = false; // �� ��° Ʈ���Ű� Ȱ��ȭ�Ǿ�����

    void Start()
    {
        // ������Ʈ Ǯ �ʱ�ȭ
        rockPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject rock = Instantiate(rockPrefab);
            rock.SetActive(false);  // ��Ȱ��ȭ ���·� Ǯ�� ����
            rockPool.Enqueue(rock);
        }

        // ���� �������� �� ���� (�ڵ� �ݺ� ����)
        InvokeRepeating("CheckAndSpawnRock", 0.1f, spawnInterval);
    }

    // ��� ��ġ���� ���� �����ϴ� �޼���
    void CheckAndSpawnRock()
    {
        // Ʈ���Ű� Ȱ��ȭ�ǰ�, ������Ʈ Ǯ�� ���� �����ִ� ��쿡�� �� ����
        if (isSpawningActive && rockPool.Count > 0)
        {
            // ��� ���� ��ġ���� �� ����
            foreach (Transform spawnPoint in spawnPoints)
            {
                // Ǯ���� �� ��������
                GameObject rock = rockPool.Dequeue();
                rock.transform.position = spawnPoint.position;  // ���� ��ġ�� ����
                rock.SetActive(true);  // Ȱ��ȭ

                // ���� �ð� �� �� ��ȯ
                StartCoroutine(ReturnToPool(rock, 5f));
            }
        }
    }

    // ù ��° Ʈ���Ű� Ȱ��ȭ�Ǿ��� ��
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") && this.gameObject.CompareTag("FirstTrigger"))
    //    {
    //        if (!isFirstTriggerActive)
    //        {
    //            Debug.Log("check");
    //            isFirstTriggerActive = true;
    //            isSpawningActive = true; // �� ���� ����
    //        }
    //    }

    //    if (other.CompareTag("Player") && this.gameObject.CompareTag("SecondTrigger"))
    //    {
    //        isSecondTriggerActive = true;
    //        isSpawningActive = false; // �� ���� ����
    //        Debug.Log("check2");
    //    }
    //}

    public void SpawnRock()
    {
        if (!isFirstTriggerActive)
        {
            Debug.Log("check");
            isFirstTriggerActive = true;
            isSpawningActive = true; // �� ���� ����
        }
    }

    public void StageEnd()
    {
        isSecondTriggerActive = true;
        isSpawningActive = false; // �� ���� ����
        Debug.Log("check2");
    }

    // ù ��° Ʈ���Ű� ��Ȱ��ȭ�Ǿ��� ��
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (isFirstTriggerActive && !isSecondTriggerActive)
    //        {
    //            isSpawningActive = false; // �� ���� ����
    //        }
    //    }
    //}

    // ���� Ǯ�� ��ȯ�ϴ� �ڷ�ƾ
    System.Collections.IEnumerator ReturnToPool(GameObject rock, float delay)
    {
        yield return new WaitForSeconds(delay);
        rock.SetActive(false);  // ���� ��Ȱ��ȭ
        rockPool.Enqueue(rock); // Ǯ�� �ٽ� ����
    }
}
