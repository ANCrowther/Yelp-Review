namespace Team4_YelpProject.Commands
{
    using System;
    using System.Windows.Input;
    using Team4_YelpProject.ViewModel;

    class UpdateUserCommand : ICommand
    {
        public UpdateUserCommand(UserViewModel viewModel)
        {
            UserView = viewModel;
        }

        private UserViewModel UserView;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return UserView.CanUpdate;
        }

        public void Execute(object parameter)
        {
            UserView.SaveChanges();
        }
    }
}
