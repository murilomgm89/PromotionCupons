using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models
{
    public class PartitipationsReportViewModel
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }

        public int ReceiptCount { get; set; }
        public int PersonCount { get; set; }

        public List<Entity.Person> People { get; set; }

        public List<ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models.DashboardViewModel.ChartItem> ReceiptsChartData { get; set; }
        public string SerializedReceiptsData
        {
            get
            {
                var response = new List<List<int>>();

                for (int i = 0; i < ReceiptsChartData.Count; i++)
                {
                    response.Add(new List<int>() { i, ReceiptsChartData.ElementAt(i).Value });
                }

                return JsonConvert.SerializeObject(response);
            }
        }

        //public List<ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models.DashboardViewModel.ChartItem> PersonsChartData { get; set; }
        //public string SerializedPersonsChartData
        //{
        //    get
        //    {
        //        var response = new List<List<int>>();

        //        for (int i = 0; i < PersonsChartData.Count; i++)
        //        {
        //            response.Add(new List<int>() { i, PersonsChartData.ElementAt(i).Value });
        //        }

        //        return JsonConvert.SerializeObject(response);
        //    }
        //}

        public string SerializedLineChartTicks
        {
            get
            {
                var response = new List<List<string>>();

                int i = -1;
                var fator = Convert.ToDecimal(ReceiptsChartData.Count) / Convert.ToDecimal(10);
                fator = Math.Floor(fator) - 1;
                int f = 0;

                foreach (var el in ReceiptsChartData)
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