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

            var httpClient = _httpClientFactory.CreateClient();

            try
            {
                string htmlContent = await httpClient.GetStringAsync(_source);

                // Load the HTML content into the HtmlDocument
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                var table = htmlDocument.DocumentNode.SelectSingleNode("//table[@id='dnn_ctr20025_View_grdRates']");
                var rows = table.SelectNodes(".//tr[@class='Item']");
                if (rows != null)
                {
                    foreach (var row in rows)
                    {
                        var cells = row.SelectNodes("td");
                        if (cells != null && cells.Count >= 4)
                        {
                            var currency = cells[0].InnerText;
                            var buy = cells[1].InnerText;
                            var sell = cells[2].InnerText;

                            try
                            {
                                result[currency] = new ParseResult
                                {
                                    BuyRate = decimal.Parse(buy),
                                    SellRate = decimal.Parse(sell)
                                };
                            }
                            catch(FormatException) { }
                        }
                    }
                }
            }
            catch (HttpRequestException e)
            {

            }

            return await Task.FromResult(result);
        }

    }
}
