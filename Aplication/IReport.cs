using Domain;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aplication
{
    public interface IReport
    {
        public  List<Report> ReportRequestGet(int? Status, DateTime FromDate, DateTime ToDate, int CallerId, string Street);
    }
}
