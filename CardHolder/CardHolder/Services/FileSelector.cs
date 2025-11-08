using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Avalonia.Logging;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CardHolder.Views;

namespace CardHolder.Services;

public static class FileSelector
    {
        public static async Task<Bitmap> SelectImage()
        {
            if (!Valide()) throw new ValidationException();

            var files = (await MainView.StorageProvider.OpenFilePickerAsync(
                new FilePickerOpenOptions() { FileTypeFilter = new[] { FilePickerFileTypes.ImageAll } }
            ));
            if (files.Count == 0)
            {
                Logger.ShowMessage("Вы не выбрали изображение!");
                return null;
            }
            var file = files[0];

            if (file == null) return null;

            return new Bitmap(await file.OpenReadAsync());
        }
        
        public static async Task<IStorageFile> SelectFile()
        {
            if (!Valide()) return null;

            var files = (await MainView.StorageProvider.OpenFilePickerAsync(
                new FilePickerOpenOptions() { FileTypeFilter = new[] { FilePickerFileTypes.All } }
            ));
            if (files.Count == 0)
            {
                Logger.ShowMessage("Вы не выбрали файл!");
                return null;
            }
            if (files[0] == null) return null;
            return files[0];
        }
        
        public static async Task SaveFile(string nameFile, string extensionFile, byte[] bytes)
        {
            if (!Valide()) throw new ValidationException();

            var fileSave = await MainView.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
            {
                DefaultExtension = extensionFile,
                SuggestedFileName = nameFile,
            });

            if (fileSave != null)
            {
                using (var stream = await fileSave.OpenWriteAsync())
                {
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                }
                Logger.ShowMessage($"Файл успешно сохранен: {fileSave.Path}");
            }
            else Logger.ShowMessage("Вы не выбрали путь!");
        }

        private static bool Valide()
        {
            if (MainView.StorageProvider == null
                || !MainView.StorageProvider.CanOpen) return false;
            return true;
        }

    }