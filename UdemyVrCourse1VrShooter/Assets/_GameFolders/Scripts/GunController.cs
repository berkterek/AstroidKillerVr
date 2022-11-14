using UnityEngine;

namespace UdemyVrCourse1
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] Transform _rayTransform;
        [SerializeField] Transform _transform;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] LayerMask _layerMask;
        [SerializeField] Rigidbody _rigidbody;
        [SerializeField] float _fireDistance = 10000f;
        [SerializeField] Vector3 _startPosition;
        [SerializeField] Vector3 _startRotation;

        RigidbodyConstraints _defaultConstraint;

        void Awake()
        {
            GetReference();
        }

        void Start()
        {
            _defaultConstraint = _rigidbody.constraints;
            GetPositionAndRotation();
        }

        void OnValidate()
        {
            GetReference();
            GetPositionAndRotation();
        }

        void OnEnable()
        {
            GameManager.Instance.OnGameOvered += HandleOnGameOvered;
        }

        void OnDisable()
        {
            GameManager.Instance.OnGameOvered -= HandleOnGameOvered;
        }

        /// <summary>
        /// This method calling from unity event
        /// </summary>
        public void Fire()
        {
            if (Physics.Raycast(_rayTransform.position, _rayTransform.forward,out RaycastHit raycastHit,_fireDistance,_layerMask))
            {
                if (raycastHit.collider.TryGetComponent(out ITakeDamageController takeDamageController))
                {
                    takeDamageController.TakeDamage();
                }
            }

            Debug.Log("Fire");
            _audioSource.Play();
        }

        /// <summary>
        /// This method calling from unity event
        /// </summary>
        public void ReleaseGun()
        {
            _transform.position = _startPosition;
            _transform.eulerAngles = _startRotation;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _rigidbody.velocity = Vector3.zero;
        }

        /// <summary>
        /// This method calling from unity event
        /// </summary>
        public void TakeGun()
        {
            _rigidbody.constraints = _defaultConstraint;
        }

        private void GetReference()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }

            if (_transform == null)
            {
                _transform = GetComponent<Transform>();
            }

            if (_audioSource == null)
            {
                _audioSource = GetComponent<AudioSource>();
            }
        }

        private void GetPositionAndRotation()
        {
            if (_startPosition != _transform.position)
            {
                _startPosition = _transform.position;    
            }

            if (_startRotation != _transform.eulerAngles)
            {
                _startRotation = _transform.eulerAngles;
            }
        }
        
        void HandleOnGameOvered()
        {
            Destroy(this.gameObject);
        }
    }
}