using holiday.search.Models;
using System.Linq.Expressions;

namespace holiday.search.Services.Interfaces
{
    public interface IHotelDataService
    {
        public List<HotelDataModel> Get();
        public List<HotelDataModel> Get(Expression<Func<HotelDataModel, bool>> filter);
    }
}
