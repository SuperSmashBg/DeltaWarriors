using BaseLib.Abstracts;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using Godot;

namespace DeltaWarriors.DeltaWarriorsCode.Character;

public class TheCageRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => TheCage.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}