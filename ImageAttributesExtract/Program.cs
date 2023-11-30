﻿using System;
using System.Drawing;
using System.IO;

namespace ExtrairInformacoesImagem
{
    class Program
    {
        static void Main()
        {
            string diretorioImagens = @"C:\Imagens";

            if (Directory.Exists(diretorioImagens))
            {
                Bitmap imagem = new Bitmap(diretorioImagens);

                Color corCabelo = IdentificarCorCabelo(imagem);
                Color corOlhos = IdentificarCorOlhos(imagem);

                Console.WriteLine($"Cor do cabelo: {corCabelo}");
                Console.WriteLine($"Cor dos olhos: {corOlhos}");
            }
            else
            {
                Console.WriteLine("O diretório de imagens especificado não existe.");
            }

            Console.WriteLine("Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }

        static Color IdentificarCorCabelo(Bitmap imagem)
        {
            int alturaRegiaoCabelo = imagem.Height / 4;

            Rectangle retanguloRegiaoCabelo = new Rectangle(0, 0, imagem.Width, alturaRegiaoCabelo);
            Bitmap regiaoCabelo = imagem.Clone(retanguloRegiaoCabelo, imagem.PixelFormat);

            Color corPredominanteCabelo = EncontrarCorPredominante(regiaoCabelo);

            return corPredominanteCabelo;
        }

        static Color IdentificarCorOlhos(Bitmap imagem)
        {
            int xInicioOlhos = imagem.Width / 4;
            int yInicioOlhos = imagem.Height / 4;
            int larguraRegiaoOlhos = imagem.Width / 2;
            int alturaRegiaoOlhos = imagem.Height / 4;

            Rectangle retanguloRegiaoOlhos = new Rectangle(xInicioOlhos, yInicioOlhos, larguraRegiaoOlhos, alturaRegiaoOlhos);
            Bitmap regiaoOlhos = imagem.Clone(retanguloRegiaoOlhos, imagem.PixelFormat);

            Color corPredominanteOlhos = EncontrarCorPredominante(regiaoOlhos);

            return corPredominanteOlhos;
        }

        static Color EncontrarCorPredominante(Bitmap imagem)
        {

            int totalRed = 0, totalGreen = 0, totalBlue = 0;
            int totalPixels = 0;

            for (int x = 0; x < imagem.Width; x++)
            {
                for (int y = 0; y < imagem.Height; y++)
                {
                    Color pixelColor = imagem.GetPixel(x, y);
                    totalRed += pixelColor.R;
                    totalGreen += pixelColor.G;
                    totalBlue += pixelColor.B;
                    totalPixels++;
                }
            }

            int mediaRed = totalRed / totalPixels;
            int mediaGreen = totalGreen / totalPixels;
            int mediaBlue = totalBlue / totalPixels;

            return Color.FromArgb(mediaRed, mediaGreen, mediaBlue);
        }
    }
}