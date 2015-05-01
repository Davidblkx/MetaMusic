using System;

namespace MetaMusic.API.Common
{
    public class ImageSize
    {
        public ImageSize(int width, int height)
        {
            Height = height;
            Width = width;
        }

        public ImageSize(ImageInfoSizes size)
        {
            switch (size)
            {
                case ImageInfoSizes.Small:
                    Width = 30;
                    Height = 30;
                    break;
                case ImageInfoSizes.Medium:
                    Width = 64;
                    Height = 64;
                    break;
                case ImageInfoSizes.Large:
                    Width = 126;
                    Height = 126;
                    break;
                case ImageInfoSizes.ExtraLarge:
                    Width = 252;
                    Height = 252;
                    break;
                case ImageInfoSizes.Mega:
                    Width = 500;
                    Height = 500;
                    break;
                default:
                    Width = -1;
                    Height = -1;
                    break;
            }
        }

        public bool IsKnown
        {
            get { return Height > 0 && Width > 0; }
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}