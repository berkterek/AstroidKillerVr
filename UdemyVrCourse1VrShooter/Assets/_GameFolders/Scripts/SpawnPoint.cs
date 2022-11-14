using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UdemyVrCourse1
{
    public class SpawnPoint : MonoBehaviour
    {
        [Header("Spawn Basic")]
        [SerializeField] GameObject _prefab;
        [SerializeField] Vector3 _spawnSize;
        [SerializeField] Transform _transform;
        
        [Header("Spawn Time")]
        [SerializeField] float _maxTime = 10f;
        [SerializeField] float _minTime = 3f;

        [Header("Spawn Size")]
        [SerializeField] float _scaleSizeMin;
        [SerializeField] float _scaleSizeMax;

        float _randomTime = 0f;
        float _currentTime = 0f;

        void Awake()
        {
            GetReference();
        }

        void Start()
        {
            GetRandomTime();
        }

        void OnEnable()
        {
            GameManager.Instance.OnGameOvered += HandleOnGameOvered;
        }

        void OnDisable()
        {
            GameManager.Instance.OnGameOvered -= HandleOnGameOvered;
        }

        void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _randomTime)
            {
                GetRandomTime();
                Spawn();
                _currentTime = 0f;
            }
        }

        void OnValidate()
        {
            GetReference();
        }

        void OnDrawGizmos()
        {
            OnDrawGizmosSelected();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(_transform.position, _spawnSize);
        }

        private void GetReference()
        {
            if (_transform == null)
            {
                _transform = transform;
            }
        }

        private void GetRandomTime()
        {
            _randomTime = Random.Range(_minTime, _maxTime);
        }

        private float GetRandomScale()
        {
            return Random.Range(_scaleSizeMin, _scaleSizeMax);
        }

        private void Spawn()
        {
            Vector3 randomPosition = _transform.position + new Vector3(
            Random.Range(-_spawnSize.x / 2f,_spawnSize.x / 2f),
            Random.Range(-_spawnSize.y / 2f,_spawnSize.y / 2f),
            Random.Range(-_spawnSize.z / 2f,_spawnSize.z / 2f)
            );

            GameObject newEnemy = Instantiate(_prefab, randomPosition, _transform.rotation);
            newEnemy.transform.SetParent(_transform);
            newEnemy.transform.localScale = new Vector3(GetRandomScale(), GetRandomScale(), GetRandomScale());
            
            Debug.Log("Spawn enemy");
        }
        
        void HandleOnGameOvered()
        {
            Destroy(this.gameObject);
        }
    }
}