using System;

class ThiefCharacter : Character
{
    const int startingHitPoints = 15;
    const int startingSpellPoints = 0;
    const int startingStrength = 12;
    const int startingStamina = 10;
    const int startingAgility = 14;
    const int startingSpeed = 12;
    const int startingIntellect = 10;
    const int startingLuck = 12;
    public ThiefCharacter(string name, bool gender)
    {
        ThiefCharacter.className = "Thief";
        this.name = name;
        this.hitPoints[0] = startingHitPoints;
        this.hitPoints[1] = startingHitPoints;
        this.spellPoints[0] = startingSpellPoints;
        this.spellPoints[1] = startingSpellPoints;
        this.strength = startingStrength;
        this.stamina = startingStamina;
        this.agility = startingAgility;
        this.speed = startingSpeed;
        this.intelligence = startingIntellect;
        this.luck = startingLuck;
    }
}
