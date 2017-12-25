using NugetPackage;
using System;

namespace NugetPackageConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const string jsonTracks = "[{ 'Artist': 'Вася1', 'Album': 'Вася1', 'Title': 'Вася1' }, { 'Artist': 'Вася2', 'Album': 'Вася2', 'Title': 'Вася2' }]";
            var tracks = MusicStorageJsonReader.ReadFromJson(jsonTracks);

            foreach(var track in tracks)
            {
                Console.WriteLine(track.Artist);
            }

            Console.ReadKey();
        }
    }
}
