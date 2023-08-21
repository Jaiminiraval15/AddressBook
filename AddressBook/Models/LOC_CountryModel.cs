namespace AddressBook.Models
{
    public class LOC_CountryModel
    {
        public int? COUNTRYID  { get; set; }
        public string COUNTRYNAME { get; set; }
        = string.Empty;
        public string COUNTRYCODE { get; set; }
        = string.Empty;
        public DateTime CREATED { get; set; }
        public DateTime MODIFIED{ get; set; }


    }
}
