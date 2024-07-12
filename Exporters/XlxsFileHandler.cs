using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using OfficeOpenXml;

namespace api.Exporters
{
    public class XlxsFileHandler
    {
        public byte[] ExportExcel(string[] columnNames, List<Person> people)
        {
            using (MemoryStream ms = new())
            {
                using (ExcelPackage package = new(ms))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("People");

                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = columnNames[i];
                    }

                    for (int row = 0; row < people.Count; row++)
                    {
                        worksheet.Cells[row + 2, 1].Value = people[row].Embg;
                        worksheet.Cells[row + 2, 2].Value = people[row].Name;
                        worksheet.Cells[row + 2, 3].Value = people[row].LastName;
                        worksheet.Cells[row + 2, 4].Value = people[row].Address;
                    }

                    package.Save();
                }

                return ms.ToArray();
            }
        }
    }
}