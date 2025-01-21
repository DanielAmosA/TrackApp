using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Utills.Common
{

    /// <summary>
    /// The section responsible for General Enums
    /// </summary>

    public enum MachineType
    {
        [EnumMember(Value = "Human Operated")]
        HumanOperated,
        Robot
    }

}
