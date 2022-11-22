using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Maps.Resources
{
    class SemaforoImages
    {
        private static readonly string path = "..\\..\\Resources\\";

        // La direccion en la cual se encuentra mirando el semaforo se puede representar en index de una matriz 3 x 3
        // o se puede representar en un valor numerico del 0 al 8. (Uno para cada direccion sin contar el centro).
        public static Image UpdateSemaforo(Image baseImage, int i, int j, MapObjects.Intersection.Estado newEstado)
        {
            Bitmap imageBase = (Bitmap)baseImage;

            Bitmap imageUpdate = GetSemaforo(newEstado, MapObjects.Intersection.ConvertDirection(i, j));

            int semaforoSize = 16;

            int Xinicio = semaforoSize * j;
            int Yinicio = semaforoSize * i;

            int Xfinal = Xinicio + semaforoSize;
            int Yfinal = Yinicio + semaforoSize;

            for (int X = 0; Xinicio < Xfinal; Xinicio++, X++)
            {
                Yinicio = semaforoSize * i;
                for (int Y = 0; Yinicio < Yfinal; Yinicio++, Y++)
                {
                    imageBase.SetPixel(Xinicio, Yinicio, imageUpdate.GetPixel(X, Y));
                }
            }

            return imageBase;
        }

        public static Bitmap GetSemaforo(MapObjects.Intersection.Estado estado, int direction)
        {
            Bitmap image = null;

            switch (direction)
            {
                case 0:
                    image = GetSemaforoEsquina(estado);
                    break;
                case 1:
                    image = GetSemaforo(estado);
                    break;
                case 2:
                    image = GetSemaforoEsquina(estado);
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case 3:
                    image = GetSemaforo(estado);
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case 4:
                    image = GetSemaforo(estado);
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 5:
                    image = GetSemaforoEsquina(estado);
                    image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;
                case 6:
                    image = GetSemaforo(estado);
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 7:
                    image = GetSemaforoEsquina(estado);
                    image.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    break;
                default:
                    throw new Exception("Se intento obtener una imagen de semaforo en un direccion que no existe.");
            }

            return image;
        }

        private static Bitmap GetSemaforo(MapObjects.Intersection.Estado estado)
        {
            Image image = null;

            switch (estado)
            {
                case MapObjects.Intersection.Estado.NoDefinido:
                    image = NoDefinido;
                    break;
                case MapObjects.Intersection.Estado.Rojo:
                    image = Rojo;
                    break;
                case MapObjects.Intersection.Estado.Amarillo:
                    image = Amarillo;
                    break;
                case MapObjects.Intersection.Estado.Verde:
                    image = Verde;
                    break;
            }

            return (Bitmap)image;
        }

        private static Bitmap GetSemaforoEsquina(MapObjects.Intersection.Estado estado)
        {
            Image image = null;

            switch (estado)
            {
                case MapObjects.Intersection.Estado.NoDefinido:
                    image = NoDefinido;
                    break;
                case MapObjects.Intersection.Estado.Rojo:
                    image = RojoEsquina;
                    break;
                case MapObjects.Intersection.Estado.Amarillo:
                    image = AmarilloEsquina;
                    break;
                case MapObjects.Intersection.Estado.Verde:
                    image = VerdeEsquina;
                    break;
            }

            return (Bitmap)image;
        }


        public static Image Base
        {
            get
            {
                Bitmap baseImage = new Bitmap(48, 48);
                Image image = Image.FromFile(path + "base.png");
                return (Image)baseImage;
            }
        }
        public static Image Rojo
        {
            get
            {
                Image image = Image.FromFile(path + "rojo.png");
                return image;
            }
        }

        public static Image RojoEsquina
        {
            get
            {
                Image image = Image.FromFile(path + "rojo_esquina.png");
                return image;
            }
        }

        public static Image Amarillo
        {
            get
            {
                Image image = Image.FromFile(path + "amarillo.png");
                return image;
            }
        }

        public static Image AmarilloEsquina
        {
            get
            {
                Image image = Image.FromFile(path + "amarillo_esquina.png");
                return image;
            }
        }

        public static Image Verde
        {
            get
            {
                Image image = Image.FromFile(path + "verde.png");
                return image;
            }
        }

        public static Image VerdeEsquina
        {
            get
            {
                Image image = Image.FromFile(path + "verde_esquina.png");
                return image;
            }
        }

        public static Image NoDefinido
        {
            get
            {
                Image image = Image.FromFile(path + "gris.png");
                return image;
            }
        }

        // Imagenes grandes

        public static Image GetSemaforoGrande(MapObjects.Intersection.Estado estado)
        {
            switch(estado)
            {
                case MapObjects.Intersection.Estado.Verde:
                    return VerdeGrande;
                case MapObjects.Intersection.Estado.Amarillo:
                    return AmarilloGrande;
                case MapObjects.Intersection.Estado.Rojo:
                    return RojoGrande;
                case MapObjects.Intersection.Estado.NoDefinido:
                    return GrisGrande;
                default:
                    return CentroGrande;
            }
        }

        public static Image RojoGrande
        {
            get
            {
                Image image = Image.FromFile(path + "semaforos\\rojo.png");
                return image;
            }
        }
        public static Image AmarilloGrande
        {
            get
            {
                Image image = Image.FromFile(path + "semaforos\\amarillo.png");
                return image;
            }
        }
        public static Image VerdeGrande
        {
            get
            {
                Image image = Image.FromFile(path + "semaforos\\verde.png");
                return image;
            }
        }
        public static Image GrisGrande
        {
            get
            {
                Image image = Image.FromFile(path + "semaforos\\gris.png");
                return image;
            }
        }
        public static Image CentroGrande
        {
            get
            {
                Image image = Image.FromFile(path + "semaforos\\centro.png");
                return image;
            }
        }
    }
}
