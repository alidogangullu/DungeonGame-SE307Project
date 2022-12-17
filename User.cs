using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class User
    {
        private long ID { get; set; }
        private List<Character> CharacterList { get; set; }
        private Character currentCharacter { get; set; }
        private List<int> ScoreList { get; set; }

        public User()
        {
            CharacterList = new List<Character>();
            ScoreList = new List<int>();
        }

        public void SaveGame(Level lvl)
        {
            
        }
        
        public void LoadGame()
        {
            
        }

        public void ChooseCharacter(FactionType factionType)
        {
            
        }
    }
}