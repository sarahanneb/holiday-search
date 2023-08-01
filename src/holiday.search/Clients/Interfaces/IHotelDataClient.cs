using holiday.search.Models;

namespace holiday.search.Clients.Interfaces
{
    public interface IHotelDataClient
    {
        public List<HotelDataModel> Get();
    }
}
