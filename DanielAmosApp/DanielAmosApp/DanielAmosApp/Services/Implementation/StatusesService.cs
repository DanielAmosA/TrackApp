using DanielAmosApp.Data.Interfaces;
using DanielAmosApp.Models;
using DanielAmosApp.Services.Interfaces;

namespace DanielAmosApp.Services.Implementation
{
    public class StatusesService : IStatusesService
    {

        private readonly IStatusesRepository statusesRepository;

        public StatusesService(IStatusesRepository statusesRepository)
        {
            this.statusesRepository = statusesRepository;
        }

        public async Task<List<Status>> StatusesGetAllStatus()
        {
            return await statusesRepository.StatusesGetAllStatus();
        }
    }
}
