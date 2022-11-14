namespace UdemyVrCourse1
{
    public class PersistentObject : SingletonMono<PersistentObject>
    {
        void Awake()
        {
            SetSingleton(this);
        }
    }
}