using System;
using System.Collections.Generic;

namespace Dependencies
{
    class Program
    {
        static void Main(string[] args)
        {
            string procedureName = "dbo.calcUpdate_TotalTax";
            StoredProcedure sp = new StoredProcedure(procedureName);
            sp.LookForDependencies();

            List<StoredProcedure> procedures = sp.Procedures;

            PrintProcedureName(procedureName, procedures);

            PrintTables(sp.Dependencies);

            Console.Read();
        }

        private static void PrintTables(List<string> tables)
        {
            foreach (var table in tables)
            {
                Console.WriteLine(table);
            }
        }

        private static void PrintProcedureName(string procedureName, List<StoredProcedure> procedures)
        {
            foreach (var item in procedures)
            {
                Console.WriteLine(procedureName + "->" + item.Name);

                PrintProcedureName(item.Name, item.Procedures);
            }
        }
    }
}
