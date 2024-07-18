using Kirilium.Themes;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;

namespace Kirilium
{
    [SupportedOSPlatform("windows")]
    internal static class IconRenderer
    {
        #region 4方向の三角形（塗りつぶし）の描画の実装

        /// <summary>
        /// 頂点が上に位置する塗りつぶされた三角形を生成する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static Image CreateFilledTopTriangleImage(int width, int height, Color color)
        {
            var img = new Bitmap(width, height);
            var renderer = Graphics.FromImage(img);

            Renderer.FillPolygon(renderer, color, new Point(width / 2, 0), new Point(0, height), new Point(width, height));

            renderer.Dispose();
            return img;
        }

        /// <summary>
        /// 頂点が左に位置する塗りつぶされた三角形を生成する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static Image CreateFilledLeftTriangleImage(int width, int height, Color color)
        {
            var img = new Bitmap(width, height);
            var renderer = Graphics.FromImage(img);

            Renderer.FillPolygon(renderer, color, new Point(0, height / 2), new Point(width, 0), new Point(width, height));

            renderer.Dispose();
            return img;
        }

        /// <summary>
        /// 頂点が下に位置する塗りつぶされた三角形を生成する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static Image CreateFilledBottomTriangleImage(int width, int height, Color color)
        {
            var img = new Bitmap(width, height);
            var renderer = Graphics.FromImage(img);

            Renderer.FillPolygon(renderer, color, new Point(0, 0), new Point(width, 0), new Point(width / 2, height));

            renderer.Dispose();
            return img;
        }

        /// <summary>
        /// 頂点が右に位置する塗りつぶされた三角形を生成する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static Image CreateFilledRightTriangleImage(int width, int height, Color color)
        {
            var img = new Bitmap(width, height);
            var renderer = Graphics.FromImage(img);

            Renderer.FillPolygon(renderer, color, new Point(width, height / 2), new Point(0, 0), new Point(0, height));

            renderer.Dispose();
            return img;
        }

        #endregion

        #region コンボボックス用アイコン

        /// <summary>
        /// コンボボックスの右側に描画される三角形を取得する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetComboBoxTriangle(int width, int height)
        {
            return CreateFilledBottomTriangleImage(width, height, ThemeManager.CurrentTheme.GetColor(ColorKeys.ComboBoxTriangleIconColor));
        }

        #endregion

        #region チェックボックス用アイコン

        /// <summary>
        /// チェックボックスのチェック状態の図形を取得する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetCheckBoxCheckedGlyph(int width, int height)
        {
            var result = new Bitmap(width, height);
            var deviceContext = Graphics.FromImage(result);
            deviceContext.SmoothingMode = SmoothingMode.AntiAlias;

            // 描画処理
            var color = ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemGlyphColor);
            Renderer.DrawLine(deviceContext, 3, height / 2, width / 3, height - 3, color);
            Renderer.DrawLine(deviceContext, 3, height / 2, width / 3, height - 3, color);
            Renderer.DrawLine(deviceContext, width / 3, height - 3, width - 3, 3, color);

            deviceContext.Dispose();
            return result;
        }

        /// <summary>
        /// チェックボックスの不確定状態の図形を取得する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetCheckBoxIndeterminateGlyph(int width, int height)
        {
            var result = new Bitmap(width, height);
            var deviceContext = Graphics.FromImage(result);

            Renderer.FillRect(deviceContext, 3, 3, width - 6, height - 6, ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemGlyphColor));

