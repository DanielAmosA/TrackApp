using DanielAmosApp.Models;
using DanielAmosApp.Utills.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Data.Interfaces
{
    public interface IMachinesRepository
    {
        Task<List<Machine>> MachinesGetMachinesByStatus(string sStatusType);
        Task<string?> MachinesDelete(int iMachineID);
        Task<List<Machine>> MachinesGetMachinesByStatusId(int iCurrentStatusId);
        Task<List<Machine>> MachinesInsert(string sName, string sDescription, MachineType eMachineType, int iCurrentStatusId, string sNotes);
        Task<List<Machine>> MachinesUpdate(int iMachineID, string sNewName, string sNewDescription,
                                                         MachineType eNewMachineType, int iNewCurrentStatusId, string sNewNotes);

    }
}
