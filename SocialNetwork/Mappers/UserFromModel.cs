using DataAccess.Models;
using SocialNetwork.Models;
using System.Runtime.CompilerServices;

namespace SocialNetwork.Mappers
{
    public static class UserFromModel
    {
        public static User Convert(this User user, UpdateUserViewModel viewModel)
        {
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.MiddleName = viewModel.MiddleName;
            user.BirthDate = viewModel.BirthDate;
            user.Email = viewModel.Email;
            user.Image = viewModel.Image;
            user.About = viewModel.About;
            user.Status = viewModel.Status;
            user.UserName = viewModel.UserName;

            return user;
        }
    }
}
