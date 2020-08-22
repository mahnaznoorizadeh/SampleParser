using System;

namespace SampleParser.ViewModel
{
    public class BaseViewModel
    {

        public int Id { get; set; }
        public DateTime InsertDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public string Comment { get; set; }
    }
}