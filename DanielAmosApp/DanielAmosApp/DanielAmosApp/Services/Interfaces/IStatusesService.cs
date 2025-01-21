using DanielAmosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Services.Interfaces
{
    public interface IStatusesService
    {
        Task<List<Status>> StatusesGetAllStatus();
    }
}
