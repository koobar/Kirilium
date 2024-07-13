using System.Drawing;

namespace Kirilium.Themes
{
    /// <summary>
    /// ダークテーマの配色を定義したクラスです。
    /// </summary>
    public class DarkTheme : Theme
    {
        // コンストラクタ
        public DarkTheme() : base()
        {
            this.UseDarkWindowTitleBar = true;

            var controlBackColorNormal = Color.FromArgb(53, 53, 53);
            var controlBackColorDisabled = Color.FromArgb(31, 31, 31);
            var windowBackColor = Color.FromArgb(31, 31, 31);
            var normalWhite = Color.FromArgb(240, 240, 240);
            var highlight = Color.FromArgb(0, 122, 204);

            SetColor(ColorKeys.ApplicationClearColor, Color.Black);

            // コントロールの境界線の配色
            SetColor(ColorKeys.ApplicationBorderNormal, Color.FromArgb(75, 75, 75));
            SetColor(ColorKeys.ApplicationBorderDisabled, Color.FromArgb(63, 63, 70));
            SetColor(ColorKeys.ApplicationBorderHighlight, highlight);

            // コントロールのテキストの配色
            SetColor(ColorKeys.ApplicationTextNormal, normalWhite);
            SetColor(ColorKeys.ApplicationTextHighlight, normalWhite);
            SetColor(ColorKeys.ApplicationTextDisabled, normalWhite);

            // ウィンドウの配色
            SetColor(ColorKeys.WindowBackColor, windowBackColor);

            // ボタンの配色
            SetColor(ColorKeys.ButtonBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ButtonBackColorDisabled, controlBackColorDisabled);
            SetColor(ColorKeys.ButtonBackColorMouseOver, Color.FromArgb(60, 60, 60));
            SetColor(ColorKeys.ButtonBackColorMouseClick, Color.FromArgb(75, 75, 75));

            // コンボボックスの配色
            SetColor(ColorKeys.ComboBoxBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ComboBoxBackColorDisabled, controlBackColorDisabled);
            SetColor(ColorKeys.ComboBoxItemBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ComboBoxItemBackColorSelected, highlight);
            SetColor(ColorKeys.ComboBoxTriangleIconColor, Color.FromArgb(153, 153, 153));

            // チェックボックスの配色
            SetColor(ColorKeys.CheckBoxBackColorNormal, windowBackColor);
            SetColor(ColorKeys.CheckBoxGlyphBackColor, windowBackColor);
            SetColor(ColorKeys.CheckBoxGlyphForeColor, normalWhite);

            // ラジオボタンの配色
            SetColor(ColorKeys.RadioButtonBackColorNormal, windowBackColor);
            SetColor(ColorKeys.RadioButtonGlyphBackColor, windowBackColor);
            SetColor(ColorKeys.RadioButtonGlyphForeColor, normalWhite);

            // リストボックスの配色
            SetColor(ColorKeys.ListBoxBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ListBoxBackColorDisabled, controlBackColorDisabled);

            // リストビューの配色
            SetColor(ColorKeys.ListViewBackColor, controlBackColorNormal);
            SetColor(ColorKeys.ListViewItemBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ListViewItemBackColorSelected, highlight);
            SetColor(ColorKeys.ListViewColumnHeaderBackColorNormal, Color.FromArgb(60, 60, 60));
            SetColor(ColorKeys.ListViewColumnHeaderBackColorMouseOver, Color.FromArgb(70, 70, 70));
            SetColor(ColorKeys.ListViewColumnHeaderBackColorMouseClick, Color.FromArgb(75, 75, 75));

            // テキストボックスの配色
            SetColor(ColorKeys.TextBoxBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.TextBoxBackColorDisabled, controlBackColorDisabled);
            SetColor(ColorKeys.TextBoxBackColorReadOnly, controlBackColorDisabled);

            // プログレスバーの配色
            SetColor(ColorKeys.ProgressBarBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ProgressBarBackColorDisabled, controlBackColorDisabled);
            SetColor(ColorKeys.ProgressBarForeColorNormal, highlight);

            // スクロールバーの配色
            SetColor(ColorKeys.ScrollBarBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ScrollBarBackColorDisabled, controlBackColorDisabled);
            SetColor(ColorKeys.ScrollBarSlideAreaBackColor, windowBackColor);
            SetColor(ColorKeys.ScrollBarThumbColor, Color.FromArgb(128, 128, 128));
            SetColor(ColorKeys.ScrollBarTriangleIconColor, Color.FromArgb(153, 153, 153));

            // メニューストリップの配色
            SetColor(ColorKeys.MenuStripBackColor, windowBackColor);

            // メニューアイテムの配色
            SetColor(ColorKeys.ToolStripMenuItemNormalBackColor, windowBackColor);
            SetColor(ColorKeys.ToolStripMenuItemHotBackColor, Color.FromArgb(62, 62, 62));
            SetColor(ColorKeys.ToolStripMenuItemGlyphColor, normalWhite);

            // ツールストリップ
            SetColor(ColorKeys.ToolStripBackColor, windowBackColor);

            // ステータスストリップ
            SetColor(ColorKeys.StatusStripBackColor, controlBackColorNormal);

            // タブコントロールの配色
            SetColor(ColorKeys.TabControlHeaderBackColorNormal, windowBackColor);
            SetColor(ColorKeys.TabControlHeaderBackColorHot, Color.FromArgb(28, 151, 234));
            SetColor(ColorKeys.TabControlHeaderBackColorSelected, highlight);
            SetColor(ColorKeys.TabControlHeaderCloseButtonBackColorHot, Color.FromArgb(200, 32, 32));
            SetColor(ColorKeys.TabControlContentAreaBackColor, controlBackColorNormal);

            // グループボックスの配色
            SetColor(ColorKeys.GroupBoxBackColorNormal, windowBackColor);

            // シークバーの配色
            SetColor(ColorKeys.SeekBarBackColor, controlBackColorNormal);
            SetColor(ColorKeys.SeekBarLineColor, Color.FromArgb(75, 75, 75));
            SetColor(ColorKeys.SeekbarThumbColor, Color.FromArgb(0, 122, 204));

            // ラベルの配色
            SetColor(ColorKeys.LabelBackColor, windowBackColor);

            // パネルの配色
            SetColor(ColorKeys.PanelBackColor, windowBackColor);

            // 詳細リストの配色
            SetColor(ColorKeys.KDetailsListBackColor, controlBackColorNormal);
            SetColor(ColorKeys.KDetailsListSelectedColor, highlight);
        }
    }
}
