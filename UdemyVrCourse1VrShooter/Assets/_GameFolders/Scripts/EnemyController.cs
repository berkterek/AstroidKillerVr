using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UdemyVrCourse1
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyController : MonoBehaviour, ITakeDamageController
    {
        [SerializeField] GameObject _dyingEffect;
        [SerializeField] Rigidbody _rigidbody;
        [SerializeField] float _minMoveSpeed = 2f;
        [SerializeField] float _maxMoveSpeed = 6f;
        [SerializeField] float _maxLifeTime = 30f;

        float _currentLifeTime = 0f;
        
        void Awake()
        {
            GetReference();
        }

        void Start()
        {
            _rigidbody.velocity = GetRandomTime() * Vector3.left;
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
            _currentLifeTime += Time.deltaTime;

            if (_currentLifeTime > _maxLifeTime)
            {
                Destroy(this.gameObject);
            }
        }

        void OnValidate()
        {
            GetReference();
        }

        private void GetReference()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }
        }

        private float GetRandomTime()
        {
            return Random.Range(_minMoveSpeed, _maxMoveSpeed);
        }

        public void TakeDamage()
        {
            var dyingEffect = Instantiate(_dyingEffect, transform.position, transform.rotation);
            dyingEffect.transform.localScale = transform.localScale;
            GameManager.Instance.IncreaseScore();
            Destroy(this.gameObject);
        }
        
        void HandleOnGameOvered()
        {
            Destroy(this.gameObject);
        }
    }
}