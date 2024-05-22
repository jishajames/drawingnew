using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Data.SqlClient;
using System;
using System.IO;
using System.Linq;
using drawingnew.API;
using drawingnew.Models;
using drawingnew.Services;

namespace drawingnew.Pages.Drawmethods
{
    public class DrawShapesModel : PageModel
    {
        //private readonly DrawShapesController _drawservices;
        private readonly IDrawDataServices _drawservices;
        public DrawShapesModel(IDrawDataServices drawservices)
        {
            _drawservices = drawservices;
        }
        //public DrawShapesModel(DrawShapesController drawservices)
        //{
        //    _drawservices = drawservices;
        //}
        public void OnGet()
        {
            dimension = new Dimension();
        }
        

        [BindProperty]
        public required Dimension dimension { get; set; }
        public JsonResult OnPostSaveDrawShapes(int id,string name, string email, string dimensionalfield)
        {

            Dimension dm = new Dimension();
            dm.Id = id;
            dm.Name = name;
            dm.Email = email;
            dm.Dimensionfield = dimensionalfield;

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
            //return new JsonResult(_drawservices.SaveDrawShapes(dm));
        }
        public JsonResult OnGetDrawShapes()
        {

           

            return new JsonResult(_drawservices.GetAllDimensions());
            //return new JsonResult(_drawservices.SaveDrawShapes(dm));
        }
    }
}
