namespace Team6JQDental.Models
{
    /// <summary>
    /// Service Model Class to be used in API
    /// </summary>
    public class Service
    {
        public int ServiceID { get; set; }

        public string ServiceName { get; set; }

        public decimal cost { get; set; }
    }
}