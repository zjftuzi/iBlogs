﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;

namespace iBlogs.Site.Core.User
{
    public class Users:IEntityBase
    {

        /**
         * user表主键
         */
        [Key]
        public int Id { get; set; }

        /**
         * 用户名称
         */
        public string Username { get; set; }

        /**
         * 用户密码
         */
        public string Password { get; set;}

        /**
         * 用户的邮箱
         */
        public string Email { get; set; }

        /**
         * 用户的主页
         */
        public string HomeUrl { get; set; }

        /**
         * 用户显示的名称
         */
        public string ScreenName { get; set; }

        /**
         * 用户注册时的GMT unix时间戳
         */
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; } 

        /**
         * 最后活动时间
         */
        public DateTime Activated { get; set; }

        /**
         * 上次登录最后活跃时间
         */
        public DateTime Logged { get; set; }

        /**
         * 用户组
         */
        public string GroupName { get; set; }

        public void PwdMd5()
        {
            Password = IBlogsUtils.md5(Username, Password);
        }

        public static string PwdMd5(string userName, string password)
        {
            return IBlogsUtils.md5(userName, password);
        }
    }
}
