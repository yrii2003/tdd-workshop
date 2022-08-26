namespace TddWorkshop.Domain.InstantCredit;

public record CalculateCreditRespons(int Points)
{
    public bool IsApproved => Points >= 80;
    
    public decimal? InterestRate => Points switch
    {
        < 80 => null,
        < 84 => 30,
        < 88 => 26,
        < 92 => 22,
        < 96 => 19,
        < 100 => 15,
        100 => 12.5m,
        _ => null
    };
}