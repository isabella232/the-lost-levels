using System;

class WizardCharacter : Character
{
    const int startingHitPoints = 10;
    const int startingSpellPoints = 20;
    const int startingStrength = 10;
    const int startingStamina = 12;
    const int startingAgility = 12;
    const int startingSpeed = 10;
    const int startingIntellect = 16;
    const int startingLuck = 10;
    public WizardCharacter(string name, bool gender)
    {
        WizardCharacter.className = "Wizard";
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
