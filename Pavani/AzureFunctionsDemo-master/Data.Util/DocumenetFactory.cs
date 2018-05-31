using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Util
{
    public class DocumenetFactory
    {
        public static Document GetDocument(string extension, string filePath, string fileContent, string delimiter)
        {
            Document Doc;
            switch (extension.ToUpperInvariant())
            {
                case "PDF":
                    Doc = new PDFHelper(filePath);
                    break;

                case "XML":
                    Doc = new XMLHelper(filePath);
                    break;

                case "CSV":
                    Doc = new CSVHelper(fileContent, delimiter);
                    break;

                case "PSV":
                    Doc = null; //Doc = new CSVHelper(fileContent);
                    break;

                default:
                    Doc = null;
                    break;
            }
            return Doc;
        }
    }
}