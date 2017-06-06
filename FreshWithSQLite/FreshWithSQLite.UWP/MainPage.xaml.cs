using FreshMvvm;
using FreshWithSQLite.Core;

namespace FreshWithSQLite.UWP
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			this.InitializeComponent();

			var repository = new Repository(FileAccessHelper.GetLocalFilePath("contacts.db3"));
			FreshIOC.Container.Register(repository);

			LoadApplication(new FreshWithSQLite.App());
		}
	}
}
