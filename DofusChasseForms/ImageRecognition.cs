using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusChasseForms
{
    public class ImageRecognition
    {
        private string imgUrl;
        private Bitmap ScreenDofus;

        private int coordX;
        private int coordY;
        public ImageRecognition()
        {
        }

        public void MajScreen(Bitmap bitmap)
        {
            this.ScreenDofus = bitmap;
        }

        public string FindArrowDirection()
        {
            int upIndex = GetHeightPixelPositionOfArrows("up");
            int leftIndex = GetHeightPixelPositionOfArrows("left");
            int rightIndex = GetHeightPixelPositionOfArrows("right");
            int downIndex = GetHeightPixelPositionOfArrows("down");

            if (upIndex > leftIndex && upIndex > rightIndex && upIndex > downIndex)
            {
                return "up";
            }
            else if (leftIndex > rightIndex && leftIndex > upIndex && leftIndex > downIndex)
            {
                return "left";
            }
            else if (rightIndex > upIndex && rightIndex > leftIndex && rightIndex > downIndex)
            {
                return "right";
            }
            else
            {
                return "down";
            }
        }

        public bool FindIfBlackFlagPresent()
        {
            if(GetHeightPixelPositionOfArrows("flag") == 0)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }

        public int[] FindLoupeCoord()
        {
            if (GetHeightPixelPositionOfArrows("loupe") == 0)
            {
                return null;
            }
            else
            {
                //1870, 192
                return new int[] { coordX + 1815, coordY + 103 };
            }
        }

        private int GetHeightPixelPositionOfArrows(string ImgCheck = "up")
        {
            double indice = 0.85;
            Image<Bgr, byte> source;
            if (ImgCheck == "flag")
            {
                TakeScreenFlag();
                source = new Image<Bgr, byte>("savescreenFlag.png"); // Image B
                indice = 0.85;
            }
            else if(ImgCheck == "loupe")
            {
                TakeScreenLoupe();
                source = new Image<Bgr, byte>("savescreenLoupe.png"); // Image B
                indice = 0.85;
            }
            else
            {
                TakeScreen();
                source = new Image<Bgr, byte>("savescreenArrow.png"); // Image B
                indice = 0.85;
            }
            Image<Bgr, byte> template;
            switch (ImgCheck)
            {
                case "up":
                    template = new Image<Bgr, byte>(@"img_compar\arrow_up.jpg"); // Image A
                    break;
                case "left":
                    template = new Image<Bgr, byte>(@"img_compar\arrow_left.jpg"); // Image A
                    break;
                case "right":
                    template = new Image<Bgr, byte>(@"img_compar\arrow_right.jpg"); // Image A
                    break;
                case "flag":
                    template = new Image<Bgr, byte>(@"img_compar\flag.jpg"); // Image A
                    break;
                case "loupe":
                    template = new Image<Bgr, byte>(@"img_compar\loupe.jpg"); // Image A
                    break;
                default:
                    template = new Image<Bgr, byte>(@"img_compar\arrow_down.jpg"); // Image A
                    break;
            }
            Image<Bgr, byte> imageToShow = source.Copy();

            Image<Gray, float> result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);
            var listOfMaxs = new List<Point>();
            var resultWithPadding = new Image<Gray, float>(source.Size);
            var heightOfPadding = (source.Height - result.Height) / 2;
            var widthOfPadding = (source.Width - result.Width) / 2;
            resultWithPadding.ROI = new Rectangle() { X = heightOfPadding, Y = widthOfPadding, Width = result.Width, Height = result.Height };
            result.CopyTo(resultWithPadding);
            resultWithPadding.ROI = Rectangle.Empty;

            for (int i = 0; i < resultWithPadding.Width; i++)
            {
                for (int j = 0; j < resultWithPadding.Height; j++)
                {
                    var centerOfRoi = new Point() { X = i + template.Width / 2, Y = j + template.Height / 2 };
                    var roi = new Rectangle() { X = i, Y = j, Width = template.Width, Height = template.Height };
                    resultWithPadding.ROI = roi;
                    double[] minValues, maxValues;
                    Point[] minLocations, maxLocations;
                    resultWithPadding.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                    resultWithPadding.ROI = Rectangle.Empty;
                    var maxLocation = maxLocations.First();
                    if (maxLocation.X == roi.Width / 2 && maxLocation.Y == roi.Height / 2 && maxValues[0] > indice)
                    {
                        var point = new Point() { X = centerOfRoi.X, Y = centerOfRoi.Y };
                        listOfMaxs.Add(point);
                    }
                }
            }
            if (listOfMaxs.Count > 0)
            {
                if(ImgCheck == "loupe")
                {
                    coordX = listOfMaxs.Last().X;
                    coordY = listOfMaxs.Last().Y;
                }
                return listOfMaxs.Last().Y;
            }
            else
            {
                return 0;
            }
        }

        private void TakeScreen() // ARROW
        {
            Rectangle cropRect = new Rectangle(1610, 172, 36, 225);
            using (Bitmap target = new Bitmap(cropRect.Width, cropRect.Height))
            {
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(this.ScreenDofus, new Rectangle(0, 0, target.Width, target.Height),
                        cropRect,
                        GraphicsUnit.Pixel);
                }
                target.Save("savescreenArrow.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            this.imgUrl = "savescreenArrow.png";
        }

        private void TakeScreenFlag()
        {
            Rectangle cropRect = new Rectangle(1874, 135, 32, 211);
            using (Bitmap target = new Bitmap(cropRect.Width, cropRect.Height))
            {
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(this.ScreenDofus, new Rectangle(0, 0, target.Width, target.Height),
                        cropRect,
                        GraphicsUnit.Pixel);
                }
                target.SetResolution(32F, 211F);
                target.Save("savescreenFlag.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            this.imgUrl = "savescreenFlag.png";
        }

        private void TakeScreenLoupe()
        {
            Rectangle cropRect = new Rectangle(1823, 134, 80, 280);
            using (Bitmap target = new Bitmap(cropRect.Width, cropRect.Height))
            {
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(this.ScreenDofus, new Rectangle(0, 0, target.Width, target.Height),
                        cropRect,
                        GraphicsUnit.Pixel);
                }
                target.Save("savescreenLoupe.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            this.imgUrl = "savescreenLoupe.png";
        }
    }
}