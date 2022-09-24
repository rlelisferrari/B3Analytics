using System;
using System.ComponentModel.DataAnnotations;

namespace Aspnet_AuthCookies1.Models
{
    public class CotacaoIntraDay
    {
        public CotacaoIntraDay()
        {
        }

        public CotacaoIntraDay(DateTime? dateTime, double? open, double? high, double? low, double? close, decimal? volume)
        {
            DateTime = (DateTime)dateTime;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DateTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double? Open { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double? High { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double? Low { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double? Close { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        //[DisplayFormat(DataFormatString = "{0,12:0,000.00}")]
        public decimal? Volume { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double? LucroPrejuizo { get; set; }

        [DisplayFormat(DataFormatString = "{0,12:0,000.00}")]
        public float VolumeTotal { get; set; }
    }
}
