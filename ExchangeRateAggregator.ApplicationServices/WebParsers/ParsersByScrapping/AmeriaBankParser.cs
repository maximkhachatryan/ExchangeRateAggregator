using ExchangeRateAggregator.ApplicationContracts.Attributes.WebParsers;
using ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers;
using ExchangeRateAggregator.Domain.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExchangeRateAggregator.ApplicationServices.WebParsers.ParsersByScrapping
{
    [WebParser(WebParser.AmeriaBank)]
    internal class AmeriaBankParser : ParserByScrapping
    {
        public AmeriaBankParser(string source, IHttpClientFactory httpClientFactory)
            : base(source, httpClientFactory)
        {
        }

        public override async Task<Dictionary<string, ParseResult>> Parse()
        {
            var result = new Dictionary<string, ParseResult>();

            //var httpClient = _httpClientFactory.CreateClient();

            //try
            //{
            //    string htmlContent = await httpClient.GetStringAsync(_source);

            //    // Load the HTML content into the HtmlDocument
            //    HtmlDocument htmlDocument = new HtmlDocument();
            //    htmlDocument.LoadHtml(htmlContent);

            //    var node = htmlDocument.GetElementbyId("dnn_ctr40461_View_grdForwardRates");

            //    //To be continued...
            //}
            //catch (HttpRequestException e)
            //{
            //}

            return await Task.FromResult(result);
        }

    }
}
