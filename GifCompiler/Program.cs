using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using AnimatedGif;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the path to the folder containing the images (THEY MUST BE \".png\"):");
        string folderPath = Console.ReadLine();

        string[] imagePaths = Directory.GetFiles(folderPath, "*.png");

        Console.WriteLine("Enter the name of your animation:");
        string gifName = Console.ReadLine();

        Console.WriteLine("Enter the frame rate (fps):");
        int fps = int.Parse(Console.ReadLine());

        int frameRate = 1000 / fps;

        Console.WriteLine("Enter the export location:");
        string exportLocation = Console.ReadLine();

        string gifFilePath = Path.Combine(exportLocation, $"{gifName}.gif");

        using (var gif = AnimatedGif.AnimatedGif.Create(gifFilePath, frameRate))
        {
            foreach (string imagePath in imagePaths)
            {
                using (var img = System.Drawing.Image.FromFile(imagePath))
                {
                    gif.AddFrame(img, delay: -1, quality: GifQuality.Bit8);
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine("GIF creation completed.");
        Console.WriteLine();
        Process.Start(new ProcessStartInfo { FileName = gifFilePath, UseShellExecute = true });

    }
}
