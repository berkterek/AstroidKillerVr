namespace UdemyVrCourse1
{
    public class ReturnMenuButton : BaseButton
    {
        protected override void HandleOnButtonClicked()
        {
            GameManager.Instance.LoadMenuScene();
        }
    }
}