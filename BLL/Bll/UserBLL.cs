using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace BLL
{
    public class UserBLL
    {
        UserDAL userDAL = new UserDAL();

        public UserDto login(string username, string password)
        {
            User user = userDAL.getByUsername(username);

            if (user != null && user.Password == password)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<User, UserDto>();
                });
                UserDto userDto = Mapper.Map<UserDto>(user);

                //user.Token = Guid.NewGuid().ToString();
                //user.Expires = DateTime.Now.AddDays(1);
                //userDAL.update(user);

                //userDto.Token = user.Token;
                //userDto.ExpiresIn = 86400;

                return userDto;
            }
            else
                return null;
        }

        public UserDto getById(int id)
        {
            User user = userDAL.getById(id);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });
            UserDto userDto = Mapper.Map<UserDto>(user);
            return userDto;
        }

        public int updatePassword(int id, string password, int updateUserId)
        {
            User user = userDAL.getById(id);
            if (user == null)
                return 0;
            user.Password = password;
            user.UpdateUserId = updateUserId;
            userDAL.update(user);
            return 1;

        }
    }
}
