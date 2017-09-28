using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;
using Common;

namespace BLL
{
    public class UserBLL
    {
        UserDAL userDAL = new UserDAL();

        public UserDto login(string username, string password)
        {
            User user = userDAL.getByUsername(username);

            string passwordEncrypt = EncryptUtil.Password(password);

            if (user != null && user.Password == passwordEncrypt)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<User, UserDto>();
                });
                UserDto userDto = Mapper.Map<UserDto>(user);

                user.Token = Guid.NewGuid().ToString();
                user.Expires = DateTime.Now.AddDays(1);
                userDAL.update(user);

                userDto.Token = user.Token;
                TimeSpan timeSpan = (DateTime)user.Expires - DateTime.Now;
                userDto.ExpiresIn = (int)timeSpan.TotalSeconds;

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

        public int changePassword(int id, string password, string newPassword, int updateUserId)
        {
            string passwordEncrypt = EncryptUtil.Password(password);
            string newPasswordEncrypt = EncryptUtil.Password(newPassword);
            User user = userDAL.getById(id);
            if (user == null)
                return 0;
            if (user.Password != passwordEncrypt)
                return -1;
            user.Password = newPasswordEncrypt;
            user.UpdateUserId = updateUserId;
            userDAL.update(user);
            return 1;

        }

        public UserDto getByToken(string token)
        {
            User user = userDAL.getByToken(token);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });
            UserDto userDto = Mapper.Map<UserDto>(user);
            TimeSpan timeSpan = (DateTime)user.Expires - DateTime.Now;
            userDto.ExpiresIn = (int)timeSpan.TotalSeconds;

            if (userDto.ExpiresIn <= 0)
            {
                return null;
            }
            return userDto;
        }
    }
}
