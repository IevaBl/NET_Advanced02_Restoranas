using NET_Advanced02_Restoranas.Models.table;
using System.Numerics;


namespace NET_Advanced02_Restoranas.Repositories
{
    internal class TablesRepository
    {
        List<Table> Tables { get; set; }

        public TablesRepository()
        {
            Tables = new List<Table>();

            Tables.Add(new Table(1, "Door table", 2));
            Tables.Add(new Table(2, "Window table", 2));
            Tables.Add(new Table(3, "Round table", 8));
            Tables.Add(new Table(4, "Side table", 4));
            Tables.Add(new Table(5, "Middle table", 5));
        }


        internal List<Table> GetTables()
        {
            return Tables;
        }
        internal Table FindTable(int no)
        {
            return Tables.Find(t => t.TableNo == no);
        }

    }
}
