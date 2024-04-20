using Microsoft.AspNetCore.Components;
using System.Xml;
using System.Xml.Linq;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContentNS.Plugins
{
	public class DocHtml : PageContentPlugin
	{
		private IDataStorageAccess _dataStorageAccess;

		public DocHtml(PageContentPlugin plugin) : base(plugin)
		{
			_dataStorageAccess = plugin.PluginParameters.dataStorageAccess;

			TemplateName = PageContentPluginType.DOC_HTML;
			InitData();
		}

		public int IdDocHTML { get; private set; }
		public string? XmlContent { get; private set; }

		private Dictionary<string, string> Values { get; set; }
		private Dictionary<int, FileAlias> Files { get; set; }

		private void InitData()
		{
			var data = _dataStorageAccess.WebContentDataStorage.GetDocHtmlData(IdPage, Id);
			if (data != null)
			{
				IdDocHTML = data.Id;
				XmlContent = data.XmlContent;

				ParseXml();
			}
		}

		private void ParseXml()
		{
			if (string.IsNullOrWhiteSpace(XmlContent))
				return;

			var values = new Dictionary<string, string>();
			var fileIdSet = new HashSet<int>();

			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(XmlContent);

			XmlNodeList itemElements = xmlDoc.GetElementsByTagName("item");

			foreach (XmlNode itemElement in itemElements)
			{
				var id = itemElement.Attributes?["id"]?.Value;

				if (!string.IsNullOrWhiteSpace(id))
				{
					var value = itemElement.InnerText;

					var isPage = itemElement.Attributes["isPage"]?.Value == "true";

					if (isPage && int.TryParse(value, out int idPage))
						values.Add(id, PluginParameters.pageTree.GetPageUrl(idPage));
					else
					{
						values.Add(id, value);

						var isFile = itemElement.Attributes["isFile"]?.Value == "true";

						if (isFile && int.TryParse(value, out int idFile))
							fileIdSet.Add(idFile);
					}
				}
			}

			if (fileIdSet.Count > 0)
				Files = _dataStorageAccess.WebContentDataStorage.LoadFileAliases(fileIdSet).ToDictionary(f => f.IdFile);

			this.Values = values;
		}

		public string GetItemValue(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return "";

			if (!Values.ContainsKey(name))
				return "";

			return Values[name];
		}

		public string GetItemValue(int number)
		{
			return GetItemValue("txt" + number);
		}

		public string GetItemValue(int i, int nameMultiplier, int nameAddNumber)
		{
			int number = ((i * nameMultiplier) + nameAddNumber);
			return GetItemValue(number);
		}

		public MarkupString GetItemValueWithHTML(string name)
		{
			return (MarkupString)GetItemValue(name);
		}

		public MarkupString GetItemValueWithHTML(int number)
		{
			return (MarkupString)GetItemValue(number);
		}

		public MarkupString GetItemValueWithHTML(int i, int nameMultiplier, int nameAddNumber)
		{
			return (MarkupString)GetItemValue(i, nameMultiplier, nameAddNumber);
		}

		public string GetFileItemValue(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return "";

			string value = GetItemValue(name).ToString();

			if (!int.TryParse(value, out int idFile))
				return "";

			if (!Files.ContainsKey(idFile))
				return "";

			return Files[idFile].Alias;
		}

		public int GetDynamicIntervalCount(string namePrefix, int nameMultiplier, int nameAddNumber)
		{
			int count = 1;
			int id = (count * nameMultiplier) + nameAddNumber;
			string name = namePrefix + id;

			while (Values.ContainsKey(name))
			{
				count++;
				id = (count * nameMultiplier) + nameAddNumber;
				name = namePrefix + id;
			}

			return count - 1;
		}
	}
}
