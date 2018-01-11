using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfKarliCards_GUI
{
    [Serializable]
    class GameOptions
    {
        public bool PlayAgainstCompuer { get; set; }
        public int NumberOfPlayers { get; set; }
        public int NumberOfBeforeLoss { get; set; }
        public ComputerSkillLevel computerSkill { get; set; }
    }

    [Serializable]
    public enum ComputerSkillLevel
    {
        Dumb,
        Good,
        Cheats
    }


}
