using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelerApp.Databases
{
    public class JewelerDatabase
    {
        private static TradeEntities _jewelerDatabase;

        public static TradeEntities GetContext()
        {
            if( _jewelerDatabase == null)
            {
                _jewelerDatabase = new TradeEntities();
            }
            return _jewelerDatabase;
        }
    }
}
