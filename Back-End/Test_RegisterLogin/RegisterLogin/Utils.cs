using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin
{
    public class Utils
    {
        public string convert2date(string date)
        {
            /*
             * convert yyyy-mm-dd to format oracle supports
             * @param date: yyyy-mm-dd
             * return: Oracle Date Format 
            */
            string[] dateArr = date.Split('-');
            string oracle_date = dateArr[2] + "-" + int.Parse(dateArr[1]).ToString() + "月-" + dateArr[0].Substring(2);

            return oracle_date;
        }

        public string calcID(string sum)
        {
            /* calculate id with current sum of ids */
            string id = (int.Parse(sum) + 1).ToString().PadLeft(10, '0');
            return id;
        }
    }
}
