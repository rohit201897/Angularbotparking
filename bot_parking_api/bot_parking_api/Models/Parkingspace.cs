namespace bot_parking_api.Models
{
    public class Parkingspace
    {

        public int pariking_id { get; set; }    
        public int org_id { get; set; }
        public int x_tlc { get; set; }
        public int y_tlc { get; set; }
        public int x_brc { get; set; }
        public int y_brc { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public bool isReserved { get; set; }
        public bool isAvailable { get; set; }
        public string reservedFor { get; set; } 
    }
}
