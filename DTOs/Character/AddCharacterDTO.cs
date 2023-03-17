using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_api_first.models;

namespace dotnet_api_first.DTOs.Character
{
    public class AddCharacterDTO
    {
        public string Name { get; set; } = "Turki";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Agility { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Paladin;

    }
}