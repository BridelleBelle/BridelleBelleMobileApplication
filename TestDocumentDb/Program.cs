using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BridalBelle.Database;

namespace TestDocumentDb
{
	class Program
	{
		static void Main(string[] args)
		{
			var documentDb = new DocumentDBClient();
			documentDb.DoStuff();
			Console.ReadKey();
		}
	}
}
