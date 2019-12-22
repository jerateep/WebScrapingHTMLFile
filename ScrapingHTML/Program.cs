using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScrapingHTML
{
    class Program
    {
        static void Main(string[] args)
        {
            string strPath = "../../../../../../_TCB_2019_5560007789_TAX_P71.htm";
            GetHtml_5560(strPath);

        }

        private static void GetHtml_5560(string strPath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var numberOfEncoding = Encoding.GetEncodings().Length;
            var encoding = System.Text.Encoding.GetEncoding(874);
            string strHtml = File.ReadAllText(strPath, encoding);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(strHtml);
            #region strBranchNo
            string strBranchNo = htmlDocument.DocumentNode.Descendants("nobr")
                                .Where(node => node.GetAttributeValue("id","")
                                .Equals("l0007002"))
                                .Select(j => j.InnerText)
                                .FirstOrDefault().Replace("&nbsp;",null);
            #endregion
            #region strCustumerID
            string strCustumerID = htmlDocument.DocumentNode.Descendants("nobr")
                      .Where(node => node.GetAttributeValue("id", "")
                      .Equals("l0009002"))
                      .Select(j => j.InnerText)
                      .FirstOrDefault().Replace("X",null);
            strCustumerID = strCustumerID.Split(',').FirstOrDefault().Replace("&nbsp;",null);
            #endregion
            #region strCustumerName
            string strCustumerName = htmlDocument.DocumentNode.Descendants("nobr")
                    .Where(node => node.GetAttributeValue("id", "")
                    .Equals("l0011002"))
                    .Select(j => j.InnerText)
                    .FirstOrDefault().Replace("&nbsp;&nbsp;", null);
            strCustumerName = strCustumerName.Replace("&nbsp;", " ");
            #endregion
            #region AddressLine_1, DocID, DocDate
            string strAddressLine_1, strDocID, strDocDate;
            strAddressLine_1 = htmlDocument.DocumentNode.Descendants("nobr")
                    .Where(node => node.GetAttributeValue("id", "")
                    .Equals("l0012002"))
                    .Select(j => j.InnerText)
                    .FirstOrDefault().Replace("&nbsp;&nbsp;", ",");
           
            List<string> lsAddressLine_1 = strAddressLine_1.Replace("&nbsp;", " ").Split(',').Where(k => !string.IsNullOrEmpty(k)).ToList();
            strAddressLine_1 = lsAddressLine_1[0];
            strDocID = lsAddressLine_1[1];
            strDocDate = lsAddressLine_1[2];
            #endregion
            #region AddressLine_2
            string strAddressLine_2 = htmlDocument.DocumentNode.Descendants("nobr")
                    .Where(node => node.GetAttributeValue("id", "")
                    .Equals("l0013002"))
                    .Select(j => j.InnerText)
                    .FirstOrDefault().Replace("&nbsp;&nbsp;", ",");
            strAddressLine_2 = strAddressLine_2.Replace(",", null).Replace("&nbsp;", " ");
            #endregion
            #region AddressLine_3
            string strAddressLine_3 = htmlDocument.DocumentNode.Descendants("nobr")
                    .Where(node => node.GetAttributeValue("id", "")
                    .Equals("l0014002"))
                    .Select(j => j.InnerText)
                    .FirstOrDefault().Replace("&nbsp;&nbsp;", ",");
            List<string> LsAddressLine_3 = strAddressLine_3.Replace("&nbsp;"," ").Split(",").Where(k => !string.IsNullOrEmpty(k)).ToList();
            strAddressLine_3 = LsAddressLine_3[0];
            string strDocDate_2 = LsAddressLine_3[1];
            #endregion
            #region Tax
            //*[@id="l0012002"]
            string strTax = htmlDocument.DocumentNode.Descendants("nobr")
                    .Where(node => node.GetAttributeValue("id", "")
                    .Equals("l0015002"))
                    .Select(j => j.InnerText)                    
                    .FirstOrDefault().Replace("&nbsp;&nbsp;", ",");
            strTax = strTax.Replace("&nbsp;", " ").Replace(",", null);
            #endregion

            Console.WriteLine("Branch No: "+ strBranchNo);
            Console.WriteLine("Custumer ID: " + strCustumerID);
            Console.WriteLine("Custumer Name: " + strCustumerName); 
            Console.WriteLine("AddressLine_1 " + strAddressLine_1);
            Console.WriteLine("AddressLine_2 " + strAddressLine_2);
            Console.WriteLine("AddressLine_3 " + strAddressLine_3);
            Console.WriteLine("DOC ID: " + strDocID);
            Console.WriteLine("DOC DATE: " + strDocDate);
            Console.WriteLine("DOC DATE 2: " + strDocDate_2);
            Console.WriteLine("Tax ID: " + strTax);
        }
    }
}
