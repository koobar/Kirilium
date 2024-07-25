using Kirilium.Themes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KControl : UserControl
    {
        // 非公開フィールド
        private readonly float scaleFactor;
        private bool flagMouseEnter;

        // コンストラクタ
        public KControl()
        {
            this.scaleFactor = this.DeviceDpi / 96.0f;

            // 描画設定
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            this.AutoScaleMode = AutoScaleMode.None;

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KControl()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        internal void InternalSetPadding(Padding padding)
        {
            base.Padding = padding;
        }

        #region プロパティ

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new BorderStyle BorderStyle { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color ForeColor { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AutoSize { set; get; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new AutoSizeMode AutoSizeMode { set; get; }

        /// <summary>
        /// コントロールがクリックされている最中であるかどうかを示す。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMouseClick { protected set; get; }

        /// <summary>
        /// マウスカーソルがコントロールの領域内に存在するかどうかを示す。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMouseOver
        {
            get
            {
                return this.flagMouseEnter;
            }
        }

        /// <summary>
        /// コントロールのテキスト
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new string Text
        {
            set
            {
                base.Text = value;
                Invalidate();
            }
            get
            {
                return base.Text;
            }
        }

        /// <summary>
        /// 親コントロールの左端からのピクセル数
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new int Left
        {
            set
            {
                base.Left = (int)(this.scaleFactor * value);
            }
            get
            {
                return (int)(base.Left / this.scaleFactor);
            }
        }

        /// <summary>
        /// 親コントロールの上端からのピクセル数
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new int Top
        {
            set
            {
                base.Top = (int)(this.scaleFactor * value);
            }
            get
            {
                return (int)(base.Top / this.scaleFactor);
            }
        }

        /// <summary>
        /// 幅
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new int Width
        {
            set
            {
                base.Width = (int)(this.scaleFactor * value);
            }
            get
            {
                return (int)(base.Width / this.scaleFactor);
            }
        }

        /// <summary>
        /// 高さ
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new int Height
        {
            set
            {
                base.Height = (int)(this.scaleFactor * value);
            }
            get
            {
                return (int)(base.Height / this.scaleFactor);
            }
        }

        #endregion

        #region 文字色の取得を実装したメソッド

        public static Color GetTextColor(bool enabled, bool highlight)
        {
            if (enabled)
            {
                if (highlight)
                {
                    return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextHighlight);
                }

                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);
            }
            else
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextDisabled);
            }
        }

        public static Color GetTextColor(bool enabled)
        {
            if (enabled)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);
            }
            else
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextDisabled);
            }
        }

        /// <summary>
        /// 文字色を取得する。
        /// </summary>
        /// <returns></returns>
        public Color GetTextColor()
        {
            return KControl.GetTextColor(this.Enabled);
        }

        #endregion

        #region 境界線の色の取得を実装したメソッド

        /// <summary>
        /// 指定された状態に最適な境界線の色を取得する。
        /// </summary>
        /// <param name="focused"></param>
        /// <param name="isMouseOver"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        public static Color GetBorderColor(bool focused, bool isMouseOver, bool isEnabled)
        {
            if (focused || isMouseOver)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderHighlight);
            }

            if (!isEnabled)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderDisabled);
            }

            return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
        }

        /// <summary>
        /// 境界線の色を取得する。
        /// </summary>
        /// <returns></returns>
        public Color GetBorderColor(bool highlight)
        {
            if (highlight || this.IsMouseOver)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderHighlight);
            }

            if (!this.Enabled)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderDisabled);
            }

            return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
        }

        /// <summary>
        /// 境界線の色を取得する。
        /// </summary>
        /// <returns></returns>
        public Color GetBorderColor()
        {
            if (this.Focused || this.IsMouseOver)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderHighlight);
            }

            if (!this.Enabled)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderDisabled);
            }

            return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
        }

        #endregion

        #region テーマの変更が発生した場合の処理の実装

        /// <summary>
        /// テーマが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnThemeChanged(object sender, EventArgs e)
        {
            OnThemeChanged();
        }

        /// <summary>
        /// テーマが変更された場合の処理
        /// </summary>
        protected virtual void OnThemeChanged()
        {
            Invalidate();
        }

        #endregion

        #region マウス処理のオーバーライド

        /// <summary>
        /// コントロールの領域にマウスカーソルが侵入した場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            this.flagMouseEnter = true;

            Invalidate();
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// コントロールの領域からマウスカーソルが離脱した場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            this.flagMouseEnter = false;

            Invalidate();
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// マウスのボタンが押下された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.IsMouseOver)
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.IsMouseClick = true;
                }
            }

            Invalidate();
            base.OnMouseDown(e);
        }

        /// <summary>
        /// マウスのボタンが離された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.IsMouseClick = false;

            Invalidate();
            base.OnMouseUp(e);
        }

        #endregion

        #region フォーカス処理のオーバーライド

        /// <summary>
        /// コントロールがフォーカスを得た場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            Invalidate();
            base.OnLostFocus(e);
        }

        /// <summary>
        /// コントロールがフォーカスを失った場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            Invalidate();
            base.OnGotFocus(e);
        }

        #endregion
    }
}
