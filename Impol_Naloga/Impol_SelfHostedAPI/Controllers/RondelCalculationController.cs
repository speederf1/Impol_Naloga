using RondelCalculationDLL;
using System.Collections.Generic;
using System.Web.Http;

namespace Impol_API.Controllers
{
    public class RondelCalculationController : ApiController
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // url: http://localhost:6935/api/rondelcalculation?width=5&length=10&r=0.75&minDistanceBetween=0&minDistanceFromEdges=0
        public List<Rondel> Get(double width, double length, double r, double minDistanceBetween, double minDistanceFromEdges)
        {
            _log.Info($"WebAPI call with parameters:[width={width.ToString()}, length={length.ToString()}, r={r.ToString()}, minDistanceBetween={minDistanceBetween.ToString()}, minDistanceFromEdges={minDistanceFromEdges.ToString()}]");

            return RondelCalculation.Calculate(width, length, r, minDistanceBetween, minDistanceFromEdges);
        }
    }
}
