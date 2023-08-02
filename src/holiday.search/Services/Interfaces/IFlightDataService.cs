using holiday.search.Models;
using System.Linq.Expressions;

namespace holiday.search.Services.Interfaces
{
    public interface IFlightDataService
    {
        public List<FlightDataModel> Get();
        public List<FlightDataModel> Get(Expression<Func<FlightDataModel, bool>> filter);
    }
}
