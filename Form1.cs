using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        // 座標保管用変数
        int Mx; int My;

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load_1;
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
        }
        // 画像の読み込み
        private void Form1_Load_1(object sender, EventArgs e)
        {
            string Path = @"D:\Users\Yuri\Desktop\Pictures\wado.png";
            show(Path);
        }

        // 画像の設定
        private void show(string Path)
        {
            //フォームの境界線をなくす
            this.FormBorderStyle = FormBorderStyle.None;
            // フォームのサイズ変更
            SizeChange(@Path);
            // 格納しているサイズに合わせて描画させる
            this.BackgroundImageLayout = ImageLayout.Stretch;
            // 背景画像の登録
            this.BackgroundImage = Image.FromFile(@Path);
            //部分を透明化する
            this.TransparencyKey = Color.White;
        }

        // ウィンドウの大きさを画像の大きさに変更
        private void SizeChange(string Path)
        {
            // 元画像の縦横サイズを調べる
            System.Drawing.Bitmap bmpSrc = new System.Drawing.Bitmap(@Path);
            int width = bmpSrc.Width;
            int height = bmpSrc.Height;

            // 画像の大きさを比率を保持したまま一定のサイズまで縮小
            float Ix = width, Iy = height;
            float Md = 1.00f, d = 0.01f;
            if (Ix > Iy)
            {
                // Ixの比率を計算した結果が500を下回るまで割合計算
                for (; Ix * Md > 400; Md -= d) ;
                Ix = Md; Iy = Md;
            }
            else if (Ix < Iy)
            {
                // Iyの比率を計算した結果が500を下回るまで割合計算
                for (; (double)Iy * Md > 400; Md -= d) ;
                Iy = Md; Ix = Md;
            }
            // ウィンドウサイズの変更
            this.Size = new Size((int)Ix, (int)Iy);
        }

        // マウスが押された時の発生イベントハンドラ
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // 押されたマウスのボタンが右以外だったら以下の処理をしない
            if (e.Button != MouseButtons.Right) return;

            // マウスの座標を保管
            this.Mx = e.X; this.My = e.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // ボタンを押したときの座標と離した現在の場所の座標の差分を求める
            int x = e.X - this.Mx; int y = e.Y - this.My;

            // 現在表示されているデスクトップ上の座標から
            this.DesktopLocation = new Point(this.DesktopLocation.X + x, this.DesktopLocation.Y + y);
            // テスト用出力
            Console.WriteLine((this.DesktopLocation.X + x) + "," + (this.DesktopLocation.Y + y));
        }

    }
}