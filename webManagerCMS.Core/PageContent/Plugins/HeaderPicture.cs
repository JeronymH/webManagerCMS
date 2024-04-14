using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContentNS.Plugins
{
	public class HeaderPicture : PageContentPlugin
	{
		private IDataStorageAccess _dataStorageAccess;

		public HeaderPicture(int idPage, int placeNumber, PageContentPluginParameters pluginParameters) : base(0)
		{
			PluginParameters = pluginParameters;
			_dataStorageAccess = PluginParameters.dataStorageAccess;

			TemplateName = PageContentPluginType.PICHEADER;
			TemplateNum = placeNumber;
			TemplateState = 0;

			IdPage = idPage;
			PlaceNumber = placeNumber;
		}

		public HeaderPicture(int idPage, int placeNumber, HeaderPictureSelectType selectType, int pictureNumber, bool randomOrder, PageContentPluginParameters pluginParameters) : this(idPage, placeNumber, pluginParameters)
		{
			InitData(PlaceNumber, selectType, pictureNumber, randomOrder);
		}

		public string? PictureFull { get; set; }
		public int PlaceNumber { get; private set; }
		public int ObjectCount { get; private set; }

		private void InitData(int placeNumber, HeaderPictureSelectType selectType, int pictureNumber, bool randomOrder)
		{
			var data = _dataStorageAccess.WebContentDataStorage.GetHeaderPicture(IdPage, placeNumber, selectType, pictureNumber, randomOrder);

			if (data != null)
			{
				Id = data.Id;
				IdPage = data.IdPage;
				Title = data.Title;
				Subtitle = data.Subtitle;
				Description = data.Description;
				Picture = data.Picture;
				PictureFull = data.PictureFull;
				ObjectCount = data.ObjectCount;
			}
		}

		public IEnumerable<HeaderPictureData> GetHeaderPictures(HeaderPictureSelectType selectType, bool randomOrder, int maxNumberOfItems)
		{
			return _dataStorageAccess.WebContentDataStorage.GetHeaderPictures(IdPage, PlaceNumber, selectType, randomOrder, maxNumberOfItems);
		}
	}
}
