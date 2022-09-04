namespace Core.Entities
{
    public class Currency : BaseEntity
    {
        public DateTime Date { get; set; }
        public double Try { get; set; }
        public double Usd { get; set; }
        public double Eur { get; set; }
        public double Gbp { get; set; }
    }
}
