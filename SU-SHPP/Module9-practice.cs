using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU_SHPP
{
    public interface IReport
    {
        string Generate();
    }

    public class SalesReport : IReport
    {
        public string Generate()
        {
            return "Отчет по продажам: \nТовар 1 - 100 шт. \nТовар 2 - 200 шт.";
        }
    }

    public class UserReport : IReport
    {
        public string Generate()
        {
            return "Отчет по пользователям: \nПользователь 1 - активный \nПользователь 2 - неактивный";
        }
    }

    public abstract class ReportDecorator : IReport
    {
        protected IReport _report;

        public ReportDecorator(IReport report)
        {
            _report = report;
        }

        public virtual string Generate()
        {
            return _report.Generate();
        }
    }

    public class DateFilterDecorator : ReportDecorator
    {
        private string _startDate;
        private string _endDate;

        public DateFilterDecorator(IReport report, string startDate, string endDate) : base(report)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public override string Generate()
        {
            return $"{base.Generate()}\nФильтр по датам: с {_startDate} до {_endDate}";
        }
    }

    public class SortingDecorator : ReportDecorator
    {
        private string _criteria;

        public SortingDecorator(IReport report, string criteria) : base(report)
        {
            _criteria = criteria;
        }

        public override string Generate()
        {
            return $"{base.Generate()}\nСортировка по: {_criteria}";
        }
    }

    public class CsvExportDecorator : ReportDecorator
    {
        public CsvExportDecorator(IReport report) : base(report) { }

        public override string Generate()
        {
            return $"{base.Generate()}\nЭкспорт в формат CSV выполнен.";
        }
    }

    public class PdfExportDecorator : ReportDecorator
    {
        public PdfExportDecorator(IReport report) : base(report) { }

        public override string Generate()
        {
            return $"{base.Generate()}\nЭкспорт в формат PDF выполнен.";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IReport report = new SalesReport();

            report = new DateFilterDecorator(report, "01.01.2024", "31.01.2024");
            report = new SortingDecorator(report, "по дате");
            report = new CsvExportDecorator(report);

            Console.WriteLine(report.Generate());
        }
    }
}
