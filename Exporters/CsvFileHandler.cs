using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Exporters
{
    public class CsvFileHandler
    {
        private static string SetCsvColumns(string[] columnNames)
        {
            string csv = string.Empty;

            foreach(string column in columnNames)
            {
                csv += column + ',';
            }

            csv += "\r\n";
            return csv;
        }

        private static string WriteDataToCsv(List<Person> people)
        {
            string csv = string.Empty;
            foreach(var person in people)
            {
                //csv += person.Id.ToString() + ",";
                csv += "=\""+new string(person.Embg) + "\",";
                csv += person.Name + ",";
                csv += person.LastName + ",";
                csv += person.Address.Replace(","," ") + ",";

                csv += "\r\n";
            }
            return csv;
        }

        public byte[] ExportCSV(string[] columns, List<Person> people)
        {
            string csv = string.Empty;

            csv = SetCsvColumns(columns);

            csv += WriteDataToCsv(people);

            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return bytes;
        }
    }
}