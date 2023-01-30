using Emgu.CV.Ocl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;

namespace DofusChasseForms
{
    public class OCR
    {
        private Bitmap DofusScreen;
        public OCR() 
        {
        }

        public void MajScreen(Bitmap bitmap)
        {
            this.DofusScreen = bitmap;
        }

        public string GetPos()
        {
            string result = "";

            Rectangle cropRect = new Rectangle(1693, 143, 180, 28);
            using (Bitmap target = new Bitmap(cropRect.Width, cropRect.Height))
            {
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(this.DofusScreen, new Rectangle(0, 0, target.Width, target.Height),
                        cropRect,
                        GraphicsUnit.Pixel);
                }
                var output = this.adjustContrast(target, 10000);
                output.SetResolution(400F, 400F);
                ToGrayScale(output);
                var cropedImg = output.Clone(new Rectangle(5, 0, 175, 28), PixelFormat.Format64bppArgb);  //CROP FRIST [
                cropedImg = AddSpacesInBitmap(cropedImg);
                cropedImg.Save("savescreenPos.tiff", System.Drawing.Imaging.ImageFormat.Tiff);
            }

            var testImagePath = "savescreenPos.tiff";
            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "fra", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(testImagePath))
                    {
                        #region debug
                        //using (var page = engine.Process(img, PageSegMode.SparseTextOsd))
                        //{
                        //    Console.WriteLine("[DEBUG 1] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.Auto))
                        //{
                        //    Console.WriteLine("[DEBUG 2] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SingleColumn))
                        //{
                        //    Console.WriteLine("[DEBUG 3] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.RawLine))
                        //{
                        //    Console.WriteLine("[DEBUG 4] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.CircleWord))
                        //{
                        //    Console.WriteLine("[DEBUG 5] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.AutoOnly))
                        //{
                        //    Console.WriteLine("[DEBUG 6] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.AutoOsd))
                        //{
                        //    Console.WriteLine("[DEBUG 7] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.Count))
                        //{
                        //    Console.WriteLine("[DEBUG 8] " + page.GetText());
                        //}
                        ////using (var page = engine.Process(img, PageSegMode.OsdOnly))
                        ////{
                        ////    Console.WriteLine("[DEBUG 9] " + page.GetText());
                        ////}
                        //using (var page = engine.Process(img, PageSegMode.SingleBlock))
                        //{
                        //    Console.WriteLine("[DEBUG 10] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SingleChar))
                        //{
                        //    Console.WriteLine("[DEBUG 11] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SingleLine))
                        //{
                        //    Console.WriteLine("[DEBUG 12] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SingleWord))
                        //{
                        //    Console.WriteLine("[DEBUG 13] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SparseText))
                        //{
                        //    Console.WriteLine("[DEBUG 13] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SparseTextOsd))
                        //{
                        //    Console.WriteLine("[DEBUG 14] " + page.GetText());
                        //}
                        #endregion //DEBUG VALUES
                        using (var page = engine.Process(img, PageSegMode.SingleLine))
                        {
                            var text = page.GetText();
                            Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                            Console.WriteLine("Text (GetText): \r\n{0}", text);
                            Console.WriteLine("Text (iterator):");
                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();

                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            do
                                            {
                                                if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                                                {
                                                    Console.WriteLine("<BLOCK>");
                                                }
                                                if (result == "")
                                                    result = iter.GetText(PageIteratorLevel.Word);
                                                else
                                                    result = result + " " + iter.GetText(PageIteratorLevel.Word);
                                                Console.Write(iter.GetText(PageIteratorLevel.Word));

                                                Console.Write(" ");

                                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                {
                                                    Console.WriteLine();
                                                    return result;
                                                }
                                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                            {
                                                Console.WriteLine();
                                            }
                                        } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                    } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                } while (iter.Next(PageIteratorLevel.Block));
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        //813, 701

        public string GetIndice(int number)
        {
            Rectangle cropRect = new Rectangle(1639, 169, 246, 305);
            using (Bitmap target = new Bitmap(cropRect.Width, cropRect.Height))
            {
                target.SetResolution(246F, 305F);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(this.DofusScreen, new Rectangle(0, 0, target.Width, target.Height),
                        cropRect,
                        GraphicsUnit.Pixel);
                }
                var output = this.adjustContrast(target, 255);
                output.SetResolution(246F, 305F);
                ToGrayScale(output);
                output.Save("savescreen.png", System.Drawing.Imaging.ImageFormat.Png);
            }

