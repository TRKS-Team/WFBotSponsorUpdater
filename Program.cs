using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WFBotSponsorUpdater
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sponsors = await GetAllSponsors();

            var sponsorsText = string.Join(' ', sponsors.Select(s => s.Name));
            const string Magic = "<!--SPONSORS_BEGIN-->";
            var sponsorsLine = Magic + sponsorsText;

            var lines = await File.ReadAllLinesAsync("README.md", new UTF8Encoding(false));
            for (var i = 0; i < lines.Length; i++)
            {
                var lineInReadme = lines[i];
                if (lineInReadme.StartsWith(Magic))
                {
                    if (lineInReadme != sponsorsLine)
                    {
                        lines[i] = sponsorsLine;
                    }
                    break;
                }
            }
            File.WriteAllLines("README.md", lines, new UTF8Encoding(false));
        }

        static async Task<List<User>> GetAllSponsors()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.54");
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            int page = 1;
            ApiResult result;
            var list = new List<User>();
            do
            {
                result = await httpClient.GetFromJsonAsync<ApiResult>($"https://afdian.net/api/creator/get-sponsors?user_id=6278895e1f1511e9836c52540025c377&type=new&page={page}");
                if (result!.Ec != 200)
                {
                    Console.WriteLine(result);
                    Environment.Exit(-1);
                }
                list.AddRange(result.Data.User);
                page++;

            } while (result.Data.User.Length != 0);

            return list;
        }
    }
}
