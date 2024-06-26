﻿using Kirilium.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Media;
using System.Windows.Forms;

namespace Kirilium.Dialogs
{
    public static class ThemedMessageBox
    {
        // 非公開フィールド
        private static readonly Dictionary<string, Dictionary<string, string>> buttonTextTranslateWordDictionary = new Dictionary<string, Dictionary<string, string>>();
        private static bool buttonTextDictionaryInitialized = false;

        #region 非公開メソッド

        /// <summary>
        /// メッセージボックスのボタンのテキストのローカライズ用辞書を生成する。
        /// </summary>
        private static void InitTranslateDictionary()
        {
            if (buttonTextDictionaryInitialized)
            {
                return;
            }

            // 英語->日本語翻訳用辞書の作成
            var jp = new Dictionary<string, string>();
            jp.Add("OK", "OK");
            jp.Add("Cancel", "キャンセル");
            jp.Add("Abort", "中止");
            jp.Add("Retry", "再試行");
            jp.Add("Ignore", "無視");
            jp.Add("Yes", "はい");
            jp.Add("No", "いいえ");
            buttonTextTranslateWordDictionary.Add("ja-JP", jp);
            
            // 初期化済みフラグを設定
            buttonTextDictionaryInitialized = true;
        }

        /// <summary>
        /// 指定されたテキストの行数を取得する。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static int GetLineCount(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            // 改行コードをLFで統一
            text = text.Replace("\r\n", "\n");
            text = text.Replace("\r", "\n");

            // 行数の取得
            int cnt = 1;
            int pos = 0;
            while (true)
            {
                int idx = text.IndexOf("\n", pos);

                if (idx != -1)
                {
                    pos = idx + 1;
                    cnt++;
                }
                else
                {
                    break;
                }
            }

            return cnt;
        }

        /// <summary>
        /// 指定された種類に対応するシステムアイコンを取得する。
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        private static Icon GetIcon(MessageBoxIcon icon)
        {
            if (icon == MessageBoxIcon.Asterisk || icon == MessageBoxIcon.Information)
            {
                return SystemIcons.Information;
            }
            else if (icon == MessageBoxIcon.Hand || icon == MessageBoxIcon.Error || icon == MessageBoxIcon.Stop)
            {
                return SystemIcons.Error;
            }
            else if (icon == MessageBoxIcon.Exclamation || icon == MessageBoxIcon.Warning)
            {
                return SystemIcons.Warning;
            }
            else if (icon == MessageBoxIcon.Question)
            {
                return SystemIcons.Question;
            }

            return SystemIcons.WinLogo;
        }

        /// <summary>
        /// ボタンのテキストを、OSの言語に翻訳する。翻訳用辞書がなければ英語のまま返す。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string TranslateButtonText(string text)
        {
            InitTranslateDictionary();

            var lang = CultureInfo.CurrentCulture.Name;

            if (buttonTextTranslateWordDictionary.ContainsKey(lang))
            {
                if (buttonTextTranslateWordDictionary[lang].ContainsKey(text))
                {
                    text = buttonTextTranslateWordDictionary[lang][text];
                }
            }

            return text;
        }

        /// <summary>
        /// メッセージボックスに表示するためのボタンを作成する。
        /// </summary>
        /// <param name="window"></param>
        /// <param name="buttonParent"></param>
        /// <param name="text"></param>
        /// <param name="dialogResult"></param>
        /// <returns></returns>
        private static KButton CreateButton(KWindow window, FlowLayoutPanel buttonParent, string text, DialogResult dialogResult)
        {
            var btn = new KButton();
            btn.Text = TranslateButtonText(text);
            btn.Parent = buttonParent;
            btn.Click += delegate
            {
                window.DialogResult = dialogResult;
                window.Close();
            };

            return btn;
        }

        /// <summary>
        /// メッセージダイアログを生成する。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        internal static KWindow CreateMessageBoxWindow(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            var dialog = new KWindow();
            dialog.Padding = new Padding(5);
            dialog.Text = title;
            dialog.Size = new Size(300, Math.Max((dialog.Font.Height + 2) * GetLineCount(message), 130));
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialog.MaximizeBox = false;
            dialog.MinimizeBox = false;
            dialog.DialogResult = DialogResult.Cancel;
            dialog.BackgroundImageLayout = ImageLayout.Center;
            dialog.Shown += delegate
            {
                PlaySound(icon);
            };
            dialog.FormClosed += delegate
            {
                dialog.Dispose();
            };

            var mainContent = new Panel();
            mainContent.BackColor = Color.Transparent;
            mainContent.Dock = DockStyle.Fill;
            mainContent.Parent = dialog;

