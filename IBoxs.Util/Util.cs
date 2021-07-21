using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace IBoxs.Util
{
    public static class Util
    {
        public static int ToInt(this string value)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(value))
            {
                int conversion = 0;
                if (int.TryParse(value, out conversion))
                {
                    result = conversion;
                }

            }
            return result;
        }

        public static string RemoverMascaras(this string value)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(value))
                result = value.Replace(" ", "").Replace(".", "").Replace("-", "").Replace("/", "").Replace("(", "").Replace(")", "");
            return result;
        }

        public static byte[] ImageToByteArray(this System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }

        public static string ImageToBase64(this string CaminhoImagem)
        {
            if (File.Exists(CaminhoImagem))
            {
                var img = Image.FromFile(CaminhoImagem);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return Convert.ToBase64String(ms.ToArray());
            }
            else
            {
                return string.Empty;
            }
        }

        public static List<string> BuscaImagens(IEnumerable<string> caminhoImagens)
        {
            List<string> lstImagens = new List<string>();
            foreach (var item in caminhoImagens)
            {
                var arrayImagens = item.Split(';');
                foreach (var arquivo in arrayImagens)
                {
                    if (File.Exists(arquivo))
                    {
                        try
                        {
                            var imagem = Image.FromFile(arquivo);
                            var img = EscalaPercentual(imagem, 70);
                            var codec = ObterCodec(imagem.RawFormat);
                            var imageComprimida = ComprimirImagem(img, 17, codec);
                            lstImagens.Add(Convert.ToBase64String(imageComprimida.ToArray()));
                        }
                        catch
                        { }
                    }
                }
            }
            return lstImagens.Distinct().ToList();
        }

        public static MemoryStream ComprimirImagem(Image imagem, long qualidade, ImageCodecInfo codec)
        {
            var stream = new System.IO.MemoryStream();
            var param = new EncoderParameters(1);
            param.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualidade);
            imagem.Save(stream, codec, param);
            stream.Position = 0;
            return stream;
        }

        public static Image EscalaPercentual(Image imgFoto, int Percentual)
        {
            float nPorcentagem = ((float)Percentual / 100);

            int fonteLargura = imgFoto.Width;     //armazena a largura original da imagem origem
            int fonteAltura = imgFoto.Height;   //armazena a altura original da imagem origem
            int origemX = 0;        //eixo x da imagem origem
            int origemY = 0;        //eixo y da imagem origem

            int destX = 0;          //eixo x da imagem destino
            int destY = 0;          //eixo y da imagem destino
            //Calcula a altura e largura da imagem redimensionada
            int destWidth = (int)(fonteLargura * nPorcentagem);
            int destHeight = (int)(fonteAltura * nPorcentagem);

            //Cria um novo objeto bitmap
            Bitmap bmImagem = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            //Define a resolu~ção do bitmap.
            bmImagem.SetResolution(imgFoto.HorizontalResolution, imgFoto.VerticalResolution);
            //Crima um objeto graphics e defina a qualidade
            Graphics grImagem = Graphics.FromImage(bmImagem);
            grImagem.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //Desenha a imge usando o método DrawImage() da classe grafica
            grImagem.DrawImage(imgFoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(origemX, origemY, fonteLargura, fonteAltura),
                GraphicsUnit.Pixel);

            grImagem.Dispose();  //libera o objeto grafico
            return bmImagem;
        }

        private static ImageCodecInfo ObterCodec(ImageFormat formato)
        {
            var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == formato.Guid);
            if (codec == null) throw new NotSupportedException();
            return codec;
        }

        public static bool IsCnpj(this string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCnpj;
            string digito;
            int soma = 0;
            int resto;

            cnpj = cnpj.Trim();
            cnpj = cnpj.RemoverMascaras();

            if (cnpj.Length != 14)
            {
                return false;
            }

            tempCnpj = cnpj.Substring(0, 12);

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;

            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }

            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool IsCpf(this string cpf)
        {
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {
                return false;
            }

            var tempCpf = cpf.Substring(0, 9);

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            var resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            var digito = resto.ToString();
            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }

            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }


        public static bool IsEmail(this string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDigit(this string value)
        {
            Regex rg = new Regex("^[0-9]+$");
            if (rg.IsMatch(value))
                return true;
            else
                return false;
        }

        public static string DateTimeToString(this DateTime? dt, string format)
            => dt == null ? DBNull.Value.ToString() : ((DateTime)dt).ToString(format);

    }
    public static class ValorEnum
    {
        public static T ObterAtributoDoTipo<T>(this Enum valorEnum) where T : System.Attribute
        {
            var type = valorEnum.GetType();
            var memInfo = type.GetMember(valorEnum.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static string ObterDescricao(this Enum valorEnum)
        {
            return valorEnum.ObterAtributoDoTipo<DescriptionAttribute>().Description;
        }
    }
}
