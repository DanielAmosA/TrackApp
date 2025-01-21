using DanielAmosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Data.Interfaces
{
    public interface IStatusesRepository
    {
        Task<List<Status>> StatusesGetAllStatus();
    }
}
