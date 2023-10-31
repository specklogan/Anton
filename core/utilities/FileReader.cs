using System.Reflection;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using StbImageSharp;
using OpenTK.Graphics.OpenGL4;

namespace Anton.core.utilities;

public class FileReader
{

    public static string loadData(string path)
    {
        string loc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string output = Path.Combine(loc, @path);
        if (output == string.Empty)
        {
            Console.WriteLine("Error loading: " + path + ", file output was null!");
        }
        
        return File.ReadAllText(output);
    }

    public static string loadShaderData(string shader)
    {
        return loadData("core/render/shader/shaders/" + shader);
    }

    public static ImageResult loadImageData(string path)
    {
        string nPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            @"core/render/textures/" + path);
        ImageResult image = ImageResult.FromStream(File.OpenRead(nPath), ColorComponents.RedGreenBlueAlpha);

        return image;
    }
}