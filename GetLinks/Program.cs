using GetLinks.Classes;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ParserHTML
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Введите Url сайта, чтобы увидеть ссылки");
            string Url = Console.ReadLine();
            Worker worker = new Worker();
            List<string> Links;

            try
            {
                worker.SetUrl(Url);
            }
            catch (Exception)
            {
                Console.WriteLine("Был введен неправильный Url");
                Console.ReadKey();

                return;
            }

            Links = worker.GetLinks();

            Console.WriteLine("Список ссылок:");

            for (int i = 0; i < 20 && i < Links.Count; i++)
            {
                Console.WriteLine(Links[i]);
            }

            Console.WriteLine("Сохранить список ссылок в файл? (Y/N)");
            bool result = Console.ReadLine() == "Y" ? true : false;

            if (!result)
            {
                return;
            }

            Console.WriteLine("Введите имя файла");
            string nameFile = Console.ReadLine();

            Console.WriteLine("Введите каталог для сохранения");
            string path = Console.ReadLine();

            XDocument xmlDoc = CreateXMLDocument(Url, Links);

            try
            {
                string fullPath = path + @"\" + nameFile + ".xml";
                xmlDoc.Save(fullPath);
                Console.WriteLine("Файл успешно сохранен в " + fullPath);
            }
            catch (Exception)
            {
                Console.WriteLine("Не удалось сохранить файл");
            }
            Console.ReadKey();
        }

        private static XDocument CreateXMLDocument(string Url, List<string> Links)
        {
            var xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Website",
                                                                        new XAttribute("Url", Url)));

            foreach (string Link in Links)
            {
                xmlDoc.Root.Add(new XElement("Link",
                                    new XElement("Url",
                                        new XAttribute("value", Link))));
            }

            return xmlDoc;
        }
    }
}
