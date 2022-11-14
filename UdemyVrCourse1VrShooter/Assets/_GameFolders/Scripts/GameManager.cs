using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UdemyVrCourse1
{
    public class GameManager : SingletonMono<GameManager>
    {
        [SerializeField] int _score;
        [SerializeField] float _maxPlayGameTime = 60f;
        [SerializeField] bool _isGameStarted = false;

        float _currentTime = 0f;

        public event System.Action<int> OnScoreChanged;
        public event System.Action<string> OnSceneChanged;
        public event System.Action OnGameOvered;

        void Awake()
        {
            Application.targetFrameRate = 60;
            SetSingleton(this);
        }

        IEnumerator Start()
        {
            yield return SceneManager.LoadSceneAsync("PlayerScene", LoadSceneMode.Additive);

            yield return new WaitForSeconds(3f);

            yield return SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
            var menuScene = SceneManager.GetSceneByName("Menu");
            SceneManager.SetActiveScene(menuScene);

            var loadingScene = SceneManager.GetSceneByName("Loading");
            SceneManager.UnloadSceneAsync(loadingScene);
        }

        void Update()
        {
            if (!_isGameStarted) return;

            _currentTime += Time.deltaTime;

            if (_currentTime > _maxPlayGameTime)
            {
                GameOver();
            }
        }

        void GameOver()
        {
            _currentTime = 0f;
            _isGameStarted = false;
            OnGameOvered?.Invoke();
        }

        public void IncreaseScore()
        {
            _score++;
            OnScoreChanged?.Invoke(_score);
        }

        private IEnumerator LoadSceneAsync(string currentName, string nextScene)
        {
            _score = 0;
            
            yield return SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading"));
            
            yield return SceneManager.UnloadSceneAsync(currentName);

            yield return new WaitForSeconds(3f);
            
            AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene,LoadSceneMode.Additive);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
            
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));

            yield return SceneManager.UnloadSceneAsync("Loading");
            
            OnSceneChanged?.Invoke(nextScene);
            _currentTime = 0f;
        }

        public void LoadGameScene()
        {
            StartCoroutine(LoadSceneAsync("Menu", "Game"));
            _isGameStarted = true;
        }

        public void LoadMenuScene()
        {
            StartCoroutine(LoadSceneAsync("Game","Menu"));
        }
    }
}