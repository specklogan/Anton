using Anton.core.utilities;
using OpenTK.Graphics.ES11;
using StbImageSharp;
using GL = OpenTK.Graphics.ES20.GL;
using PixelFormat = OpenTK.Graphics.ES20.PixelFormat;
using PixelInternalFormat = OpenTK.Graphics.ES20.PixelInternalFormat;
using PixelType = OpenTK.Graphics.ES20.PixelType;
using TextureTarget = OpenTK.Graphics.ES20.TextureTarget;
using TextureUnit = OpenTK.Graphics.ES20.TextureUnit;
using TextureParameterName = OpenTK.Graphics.ES20.TextureParameterName;

namespace Anton.render;

public class Texture2D
{

    private int handle;

    public Texture2D(string filePath)
    {
        handle = GL.GenTexture();
        ImageResult image = FileReader.loadImageData(filePath);
        Console.WriteLine("Loaded texture: " + handle);
        
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        
        GL.GenerateMipmap(TextureTarget.Texture2D);
        use();
    }

    public Texture2D(string filePath, bool repeat)
    {
        handle = GL.GenTexture();
        ImageResult image = FileReader.loadImageData(filePath);
        Console.WriteLine("Loaded texture: " + handle);
        
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);
        
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        
        if (repeat)
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        }
        
        GL.GenerateMipmap(TextureTarget.Texture2D);
        use();
    } 
    public void use()
    {
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);
    }
}