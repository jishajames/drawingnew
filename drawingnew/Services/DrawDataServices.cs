using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using drawingnew.Models;
using MyClassLibrarynew.Helpers;
using MyClassLibrarynew.Models;
using System.ComponentModel.Design;

namespace drawingnew.Services
{
    public interface IDrawDataServices
    {
        void SaveDimensionDetails(Dimension dimension);
        List<object> GetAllDimensions(int dimensionId);
    }
    public class DrawDataServices: IDrawDataServices
    {
        public readonly TestContext _context;
        public DrawDataServices(TestContext context)
        {
            _context = context;
        }
        public void SaveDimensionDetails(Dimension dimension)
        {
            if (dimension.Id == 0)
            {
                _context.Dimensions.Add(dimension);
            }
            else
            {
                var dimen = _context.Dimensions.SingleOrDefault(x => x.Id == dimension.Id);
                if(dimen!=null)
                {
                    dimen.Name = dimension.Name;
                    dimen.Email = dimension.Email;
                    dimen.Dimensionfield = dimension.Dimensionfield;
                }
            }
            _context.SaveChanges();

        }
        public List<object> GetAllDimensions(int dimensionId)
        {
            var results = new List<object>();
            var coreSettings = _context.Dimensions.Where(x => x.Id == dimensionId).ToList();
            foreach (var company in coreSettings)
            {


                results.Add(new
                {
                    company.Id,
                    company.Name,
                    company.Email,
                    company.Dimensionfield,


                });
            }
            return results;
        }
    }
}
