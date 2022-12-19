namespace NET_Advanced02_Restoranas.Models.table
{
    internal class Table
    {
        public int TableNo { get; set; }

        public string Description { get; set; }

        public int Places { get; set; }

        public Table(int tableNo, string description, int places)
        {
            TableNo = tableNo;
            Description = description;
            Places = places;
        }
    }
}
