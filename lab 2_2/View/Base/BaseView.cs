using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.View.Base
{
    public class BaseView
    {

        public string GetWhereCondition()
        {
            string [] fieldsToFind = new string[100];
            string [] valuesToFind = new string[100];

            int actualSize = 0;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Field to find");
                fieldsToFind[i] = Console.ReadLine();
                Console.WriteLine("Value to find");
                valuesToFind[i] = Console.ReadLine();
                Console.WriteLine("Enter 1 to add criteria");
                actualSize++;

                int choose = 0;
                bool correct = Int32.TryParse(Console.ReadLine(), out choose);
                if (correct = false || choose != 1)
                {
                    break;
                }
            }

            string whereCondition = " where ";

            int parseInt;
            if (Int32.TryParse(valuesToFind[0], out parseInt) == false)
            {
                valuesToFind[0] = "'" + valuesToFind[0] + "'";
            }
            whereCondition += fieldsToFind[0] + " = " + valuesToFind[0];

            for (int i = 1; i < actualSize; i++)
            {
                if (Int32.TryParse(valuesToFind[i], out parseInt) == false)
                {
                    valuesToFind[i] = "'" + valuesToFind[i] + "'";
                }
                whereCondition += " and " + fieldsToFind[i] + " = " + valuesToFind[i];
            }

            return whereCondition;
        }

       
    }
}
