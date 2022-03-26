using System.Collections.Generic;

namespace Bakery.Models
{
	public class FlavorTreatViewModel
	{
		public IList<Flavor> Flavors { get; set; }
		public IList<Treat> Treats { get; set; }
	}
}