            var messageLabel = new Label();
            messageLabel.BackColor = Color.Transparent;
            messageLabel.ForeColor = ThemeManager.CurrentTheme.GetColor(Themes.ColorKeys.ApplicationTextNormal);
            messageLabel.Parent = mainContent;
            messageLabel.Dock = DockStyle.Fill;
            messageLabel.Text = message;
            messageLabel.AutoSize = false;
            messageLabel.Padding = new Padding(15, 5, 15, 0);

            var iconBox = new PictureBox();
            iconBox.Size = new Size(48, 48);
            iconBox.Dock = DockStyle.Left;
            iconBox.Parent = mainContent;
            iconBox.Padding = new Padding(5) ;

            var bottomPanel = new FlowLayoutPanel();
            bottomPanel.Parent = dialog;
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.BackColor = Color.Transparent;
            bottomPanel.Height = 30;
            bottomPanel.FlowDirection = FlowDirection.RightToLeft;

            if (icon == MessageBoxIcon.None)
            {
                iconBox.Visible = false;
            }
            else
            {
                iconBox.Image = GetIcon(icon).ToBitmap();
            }

            switch (buttons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        CreateButton(dialog, bottomPanel, "Abort", DialogResult.Abort);
                        CreateButton(dialog, bottomPanel, "Retry", DialogResult.Retry);
                        CreateButton(dialog, bottomPanel, "Ignore", DialogResult.Ignore);
                    }
                    break;
                case MessageBoxButtons.OK:
                    {
                        CreateButton(dialog, bottomPanel, "OK", DialogResult.OK);
                    }
                    break;
                case MessageBoxButtons.OKCancel:
                    {
                        CreateButton(dialog, bottomPanel, "OK", DialogResult.OK);
                        CreateButton(dialog, bottomPanel, "Cancel", DialogResult.Cancel);
                    }
                    break;
                case MessageBoxButtons.RetryCancel:
                    {
                        CreateButton(dialog, bottomPanel, "Retry", DialogResult.Retry);
                        CreateButton(dialog, bottomPanel, "Cancel", DialogResult.Retry);
                    }
                    break;
                case MessageBoxButtons.YesNo:
                    {
                        CreateButton(dialog, bottomPanel, "Yes", DialogResult.Yes);
                        CreateButton(dialog, bottomPanel, "No", DialogResult.No);
                    }
                    break;
                case MessageBoxButtons.YesNoCancel:
                    {
                        CreateButton(dialog, bottomPanel, "Yes", DialogResult.Yes);
                        CreateButton(dialog, bottomPanel, "No", DialogResult.No);
                        CreateButton(dialog, bottomPanel, "Cancel", DialogResult.Cancel);
                    }
                    break;
            }

            return dialog;
        }

        /// <summary>
        /// 指定されたメッセージボックスのアイコンを持つメッセージボックスが表示された場合の効果音を再生する。
        /// </summary>
        /// <param name="icon"></param>
        private static void PlaySound(MessageBoxIcon icon)
        {
            if (icon == MessageBoxIcon.Asterisk || icon == MessageBoxIcon.Information)
            {
                SystemSounds.Asterisk.Play();
            }
            else if (icon == MessageBoxIcon.Hand || icon == MessageBoxIcon.Error || icon == MessageBoxIcon.Stop)
            {
                SystemSounds.Hand.Play();
            }
            else if (icon == MessageBoxIcon.Exclamation || icon == MessageBoxIcon.Warning)
            {
                SystemSounds.Exclamation.Play();
            }
            else if (icon == MessageBoxIcon.Question)
            {
                SystemSounds.Question.Play();
            }
        }

        #endregion

        /// <summary>
        /// メッセージボックスを表示する。
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DialogResult Show(string message)
        {
            return CreateMessageBoxWindow(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.None).ShowDialog();
        }

        /// <summary>
        /// メッセージボックスを表示する。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static DialogResult Show(string message, string title)
        {
            return CreateMessageBoxWindow(message, title, MessageBoxButtons.OK, MessageBoxIcon.None).ShowDialog();
        }

        /// <summary>
        /// メッセージボックスを表示する。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public static DialogResult Show(string message, string title, MessageBoxButtons buttons)
        {
            return CreateMessageBoxWindow(message, title, buttons, MessageBoxIcon.None).ShowDialog();
        }

        /// <summary>
        /// メッセージボックスを表示する。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return CreateMessageBoxWindow(message, title, buttons, icon).ShowDialog();
        }
    }
}
