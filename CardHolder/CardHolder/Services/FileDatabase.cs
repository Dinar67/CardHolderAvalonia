using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CardHolder.Interfaces;

namespace CardHolder.Services;

public class FileDatabase : IDatabase
{
    private List<Type> _types = new List<Type>() { typeof(Card) };
    private Dictionary<Type, object> _data = new Dictionary<Type, object>();
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions() { WriteIndented = true };

    public FileDatabase()
    {
        LoadData();
    }

    private string GetPath(string nameClass) =>
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{nameClass}.json");

    private void LoadData()
    {
        foreach (var type in _types)
        {
            var listType = typeof(List<>).MakeGenericType(type);
            var path = GetPath(type.Name);
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var listObject = JsonSerializer.Deserialize(json, listType, _options);
                _data.Add(type, listObject!);
            }
            else _data.Add(type, Activator.CreateInstance(listType)!);
        }
    }

    public List<T> Get<T>()
    {
        if (!_types.Contains(typeof(T))) throw new Exception("That type is not registered in database!");
        var type = typeof(T);
        if (!_data.ContainsKey(type))
            _data.Add(type, new List<T>());
        return (List<T>)_data[type];
    }

    public void Add<T>(T element)
    {
        var type = typeof(T);
        Get<T>().Add(element);
    }

    public void Edit<T>(T element)
    {
        var elements = Get<T>();
        var index = elements.IndexOf(element);
        if (index != -1) elements[index] = element;
    }

    public void Delete<T>(T element)
    {
        Get<T>().Remove(element);
    }

    public void SaveChanges()
    {
        foreach (var data in _data)
        {
            var listType = typeof(List<>).MakeGenericType(data.Key);
            var path = GetPath(data.Key.Name);
            var json = JsonSerializer.Serialize(data.Value, listType, _options);
            File.WriteAllText(path, json);
        }
    }

    public async Task Export<T>()
    {
        if (!_types.Contains(typeof(T))) throw new Exception("That type is not registered in database!");
        var type = typeof(T);
        var path = GetPath(type.Name);
        if (File.Exists(path))
            FileSelector.SaveFile($"{type.Name}.json", ".json", File.ReadAllBytes(path));
        else throw new Exception("Нет зарегистрированных элементов в таблице!");
    }
    public async Task Improt<T>()
    {
        if (!_types.Contains(typeof(T))) throw new Exception("That type is not registered in database!");
        var type = typeof(T);
        var path = GetPath(type.Name + "Import");
        var file = await FileSelector.SelectFile();
        if (file == null) throw new Exception("Файл для импорта не был выбран!");
        var ms = new MemoryStream();
        await (await file.OpenReadAsync()).CopyToAsync(ms);
        var bytes = ms.ToArray();
        await File.WriteAllBytesAsync(path, bytes);
        var listType = typeof(List<>).MakeGenericType(type);
        var json = await File.ReadAllTextAsync(path);
        var listObject = (List<T>)JsonSerializer.Deserialize(json, listType, _options);
        var cards = Get<T>();
        foreach (var card in listObject)
            cards.Add(card);
        App.db.SaveChanges();
    }
}