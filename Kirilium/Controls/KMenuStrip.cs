using Kirilium.Controls.Elements;
using Kirilium.Themes;
using System;
using System.ComponentModel;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KMenuStrip : MenuStrip
    {
        // コンストラクタ
        public KMenuStrip()
        {
            base.RenderMode = ToolStripRenderMode.Professional;
            base.Renderer = new ToolStripProfessionalRenderer(new KColorTable());
            base.GripStyle = ToolStripGripStyle.Hidden;
            base.Padding = new Padding(3);
            base.AutoSize = false;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KMenuStrip()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region プロパティ

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ToolStripRenderMode RenderMode { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ToolStripGripStyle GripStyle { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding { get; set; }

        #endregion

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.MenuStripBackColor);

            e.Graphics.Clear(backColor);
            Kirilium.Renderer.FillRect(e.Graphics, this.DisplayRectangle, backColor);

            base.OnPaint(e);
        }
    }
}
