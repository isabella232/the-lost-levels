using System;
namespace TheLostLevels
{
    public class Character
    {
        public enum CharacterType { Fighter = 0, Wizard = 1, Priest = 2, Theif = 3 };

        static string[] typeNmaes = 
        {
            "Fighter",
            "Wizard",
            "Priest",
            "Thief"
        };

        protected string name;
        protected int[] hitPoints = new int[2];
        protected int[] spellPoints = new int[2];

        protected int level = 1;
        protected int strength = 10;
        protected int stamina = 10;
        protected int agility = 10;
        protected int speed = 10;
        protected int intelligence = 10;
        protected int luck = 10;

        protected static string className;

        public static string[] TypeNames
        {
            get { return typeNames; }
        }

        public int HitPointsMax
        {
            get { return hitPoints[1]; }
        }

        public int SpellPointsMax
        {
            get { return spellPointsw[1]; }
        }

        public int SpellPointsCurrent
        {
            get { return spellPoints[0]; }
        }

        public virtual int Strength
        {
            get { return strength; }
        }

        public virtual int Stamina
        {
            get { return stamina; }
        }

        public virtual int Agility
        {
            get { return agility; }
        }

        public virtual int Speed
        {
            get { return speed; }
        }

        public virtual int Intelligence
        {
            get { return intelligence; }
        }

        public virtual int Luck
        {
            get { return luck; }
        }
    }
}