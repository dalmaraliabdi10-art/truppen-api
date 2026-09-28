namespace TruppenApi.Models;

public enum Linje
{ // Enum values för categorisering av positioner i linjer
    Målvakt,
    Försvar,
    Mittfält,
    Anfall
}

public enum Position
{ // Enum values för mer specifika positioner på planen
    Målvakt,
    Högerback,
    Vänsterback,
    Mittback,
    Wingback,
    Defensivmittfältare,
    Centralmittfältare,
    Offensivmittfältare,
    Yttermittfältare,
    Högerytter,
    Vänsterytter,
    Anfallare
}

public static class PositionExtensions
{ // extensionsmetoder för Position enum
    public static Linje GetLinje(this Position position) => position switch
    {
        Position.Målvakt => Linje.Målvakt,

        Position.Högerback or Position.Vänsterback or
        Position.Mittback or Position.Wingback => Linje.Försvar,

        Position.Defensivmittfältare or Position.Centralmittfältare or
        Position.Offensivmittfältare or Position.Yttermittfältare => Linje.Mittfält,

        _ => Linje.Anfall
    };

    // Denna är en extensionmetod som returnerar det klassiska numret för en given position.
    // Wingback och yttermittfältare saknar klassiskt nummer. De oftas har samma som höger/väsnter back och samma gäller för vänster/höger ytter.
    // De kommer därför returnera null.
    // Dessa nummer som 6,8,10,4 och 1 är de typiska för deras positioner i fotboll.
    // En anfallare kan ha nummer 7,11 eller 10 och samma gäller för vissa nummer som inte nämndes.
    public static string? GetKlassisktNummer(this Position position) => position switch
    {
        Position.Målvakt => "1",
        Position.Högerback => "2",
        Position.Vänsterback => "3",
        Position.Mittback => "4 / 5",
        Position.Defensivmittfältare => "6",
        Position.Högerytter => "7",
        Position.Centralmittfältare => "8",
        Position.Anfallare => "9",
        Position.Offensivmittfältare => "10",
        Position.Vänsterytter => "11",
        _ => null
    };
}