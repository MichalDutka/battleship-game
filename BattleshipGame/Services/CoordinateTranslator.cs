using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipGame.Models;
using BattleshipGame.Services.Abstractions;

namespace BattleshipGame.Services
{
    public class CoordinateTranslator : ICoordinateTranslator
    {
        public Coordinates TranslateCoordinates(string inputCoordinates)
        {
            if (inputCoordinates.Length < 2 
                || inputCoordinates[0] < 65 
                || inputCoordinates[0] > 90
                || !int.TryParse(inputCoordinates.Substring(1), out var y))
            {
                throw new ArgumentException("Invalid coordinates");
            }

            var coordinates = new Coordinates()
            {
                X = inputCoordinates[0] - 65,
                Y = y - 1 
            };

            return coordinates;
        }
    }
}
