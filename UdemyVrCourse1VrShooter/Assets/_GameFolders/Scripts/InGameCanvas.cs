using UnityEngine;

namespace UdemyVrCourse1
{
    [RequireComponent(typeof(Canvas))]
    public class InGameCanvas : MonoBehaviour
    {
        [SerializeField] GameObject _gameOverPanel;
        [SerializeField] Canvas _canvas;

        void Awake()
        {
            GetReference();
        }

        void OnValidate()
        {
            GetReference();
        }

        void Start()
        {
            _gameOverPanel.SetActive(false);
            _canvas.worldCamera = Camera.main;
        }

        void OnEnable()
        {
            GameManager.Instance.OnGameOvered += HandleOnGameOvered;
        }

        void OnDisable()
        {
            GameManager.Instance.OnGameOvered -= HandleOnGameOvered;
        }
        
        void HandleOnGameOvered()
        {
            _gameOverPanel.SetActive(true);
        }

        private void GetReference()
        {
            if (_canvas == null)
            {
                _canvas = GetComponent<Canvas>();
            }
        }
    }
}