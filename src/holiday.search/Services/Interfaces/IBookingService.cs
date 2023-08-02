using holiday.search.Models;

namespace holiday.search.Services.Interfaces
{
    public interface IBookingService
    {
        List<BookingResponseModel> GetBookingInformation(BookingRequestModel request);
    }
}
