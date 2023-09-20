using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _EnemyPrefabs;

    // A SerializableObject or other data type would be preferable for defining this, 
    // but going for a more direct route in the name of expediency
    [SerializeField]
    private List<int> _EnemyStartRows;

    [SerializeField]
    private int _RowCount;
    [SerializeField]
    private int _ColumnCount;

    [SerializeField]
    private Vector2 _Spacing;

    [SerializeField]
    private Vector3 _UpperLeftSpawnPos;

    [SerializeField]
    private WaveController _WaveRoot;

    private List<List<Enemy>> _Enemies;

    public int StartRow = 0;

    [SerializeField]
    private GameObject _UFOPrefab;

    [SerializeField]
    private float _UFOSpawnFrequency;

    [SerializeField]
    private float _UFOStartX;

    private Coroutine _UFORoutine;
    private GameObject _UFO;

    private void Start()
    {
        SpawnWave();
    }

    public void SpawnWave()
    {
        _Enemies = new List<List<Enemy>>();
        for (int r = 0; r < _RowCount; r++)
        {
            _Enemies.Add(new List<Enemy>());
            for (int c = 0; c < _ColumnCount; c++)
            {
                var position = _UpperLeftSpawnPos;
                position.x -= _Spacing.x * c;
                position.y -= _Spacing.y * (r + StartRow);

                GameObject prefab = null;
                for (int i = 0; i < _EnemyStartRows.Count; i++)
                {
                    if (r >= _EnemyStartRows[i])
                    {
                        prefab = _EnemyPrefabs[i];
                    }
                }

                _Enemies[r].Add(Instantiate(prefab, CoreController.WaveController.transform).GetComponent<Enemy>());
                _Enemies[r][c].transform.position = position;
                _Enemies[r][c].Initialize(r, c);
            }
        }

        CoreController.WaveController.Enemies = _Enemies;
        CoreController.WaveController.Initialize();

        _UFORoutine = StartCoroutine(UFOSpawnRoutine());
    }

    public void ClearEnemies()
    {
        for (int r = 0; r < _RowCount; r++)
        {
            for (int c = 0; c < _ColumnCount; c++)
            {
                Destroy(_Enemies[r][c].gameObject);
            }
        }

        _Enemies = null;

        StopCoroutine(_UFORoutine);
        Destroy(_UFO);
        _UFO = null;
    }

    private IEnumerator UFOSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_UFOSpawnFrequency);
            _UFO = Instantiate(_UFOPrefab, transform);

            var position = _UFO.transform.position;
            position.y = _UpperLeftSpawnPos.y + _Spacing.y;
            position.x = _UFOStartX;
            _UFO.transform.position = position;
        }
    }
}
