﻿using Fizz.SalesOrder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Service
{
    public class CommonService
    {

        //根据name查找用户
        public static User FindUser(string name)
        {
            try
            {
                User user = Startup.users.Where(o => o.Name == name).First();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //创建用户
        public static User CreateUser(string name, string no)
        {
            User user = FindUser(name);
            if (user == null)
            {
                user = new User { Name = name };
                user.No = new List<string>();
                UserAddNo(user, no);
                Startup.users.Add(user);
            }
            else
            {
                UserAddNo(user, no);
            }

            return user;
        }

        //用户添加no
        public static void UserAddNo(User user, string no)
        {
            if (null != no)
            {
                if (!user.No.Contains(no))
                    user.No.Add(no);
            }
        }


     
    }
}
