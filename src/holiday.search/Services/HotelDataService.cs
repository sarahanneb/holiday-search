using holiday.search.Clients.Interfaces;
using holiday.search.Models;
using holiday.search.Services.Interfaces;
using System.Linq.Expressions;

namespace holiday.search.Services
{
    public class HotelDataService : IHotelDataService
    {
        private readonly IHotelDataClient _dataClient;

        public HotelDataService(IHotelDataClient dataClient)
        {
            _dataClient = dataClient;
        }

        public List<HotelDataModel> Get()
        {
            return _dataClient.Get();
        }

        public List<HotelDataModel> Get(Expression<Func<HotelDataModel, bool>> filter)
        {
            var results = Get().AsQueryable();
            return results.Where(filter).ToList();
        }
    }
}
