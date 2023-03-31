﻿using Nhom1_QuanLyHocVu.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom1_QuanLyHocVu.Dialog
{
    public partial class TaoDamNhiemMon : Form
    {
        QuanLyHocVuEntities entities = new QuanLyHocVuEntities();
        public TaoDamNhiemMon()
        {
            InitializeComponent();

            RenderData();
        }

        private void RenderData()
        {
            txtGiaoVien.DisplayMember = "HoTen";
            txtGiaoVien.ValueMember = "MaGiaoVien";
            txtGiaoVien.DataSource = entities.GIAOVIENs.Select(x => new {x.HoTen, x.MaGiaoVien}).ToList();

            txtChuongTrinh.DisplayMember = "TenChuongTrinh";
            txtChuongTrinh.ValueMember = "MaChuongTrinh";
            txtChuongTrinh.DataSource = entities.CHUONGTRINHs.Select(x=>new { x.MaChuongTrinh, x.TenChuongTrinh}).ToList();

            txtMonHoc.DisplayMember = "TenMonHoc";
            txtMonHoc.ValueMember = "MaMonHoc";
            txtMonHoc.DataSource = entities.MONHOCs.Select(x=>new {x.TenMonHoc, x.MaMonHoc}).ToList();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGiaoVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
