using System.Reflection;
using Anton.core.utilities;
using OpenTK.Graphics.OpenGL4;

namespace Anton.render.shader;

public class Shader
{
    private int shaderHandle;
    private bool destroyed = false;

    public int getID()
    {
        return shaderHandle;
    }
    
    public void use()
    {
        GL.UseProgram(shaderHandle);
    }

    protected virtual void destroy(bool destroying)
    {
        if (!destroyed)
        {
            GL.DeleteProgram(shaderHandle);
            destroyed = true;
        }
    }

    public void destroy()
    {
        destroy(true);
        GC.SuppressFinalize(this);
    }

    public static Shader loadShader(string vertexPath, string fragmentPath)
    {
        Shader shader = new Shader();
        return shader;
    }

    ~Shader()
    {
        if (destroyed == false)
        {
            Console.WriteLine("Shader destructed without being destroyed. Did you forget to use destroy()");
        }
    }

    public Shader()
    {
        int vertexShader, fragmentShader;
        string vertexSource = FileReader.loadShaderData("shader.vert");
        string fragmentSource = FileReader.loadShaderData("shader.frag");

        vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexSource);

        fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentSource);

        bool hasSucceeded = true;
        
        //Now compile the shaders now that we've loaded everything in
        GL.CompileShader(vertexShader);
        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            string failureReason = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine("Error loading vertex shader: " + failureReason);
            hasSucceeded = false;
        }
        
        GL.CompileShader(fragmentShader);
        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            string failureReason = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine("Error loading fragment shader: " + failureReason);
            hasSucceeded = false;
        }

        if (hasSucceeded == false)
        {
            Console.WriteLine("Error loading shader program, vertex/fragment shader not correctly loaded");
            return;
        }
        

        shaderHandle = GL.CreateProgram();
        GL.AttachShader(shaderHandle, vertexShader);
        GL.AttachShader(shaderHandle, fragmentShader);
        
        GL.LinkProgram(shaderHandle);
        
        GL.GetProgram(shaderHandle, GetProgramParameterName.LinkStatus, out success);
        if (success == 0)
        {
            string failureReason = GL.GetProgramInfoLog(shaderHandle);
            Console.WriteLine("Error loading shader program: " + failureReason);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            return;
        }
        Console.WriteLine("Successfully loaded shader program: " + shaderHandle);
        
        //If everything is set up correctly, all that is left to do is clean up the old shaders
        GL.DetachShader(shaderHandle, vertexShader);
        GL.DetachShader(shaderHandle, fragmentShader);
        
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }
}