namespace SocialNetwork.Views.ViewModels
{
    public class StartViewModel
    {
        public LoginViewModel LoginView { get; set; }
        public RegisterViewModel RegisterView { get; set; }

        public StartViewModel()
        {
            LoginView = new LoginViewModel();
            RegisterView = new RegisterViewModel();
        }
    }
}
