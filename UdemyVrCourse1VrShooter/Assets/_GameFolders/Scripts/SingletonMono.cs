using UnityEngine;

namespace UdemyVrCourse1
{
    public abstract class SingletonMono<T> : MonoBehaviour where T: Component 
    {
        public static T Instance { get; private set; }
        
        protected void SetSingleton(T value)
        {
            if (Instance == null)
            {
                Instance = value;
                value.transform.parent = null;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}