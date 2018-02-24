using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Areas.Admin.Models
{
    public class LuckyCodeReportViewModel
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }

        public int LuckyCodeCount { get; set; }
        //public List<LuckyCode> LuckyCodes { get; set; }

        public List<DashboardViewModel.ChartItem> LuckyCodeChartData { get; set; }
        public string SerializedLuckyCodeData
        {
            get
            {
                var response = new List<List<int>>();

                for (int i = 0; i < LuckyCodeChartData.Count; i++)
                {
                    response.Add(new List<int>() { i, LuckyCodeChartData.ElementAt(i).Value });
                }

                return JsonConvert.SerializeObject(response);
            }
        }

        public string SerializedLineChartTicks
        {
            get
            {
                var response = new List<List<string>>();

                int i = -1;
                var fator = Convert.ToDecimal(LuckyCodeChartData.Count) / Convert.ToDecimal(10);
                fator = Math.Floor(fator) - 1;
                int f = 0;

                foreach (var el in LuckyCodeChartData)
                {
                    i++;
                    if (f < fator && i != 0)
                    {
                        f++;
                    }
                    else
                    {
                        var date = el.Label.Split('-');

                        response.Add(new List<string>() { i.ToString(), date[2] + "/" + date[1] });

                        f = 0;
                    }
                }

                return JsonConvert.SerializeObject(response);
            }
        }
    }
}