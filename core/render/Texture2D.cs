using Anton.core.utilities;
using OpenTK.Graphics.ES20;
using StbImageSharp;

namespace Anton.render;

public class Texture2D
{

    private int handle;

    public Texture2D(string filePath)
    {
        handle = GL.GenTexture();
        ImageResult image = FileReader.loadImageData(filePath);
        
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        use();
    }

    public void use()
    {
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);
    }
}