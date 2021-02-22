using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
//
namespace HelloApp
{
  class Person
  {
    public string Name { get; set; }
    public int Age { get; set; }
  }
  class Program
  {
    
    static void Main(string[] args)
    {
      Console.WriteLine("Выберите пункт");
      string selection = Console.ReadLine();
      switch (selection)
      {
        case "1":
          DriveInfo[] drives = DriveInfo.GetDrives();

          foreach (DriveInfo drive in drives)
          {
            Console.WriteLine($"Название: {drive.Name}");
            Console.WriteLine($"Тип: {drive.DriveType}");
            if (drive.IsReady)
            {
              Console.WriteLine($"Объем диска: {drive.TotalSize}");
              Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
              Console.WriteLine($"Метка: {drive.VolumeLabel}");
            }
            Console.WriteLine();
          }
          break;
        case "2":
          // создаем каталог для файла
          string path = @"C:\SomeDir2";
          DirectoryInfo dirInfo = new DirectoryInfo(path);
          if (!dirInfo.Exists)
          {
            dirInfo.Create();
          }
          Console.WriteLine("Введите строку для записи в файл:");
          string text = Console.ReadLine();

          // запись в файл
          using (FileStream fstream = new FileStream($@"{path}\note.txt", FileMode.OpenOrCreate))
          {
            // преобразуем строку в байты
            byte[] array = System.Text.Encoding.Default.GetBytes(text);
            // запись массива байтов в файл
            fstream.Write(array, 0, array.Length);
            Console.WriteLine("Текст записан в файл");
          }

          // чтение из файла
          using (FileStream fstream = File.OpenRead($@"{path}\note.txt"))
          {
            // преобразуем строку в байты
            byte[] array = new byte[fstream.Length];
            // считываем данные
            fstream.Read(array, 0, array.Length);
            // декодируем байты в строку
            string textFromFile = System.Text.Encoding.Default.GetString(array);
            Console.WriteLine($"Текст из файла: {textFromFile}");
          }
          // удаление файла
          string path1 = @"C:\SomeDir2\note.txt";
          FileInfo fileInf = new FileInfo(path1);
          if (fileInf.Exists)
          {
            File.Delete(path1);
          }
          break;
        case "3":


          static async Task Main(string[] args)
          {

            // сохранение данных
            using (FileStream fs = new FileStream(@"C:\SomeDir2\user.json", FileMode.OpenOrCreate))
            {
              Person tom = new Person() { Name = "Tom", Age = 35 };
              await JsonSerializer.SerializeAsync<Person>(fs, tom);
              Console.WriteLine("Data has been saved to file");
            }

            // чтение данных
            using (FileStream fs = new FileStream(@"C:\SomeDir2\user.json", FileMode.OpenOrCreate))
            {
              Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
              Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
            }

          }

          break;
        default:
          break;
      }
    }
  }
}
