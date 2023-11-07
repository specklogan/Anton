using Anton.render.shader;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using StbImageSharp;

namespace Anton.render;

public class Renderer
{

    private static int VBO;
    private static int VAO;
    private static Shader shader;
    private static Texture2D texture;

    private static readonly float[] vertices =
    {
       -0.5f, -0.5f, 0.0f, 0.0f, 0.0f,
       0.5f, -0.5f, 0.0f, 1.0f, 0.0f,
       0.0f, 0.5f, 0.0f, 0.5f, 1.0f,
    };
    
    public static void load()
    {
        VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        StbImage.stbi_set_flip_vertically_on_load(1);

        texture = new Texture2D("brick_block.png", true);
        
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        
        shader = Shader.loadShader("shader.vert", "shader.frag");
        shader.use();

        VAO = GL.GenVertexArray();
        GL.BindVertexArray(VAO);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        
        int texCoordLocation = GL.GetAttribLocation(shader.getID(), "aTexCoord");
        GL.EnableVertexAttribArray(texCoordLocation);
        GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(0);
    }

    public static void unload()
    {
        Console.WriteLine("Closing Anton...");
        GL.BindBuffer(BufferTarget.ArrayBuffer,0);
        GL.DeleteBuffer(VBO);
    }

    private static double last;
    private static bool ascending = true;
    
    public static void onRender(double delta)
    {
        last += 0.01;
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        GL.BindVertexArray(VAO);
        
        texture.use();
        shader.use();

        int loc = OpenTK.Graphics.ES20.GL.GetUniformLocation(shader.getID(), "transform");
        Matrix4 rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians((int)last));
        GL.UniformMatrix4(loc, true, ref rotation);
        
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }

}