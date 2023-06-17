using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.Services.Abstractions
{
    public interface IConsoleIO
    {
        void Clear();
        void Write(string text);
        void WriteLine(string text);
        string ReadLine();
    }
}
