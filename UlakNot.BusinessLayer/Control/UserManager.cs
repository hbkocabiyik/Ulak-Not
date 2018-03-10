﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlakNot.BusinessLayer.Results;
using UlakNot.DataLayer.EntityFramework;
using UlakNot.Entity;
using UlakNot.Entity.UserObjects;

namespace UlakNot.BusinessLayer.Control
{
    public class UserManager
    {
        private Repository<UnUsers> repo_user = new Repository<UnUsers>();

        public ErrorResult<UnUsers> RegisterUser(RegisterModel data)
        {
            ErrorResult<UnUsers> error_res = new ErrorResult<UnUsers>();
            UnUsers user = repo_user.Find(x => x.Username == data.Username || x.Email == data.EMail);

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    error_res.Error.Add("Bu kullanıcı adı kullanılıyor!");
                }

                if (user.Email == data.EMail)
                {
                    error_res.Error.Add("Bu e-posta adresi kullanılıyor!");
                }
            }
            else
            {
                int SaveResult = repo_user.Insert(new UnUsers()
                {
                    Username = data.Username,
                    Email = data.EMail,
                    Name = data.Name,
                    Surname = data.Surname,
                    Password = data.Password,
                    GuidControl = Guid.NewGuid(),
                    ActiveStatus = false,
                    AdminAuthority = false,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                });

                if (SaveResult > 0)
                {
                    error_res.Result = repo_user.Find(x => x.Username == data.Username && x.Email == data.EMail);
                }
            }
            return error_res;
        }

        public ErrorResult<UnUsers> LoginUser(LoginModel data)
        {
            ErrorResult<UnUsers> error_res = new ErrorResult<UnUsers>();
            UnUsers user = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);

            if (user != null)
            {
            }
        }
    }
}