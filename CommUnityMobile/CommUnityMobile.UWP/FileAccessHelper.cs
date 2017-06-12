namespace FreshWithSQLite.UWP
{
	public class FileAccessHelper
	{
		public static string GetLocalFilePath(string filename)
		{
			// For UWP, we store the database file in our application data's local folder.
			var path = global::Windows.Storage.ApplicationData.Current.LocalFolder.Path;
			return System.IO.Path.Combine(path, filename);
		}
	}
}
