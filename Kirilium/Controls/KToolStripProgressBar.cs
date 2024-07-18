using System;
using System.Runtime.Versioning;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
    public class KToolStripProgressBar : ToolStripControlHost
    {
        // イベント
        public event EventHandler MaximumValueChanged;
        public event EventHandler MinimumValueChanged;
        public event EventHandler ValueChanged;

        // コンストラクタ
        public KToolStripProgressBar() : base(new KProgressBar())
        {
            var ctrl = ((KProgressBar)this.Control);
            ctrl.MaximumValueChanged += delegate (object sender, EventArgs e)
            {
                this.MaximumValueChanged?.Invoke(sender, e);
            };
            ctrl.MinimumValueChanged += delegate (object sender, EventArgs e)
            {
                this.MinimumValueChanged?.Invoke(sender, e);
            };
            ctrl.ValueChanged += delegate (object sender, EventArgs e)
            {
                this.ValueChanged?.Invoke(sender, e);
            };

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KToolStripProgressBar()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region プロパティ

        /// <summary>
        /// 最大値
        /// </summary>
        public int MaximumValue
        {
            set
            {
                ((KProgressBar)this.Control).MaximumValue = value;
            }
            get
            {
                return ((KProgressBar)this.Control).MaximumValue;
            }
        }

        /// <summary>
        /// 最小値
        /// </summary>
        public int MinimumValue
        {
            set
            {
                ((KProgressBar)this.Control).MinimumValue = value;
            }
            get
            {
                return ((KProgressBar)this.Control).MinimumValue;
            }
        }

        /// <summary>
        /// 値
        /// </summary>
        public int Value
        {
            set
            {
                ((KProgressBar)this.Control).Value = value;
            }
            get
            {
                return ((KProgressBar)this.Control).Value;
            }
        }

        /// <summary>
        /// パーセントをテキストとして表示するかどうか
        /// </summary>
        public bool ShowPercentAsText
        {
            set
            {
                ((KProgressBar)this.Control).ShowPercentAsText = value;
            }
            get
            {
                return ((KProgressBar)this.Control).ShowPercentAsText;
            }
        }

        #endregion
    }
}
