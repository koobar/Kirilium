using System.Drawing;

namespace Kirilium.Themes
{
    public class LightTheme : Theme
    {
        public LightTheme() : base()
        {
            this.UseDarkWindowTitleBar = false;

            var windowBackColor = Color.FromArgb(245, 245, 245);
            var controlBackColorNormal = Color.FromArgb(235, 235, 235);
            var controlBackColorDisabled = Color.FromArgb(195, 195, 195);
            var normalTextColor = Color.FromArgb(5, 5, 5);
            var highlightTextColor = Color.FromArgb(240, 240, 240);
            var disabledTextColor = Color.FromArgb(162, 164, 165);
            var glyphForeColor = Color.FromArgb(5, 5, 5);

            // アプリケーション全体の配色
            SetColor(ColorKeys.ApplicationClearColor, Color.Black);

            // コントロールの境界線の配色
            SetColor(ColorKeys.ApplicationBorderNormal, Color.FromArgb(204, 206, 219));
            SetColor(ColorKeys.ApplicationBorderDisabled, Color.FromArgb(204, 206, 219));
            SetColor(ColorKeys.ApplicationBorderHighlight, Color.FromArgb(0, 122, 204));

            // コントロールのテキストの配色
            SetColor(ColorKeys.ApplicationTextNormal, normalTextColor);
            SetColor(ColorKeys.ApplicationTextHighlight, highlightTextColor);
            SetColor(ColorKeys.ApplicationTextDisabled, disabledTextColor);

            // ウィンドウの配色
            SetColor(ColorKeys.WindowBackColor, windowBackColor);

            // ボタンの配色
            SetColor(ColorKeys.ButtonBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ButtonBackColorDisabled, controlBackColorDisabled);
            SetColor(ColorKeys.ButtonBackColorMouseOver, Color.FromArgb(201, 222, 245));
            SetColor(ColorKeys.ButtonBackColorMouseClick, Color.FromArgb(191, 212, 235));

            // コンボボックスの配色
            SetColor(ColorKeys.ComboBoxBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ComboBoxBackColorDisabled, controlBackColorDisabled);
            SetColor(ColorKeys.ComboBoxItemBackColorNormal, Color.FromArgb(246, 246, 246));
            SetColor(ColorKeys.ComboBoxItemBackColorSelected, Color.FromArgb(201, 222, 245));
            SetColor(ColorKeys.ComboBoxTriangleIconColor, Color.FromArgb(153, 153, 153));

            // チェックボックスの配色
            SetColor(ColorKeys.CheckBoxBackColorNormal, windowBackColor);
            SetColor(ColorKeys.CheckBoxGlyphBackColor, windowBackColor);
            SetColor(ColorKeys.CheckBoxGlyphForeColor, glyphForeColor);

            // ラジオボタンの配色
            SetColor(ColorKeys.RadioButtonBackColorNormal, windowBackColor);
            SetColor(ColorKeys.RadioButtonGlyphBackColor, windowBackColor);
            SetColor(ColorKeys.RadioButtonGlyphForeColor, glyphForeColor);

            // リストボックスの配色
            SetColor(ColorKeys.ListBoxBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ListBoxBackColorDisabled, controlBackColorDisabled);

            // リストビューの配色
            SetColor(ColorKeys.ListViewBackColor, controlBackColorNormal);
            SetColor(ColorKeys.ListViewItemBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ListViewItemBackColorSelected, Color.FromArgb(0, 122, 204));
            SetColor(ColorKeys.ListViewColumnHeaderBackColorNormal, Color.FromArgb(240, 240, 240));
            SetColor(ColorKeys.ListViewColumnHeaderBackColorMouseOver, Color.FromArgb(201, 222, 245));
            SetColor(ColorKeys.ListViewColumnHeaderBackColorMouseClick, Color.FromArgb(191, 212, 235));

            // テキストボックスの配色
            SetColor(ColorKeys.TextBoxBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.TextBoxBackColorDisabled, controlBackColorDisabled);
            SetColor(ColorKeys.TextBoxBackColorReadOnly, controlBackColorDisabled);

            // プログレスバーの配色
            SetColor(ColorKeys.ProgressBarBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ProgressBarBackColorDisabled, controlBackColorDisabled);
            SetColor(ColorKeys.ProgressBarForeColorNormal, Color.FromArgb(0, 122, 204));

            // スクロールバーの配色
            SetColor(ColorKeys.ScrollBarBackColorNormal, controlBackColorNormal);
            SetColor(ColorKeys.ScrollBarBackColorDisabled, controlBackColorDisabled);
            SetColor(ColorKeys.ScrollBarSlideAreaBackColor, windowBackColor);
            SetColor(ColorKeys.ScrollBarThumbColor, Color.FromArgb(194, 195, 201));
            SetColor(ColorKeys.ScrollBarTriangleIconColor, Color.FromArgb(153, 153, 153));

            // メニューストリップの配色
            SetColor(ColorKeys.MenuStripBackColor, windowBackColor);

            // メニューアイテムの配色
            SetColor(ColorKeys.ToolStripMenuItemNormalBackColor, windowBackColor);
            SetColor(ColorKeys.ToolStripMenuItemHotBackColor, Color.FromArgb(201, 222, 245));
            SetColor(ColorKeys.ToolStripMenuItemGlyphColor, glyphForeColor);

            // ツールストリップ
            SetColor(ColorKeys.ToolStripBackColor, windowBackColor);

            // ステータスストリップ
            SetColor(ColorKeys.StatusStripBackColor, controlBackColorNormal);

            // タブコントロールの配色
            SetColor(ColorKeys.TabControlHeaderBackColorNormal, windowBackColor);
            SetColor(ColorKeys.TabControlHeaderBackColorHot, Color.FromArgb(28, 151, 234));
            SetColor(ColorKeys.TabControlHeaderBackColorSelected, Color.FromArgb(0, 122, 204));
            SetColor(ColorKeys.TabControlHeaderCloseButtonBackColorHot, Color.FromArgb(28, 151, 234));
            SetColor(ColorKeys.TabControlContentAreaBackColor, controlBackColorNormal);

            // グループボックスの配色
            SetColor(ColorKeys.GroupBoxBackColorNormal, windowBackColor);

            // シークバーの配色
            SetColor(ColorKeys.SeekBarBackColor, controlBackColorNormal);
            SetColor(ColorKeys.SeekBarLineColor, Color.FromArgb(204, 206, 219));
            SetColor(ColorKeys.SeekbarThumbColor, Color.FromArgb(0, 122, 204));

            // ラベルの配色
            SetColor(ColorKeys.LabelBackColor, windowBackColor);

            // パネルの配色
            SetColor(ColorKeys.PanelBackColor, windowBackColor);
        }
    }
}
