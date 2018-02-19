using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Promotion.Coupon.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public List<ChartItem> LineChartVPowerData { get; set; }
        public string SerializedLineVPowerData
        {
            get
            {
                var response = new List<List<int>>();

                for (int i = 0; i < LineChartVPowerData.Count; i++)
                {
                    response.Add(new List<int>() { i, LineChartVPowerData.ElementAt(i).Value });
                }

                return JsonConvert.SerializeObject(response);
            }
        }

        public List<DashboardViewModel.ChartItem> PersonsChartData { get; set; }
        public string SerializedPersonsChartData
        {
            get
            {
                var response = new List<List<int>>();

                for (int i = 0; i < PersonsChartData.Count; i++)
                {
                    response.Add(new List<int>() { i, PersonsChartData.ElementAt(i).Value });
                }

                return JsonConvert.SerializeObject(response);
            }
        }

        public List<ChartItem> LineChartLubrificantesData { get; set; }
        public string SerializedLineLubrificantesData
        {
            get
            {
                var response = new List<List<int>>();

                for (int i = 0; i < LineChartLubrificantesData.Count; i++)
                {
                    response.Add(new List<int>() { i, LineChartLubrificantesData.ElementAt(i).Value });
                }

                return JsonConvert.SerializeObject(response);
            }
        }

        public string SerializedLineChartTicks
        {
            get
            {
                var response = new List<List<string>>();

                int i = 0;
                foreach(var el in LineChartVPowerData) {
                    var date = el.Label.Split('-');

                    response.Add(new List<string>() { i.ToString(), date[2] + "/" + date[1] });

                    i++;
                }

                return JsonConvert.SerializeObject(response);
            }
        }

        public List<ChartItem> PieChartData { get; set; }
        public string SerializedPieChartData
        {
            get
            {
                var response = new List<List<string>>();

                int i = 0;
                foreach (var el in PieChartData)
                {
                    response.Add(new List<string>() { el.Label, el.Value.ToString() });

                    i++;
                }

                return JsonConvert.SerializeObject(response);
            }
        }

        public int VPowerWinners { get; set; }
        public int LubrificantesWinners { get; set; }

        public class ChartItem
        {
            public string Label { get; set; }
            public int Value { get; set; }
        }
    }
}