            var testImagePath = "savescreen.png";
            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "fra"))
                {
                    using (var img = Pix.LoadFromFile(testImagePath))
                    {
                        #region debug
                        //using (var page = engine.Process(img, PageSegMode.SparseTextOsd))
                        //{
                        //    Console.WriteLine("[DEBUG 1] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.Auto))
                        //{
                        //    Console.WriteLine("[DEBUG 2] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SingleColumn))
                        //{
                        //    Console.WriteLine("[DEBUG 3] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.RawLine))
                        //{
                        //    Console.WriteLine("[DEBUG 4] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.CircleWord))
                        //{
                        //    Console.WriteLine("[DEBUG 5] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.AutoOnly))
                        //{
                        //    Console.WriteLine("[DEBUG 6] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.AutoOsd))
                        //{
                        //    Console.WriteLine("[DEBUG 7] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.Count))
                        //{
                        //    Console.WriteLine("[DEBUG 8] " + page.GetText());
                        //}
                        ////using (var page = engine.Process(img, PageSegMode.OsdOnly))
                        ////{
                        ////    Console.WriteLine("[DEBUG 9] " + page.GetText());
                        ////}
                        //using (var page = engine.Process(img, PageSegMode.SingleBlock))
                        //{
                        //    Console.WriteLine("[DEBUG 10] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SingleChar))
                        //{
                        //    Console.WriteLine("[DEBUG 11] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SingleLine))
                        //{
                        //    Console.WriteLine("[DEBUG 12] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SingleWord))
                        //{
                        //    Console.WriteLine("[DEBUG 15] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SparseText))
                        //{
                        //    Console.WriteLine("[DEBUG 13] " + page.GetText());
                        //}
                        //using (var page = engine.Process(img, PageSegMode.SparseTextOsd))
                        //{
                        //    Console.WriteLine("[DEBUG 14] " + page.GetText());
                        //}
                        #endregion //DEBUG VALUES
                        using (var page = engine.Process(img, PageSegMode.SingleBlock))
                        {
                            var text = page.GetText(); //POUR CAIRM    14     13  7(espeace chelou)   6(pareil que 7)

                            if (text.Contains("?"))
                            {
                                int index = text.IndexOf("?");
                                if (index >= 0)
                                    text = text.Substring(0, index);
                            }
                            else if (text.Contains("sais restants"))
                            {
                                int index = text.IndexOf("sais restants");
                                if (index >= 0)
                                    text = text.Substring(0, index);
                            }

                            string[] substring = text.Split('\n');
                            List<string> listString = new List<string>();
                            listString = substring.ToList();
                            foreach (var item in substring)
                            {
                                if (item == null || item == "" || item == " " || item == "  " || item == "  ")
                                {
                                    listString.Remove(item);
                                }
                            }
                            return listString[number].Replace("\n", "");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        public bool GetFight()
        {
            Rectangle cropRect = new Rectangle(1716, 176, 81, 16);
            using (Bitmap target = new Bitmap(cropRect.Width, cropRect.Height))
            {
                target.SetResolution(81F, 16F);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(this.DofusScreen, new Rectangle(0, 0, target.Width, target.Height),
                        cropRect,
                        GraphicsUnit.Pixel);
                }
                var output = this.adjustContrast(target, 255);
                output.SetResolution(81F, 16F);
                ToGrayScale(output);
                output.Save("savescreenFight.png", System.Drawing.Imaging.ImageFormat.Png);
            }

            var testImagePath = "savescreenFight.png";
            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "fra"))
                {
                    using (var img = Pix.LoadFromFile(testImagePath))
                    {
                        using (var page = engine.Process(img, PageSegMode.SingleBlock))
                        {
                            if(page.GetText().ToLower().Contains("combattre"))
                            {
                                return true;
                            };
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool GetFinishFight()
        {
            Rectangle cropRect = new Rectangle(892, 618, 66, 25);
            using (Bitmap target = new Bitmap(cropRect.Width, cropRect.Height))
            {
                target.SetResolution(66F, 25F);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(this.DofusScreen, new Rectangle(0, 0, target.Width, target.Height),
                        cropRect,
                        GraphicsUnit.Pixel);
                }
                var output = this.adjustContrast(target, 255);
                output.SetResolution(66F, 25F);
                ToGrayScale(output);
                output.Save("savescreenEndFight.png", System.Drawing.Imaging.ImageFormat.Png);
            }

            var testImagePath = "savescreenEndFight.png";
            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "fra"))
                {
                    using (var img = Pix.LoadFromFile(testImagePath))
                    {
                        using (var page = engine.Process(img, PageSegMode.SingleBlock))
                        {
                            if (page.GetText().ToLower().Contains("fermer"))
                            {
                                return true;
                            };
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        private Bitmap adjustContrast(Bitmap bitmap, int threshold)
        {
            BitmapData sourceData = bitmap.LockBits(new Rectangle(0, 0,
                                bitmap.Width, bitmap.Height),
                                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);


            bitmap.UnlockBits(sourceData);


            double contrastLevel = Math.Pow((100.0 + threshold) / 100.0, 2);


            double blue = 0;
            double green = 0;
            double red = 0;


            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                blue = ((((pixelBuffer[k] / 255.0) - 0.5) *
                            contrastLevel) + 0.5) * 255.0;


                green = ((((pixelBuffer[k + 1] / 255.0) - 0.5) *
                            contrastLevel) + 0.5) * 255.0;


                red = ((((pixelBuffer[k + 2] / 255.0) - 0.5) *
                            contrastLevel) + 0.5) * 255.0;


                if (blue > 255)
                { blue = 255; }
                else if (blue < 0)
                { blue = 0; }


                if (green > 255)
                { green = 255; }
                else if (green < 0)
                { green = 0; }


                if (red > 255)
                { red = 255; }
                else if (red < 0)
                { red = 0; }


                pixelBuffer[k] = (byte)blue;
                pixelBuffer[k + 1] = (byte)green;
                pixelBuffer[k + 2] = (byte)red;
            }


            Bitmap resultBitmap = new Bitmap(bitmap.Width, bitmap.Height);


            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                        resultBitmap.Width, resultBitmap.Height),
                                        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        public void ToGrayScale(Bitmap Bmp)
        {
            int rgb;
            Color c;
            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    rgb = (int)Math.Round(.299 * c.R + .587 * c.G + .114 * c.B);
                    Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
        }

        public Bitmap AddSpacesInBitmap(Bitmap baseBMap)
        {
            //sur les 40 premiere colonne tripler les colonne de pixel blanc
            bool isCopyEnable = true;
            for (int Xcount = 0; Xcount < baseBMap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < baseBMap.Height; Ycount++)
                {
                    if(isCopyEnable)
                    {
                        var color = baseBMap.GetPixel(Xcount, Ycount);
                        if (!(color.R > 200) && !(color.G > 200) && !(color.B > 200))
                            break;
                        else if(Ycount == baseBMap.Height - 1)
                        {
                            //baseBMap.Save("savescreenPos" + Xcount + "-" + Ycount + ".tiff", System.Drawing.Imaging.ImageFormat.Tiff);

                            Graphics grphPos = Graphics.FromImage(baseBMap);
                            var destRegion = new Rectangle(Xcount+1, 0, baseBMap.Width - (Xcount+1), baseBMap.Height);
                            var srcRegion = new Rectangle(Xcount, 0, baseBMap.Width - (Xcount+1) , baseBMap.Height);
                            grphPos.DrawImage(baseBMap, destRegion, srcRegion, GraphicsUnit.Pixel); // generation 1ere Colonne

                            destRegion = new Rectangle(Xcount + 2, 0, baseBMap.Width - (Xcount + 2), baseBMap.Height);
                            srcRegion = new Rectangle(Xcount, 0, baseBMap.Width - (Xcount + 2), baseBMap.Height);
                            grphPos.DrawImage(baseBMap, destRegion, srcRegion, GraphicsUnit.Pixel);

                            destRegion = new Rectangle(Xcount + 3, 0, baseBMap.Width - (Xcount + 3), baseBMap.Height);
                            srcRegion = new Rectangle(Xcount, 0, baseBMap.Width - (Xcount + 3), baseBMap.Height);
                            grphPos.DrawImage(baseBMap, destRegion, srcRegion, GraphicsUnit.Pixel);

                            isCopyEnable = false;

                            //baseBMap.Save("savescreenPos"+Xcount+"-"+Ycount+".tiff", System.Drawing.Imaging.ImageFormat.Tiff);
                            Xcount = Xcount + 2;
                            break;
                        }
                    }
                    else
                    {
                        baseBMap.Save("savescreenPosYOP.tiff", System.Drawing.Imaging.ImageFormat.Tiff);

                        var color = baseBMap.GetPixel(Xcount, Ycount);
                        if ((color.R < 200) && (color.G < 200) && (color.B < 200))
                        {
                            isCopyEnable = true;
                            break;
                        }
                    }
                }
            }
            return baseBMap;
        }
    }
}
