
namespace UdemyVrCourse1
{
    public class StartButton : BaseButton
    {
        protected override void HandleOnButtonClicked()
        {
            GameManager.Instance.LoadGameScene();
        }
    }
}

