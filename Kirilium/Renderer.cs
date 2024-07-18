using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium
{
    [SupportedOSPlatform("windows")]
    public static class Renderer
    {
        // 非公開フィールド
        private readonly static float PPI = GetPPI();                           // 1インチあたりのピクセル数
        private readonly static float NormalPenWidth = PPI / 96.0f;             // 標準のペンの幅
        private readonly static float BoldPenWidth = NormalPenWidth * 2.0f;     // 太いペンの幅

        /// <summary>
        /// デバイスのPPI(1インチあたりのピクセル数）を取得する。
        /// </summary>
        /// <returns></returns>
        private static float GetPPI()
        {
            using (var bmp = new Bitmap(1, 1))
            {
                return Math.Max(bmp.HorizontalResolution, bmp.VerticalResolution);
            }
        }

        #region ペンの作成

        /// <summary>
        /// 標準ペンを作成する。
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Pen CreateNormalPen(Color color)
        {
            return new Pen(color, NormalPenWidth);
        }

        /// <summary>
        /// 太いペンを作成する。
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Pen CreateBoldPen(Color color)
        {
            return new Pen(color, BoldPenWidth);
        }

        #endregion

        #region 線の描画

        /// <summary>
        /// 線を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="pen"></param>
        public static void DrawLine(Graphics deviceContext, float x, float y, float x2, float y2, Pen pen)
        {
            deviceContext.DrawLine(pen, x, y, x2, y2);
        }

        /// <summary>
        /// 線を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        public static void DrawLine(Graphics deviceContext, float x, float y, float x2, float y2, Color color)
        {
            using (var pen = CreateNormalPen(color))
            {
                DrawLine(deviceContext, x, y, x2, y2, pen);
            }
        }

        #endregion

        #region 矩形の描画

        /// <summary>
        /// 矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="pen"></param>
        public static void DrawRect(Graphics deviceContext, float x, float y, float width, float height, Pen pen)
        {
            deviceContext.DrawRectangle(pen, x, y, width, height);
        }

        /// <summary>
        /// 矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="lineColor"></param>
        public static void DrawRect(Graphics deviceContext, float x, float y, float width, float height, Color lineColor)
        {
            using (var pen = CreateNormalPen(lineColor))
            {
                DrawRect(deviceContext, x, y, width, height, pen);
            }
        }

        /// <summary>
        /// 矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="pen"></param>
        public static void DrawRect(Graphics deviceContext, Rectangle rectangle, Pen pen)
        {
            deviceContext.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }


        /// <summary>
        /// 矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="lineColor"></param>
        public static void DrawRect(Graphics deviceContext, Rectangle rectangle, Color lineColor)
        {
            using (var pen = CreateNormalPen(lineColor))
            {
                DrawRect(deviceContext, rectangle, pen);
            }
        }

        /// <summary>
        /// 矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="pen"></param>
        public static void DrawRect(Graphics deviceContext, RectangleF rectangle, Pen pen)
        {
            deviceContext.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// 矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="lineColor"></param>
        public static void DrawRect(Graphics deviceContext, RectangleF rectangle, Color lineColor)
        {
            using (var pen = CreateNormalPen(lineColor))
            {
                DrawRect(deviceContext, rectangle, pen);
            }
        }

        #endregion

        #region 矩形（塗りつぶし）の描画

        /// <summary>
        /// 塗りつぶされた矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="lineColor"></param>
        /// <param name="fillColor"></param>
        public static void FillRect(Graphics deviceContext, float x, float y, float width, float height, Color lineColor, Color fillColor)
        {
            // 矩形領域を塗りつぶす。
            using (var brush = new SolidBrush(fillColor))
            {
                deviceContext.FillRectangle(brush, x, y, width, height);
            }

            // 境界線を描画する。
            using (var pen = CreateNormalPen(lineColor))
            {
                DrawRect(deviceContext, x, y, width, height, pen);
            }
        }

        /// <summary>
        /// 塗りつぶされた矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        public static void FillRect(Graphics deviceContext, float x, float y, float width, float height, Color color)
        {
            FillRect(deviceContext, x, y, width, height, color, color);
        }

        /// <summary>
        /// 塗りつぶされた矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="lineColor"></param>
        /// <param name="fillColor"></param>
        public static void FillRect(Graphics deviceContext, Rectangle rectangle, Color lineColor, Color fillColor)
        {
            // 矩形領域を塗りつぶす。
            using (var brush = new SolidBrush(fillColor))
            {
                deviceContext.FillRectangle(brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            }

            // 境界線を描画する。
            using (var pen = CreateNormalPen(lineColor))
            {
                DrawRect(deviceContext, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, pen);
            }
        }

        /// <summary>
        /// 塗りつぶされた矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        public static void FillRect(Graphics deviceContext, Rectangle rectangle, Color color)
        {
            FillRect(deviceContext, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color, color);
        }

        /// <summary>
        /// 塗りつぶされた矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="lineColor"></param>
        /// <param name="fillColor"></param>
        public static void FillRect(Graphics deviceContext, RectangleF rectangle, Color lineColor, Color fillColor)
        {
            // 矩形領域を塗りつぶす。
            using (var brush = new SolidBrush(fillColor))
            {
                deviceContext.FillRectangle(brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            }

            // 境界線を描画する。
            using (var pen = CreateNormalPen(lineColor))
            {
                DrawRect(deviceContext, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, pen);
            }
        }

        /// <summary>
        /// 塗りつぶされた矩形を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        public static void FillRect(Graphics deviceContext, RectangleF rectangle, Color color)
        {
            FillRect(deviceContext, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color, color);
        }

        #endregion

        #region 円の描画

        /// <summary>
        /// 指定された矩形領域に外接する四角形によって定義された、円を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        public static void DrawEllipse(Graphics deviceContext, float x, float y, float width, float height, Color color)
        {
            var mode = deviceContext.SmoothingMode;
            deviceContext.SmoothingMode = SmoothingMode.AntiAlias;

            using (var pen = CreateNormalPen(color))
            {
                deviceContext.DrawEllipse(pen, x, y, width, height);
            }

            deviceContext.SmoothingMode = mode;
        }

        /// <summary>
        /// 指定された矩形領域に外接する四角形によって定義された、円を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        public static void DrawEllipse(Graphics deviceContext, Rectangle rectangle, Color color)
        {
            DrawEllipse(deviceContext, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color);
        }

        /// <summary>
        /// 指定された矩形領域に外接する四角形によって定義された、円を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        public static void DrawEllipse(Graphics deviceContext, RectangleF rectangle, Color color)
        {
            DrawEllipse(deviceContext, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color);
        }

        #endregion

        #region 円（塗りつぶし）の描画

        /// <summary>
        /// 指定された矩形領域に外接する四角形によって定義された、塗りつぶされた円を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        public static void FillEllipse(Graphics deviceContext, float x, float y, float width, float height, Color color)
        {
            var mode = deviceContext.SmoothingMode;
            deviceContext.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(color))
            {
                deviceContext.FillEllipse(brush, x, y, width, height);
            }

            deviceContext.SmoothingMode = mode;
        }

        /// <summary>
        /// 指定された矩形領域に外接する四角形によって定義された、塗りつぶされた円を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        public static void FillEllipse(Graphics deviceContext, Rectangle rectangle, Color color)
        {
            FillEllipse(deviceContext, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color);
        }

        /// <summary>
        /// 指定された矩形領域に外接する四角形によって定義された、塗りつぶされた円を描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        public static void FillEllipse(Graphics deviceContext, RectangleF rectangle, Color color)
        {
            FillEllipse(deviceContext, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color);
        }

        #endregion

        #region ポリゴンの描画

        /// <summary>
        /// 指定された座標で表現されるポリゴンを、指定された色で描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="points"></param>
        /// <param name="color"></param>
        public static void DrawPolygon(Graphics deviceContext, Point[] points, Color color)
        {
            using (var pen = new Pen(color))
            {
                deviceContext.DrawPolygon(pen, points);
            }
        }

        /// <summary>
        /// 指定された座標で表現されるポリゴンを、指定された色で描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="color"></param>
        /// <param name="points"></param>
        public static void DrawPolygon(Graphics deviceContext, Color color, params Point[] points)
        {
            using (var pen = new Pen(color))
            {
                deviceContext.DrawPolygon(pen, points);
            }
        }

        /// <summary>
        /// 指定された座標で表現されるポリゴンを、指定された色で描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="points"></param>
        /// <param name="color"></param>
        public static void DrawPolygon(Graphics deviceContext, PointF[] points, Color color)
        {
            using (var pen = new Pen(color))
            {
                deviceContext.DrawPolygon(pen, points);
            }
        }

        /// <summary>
        /// 指定された座標で表現されるポリゴンを、指定された色で描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="color"></param>
        /// <param name="points"></param>
        public static void DrawPolygon(Graphics deviceContext, Color color, params PointF[] points)
        {
            using (var pen = new Pen(color))
            {
                deviceContext.DrawPolygon(pen, points);
            }
        }

        #endregion

        #region ポリゴン（塗りつぶし）の描画

        /// <summary>
        /// 指定された座標で表現されるポリゴンを、指定された色で塗りつぶして描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="points"></param>
        /// <param name="color"></param>
        public static void FillPolygon(Graphics deviceContext, Point[] points, Color color)
        {
            using (var brush = new SolidBrush(color))
            {
                deviceContext.FillPolygon(brush, points);
            }
        }

        /// <summary>
        /// 指定された座標で表現されるポリゴンを、指定された色で塗りつぶして描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="color"></param>
        /// <param name="points"></param>
        public static void FillPolygon(Graphics deviceContext, Color color, params Point[] points)
        {
            using (var brush = new SolidBrush(color))
            {
                deviceContext.FillPolygon(brush, points);
            }
        }

        /// <summary>
        /// 指定された座標で表現されるポリゴンを、指定された色で塗りつぶして描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="points"></param>
        /// <param name="color"></param>
        public static void FillPolygon(Graphics deviceContext, PointF[] points, Color color)
        {
            using (var brush = new SolidBrush(color))
            {
                deviceContext.FillPolygon(brush, points);
            }
        }

        /// <summary>
        /// 指定された座標で表現されるポリゴンを、指定された色で塗りつぶして描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="color"></param>
        /// <param name="points"></param>
        public static void FillPolygon(Graphics deviceContext, Color color, params PointF[] points)
        {
            using (var brush = new SolidBrush(color))
            {
                deviceContext.FillPolygon(brush, points);
            }
        }

        #endregion

        #region ビットマップイメージの描画

        /// <summary>
        /// 指定された位置に、ビットマップイメージを元の物理サイズで描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="img"></param>
        public static void DrawImageUnscaled(Graphics deviceContext, float x, float y, float width, float height, Image img)
        {
            deviceContext.DrawImageUnscaled(img, (int)x, (int)y, (int)width, (int)height);
        }

        /// <summary>
        /// 指定された位置に、ビットマップイメージを元の物理サイズで描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="img"></param>
        public static void DrawImageUnscaled(Graphics deviceContext, Rectangle rectangle, Image img)
        {
            DrawImageUnscaled(deviceContext, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, img);
        }

        /// <summary>
        /// 指定された位置に、ビットマップイメージを元の物理サイズで描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="img"></param>
        public static void DrawImageUnscaled(Graphics deviceContext, Point point, Image img)
        {
            DrawImageUnscaled(deviceContext, point.X, point.Y, img.Width, img.Height, img);
        }

        /// <summary>
        /// 指定された位置にビットマップイメージを描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="img"></param>
        public static void DrawImage(Graphics deviceContext, float x, float y, float width, float height, Image img)
        {
            deviceContext.DrawImage(img, (int)x, (int)y, (int)width, (int)height);
        }

        /// <summary>
        /// 指定された位置にビットマップイメージを描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="img"></param>
        public static void DrawImage(Graphics deviceContext, Rectangle rectangle, Image img)
        {
            DrawImageUnscaled(deviceContext, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, img);
        }

        /// <summary>
        /// 指定された位置にビットマップイメージを描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="rectangle"></param>
        /// <param name="img"></param>
        public static void DrawImage(Graphics deviceContext, Point point, Image img)
        {
            DrawImageUnscaled(deviceContext, point.X, point.Y, img.Width, img.Height, img);
        }

        #endregion

        #region テキストの描画

        /// <summary>
        /// テキストを描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="rectangle"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        /// <param name="textFormatFlags"></param>
        public static void DrawText(
            Graphics deviceContext,
            string text,
            Font font,
            Rectangle rectangle,
            Color foreColor,
            Color backColor,
            TextFormatFlags textFormatFlags)
        {
            TextRenderer.DrawText(
                    deviceContext,
                    text,
                    font,
                    rectangle,
                    foreColor,
                    backColor,
                    textFormatFlags);
        }

        /// <summary>
        /// テキストを描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        /// <param name="textFormatFlags"></param>
        public static void DrawText(
            Graphics deviceContext,
            string text,
            Font font,
            float x,
            float y,
            float width,
            float height,
            Color foreColor,
            Color backColor,
            TextFormatFlags textFormatFlags)
        {
            DrawText(
                deviceContext,
                text,
                font,
                new Rectangle((int)x, (int)y, (int)width, (int)height),
                foreColor,
                backColor,
                textFormatFlags);
        }

        /// <summary>
        /// 指定されたテキストを指定されたフォントで描画した場合のサイズを計測する。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public static Size MeasureText(string text, Font font)
        {
            return TextRenderer.MeasureText(text, font);
        }

        #endregion
    }
}
