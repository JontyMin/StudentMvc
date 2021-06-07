using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StudentMvc.Controllers
{
    public class ErrorController:Controller
    {
        private readonly ILogger<ErrorController> _logger;

        /// <summary>
        /// 注入ILogger
        /// </summary>
        /// <param name="logger"></param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusHandler(int statusCode)
        {
            var execption = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "抱歉，您访问的页面不存在";
                    _logger.LogWarning($"发生了404错误,路径：{execption.OriginalPath},以及查询字符串：{execption.OriginalQueryString}");
                    break;
            }

            return View("NotFound");
        }


        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var execption = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            // _logger.LogError($"路径：{execption.Path},产生了一个错误{execption.Error.Message}");
            ViewBag.ExceptionPath = execption.Path;
            ViewBag.ErrorMessage = execption.Error.Message;
            ViewBag.ErrorStackTrace = execption.Error.StackTrace;
            return View();
        }
    }
}
