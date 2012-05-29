using System;

public class FighterCharacter : Character
{
	const int startingSpellPoints = 0;
    const int startingStrength = 16;
    const int startingStamina = 14;
    const int startingAgility = 12;
    const int startingSpeed = 10;
    const int startingIntellect = 10;
    const int startingLuck = 10;
    public FighterCharacter(string name, bool gender)
    {
        FighterCharacter.className = "Fighter";
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
    }
}
