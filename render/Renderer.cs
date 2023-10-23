using Anton.render.shader;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Anton.render;

public class Renderer
{

    private static int VBO;
    private static int VAO;
    private static Shader shader;

    private static readonly float[] vertices =
    {
       -0.5f, -0.5f, 0.0f,
       0.5f, -0.5f, 0.0f,
       0.0f, 0.5f, 0.0f,
    };
    
    public static void load()
    {
        VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        
        shader = Shader.loadShader("shader.vert", "shader.frag");
        shader.use();

        VAO = GL.GenVertexArray();
        GL.BindVertexArray(VAO);
        
        GL.VertexAttribPointer(0,3,VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
    }

    public static void unload()
    {
        Console.WriteLine("Closing Anton...");
        GL.BindBuffer(BufferTarget.ArrayBuffer,0);
        GL.DeleteBuffer(VBO);
    }

    private static int last;
    private static bool ascending = true;
    public static void onRender(double delta)
    {
        if (ascending)
        {
            last += 1;
        }
        if (last > 10000)
        {
            ascending = false;
            last = 10000;
        }
        else if (!ascending)
        {
            last -= 1;
            if (last <= 0)
            {
                ascending = true;
                last = 0;
            }
        }
        GL.Clear(ClearBufferMask.ColorBufferBit);
        int loc = GL.GetUniformLocation(shader.getID(), "inColor");
        GL.Uniform3(loc, new Vector3((float)Math.Sin((2 * Math.PI * (last/10000f))/10), 0, 0));
        shader.use();
        
        GL.BindVertexArray(VAO);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }

}