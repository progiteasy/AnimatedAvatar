﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace AnimatedAvatar
{
    public partial class MainForm : Form
    {
        private Point _previousPosition;
        private Animator _avatarAnimator;

        public MainForm()
        {
            InitializeComponent();
            InitialData.ReadIniFile();
            AnimationPictureBox.BackColor = InitialData.BackgroundColor;
            _avatarAnimator = new Animator();
            _avatarAnimator.FrameWasChanged += AnimationFrameWasChanged;
        }

        private void AnimationFrameWasChanged(object sender, EventArgs e)
        {
            AnimationPictureBox.Invalidate();
        }

        private void AnimationPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (_avatarAnimator.CurrentFrame != null)
            {
                e.Graphics.Clear(InitialData.BackgroundColor);
                e.Graphics.DrawImage(_avatarAnimator.CurrentFrame, 0, 0, _avatarAnimator.CurrentFrame.Width, _avatarAnimator.CurrentFrame.Height);
                GC.Collect();
            }
        }

        private void AnimationPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _previousPosition = MousePosition;
        }

        private void AnimationPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point currentPosition = MousePosition;
                Location = new Point(Location.X + (currentPosition.X - _previousPosition.X), Location.Y + (currentPosition.Y - _previousPosition.Y));
                _previousPosition = currentPosition;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.F1)
                    this.WindowState = FormWindowState.Minimized;
                else if (e.KeyCode == Keys.F2)
                    this.Close();
            }
        }
    }
}