# Kirilium
Kirilium は、Windows Forms 向けのコントロールを提供するライブラリです。

## 特徴
Kirilium には、次のような便利な機能があります。  

 - テーマ機能  
   Kirilium には、便利なテーマ機能があり、コントロールの配色を自由自在に変更可能です。  
   また、「Dark」テーマと「Light」テーマがあらかじめ用意されており、このテーマを使用するだけで  
   Windows Forms でダーク・ライトテーマを実装することができます。

  - テーマが反映されたダイアログ  
    Kirilium では、Kirilium のコントロールを用いて再設計されたメッセージボックスが提供されています。  
    もちろん、これらのダイアログの配色も、テーマ機能を用いて変更可能です。

  - 標準の Windows Forms コントロールと共存可能  
    Kirilium で提供されるコントロールと、Windows Forms 標準のコントロールは共存可能で、  
    同時に使用することができます。

![ライトテーマ](screenshot/light.png)
![ダークテーマ](screenshot/dark.png)

## 使い方
ここでは、Kirilium の使用方法について、簡単にご説明します。具体的な使用方法については、サンプルアプリケーションを参考にしてください。

### エントリポイント
Kirilium を使用したアプリケーションを作成するには、まず、プロジェクトに Kirilium の DLL を参照する設定を追加する必要があります。
Kirilium の DLL を参照するには、次のコードを、プロジェクトファイル(.csproj)に記述してください。

```
<ItemGroup>
    <Reference Include="Kirilium.dll"/>
</ItemGroup>
```

次に、エントリポイントを下記のように記述します。

```cs
using Kirilium;
using Kirilium.Themes;
using System;
using System.Windows.Forms;

namespace SampleApplication
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // アプリケーションのテーマを設定します。
            // 次の例は、ライトテーマを適用するコードです。
            ThemeManager.Init(new LightTheme());        // ThemeManager.Init(new DarkTheme()); でダークテーマになります。

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
```

### Kirilium のコントロール
Kirilium のコントロールは、KButtonやKComboBoxなど、Kxxxのようなネーミングのクラスとして提供されます。
これらのコントロールを、Windows Forms と同様の方法で配置することで、Kirilium のコントロールを使用することができます。

### テーマの動的な変更
エントリポイントで設定したテーマを起動後に変更したい場合、次のコードで変更可能です。

```cs
ThemeManager.CurrentTheme = new DarkTheme();
```

但し、ウィンドウのタイトルバーなど、一部の配色は正常に変更されない場合があります。

## 動作環境
Kirilium は、次の要件を満たす環境で動作します。  

  - .NET 6 またはそれ以降
  - Windows 10 以降

## ライセンス
Kirilium は、MITライセンスで配布されるオープンソースソフトウェアです。
MITライセンスにご同意いただける場合、商用・非商用を問わず、自由にご使用いただけます。