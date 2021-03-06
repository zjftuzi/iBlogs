﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.DTO;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Common.Service;
using iBlogs.Site.Core.Install;
using iBlogs.Site.Core.Install.DTO;
using iBlogs.Site.Core.Install.Service;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.User;
using iBlogs.Site.Core.User.Service;
using iBlogs.Site.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Web.Controllers
{
    public class InstallController : Controller
    {
        private readonly IInstallService _installService;
        private readonly IConfiguration _configuration;
        private readonly ISiteService _siteService;
        private readonly IUserService _userService;
        private readonly IOptionService _optionService;

        public InstallController(IInstallService installService, IConfiguration configuration, ISiteService siteService, IOptionService optionService, IUserService userService)
        {
            _installService = installService;
            _configuration = configuration;
            _siteService = siteService;
            _optionService = optionService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var installed = _configuration[ConfigKey.DbInstalled].ToBool();
            return View(new ViewBaseModel
            {
                Installed = installed
            });
        }

        [HttpPost]
        public async Task<ApiResponse<int>> Index(InstallParam param)
        {
            var installed = _configuration[ConfigKey.DbInstalled].ToBool();
            if (!param.AdminPwd.IsNullOrWhiteSpace() && !installed)
            {
                if (await _installService.InitializeDb(param))
                {
                    return ApiResponse<int>.Ok();
                }
            }
            return ApiResponse<int>.Fail("安装失败");
        }
    }
}