using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Book
    {
        public string Accession { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string DatePublish { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public int PageNumber { get; set; }

        public Book(string accession, string title, string author, string publisher, string language, string datePublish, string subject, string type, string department, string location, string status, int quantity, int pageNumber)
        {
            Accession = accession;
            Title = title;
            Author = author;
            Publisher = publisher;
            Language = language;
            DatePublish = datePublish;
            Subject = subject;
            Type = type;
            Department = department;
            Location = location;
            Status = status;
            Quantity = quantity;
            PageNumber = pageNumber;
        }    
        public Book(string accession, string status)
        {
            Accession = accession;
            Status = status;
        }
    }
}
