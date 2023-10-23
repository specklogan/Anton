// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Anton.render.shader;
using Anton.window;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anton;

public class AntonMain
{
    public static void Main(string[] args)
    {
        AntonWindow window = new AntonWindow(1920,1080, "Anton Engine");
        window.Run();
    }
}