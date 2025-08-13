using Pax360.Models;
using Pax360DAL.Models;

namespace Pax360.Interfaces
{
    public interface IAdminService
    {
        public UserDetailsModel EditGetUser(Users user);
        public List<UserItem> UsersList();
        public UserDetailsModel SearchedUsersList(List<Users> userList);
        public List<Users> SearchedUsers(UserDetailsModel dataModel);
        public bool RemoveUser(UserDetailsModel dataModel);
        public string SaveUser(UserDetailsModel dataModel, IFormFileCollection images);
        public List<string> TeamsList();
        public bool EditSetUser(UserDetailsModel dataModel, IFormFileCollection images);

    }
}
