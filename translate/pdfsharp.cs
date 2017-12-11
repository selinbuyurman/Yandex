using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace translate
{
    class pdfsharp
    {

        public void createPDF(string fileName,string data)
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4, 20, 20, 42, 35);
            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));
            doc.Open();
            Paragraph p = new Paragraph(data);
            doc.AddAuthor("Kubra");
            doc.AddCreator("visual studio");
            doc.AddSubject("Pdf File");
            doc.AddTitle("Title");
            doc.Add(p);
            doc.Close();

        }
      
    }
}
