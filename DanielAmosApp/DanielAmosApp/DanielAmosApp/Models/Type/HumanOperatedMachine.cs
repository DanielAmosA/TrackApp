using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Models
{
    public class HumanOperatedMachine : Machine
    {
        public string GetMachineType()
        {
            return "Human Operatedmachine";
        }
    }
}
