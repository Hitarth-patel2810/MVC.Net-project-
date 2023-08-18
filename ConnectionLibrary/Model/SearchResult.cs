
using ConnectionLibrary.Model;
using project7thSem.Model;

namespace project7thSem.Models
{
    public class SearchResult
    {
        public List<DataList> TenderDetails { get; set; }
        public List<Tender_Filed> tenderFields { get; set;}
        public List<header_modal> searchFields { get; set; }
    }
}
