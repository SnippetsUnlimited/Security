using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Company.Security.Captcha.Framework
{
    public class CaptchaImageLibrary
    {
        private Random _rand = new Random();
        private string _text;
        private int _height;
        private int _width;
        private int _fontSize;
        private string _fontFamilyName;
        private string _fontWhitelist;
        private FontWrapFactor _fontWrap = FontWrapFactor.None;
        private BackgroundNoiseLevel _backgroundNoise = BackgroundNoiseLevel.None;
        private LineNoiseLevel _lineNoise = LineNoiseLevel.None;

        private CaptchaImageLibrary() { }

        public static Bitmap CreateImage(int width, int height, int fontSize, string text, string fontFamilyName, FontWrapFactor fontWrap, BackgroundNoiseLevel backgroundNoise, LineNoiseLevel lineNoise, string fontWhitelist)
        {
            var instance = new CaptchaImageLibrary();
            instance._width = width;
            instance._height = height;
            instance._fontSize = fontSize;
            instance._text = text;
            instance._fontWrap = fontWrap;
            instance._backgroundNoise = backgroundNoise;
            instance._lineNoise = lineNoise;
            instance._fontWhitelist = fontWhitelist;
            instance._fontFamilyName = fontFamilyName;
            return instance.GenerateImagePrivate();
        }


        private static string[] ff;

        private string RandomFontFamily()
        {
            if (ff == null)
            {
                ff = this._fontWhitelist.Split(new char[] { ';' });
            }
            return ff[_rand.Next(0, ff.Length)];
        }

        private PointF RandomPoint(int xmin, int xmax, int ymin, int ymax)
        {
            return new PointF(_rand.Next(xmin, xmax), _rand.Next(ymin, ymax));
        }

        private PointF RandomPoint(Rectangle rect)
        {
            return RandomPoint(rect.Left, rect.Width, rect.Top, rect.Bottom);
        }

        private GraphicsPath TextPath(string s, Font f, Rectangle r)
        {
            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            var gp = new GraphicsPath();
            gp.AddString(s, f.FontFamily, (int)f.Style, f.Size, r, sf);
            return gp;
        }

        private Font GetFont()
        {
            Single fsize = 1;
            string fname = _fontFamilyName;
            if (fname == "")
            {
                fname = RandomFontFamily();
            }

            fsize = _fontSize;

            return new Font(fname, fsize, FontStyle.Bold);
        }

        private Bitmap GenerateImagePrivate()
        {
            Font fnt = null;
            Rectangle rect;
            Brush br;
            Bitmap bmp = new Bitmap(_width, _height, PixelFormat.Format32bppArgb);
            Graphics gr = Graphics.FromImage(bmp);
            gr.SmoothingMode = SmoothingMode.AntiAlias;

            //'-- fill an empty white rectangle
            rect = new Rectangle(0, 0, _width, _height);
            br = new SolidBrush(Color.White);
            gr.FillRectangle(br, rect);

            int charOffset = 0;
            double charWidth = _width / _text.Length;
            Rectangle rectChar;

            foreach (char c in _text)
            {
                //'-- establish font and draw area
                fnt = GetFont();
                rectChar = new Rectangle(Convert.ToInt32(charOffset * charWidth), 0, Convert.ToInt32(charWidth), _height);

                //'-- warp the character
                GraphicsPath gp = TextPath(Convert.ToString(c), fnt, rectChar);
                WarpText(gp, rectChar);

                //'-- draw the character
                br = new SolidBrush(Color.Black);
                gr.FillPath(br, gp);

                charOffset += 1;
            }

            AddBackgroundNoise(gr, rect);
            AddLineNoise(gr, rect);

            fnt.Dispose();
            br.Dispose();
            gr.Dispose();

            return bmp;
        }

        private void WarpText(GraphicsPath textPath, Rectangle rect)
        {
            Single WarpDivisor = 1;
            Single RangeModifier = 1;

            switch (_fontWrap)
            {
                case FontWrapFactor.None:
                    return;
                case FontWrapFactor.Low:
                    WarpDivisor = 6;
                    RangeModifier = 1.8f;
                    break;
                case FontWrapFactor.Medium:
                    WarpDivisor = 4;
                    RangeModifier = 2.5f;
                    break;
                case FontWrapFactor.High:
                    WarpDivisor = 2.5f;
                    RangeModifier = 2.8f;
                    break;
                case FontWrapFactor.Extreme:
                    WarpDivisor = 1.9f;
                    RangeModifier = 3.0f;
                    break;
            }

            RectangleF rectF;
            rectF = new RectangleF(Convert.ToSingle(rect.Left), 0, Convert.ToSingle(rect.Width), rect.Height);

            int hrange = Convert.ToInt32(rect.Height / WarpDivisor);
            int wrange = Convert.ToInt32(rect.Width / WarpDivisor);
            int left = rect.Left - Convert.ToInt32(wrange * RangeModifier);
            int top = rect.Top - Convert.ToInt32(hrange * RangeModifier);
            int width = rect.Left + rect.Width + Convert.ToInt32(wrange * RangeModifier);
            int height = rect.Top + rect.Height + Convert.ToInt32(hrange * RangeModifier);

            if (left < 0) left = 0;
            if (top < 0) top = 0;
            if (width > this._width) width = this._width;
            if (height > this._height) height = this._height;

            PointF leftTop = RandomPoint(left, left + wrange, top, top + hrange);
            PointF rightTop = RandomPoint(width - wrange, width, top, top + hrange);
            PointF leftBottom = RandomPoint(left, left + wrange, height - hrange, height);
            PointF rightBottom = RandomPoint(width - wrange, width, height - hrange, height);

            PointF[] points = new PointF[] { leftTop, rightTop, leftBottom, rightBottom };
            Matrix m = new Matrix();
            m.Translate(0, 0);
            textPath.Warp(points, rectF, m, WarpMode.Perspective, 0);
        }


        private void AddBackgroundNoise(Graphics graphics1, Rectangle rect)
        {
            int density = 1;
            int size = 1;

            switch (_backgroundNoise)
            {
                case BackgroundNoiseLevel.None:
                    return;
                case BackgroundNoiseLevel.Low:
                    density = 45;
                    size = 65;
                    break;
                case BackgroundNoiseLevel.Medium:
                    density = 40;
                    size = 60;
                    break;
                case BackgroundNoiseLevel.High:
                    density = 35;
                    size = 55;
                    break;
                case BackgroundNoiseLevel.Extreme:
                    density = 30;
                    size = 50;
                    break;
            }

            SolidBrush br = new SolidBrush(Color.Black);
            int max = Convert.ToInt32(Math.Max(rect.Width, rect.Height) / size);

            for (int i = 0; i <= Convert.ToInt32((rect.Width * rect.Height) / density); i++)
            {
                graphics1.FillEllipse(br, _rand.Next(rect.Width), _rand.Next(rect.Height),
                    _rand.Next(max), _rand.Next(max));
            }
            br.Dispose();
        }

        private void AddLineNoise(Graphics graphics1, Rectangle rect)
        {
            int length = 0;
            int width = 1;
            int linecount = 0;

            switch (_lineNoise)
            {
                case LineNoiseLevel.None:
                    return;
                case LineNoiseLevel.Low:
                    length = 4;
                    width = Convert.ToInt32(_height / 31.25f); // 1.6
                    linecount = 1;
                    break;
                case LineNoiseLevel.Medium:
                    length = 5;
                    width = Convert.ToInt32(_height / 27.7777f); // 1.8
                    linecount = 1;
                    break;
                case LineNoiseLevel.High:
                    length = 3;
                    width = Convert.ToInt32(_height / 25f); // 2.0
                    linecount = 2;
                    break;
                case LineNoiseLevel.Extreme:
                    length = 3;
                    width = Convert.ToInt32(_height / 22.7272f); // 2.2
                    linecount = 3;
                    break;
            }

            PointF[] pf = new PointF[length];
            using (var p = new Pen(Color.Black, width))
            {

                for (var l = 1; l <= linecount; l++)
                {
                    for (var i = 0; i < length; i++)
                    {
                        pf[i] = RandomPoint(rect);
                    }
                    graphics1.DrawCurve(p, pf, 1.75f);
                }
            }
        }
    }

    public enum BackgroundNoiseLevel
    {
        None,
        Low,
        Medium,
        High,
        Extreme
    }

    public enum FontWrapFactor
    {
        None,
        Low,
        Medium,
        High,
        Extreme
    }

    public enum LineNoiseLevel
    {
        None,
        Low,
        Medium,
        High,
        Extreme
    }


}
