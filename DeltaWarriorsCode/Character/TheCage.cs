using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Basic;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Relics.Cage;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;

namespace DeltaWarriors.DeltaWarriorsCode.Character;
public class TheCage : PlaceholderCharacterModel
{
    //TODO: 
    // More manual cards (Esp Commons)
    // More Otherworldly Corruption
    // Better use of draw Next turn
    
    public const string CharacterId = "KrisTheCage";

    public static readonly Color Color = new("00bbbb");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 75;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeCage>(),
        ModelDb.Card<StrikeCage>(),
        ModelDb.Card<StrikeCage>(),
        ModelDb.Card<StrikeCage>(),
        ModelDb.Card<DefendCage>(),
        ModelDb.Card<DefendCage>(),
        ModelDb.Card<DefendCage>(),
        ModelDb.Card<DefendCage>(),
        ModelDb.Card<ImmerseCage>(),
        ModelDb.Card<ActCage>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<BallOfJunk>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<TheCageCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<TheCageRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<TheCagePotionPool>();
    
    public override Color MapDrawingColor => new Color("2255CC");

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}