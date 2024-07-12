using api.Models;
// using iTextSharp.text;
// using iTextSharp.text.pdf;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using System.Collections.Generic;




namespace api.Exporters
{
    public class PdfFileHandler
    {
        public byte[] ExportPDF(string[] columnNames, List<Person> people)
        {
            using (MemoryStream ms = new())
            {
                PdfWriter writer = new(ms);
                PdfDocument pdf = new(writer);
                Document doc = new(pdf);

                Table table = new(columnNames.Length, true);

                foreach (string column in columnNames)
                {
                    table.AddHeaderCell(column);
                }

                foreach (var person in people)
                {
                    table.AddCell(person.Embg);
                    table.AddCell(person.Name);
                    table.AddCell(person.LastName);
                    table.AddCell(person.Address);
                }

                doc.Add(table);
                doc.Close();

                return ms.ToArray();
            }
        }
    }
}