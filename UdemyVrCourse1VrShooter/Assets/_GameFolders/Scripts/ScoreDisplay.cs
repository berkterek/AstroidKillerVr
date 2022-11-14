using TMPro;
using UnityEngine;

namespace UdemyVrCourse1
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text _scoreText;

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
            GameManager.Instance.OnScoreChanged += HandleOnScoreChanged;
        }

        void OnDisable()
        {
            GameManager.Instance.OnScoreChanged -= HandleOnScoreChanged;
        }
        
        void HandleOnScoreChanged(int value)
        {
            _scoreText.SetText(value.ToString());
        }

        void GetReference()
        {
            if (_scoreText == null)
            {
                _scoreText = GetComponent<TMP_Text>();
            }
        }
    }
}