using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Pickers;
using Newtonsoft.Json;

namespace WarUp.Utils.File
{
	public static class SaveLoadGame
	{
		private static JsonSerializerSettings Setting;

		static SaveLoadGame()
		{
			Setting = new JsonSerializerSettings();
			Setting.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
			Setting.TypeNameHandling = TypeNameHandling.All;
			Setting.MaxDepth = int.MaxValue;
		}

		public static async Task<bool> Save(object @object, Type type)
		{
			StorageFile file = null;

			FileSavePicker picker = new FileSavePicker();
			picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
			picker.FileTypeChoices.Add("Binary file", new List<string> { ".bin" });
			picker.SuggestedFileName = "NewGame";

			try
			{
				file = await picker.PickSaveFileAsync();
			}
			catch (Exception)
			{
				return false;
			}
			
			var formatter = new BinaryFormatter();

			try
			{
				using (var stream = await file.OpenStreamForWriteAsync())
				{
					formatter.Serialize(stream, @object);
				}
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		public static async Task<T> Load<T>()
		{
			var picker = new FileOpenPicker();
			picker.ViewMode = PickerViewMode.Thumbnail;
			picker.SuggestedStartLocation = PickerLocationId.Desktop;
			picker.FileTypeFilter.Add(".bin");

			StorageFile file;

			try
			{
				file = await picker.PickSingleFileAsync();
				if (file == null)
					return default(T);
			}
			catch (Exception)
			{

				return default(T);
			}

			try
			{
				using (var stream = await file.OpenStreamForReadAsync())
				{
					var binaryFormatter = new BinaryFormatter();
					var res = (T)(binaryFormatter.Deserialize(stream));
					return res;
				}
			}
			catch (Exception)
			{
				return default(T);
			}
		}
	}
}
