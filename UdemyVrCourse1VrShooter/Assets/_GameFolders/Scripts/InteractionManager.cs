using UnityEngine;

namespace UdemyVrCourse1
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] GameObject[] _interfactionInGameObject;

        void OnEnable()
        {
            GameManager.Instance.OnSceneChanged += HandleOnSceneChanged;
        }

        void OnDisable()
        {
            GameManager.Instance.OnSceneChanged -= HandleOnSceneChanged;
        }
        
        void HandleOnSceneChanged(string value)
        {
            foreach (var interfactionGameObject in _interfactionInGameObject)
            {
                interfactionGameObject.SetActive(value.Equals("Game"));
            }
        }
    }    
}

