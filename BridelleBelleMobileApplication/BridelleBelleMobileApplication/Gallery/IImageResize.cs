using System;

namespace BridelleBelleMobileApplication.Gallery
{
    public interface IImageResize
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
