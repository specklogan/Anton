
using Anton.render;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anton.window;

public class AntonWindow : GameWindow
{

    public AntonWindow(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings()
    {
        Size = (width, height),
        Title = title,
        API = ContextAPI.OpenGL,
        IsEventDriven = false,
        AutoLoadBindings = true
    }){}

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        Renderer.load();
        GL.ClearColor(0.431f,0.694f,1f,1f);
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        Renderer.unload();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        
        Renderer.onRender(args.Time);
        
        SwapBuffers();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        
        GL.Viewport(0,0,e.Width,e.Height);
    }

}