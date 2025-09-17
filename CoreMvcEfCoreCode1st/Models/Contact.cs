namespace CoreMvcEfCoreCode1st.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string ContactType { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
