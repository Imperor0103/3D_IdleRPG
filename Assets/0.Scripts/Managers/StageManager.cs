using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy�� ��� ���ָ� ���������� �ø���, �� ���� Enemy�� �����Ѵ� (������Ʈ Ǯ�� ����)
/// </summary>
public class StageManager : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ Enemy ������
    public int initialEnemyCount = 2; // �ʱ� �� ����
    private int currentStage = 1;   // ���� ��������
    public List<GameObject> activeEnemies = new List<GameObject>();
    public List<GameObject> enemyPool = new List<GameObject>();
    private int poolSize = 3;

    public UIManager uiManager;


    void Start()
    {
        InitializePool();
        uiManager.ShowCurrentStage();
        SpawnEnemies(initialEnemyCount);
    }

    void Update()
    {
        // ��� Enemy�� ���ŵǾ����� Ȯ��
        activeEnemies.RemoveAll(enemy => enemy == null || !enemy.activeInHierarchy);
        if (activeEnemies.Count == 0)
        {
            NextStage();
        }
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-6f, 10f), 0, Random.Range(5f, 20f));
            GameObject enemy = GetEnemyFromPool();
            enemy.transform.position = spawnPosition;
            enemy.SetActive(true);
            activeEnemies.Add(enemy);
        }
    }

    GameObject GetEnemyFromPool()
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool[0];
            enemyPool.RemoveAt(0);
            return enemy;
        }
        else
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            return newEnemy;
        }
    }

    void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Add(enemy);
    }

    void NextStage()
    {
        currentStage++;
        uiManager.ShowCurrentStage();

        int newEnemyCount = initialEnemyCount + currentStage; // ������������ �� ����
        SpawnEnemies(newEnemyCount);
    }

    public int GetCurrentStageNumber()
    {
        return currentStage;

    }
}
