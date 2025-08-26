﻿using ProjectZ.InGame.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.InGame.SaveLoad
{
    internal class SaveStateManager
    {
        public class SaveState
        {
            public string Name;
            public bool Thief;
            public int MaxHearts;
            public int CurrentHealth;
            public int CurrentRubee;
            public float TotalPlaytimeMinutes;
        }

        public static SaveState[] SaveStates = new SaveState[SaveCount];

        public const int SaveCount = 4;

        public static void LoadSaveData()
        {
            for (var i = 0; i < SaveCount; i++)
            {
                var saveManager = new SaveManager();

                // check if the save was loaded or not
                if (saveManager.LoadFile(Values.PathSaveFolder + SaveGameSaveLoad.SaveFileName + i))
                {
                    SaveStates[i] = new SaveState();
                    SaveStates[i].Thief = saveManager.GetBool("ThiefState", false);
                    SaveStates[i].Name = saveManager.GetString("savename");
                    SaveStates[i].CurrentHealth = saveManager.GetInt("currentHealth");
                    SaveStates[i].MaxHearts = saveManager.GetInt("maxHearts");
                    SaveStates[i].CurrentRubee = saveManager.GetInt("rubyCount", 0);
                    SaveStates[i].TotalPlaytimeMinutes = saveManager.GetFloat("totalPlaytime", 0.0f);
                }
                else
                    SaveStates[i] = null;
            }
        }
    }
}
