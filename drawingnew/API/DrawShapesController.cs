using drawingnew.Models;
using drawingnew.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace drawingnew.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrawShapesController : ControllerBase
    {
        private readonly IDrawDataServices _drawservices;
        public DrawShapesController(IDrawDataServices drawserices)
        {
            _drawservices = drawserices;
        }
        [Route("[action]", Name = "SaveDrawShapes")]
        [HttpPost]
        public IActionResult SaveDrawShapes(Dimension dm)
        {
            var status = true;
            var message = "Success";
            try
            {
                _drawservices.SaveDimensionDetails(dm);
            }
            catch (Exception ex)
            {
                status = false;
                message = "Error " + ex.Message;
            }

            return new JsonResult(new { status = status, message = message });
        }
        [Route("[action]", Name = "GetDrawShapes")]
        [HttpGet]
        public IActionResult GetDrawShapes(int dimensionId)
        {
            var template = _drawservices.GetAllDimensions(dimensionId);
            return new JsonResult(template);
        }
    }
    

}
