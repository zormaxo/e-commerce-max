namespace Core.Dtos
{
    public class CurrencyDto
    {
        private string _date;

        public string Date
        {
            get => _date;
            set => _date = value.Split('+')[0];
        }
        public Rates Rates { get; set; }
        public string Base { get; set; }
    }

    public class Rates
    {
        //public double TRY { get; set; }
        //public string GBP { get; set; }
        //public string EUR { get; set; }
        //public string USD { get; set; }

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

        public double GBP
        {
            get => _gbp;
            set => _gbp = Convert.ToDouble($"{value:F2}");
        }

        private double _eur;

        public double EUR
        {
            get => _eur;
            set => _eur = Convert.ToDouble($"{value:F2}");
        }

        private double _usd;

        public double USD
        {
            get => _usd;
            set => _usd = Convert.ToDouble($"{value:F2}");
        }
    }
}