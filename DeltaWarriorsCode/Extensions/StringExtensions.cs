using Godot;

namespace DeltaWarriors.DeltaWarriorsCode.Extensions;

//Mostly utilities to get asset paths.
public static class StringExtensions
{
    public static string ImagePath(this string path)
    {
        return Path.Join(MainFile.ResPath, "images", path);
    }

    public static string CardImagePath(this string path)
    {
        string cardPath = Path.Join(MainFile.ResPath, "images", "card_portraits", path);
        if (ResourceLoader.Exists(cardPath)) return cardPath;
        
        string betaPath = Path.Join(MainFile.ResPath, "images", "card_portraits", "beta", path);
        if (ResourceLoader.Exists(betaPath)) return betaPath;
        
        MainFile.Logger.Info("Could not find beta card image path: " + path);
        return Path.Join(MainFile.ResPath, "images", "card_portraits", "card.png");
    }

    public static string BigCardImagePath(this string path)
    {
        string cardPath = Path.Join(MainFile.ResPath, "images", "card_portraits", "big", path);
        if (ResourceLoader.Exists(cardPath)) return cardPath;
        
        //MainFile.Logger.Info("Could not find big card image path: " + path);
        string betaPath = Path.Join(MainFile.ResPath, "images", "card_portraits", "big", "beta", path);
        if (ResourceLoader.Exists(betaPath)) return betaPath;

        //MainFile.Logger.Info("Could not find beta big card image path: " + path);
        return Path.Join(MainFile.ResPath, "images", "card_portraits", "big", "card.png");
    }

    public static string PowerImagePath(this string path)
    {
        path = Path.Join(MainFile.ResPath, "images", "powers", path);
        if (ResourceLoader.Exists(path)) return path;

        //  MainFile.Logger.Info("Could not find power image path: " + path);
        return Path.Join(MainFile.ResPath, "images", "powers", "power.png");
    }
    

    public static string BigPowerImagePath(this string path)
    {
        path = Path.Join(MainFile.ResPath, "images", "powers", "big", path);
        if (ResourceLoader.Exists(path)) return path;

        // MainFile.Logger.Info("Could not find big power image path: " + path);
        return Path.Join(MainFile.ResPath, "images", "powers", "big", "power.png");
    }

    public static string RelicImagePath(this string path)
    {
        path = Path.Join(MainFile.ResPath, "images", "relics", path);
        if (ResourceLoader.Exists(path)) return path;

        // MainFile.Logger.Info("Could not find relic image path: " + path);
        return Path.Join(MainFile.ResPath, "images", "relics", "relic.png");
    }

    public static string BigRelicImagePath(this string path)
    {
        path = Path.Join(MainFile.ResPath, "images", "relics", "big", path);
        if (ResourceLoader.Exists(path)) return path;

        // MainFile.Logger.Info("Could not find big relic image path: " + path);
        return Path.Join(MainFile.ResPath, "images", "relics", "big", "relic.png");
    }

    public static string CharacterUiPath(this string path)
    {
        return Path.Join(MainFile.ResPath, "images", "charui", path);
    }
}