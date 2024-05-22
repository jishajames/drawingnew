using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.IO;
using System.Linq;
using drawingnew.API;
using drawingnew.Models;

namespace drawingnew.Pages.Drawmethods
{
    public class DrawShapesModel : PageModel
    {
        private readonly DrawShapesController _drawShapesController;
        public void OnGet()
        {
            dimension = new Dimension();
        }
        [BindProperty]
        public required Dimension dimension { get; set; }
        public JsonResult OnPostSaveDrawShapes(string name )
        {
            Dimension dm = new Dimension();
            dm.Name = name;

            return new JsonResult(_drawShapesController.SaveDrawShapes(dm));
        }
    }
}
