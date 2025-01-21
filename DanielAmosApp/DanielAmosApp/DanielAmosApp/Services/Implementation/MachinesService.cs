using DanielAmosApp.Data.Implementation;
using DanielAmosApp.Data.Interfaces;
using DanielAmosApp.Models;
using DanielAmosApp.Services.Interfaces;
using DanielAmosApp.Utills.Common;

namespace DanielAmosApp.Services.Implementation
{
    public class MachinesService : IMachinesService
    {
        private readonly IMachinesRepository machineRepository;

        public MachinesService(IMachinesRepository machineRepository)
        {
            this.machineRepository = machineRepository;
        }

        public async Task<List<Machine>> MachinesGetMachinesByStatus(string sStatusType)
        {
            return await machineRepository.MachinesGetMachinesByStatus(sStatusType);
        }

        public async Task<string?> MachinesDelete(int iMachineID)
        {
            return await machineRepository.MachinesDelete(iMachineID);
        }

        public async Task<List<Machine>> MachinesGetMachinesByStatusId(int iCurrentStatusId)
        {
            return await machineRepository.MachinesGetMachinesByStatusId(iCurrentStatusId);
        }

        public async Task<List<Machine>> MachinesUpdate(int iMachineID, string sNewName, string sNewDescription, MachineType eNewMachineType, int iNewCurrentStatusId, string sNewNotes)
        {
            return await machineRepository.MachinesUpdate(iMachineID, sNewName, sNewDescription, eNewMachineType,  iNewCurrentStatusId, sNewNotes);
        }

        public async Task<List<Machine>> MachinesInsert(string sName, string sDescription, MachineType eMachineType, int iCurrentStatusId, string sNotes)
        {
            return await machineRepository.MachinesInsert(sName, sDescription, eMachineType, iCurrentStatusId, sNotes);
        }
    }
}
