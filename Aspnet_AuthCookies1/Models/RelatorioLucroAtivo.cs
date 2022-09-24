using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aspnet_AuthCookies1.Models
{
    public class RelatorioLucroAtivo
    {
        public string NomeAtivo;
        public float Desagio;
        public DateTime DataInicial;
        public DateTime DataFinal;
        public DateTime HoraInicial;
        public DateTime HoraFinal;
        public int Entradas;
        public int EntradasLucro;
        public int EntradasPrejuizo;
        public float PercentEntradasLucro;
        public float PercentEntradasPrejuizo;
        public float LucroMedio;
        public float LucroSomatorio;
        public float LucroMax;
        public float LucroMin;
        [DisplayFormat(DataFormatString = "{0,12:0,000.00}")]
        public float VolumeTotalMedio;
        public TimeSpan TempoProcessamento;

        public List<CotacaoIntraDay> cotacoesIntraDay;

        public RelatorioLucroAtivo(string ativo, float desagio, DateTime dataInicial, DateTime dataFinal, DateTime horaInicial, DateTime horaFinal)
        {
            NomeAtivo = ativo;
            Desagio = desagio;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
        }
    }
}
