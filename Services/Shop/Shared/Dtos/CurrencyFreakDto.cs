namespace Shop.Shared.Dtos;

public class CurrencyFreakDto
{
    private string _date = string.Empty;

    public string Date { get => _date; set => _date = value.Split('+')[0]; }

    public Rates? Rates { get; set; }

    public string Base { get; set; } = string.Empty;
}

public class Rates
{
    private double _try;

    public double TRY
    {
        get => _try;
        set
        {
            _ = double.TryParse($"{value:F2}", out double newValue);
            _try = newValue;
        }
    }

    private double _gbp;

    public double GBP { get => _gbp; set => _gbp = Convert.ToDouble($"{value:F2}"); }

    private double _eur;

    public double EUR { get => _eur; set => _eur = Convert.ToDouble($"{value:F2}"); }

    private double _usd;

    public double USD { get => _usd; set => _usd = Convert.ToDouble($"{value:F2}"); }
}