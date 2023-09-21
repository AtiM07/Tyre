using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject startPointPrefab; // префабы точек старта врагов
    [SerializeField] private GameObject enemyOrangePrefab; // оранжевый префаб
    [SerializeField] private GameObject enemyGreenPrefab; //зеленый префаб
    private const int CountPoint = 7; //количество дорожек
    private readonly GameObject[] _points = new GameObject[CountPoint]; // массив точек старта

    private readonly GameObject[]
        _checkPoints = new GameObject[CountPoint]; // массив для проверка, находится ли сейчас враг на дорожке

    private Transform[] _children;

    private void Start()
    {
        _children = GetComponentsInChildren<Transform>();
        for (int i = 0; i < CountPoint; i++)
        {
            _points[i] = GenerateObject(startPointPrefab, _children[1], $"Point {i}");
        }

        StartCoroutine(CreateEnemy());
    }

    private IEnumerator CreateEnemy()
    {
        if (GameController.IsOver)
            yield return new WaitForSeconds(4.5f);

        var enemyPrefab =
            Random.Range(1, 101) > 30
                ? enemyGreenPrefab
                : enemyOrangePrefab; //выбор префаба врага, соблюдая вероятность выпадания
        var enemy = GenerateObject(enemyPrefab, _children[2], "Enemy"); //создаем врага

        var num = GetRandom(0, CountPoint);
        enemy.transform.position = _points[num].transform.position; //помещаем его на точку старта
        _checkPoints[num] = enemy; //занимаем место на дорожке

        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        StartCoroutine(CreateEnemy());
    }

    private int GetRandom(int start, int end)
    {
        var num = 0;
        do
        {
            num = Random.Range(start, end);
        } while (_checkPoints[num] != null);

        return num;
    }

    private GameObject GenerateObject(GameObject prefab, Transform parent, string newName = null)
    {
        var point = Instantiate(prefab, parent);
        if (!string.IsNullOrEmpty(newName))
        {
            point.gameObject.name = newName;
        }

        return point;
    }
}