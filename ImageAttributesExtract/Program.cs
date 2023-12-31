﻿using System;
using System.Drawing;
using System.IO;

namespace ExtrairInformacoesImagem
{
    class Program
    {
        static void Main()
        {
            string diretorioImagens = @"E:\Imagens";
            string urlImagem = @"E:\Imagens\teste.JPG";

            if (Directory.Exists(diretorioImagens))
            {
                Bitmap imagem = new Bitmap(urlImagem);

                Color corCabelo = IdentificarCorCabelo(imagem);
                Color corOlhos = IdentificarCorOlhos(imagem);
                Color corRoupa = IdentificarCorRoupa(imagem);

                string cabelo = IdentificarNomeCor(corCabelo);
                string olhos = IdentificarNomeCor(corOlhos);
                string roupa = IdentificarNomeCor(corRoupa);

                Console.WriteLine($"Cor do cabelo: {cabelo}");
                Console.WriteLine($"Cor dos olhos: {olhos}");
                Console.WriteLine($"Cor da roupa: {roupa}");
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

        static Color IdentificarCorRoupa(Bitmap imagem)
        {

            int yInicioRoupa = imagem.Height / 4;
            int alturaRegiaoRoupa = imagem.Height - yInicioRoupa;

            // Extrair a região que provavelmente contém as roupas
            Rectangle retanguloRegiaoRoupa = new Rectangle(0, yInicioRoupa, imagem.Width, alturaRegiaoRoupa);
            Bitmap regiaoRoupa = imagem.Clone(retanguloRegiaoRoupa, imagem.PixelFormat);

            // Encontrar a cor predominante na região das roupas
            Color corPredominanteRoupa = EncontrarCorPredominante(regiaoRoupa);

            return corPredominanteRoupa;
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

        static string IdentificarNomeCor(Color cor)
        {
            #region Dicionario de cores
            // Mapeamento de cores predefinidas com nomes de cores
            Dictionary<string, Color> coresPredefinidas = new Dictionary<string, Color>
            {
                {"Black", Color.Black},
                {"Green", Color.Green},
                {"Blue", Color.Blue},
                {"Coral", Color.Coral},
                {"CornflowerBlue", Color.CornflowerBlue},
                {"Cornsilk", Color.Cornsilk},
                {"Crimson", Color.Crimson},
                {"Cyan", Color.Cyan},
                {"DarkBlue", Color.DarkBlue},
                {"DarkCyan", Color.DarkCyan},
                {"DarkGoldenrod", Color.DarkGoldenrod},
                {"DarkGray", Color.DarkGray},
                {"DarkGreen", Color.DarkGreen},
                {"DarkKhaki", Color.DarkKhaki},
                {"DarkMagenta", Color.DarkMagenta},
                {"DarkOliveGreen", Color.DarkOliveGreen},
                {"DarkOrange", Color.DarkOrange},
                {"DarkOrchid", Color.DarkOrchid},
                {"DarkRed", Color.DarkRed},
                {"DarkSalmon", Color.DarkSalmon},
                {"DarkSeaGreen", Color.DarkSeaGreen},
                {"DarkSlateBlue", Color.DarkSlateBlue},
                {"DarkSlateGray", Color.DarkSlateGray},
                {"DarkTurquoise", Color.DarkTurquoise},
                {"DarkViolet", Color.DarkViolet},
                {"DeepPink", Color.DeepPink},
                {"DeepSkyBlue", Color.DeepSkyBlue},
                {"DimGray", Color.DimGray},
                {"DodgerBlue", Color.DodgerBlue},
                {"Firebrick", Color.Firebrick},
                {"FloralWhite", Color.FloralWhite},
                {"ForestGreen", Color.ForestGreen},
                {"Fuchsia", Color.Fuchsia},
                {"Gainsboro", Color.Gainsboro},
                {"GhostWhite", Color.GhostWhite},
                {"Gold", Color.Gold},
                {"Goldenrod", Color.Goldenrod},
                {"Gray", Color.Gray},
                {"GreenYellow", Color.GreenYellow},
                {"Honeydew", Color.Honeydew},
                {"HotPink", Color.HotPink},
                {"IndianRed", Color.IndianRed},
                {"Indigo", Color.Indigo},
                {"Ivory", Color.Ivory},
                {"Khaki", Color.Khaki},
                {"Lavender", Color.Lavender},
                {"LavenderBlush", Color.LavenderBlush},
                {"LawnGreen", Color.LawnGreen},
                {"LemonChiffon", Color.LemonChiffon},
                {"LightBlue", Color.LightBlue},
                {"LightCoral", Color.LightCoral},
                {"LightCyan", Color.LightCyan},
                {"LightGoldenrodYellow", Color.LightGoldenrodYellow},
                {"LightGreen", Color.LightGreen},
                {"LightGray", Color.LightGray},
                {"LightPink", Color.LightPink},
                {"LightSalmon", Color.LightSalmon},
                {"LightSeaGreen", Color.LightSeaGreen},
                {"LightSkyBlue", Color.LightSkyBlue},
                {"LightSlateGray", Color.LightSlateGray},
                {"LightSteelBlue", Color.LightSteelBlue},
                {"LightYellow", Color.LightYellow},
                {"Lime", Color.Lime},
                {"LimeGreen", Color.LimeGreen},
                {"Linen", Color.Linen},
                {"Magenta", Color.Magenta},
                {"Maroon", Color.Maroon},
                {"MediumAquamarine", Color.MediumAquamarine},
                {"MediumBlue", Color.MediumBlue},
                {"MediumOrchid", Color.MediumOrchid},
                {"MediumPurple", Color.MediumPurple},
                {"MediumSeaGreen", Color.MediumSeaGreen},
                {"MediumSlateBlue", Color.MediumSlateBlue},
                {"MediumSpringGreen", Color.MediumSpringGreen},
                {"MediumTurquoise", Color.MediumTurquoise},
                {"MediumVioletRed", Color.MediumVioletRed},
                {"MidnightBlue", Color.MidnightBlue},
                {"MintCream", Color.MintCream},
                {"MistyRose", Color.MistyRose},
                {"Moccasin", Color.Moccasin},
                {"NavajoWhite", Color.NavajoWhite},
                {"Navy", Color.Navy},
                {"OldLace", Color.OldLace},
                {"Olive", Color.Olive},
                {"OliveDrab", Color.OliveDrab},
                {"Orange", Color.Orange},
                {"OrangeRed", Color.OrangeRed},
                {"Orchid", Color.Orchid},
                {"PaleGoldenrod", Color.PaleGoldenrod},
                {"PaleGreen", Color.PaleGreen},
                {"PaleTurquoise", Color.PaleTurquoise},
                {"PaleVioletRed", Color.PaleVioletRed},
                {"PapayaWhip", Color.PapayaWhip},
                {"PeachPuff", Color.PeachPuff},
                {"Peru", Color.Peru},
                {"Pink", Color.Pink},
                {"Plum", Color.Plum},
                {"PowderBlue", Color.PowderBlue},
                {"Purple", Color.Purple},
                {"RebeccaPurple", Color.RebeccaPurple},
                {"Red", Color.Red},
                {"RosyBrown", Color.RosyBrown},
                {"RoyalBlue", Color.RoyalBlue},
                {"SaddleBrown", Color.SaddleBrown},
                {"Salmon", Color.Salmon},
                {"SandyBrown", Color.SandyBrown},
                {"SeaGreen", Color.SeaGreen},
                {"SeaShell", Color.SeaShell},
                {"Sienna", Color.Sienna},
                {"Silver", Color.Silver},
                {"SkyBlue", Color.SkyBlue},
                {"SlateBlue", Color.SlateBlue},
                {"SlateGray", Color.SlateGray},
                {"Snow", Color.Snow},
                {"SpringGreen", Color.SpringGreen},
                {"SteelBlue", Color.SteelBlue},
                {"Tan", Color.Tan},
                {"Teal", Color.Teal},
                {"Thistle", Color.Thistle},
                {"Tomato", Color.Tomato},
                {"Turquoise", Color.Turquoise},
                {"Violet", Color.Violet},
                {"Wheat", Color.Wheat},
                {"White", Color.White},
                {"WhiteSmoke", Color.WhiteSmoke},
                {"Yellow", Color.Yellow},
                {"YellowGreen", Color.YellowGreen}
            };
            #endregion

            // Procura pela cor mais próxima do valor RGB fornecido
            string nomeCorMaisProxima = "Desconhecida";
            double menorDistancia = double.MaxValue;

            foreach (var nomeCor in coresPredefinidas.Keys)
            {
                double distancia = CalcularDistancia(cor, coresPredefinidas[nomeCor]);
                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia;
                    nomeCorMaisProxima = nomeCor;
                }
            }

            return nomeCorMaisProxima;
        }

        static double CalcularDistancia(Color corA, Color corB)
        {
            int dR = corA.R - corB.R;
            int dG = corA.G - corB.G;
            int dB = corA.B - corB.B;

            return Math.Sqrt(dR * dR + dG * dG + dB * dB);
        }

    }
}