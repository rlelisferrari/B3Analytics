using EOD;
using EOD.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EOD.API;

namespace Aspnet_AuthCookies1.Models
{
    public class B3ApiService
    {
        public API _api;
        private readonly ILogger<B3ApiService> _logger;

        public B3ApiService(string apiToken, ILogger<B3ApiService> logger)
        {
            _api = new API(apiToken);
            _logger = logger;
        }

        public virtual async Task<ICollection<HistoricalStockPrice>> GetEndOfDay(string ticker, DateTime dataInicio, DateTime dataFim, int periodo)
        {
            try
            {
                // AngloAmerican stock that trades from January 1 to December 11 in the London Stock Exchange
                List<HistoricalStockPrice> response = await _api.GetEndOfDayHistoricalStockPriceAsync(ticker, dataInicio, dataFim, (HistoricalPeriod)periodo);
                return response;
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public virtual async Task<ICollection<IntradayHistoricalStockPrice>> GetIntraday(string ticker, DateTime dataInicio, DateTime dataFim, int periodo)
        {
            try
            {
                // An example of every hour intraday historical stock price data for AAPL (Apple Inc)
                List<IntradayHistoricalStockPrice>? response = await _api.GetIntradayHistoricalStockPriceAsync(ticker, dataInicio, dataFim, (IntradayHistoricalInterval)periodo);
                return response;
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public RelatorioLucroAtivo AnaliseLucroPorAtivo(ICollection<IntradayHistoricalStockPrice> cotacoes, string ativo, float desagio, DateTime dataInicial, DateTime dataFinal, DateTime horaInicial, DateTime horaFinal)
        {
            var relatorio = new RelatorioLucroAtivo(ativo, desagio, dataInicial, dataFinal, horaInicial, horaFinal);
            relatorio.cotacoesIntraDay = AnaliseLucroPeriodo(cotacoes, horaInicial, horaFinal, desagio);

            return relatorio;
        }

        public RelatorioLucroAtivo AnaliseLucroPorAtivoResumo(ICollection<IntradayHistoricalStockPrice> cotacoes, string ativo, float desagio, DateTime dataInicial, DateTime dataFinal, DateTime horaInicial, DateTime horaFinal)
        {
            var relatorio = new RelatorioLucroAtivo(ativo, desagio, dataInicial, dataFinal, horaInicial, horaFinal);
            relatorio = AnaliseLucroPeriodoResumo(ativo, relatorio, cotacoes, horaInicial, horaFinal, desagio);

            return relatorio;
        }

        public RelatorioLucroAtivo AnaliseLucroPorAtivoResumoComVolume(ICollection<IntradayHistoricalStockPrice> cotacoes, string ativo, float desagio, DateTime dataInicial, DateTime dataFinal, DateTime horaInicial, DateTime horaFinal)
        {
            var relatorio = new RelatorioLucroAtivo(ativo, desagio, dataInicial, dataFinal, horaInicial, horaFinal);
            relatorio = AnaliseLucroPeriodoResumoComVolume(ativo, relatorio, cotacoes, horaInicial, horaFinal, desagio);

            return relatorio;
        }

        public List<CotacaoIntraDay> AnaliseLucroPeriodo(ICollection<IntradayHistoricalStockPrice> cotacoes, DateTime horaInicial, DateTime horaFinal, float desagio)
        {
            var diaInicial = cotacoes.FirstOrDefault().DateTime;
            var diaFinal = cotacoes.LastOrDefault().DateTime;
            var listCotacaoIntraDay = new List<CotacaoIntraDay>();
            for (var itemData = diaInicial.Value.Date; itemData.Date <= diaFinal.Value.Date; itemData = itemData.AddDays(1))
            {                
                var horaInicio = new DateTime(itemData.Year, itemData.Month, itemData.Day, horaInicial.Hour, horaInicial.Minute, 0);
                var horaFim = new DateTime(itemData.Year, itemData.Month, itemData.Day, horaFinal.Hour, horaFinal.Minute, 0);
                var cotacoesFiltradas = cotacoes.Where(it => horaInicio <= it.DateTime && it.DateTime <= horaFim).ToList();

                if (cotacoesFiltradas == null || cotacoesFiltradas.Count <= 0)
                    continue;

                var cotacaoReferenciaInicial = cotacoesFiltradas.FirstOrDefault(it => it.Open != null);
                var cotacaoReferenciaFinal = cotacoesFiltradas.LastOrDefault(it => it.Open != null);
                var target = cotacaoReferenciaInicial.Open - desagio;

                double? lucro = 0.0;
                var achouLucro = false;
                foreach (var item in cotacoesFiltradas)
                {
                    var cotacaoIntraDay = IntradayHistStockPriceToCotacaoIntraDay(item);
                    achouLucro = achouLucro ? achouLucro: item.Low <= target;
                    if (achouLucro)
                        lucro = cotacaoReferenciaFinal.Close - target;
                    listCotacaoIntraDay.Add(cotacaoIntraDay);
                }

                if (achouLucro)
                    listCotacaoIntraDay.LastOrDefault().LucroPrejuizo = Math.Round((float)lucro,2);
            }
            
            return listCotacaoIntraDay;
        }

        public RelatorioLucroAtivo AnaliseLucroPeriodoResumo(string ativo, RelatorioLucroAtivo relatorio, ICollection<IntradayHistoricalStockPrice> cotacoes, DateTime horaInicial, DateTime horaFinal, float desagio)
        {
            var diaInicial = cotacoes.FirstOrDefault().DateTime;
            var diaFinal = cotacoes.LastOrDefault().DateTime;
            var listCotacaoIntraDay = new List<CotacaoIntraDay>();

            relatorio.LucroMax = 0;
            relatorio.LucroMin = 0;
            for (var itemData = diaInicial.Value.Date; itemData.Date <= diaFinal.Value.Date; itemData = itemData.AddDays(1))
            {
                var horaInicio = new DateTime(itemData.Year, itemData.Month, itemData.Day, horaInicial.Hour, horaInicial.Minute, 0);
                var horaFim = new DateTime(itemData.Year, itemData.Month, itemData.Day, horaFinal.Hour, horaFinal.Minute, 0);
                var cotacoesFiltradas = cotacoes.Where(it => horaInicio <= it.DateTime && it.DateTime <= horaFim).ToList();

                if (cotacoesFiltradas == null || cotacoesFiltradas.Count <= 0)
                    continue;

                var cotacaoReferenciaInicial = cotacoesFiltradas.FirstOrDefault(it => it.Open != null);
                var cotacaoReferenciaFinal = cotacoesFiltradas.LastOrDefault(it => it.Open != null);
                if (cotacaoReferenciaInicial == null || cotacaoReferenciaFinal == null)
                {
                    _logger.LogInformation($"Cotação com dados nulos ativo {ativo}");
                    break;
                }

                var target = cotacaoReferenciaInicial.Open - desagio;

                double? lucro = 0.0;
                var achouLucro = false;
                foreach (var item in cotacoesFiltradas)
                {
                    var cotacaoIntraDay = IntradayHistStockPriceToCotacaoIntraDay(item);
                    achouLucro = achouLucro ? achouLucro : item.Low <= target;
                    if (achouLucro)
                    {
                        lucro = cotacaoReferenciaFinal.Close - target;
                        RelatorioLucro(relatorio, (float)lucro);
                        cotacaoIntraDay.LucroPrejuizo = Math.Round((float)lucro, 2);
                        listCotacaoIntraDay.Add(cotacaoIntraDay);
                        break;
                    }                        
                }
            }
            
            relatorio.cotacoesIntraDay = listCotacaoIntraDay;
            return relatorio;
        }

        public RelatorioLucroAtivo AnaliseLucroPeriodoResumoComVolume(string ativo, RelatorioLucroAtivo relatorio, ICollection<IntradayHistoricalStockPrice> cotacoes, DateTime horaInicial, DateTime horaFinal, float desagio)
        {
            var diaInicial = cotacoes.FirstOrDefault().DateTime;
            var diaFinal = cotacoes.LastOrDefault().DateTime;
            var listCotacaoIntraDay = new List<CotacaoIntraDay>();

            relatorio.LucroMax = 0;
            relatorio.LucroMin = 0;
            float volumeTodosDias = 0f;
            int quantDias = 0;
            for (var itemData = diaInicial.Value.Date; itemData.Date <= diaFinal.Value.Date; itemData = itemData.AddDays(1))
            {
                var horaInicio = new DateTime(itemData.Year, itemData.Month, itemData.Day, horaInicial.Hour, horaInicial.Minute, 0);
                var horaFim = new DateTime(itemData.Year, itemData.Month, itemData.Day, horaFinal.Hour, horaFinal.Minute, 0);
                var cotacoesFiltradas = cotacoes.Where(it => horaInicio <= it.DateTime && it.DateTime <= horaFim).ToList();

                if (cotacoesFiltradas == null || cotacoesFiltradas.Count <= 0)
                    continue;

                var cotacaoReferenciaInicial = cotacoesFiltradas.FirstOrDefault(it => it.Open != null);
                var cotacaoReferenciaFinal = cotacoesFiltradas.LastOrDefault(it => it.Open != null);
                if (cotacaoReferenciaInicial == null || cotacaoReferenciaFinal == null)
                {
                    _logger.LogInformation($"Cotação com dados nulos ativo {ativo}");
                    break;
                }

                quantDias++;
                var target = cotacaoReferenciaInicial.Open - desagio;

                double? lucro = 0.0;
                var achouLucro = false;
                float volumeDia = 0f;
                var proximoDia = true;
                var cotacaoLucro = new CotacaoIntraDay();
                foreach (var item in cotacoesFiltradas)
                {
                    cotacaoLucro = IntradayHistStockPriceToCotacaoIntraDay(item);
                    volumeDia += cotacaoLucro.Volume != null ? (float)cotacaoLucro.Volume: 0f;
                    achouLucro = achouLucro ? achouLucro : item.Low <= target;
                    if (achouLucro && proximoDia)
                    {
                        lucro = cotacaoReferenciaFinal.Close - target;
                        RelatorioLucro(relatorio, (float)lucro);
                        cotacaoLucro.LucroPrejuizo = Math.Round((float)lucro, 2);
                        listCotacaoIntraDay.Add(cotacaoLucro);
                        proximoDia = false;
                    }
                }
                if (listCotacaoIntraDay.LastOrDefault() != null)
                    listCotacaoIntraDay.LastOrDefault().VolumeTotal = volumeDia; 
                volumeTodosDias += volumeDia;
            }

            relatorio.VolumeTotalMedio = (volumeTodosDias / quantDias).ToString("N2");
            relatorio.cotacoesIntraDay = listCotacaoIntraDay;
            return relatorio;
        }

        public CotacaoIntraDay IntradayHistStockPriceToCotacaoIntraDay(IntradayHistoricalStockPrice itemIn)
        {
            return new CotacaoIntraDay(itemIn.DateTime.Value.AddHours(-3), itemIn.Open, itemIn.High, itemIn.Low, itemIn.Close, itemIn.Volume);
        }

        public void RelatorioLucro(RelatorioLucroAtivo relatorio, float lucro)
        {
            if (lucro > 0)
                relatorio.EntradasLucro++;
            else if (lucro < 0)
                relatorio.EntradasPrejuizo++;

            relatorio.Entradas = relatorio.EntradasLucro + relatorio.EntradasPrejuizo;
            relatorio.PercentEntradasLucro = relatorio.Entradas != 0 ? (float)Math.Round((float)relatorio.EntradasLucro / (float)relatorio.Entradas * 100,2): 0f;
            relatorio.PercentEntradasPrejuizo = relatorio.Entradas != 0 ? (float)Math.Round((float)relatorio.EntradasPrejuizo / (float)relatorio.Entradas * 100,2) : 0f;
            relatorio.LucroMax = (float)Math.Round(Math.Max(relatorio.LucroMax, lucro),2);
            relatorio.LucroMin = (float)Math.Round(Math.Min(relatorio.LucroMin, lucro),2);
            relatorio.LucroSomatorio += lucro;
            relatorio.LucroMedio = relatorio.Entradas != 0 ? (float)Math.Round(relatorio.LucroSomatorio / relatorio.Entradas,2) : 0f;
        }
    }
}
