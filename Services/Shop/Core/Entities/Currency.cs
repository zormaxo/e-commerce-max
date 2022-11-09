namespace Shop.Core.Entities;

public class Currency : BaseEntity
{
    public DateTime Date { get; set; }

    public decimal Try { get; set; }

    public decimal Usd { get; set; }

    public decimal Eur { get; set; }

    public decimal Gbp { get; set; }
}
