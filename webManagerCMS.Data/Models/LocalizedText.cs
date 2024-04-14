namespace webManagerCMS.Data.Models
{
	public class LocalizedText
	{
		public int IdLanguage { get; set; }
		//public int IdCategory { get; set; }
		public string? Code { get; set; }
		public string? Text { get; set; }

		public string Identifier
		{
			get
			{
				//return GetIdentifier(this.IdLanguage, this.IdCategory, this.Code);
				return GetIdentifier(this.IdLanguage, this.Code);
			}
		}

		//public static string GetIdentifier(int idLanguage, int idCategory, string? Code)
		//{
		//	return $"{idLanguage}-{idCategory}-{Code}";
		//}

		public static string GetIdentifier(int idLanguage, string? Code)
		{
			return $"{idLanguage}-{Code}";
		}
	}
}
