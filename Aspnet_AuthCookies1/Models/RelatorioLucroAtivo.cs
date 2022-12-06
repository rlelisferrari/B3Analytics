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
        public float LucroMedioPercentual;
        public float LucroSomatorio;
        public float LucroSomatorioPercentual;
        public float LucroMax;
        public float LucroMaxPercentual;
        public float LucroMin;
        public float LucroMinPercentual;
        public string VolumeTotalMedio;
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
