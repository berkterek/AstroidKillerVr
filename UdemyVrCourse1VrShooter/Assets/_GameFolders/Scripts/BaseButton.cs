using UnityEngine;
using UnityEngine.UI;

namespace UdemyVrCourse1
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] protected Button _button;
        
        void Awake()
        {
            GetReference();
        }

        void OnValidate()
        {
            GetReference();
        }

        void OnEnable()
        {
            _button.onClick.AddListener(HandleOnButtonClicked);
        }

        void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnButtonClicked);
        }

        private void GetReference()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }
        }

        protected abstract void HandleOnButtonClicked();
    }
}