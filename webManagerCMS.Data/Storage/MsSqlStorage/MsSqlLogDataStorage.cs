using Microsoft.Extensions.Logging;
using S9.Core.Data.Models;
using S9.Core.Data.Storage;
using S9.Core.Data.Storage.MsSqlStorage;
using S9.Core.Extensions.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Storage.MsSqlStorage.Access;
using webManagerCMS.Data.Storage.MsSqlStorage.Base;

namespace webManagerCMS.Data.Storage.MsSqlStorage
{
	public class MsSqlLogDataStorage : MsSqlDataStorageBase, ILogDataStorage
	{
		public MsSqlLogDataStorage(MsSqlDataStorageAccess dataAccess, MsSqlDataStorageSettings settings) : base(dataAccess, settings)
		{ }

		public void InsertLog(LogLevel logLevel, string message)
		{
			int? idWWW;

			try
			{
				idWWW = this.IdWWW;
			}
			catch
			{
				//Exception can be thrown before tenant middleware
				idWWW = null;
			}

			try
			{
				using var cmd = this.NewCommandProc("dbo.WebManagerCMSLog_Insert");
				cmd.AddParam("@LogLevel", (int)logLevel);
				cmd.AddParam("@IDWWW", idWWW);
				cmd.AddParam("@Message", message);

				this.ExecNonQuery(cmd);
			}
			catch (Exception ex)
			{
				var filePath = this.DataStorageSettings.AlternativeLogFilePath;

				if (!string.IsNullOrWhiteSpace(filePath))
				{
					var msg = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}: Logging to database failed with exception: {ex}. Original data to log: LogLevel: {(int)logLevel}, IdWWW: {idWWW}, Message: {message}";

					using StreamWriter sw = File.AppendText(filePath);
					sw.WriteLine(msg);
				}
			}
		}

		public void InsertLoginLog(LoginLog loginLog)
		{

		}
	}
}
