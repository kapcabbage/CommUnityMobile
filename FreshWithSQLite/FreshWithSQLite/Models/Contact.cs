using SQLite;
using System;

namespace FreshWithSQLite.Models
{
	/// <summary>
	/// This class uses attributes that SQLite.Net can recognize
	/// and use to create the table schema.
	/// </summary>
	[Table(nameof(Contact))]
	public class Contact
	{
		[PrimaryKey, AutoIncrement]
		public int? Id { get; set; }

		[NotNull, MaxLength(250)]
		public string Name { get; set; }

		[MaxLength(250)]
		public string Email { get; set; }

		public bool IsValid()
		{
			return (!String.IsNullOrWhiteSpace(Name));
		}
	}
}