            deviceContext.Dispose();
            return result;
        }

        #endregion

        #region スクロールバー用アイコン

        /// <summary>
        /// 水平方向のスクロールバーのインクリメントボタンのアイコンを取得する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetHScrollBarIncrementButtonIcon(int width, int height)
        {
            return CreateFilledRightTriangleImage(width, height, ThemeManager.CurrentTheme.GetColor(ColorKeys.ScrollBarTriangleIconColor));
        }

        /// <summary>
        /// 水平方向のスクロールバーのデクリメントボタンのアイコンを取得する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetHScrollBarDecrementButtonIcon(int width, int height)
        {
            return CreateFilledLeftTriangleImage(width, height, ThemeManager.CurrentTheme.GetColor(ColorKeys.ScrollBarTriangleIconColor));
        }

        /// <summary>
        /// 垂直方向のスクロールバーのインクリメントボタンのアイコンを取得する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetVScrollBarIncrementButtonIcon(int width, int height)
        {
            return CreateFilledBottomTriangleImage(width, height, ThemeManager.CurrentTheme.GetColor(ColorKeys.ScrollBarTriangleIconColor));
        }

        /// <summary>
        /// 垂直方向のスクロールバーのデクリメントボタンのアイコンを取得する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetVScrollBarDecrementButtonIcon(int width, int height)
        {
            return CreateFilledTopTriangleImage(width, height, ThemeManager.CurrentTheme.GetColor(ColorKeys.ScrollBarTriangleIconColor));
        }

        #endregion

        #region ラジオボタン用アイコン

        /// <summary>
        /// ラジオボタンのチェック状態の図形を取得する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetRadioButtonCheckedGlyph(int width, int height)
        {
            var result = new Bitmap(width, height);
            var renderer = Graphics.FromImage(result);
            var ellipseWidth = 8;
            var ellipseHeight = 8;

            Renderer.FillEllipse(
                renderer,
                (width / 2) - (ellipseWidth / 2),
                (height / 2) - (ellipseHeight / 2),
                ellipseWidth,
                ellipseHeight,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemGlyphColor));

            renderer.Dispose();
            return result;
        }

        #endregion

        #region メニューアイテム用アイコン

        /// <summary>
        /// メニューアイテムのチェック状態の図形を取得する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetToolStripMenuItemCheckedGlyph(int width, int height)
        {
            var result = new Bitmap(width, height);
            var renderer = Graphics.FromImage(result);
            var color = ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemGlyphColor);
            renderer.SmoothingMode = SmoothingMode.AntiAlias;

            Renderer.DrawLine(renderer, 3, height / 2, width / 3, height - 3, color);
            Renderer.DrawLine(renderer, width / 3, height - 3, width - 3, 3, color);

            renderer.Dispose();
            return result;
        }

        /// <summary>
        /// メニューアイテムの不確定状態時の図形を取得する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetToolStripMenuItemIndeterminateGlyph(int width, int height)
        {
            var result = new Bitmap(width, height);
            var renderer = Graphics.FromImage(result);
            var ellipseWidth = 8;
            var ellipseHeight = 8;

            Renderer.FillEllipse(
                renderer,
                (width / 2) - (ellipseWidth / 2),
                (height / 2) - (ellipseHeight / 2),
                ellipseWidth,
                ellipseHeight,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemGlyphColor));

            renderer.Dispose();
            return result;
        }

        public static Image GetToolStripMenuItemHasDropdownGlyph(int width, int height)
        {
            return CreateFilledRightTriangleImage(width, height, ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemGlyphColor));
        }

        #endregion

        #region 閉じるボタン

        /// <summary>
        /// 閉じるボタンの画像を取得する。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="backColor"></param>
        /// <param name="foreColor"></param>
        /// <returns></returns>
        public static Image GetCloseButton(int width, int height, Color backColor, Color foreColor)
        {
            var result = new Bitmap(width, height);
            var renderer = Graphics.FromImage(result);

            // 塗りつぶし
            Renderer.FillRect(renderer, 0, 0, width, height, backColor);

            // バツ印の描画
            var img = new Bitmap(width, height);
            var g = Graphics.FromImage(img);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(foreColor, 1))
            {
                g.DrawLine(pen, 3, 3, img.Width - 3, img.Height - 3);
                g.DrawLine(pen, 3, img.Height - 3, img.Width - 3, 3);
            }

            // バツ印のイメージを中央に描画する。
            Renderer.DrawImageUnscaled(
                renderer,
                (result.Width / 2) - (img.Width / 2),
                (result.Height / 2) - (img.Height / 2) - 1,
                img.Width,
                img.Height,
                img);

            // 後始末
            renderer.Dispose();
            g.Dispose();
            img.Dispose();

            return result;
        }

        #endregion
    }
}
