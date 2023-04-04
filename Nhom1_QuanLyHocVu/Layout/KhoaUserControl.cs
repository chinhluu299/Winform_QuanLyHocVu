﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nhom1_QuanLyHocVu.Model;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Nhom1_QuanLyHocVu.Dialog;
using System.Runtime.Remoting.Contexts;

namespace Nhom1_QuanLyHocVu.Layout
{
    public partial class KhoaUserControl : UserControl
    {
        QuanLyHocVuEntities QuanLyHocVuEntities = new QuanLyHocVuEntities();
        private string lastedMK = "";
        public KhoaUserControl()
        {
            InitializeComponent();

            RenderKhoa();
        }

        private void btnTaoKhoa_Click(object sender, EventArgs e)
        {
            TaoKhoaDialog taoKhoaDialog = new TaoKhoaDialog();
            
            if(taoKhoaDialog.ShowDialog() == DialogResult.OK)
            {
                var maKhoa = taoKhoaDialog.txtTaoMaKhoa.Text;
                var tenKhoa = taoKhoaDialog.txtTaoTenKhoa.Text;

                KHOA newKhoa = new KHOA();
                newKhoa.MaKhoa = maKhoa;
                newKhoa.TenKhoa = tenKhoa;

                QuanLyHocVuEntities.KHOAs.Add(newKhoa);
                try {
                    int result = QuanLyHocVuEntities.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("Thêm khoa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RenderKhoa();
                    }
                    else
                    {
                        MessageBox.Show("Dữ liệu không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dữ liệu không hợp lệ hoặc bị trùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnTaoGV_Click(object sender, EventArgs e)
        {
            TaoGiaoVienDialog taoGiaoVienDialog = new TaoGiaoVienDialog();
            taoGiaoVienDialog.cboMaKhoaTaoGV.Text = lastedMK;

            if (taoGiaoVienDialog.ShowDialog() == DialogResult.OK)
            {
                var maGV = taoGiaoVienDialog.txtTaoMaGV.Text;
                var tenGV = taoGiaoVienDialog.txtTaoTenGV.Text;
                var maKhoa = taoGiaoVienDialog.cboMaKhoaTaoGV.Text;

                GIAOVIEN newGV = new GIAOVIEN();
                newGV.MaGiaoVien = maGV;
                newGV.HoTen = tenGV;
                newGV.MaKhoa = maKhoa;

                QuanLyHocVuEntities.GIAOVIENs.Add(newGV);
                try
                {
                    int result = QuanLyHocVuEntities.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("Thêm giáo viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RenderKhoa();
                    }
                    else
                    {
                        MessageBox.Show("Dữ liệu không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dữ liệu không hợp lệ hoặc bị trùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void RenderKhoa()
        {
            lsvKhoa.Items.Clear();
            var khoas = QuanLyHocVuEntities.KHOAs.ToList();
            foreach(var khoa in khoas)
            {
                ListViewItem item = new ListViewItem(khoa.TenKhoa);
                item.SubItems.Add(khoa.MaKhoa);
               
                lsvKhoa.Items.Add(item);
            }
        }

        private void RenderGV(string MaKhoa)
        {
            lsvGV.Items.Clear();
            var gvs = QuanLyHocVuEntities.GIAOVIENs.Where(x => x.MaKhoa == MaKhoa).ToList();
            foreach (var gv in gvs)
            {
                ListViewItem item = new ListViewItem(gv.HoTen);
                item.SubItems.Add(gv.MaGiaoVien);
             
                lsvGV.Items.Add(item);
            }
        }

        private void lsvKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvKhoa.SelectedItems.Count > 0) // kiểm tra xem có dòng được chọn hay không
            {
                string maKhoa = lsvKhoa.SelectedItems[0].SubItems[1].Text;
                lastedMK = maKhoa;
                RenderGV(maKhoa);                                               
            }
        }


        private void btnXoaKhoa_Click(object sender, EventArgs e)
        {
            if (lsvKhoa.SelectedItems.Count > 0) // kiểm tra xem có dòng được chọn hay không
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa dữ liệu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    string maKhoa = lsvKhoa.SelectedItems[0].SubItems[1].Text;
                    var khoa = QuanLyHocVuEntities.KHOAs.Find(maKhoa);
                    QuanLyHocVuEntities.KHOAs.Remove(khoa);
                    try
                    {
                        int result = QuanLyHocVuEntities.SaveChanges();
                        if (result > 0)
                        {
                            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RenderKhoa();
                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu không đang được sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dữ liệu đang được sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn phải chọn dòng dữ liệu muốn xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoaGV_Click(object sender, EventArgs e)
        {
            if (lsvGV.SelectedItems.Count > 0) // kiểm tra xem có dòng được chọn hay không
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa dữ liệu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    string maGV = lsvGV.SelectedItems[0].SubItems[1].Text;
                    var gv = QuanLyHocVuEntities.GIAOVIENs.Find(maGV);
                    var maKhoa = gv.MaKhoa;
                    QuanLyHocVuEntities.GIAOVIENs.Remove(gv);
                    try
                    {
                        int result = QuanLyHocVuEntities.SaveChanges();
                        if (result > 0)
                        {
                            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RenderGV(maKhoa);
                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu không đang được sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dữ liệu đang được sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn phải chọn dòng dữ liệu muốn xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lsvKhoa_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = lsvKhoa.GetItemAt(e.X, e.Y); // lấy đối tượng Item được double click

            if (item != null)
            {
                TaoKhoaDialog suaKhoaDialog = new TaoKhoaDialog();
                //Gán giá trị hiện tại 
                suaKhoaDialog.txtTaoMaKhoa.Text = lsvKhoa.SelectedItems[0].SubItems[1].Text;
                suaKhoaDialog.txtTaoMaKhoa.ReadOnly = true;
                suaKhoaDialog.txtTaoTenKhoa.Text = lsvKhoa.SelectedItems[0].SubItems[0].Text;


                if (suaKhoaDialog.ShowDialog() == DialogResult.OK)
                {
                    var maKhoa = suaKhoaDialog.txtTaoMaKhoa.Text;
                    var tenKhoa = suaKhoaDialog.txtTaoTenKhoa.Text;

                    KHOA updKhoa = QuanLyHocVuEntities.KHOAs.Find(maKhoa);

                    updKhoa.TenKhoa = tenKhoa;

                
                    try
                    {
                        int result = QuanLyHocVuEntities.SaveChanges();
                        if (result > 0)
                        {
                            MessageBox.Show("Cập nhật khoa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RenderKhoa();
                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dữ liệu không hợp lệ hoặc bị trùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void lsvGV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = lsvKhoa.GetItemAt(e.X, e.Y); // lấy đối tượng Item được double click

            if (item != null)
            {
                TaoGiaoVienDialog suaGVDialog = new TaoGiaoVienDialog();
                //Gán giá trị hiện tại 
                suaGVDialog.txtTaoMaGV.Text = lsvGV.SelectedItems[0].SubItems[1].Text;
                suaGVDialog.txtTaoMaGV.ReadOnly = true;
                suaGVDialog.txtTaoTenGV.Text = lsvGV.SelectedItems[0].SubItems[0].Text;
                suaGVDialog.cboMaKhoaTaoGV.Text = lastedMK; 


                if (suaGVDialog.ShowDialog() == DialogResult.OK)
                {

                    var maGV = suaGVDialog.txtTaoMaGV.Text;
                    var tenGV = suaGVDialog.txtTaoTenGV.Text;
                    var maKhoa = suaGVDialog.cboMaKhoaTaoGV.Text;

                    GIAOVIEN updGV = QuanLyHocVuEntities.GIAOVIENs.Find(maGV);
                    updGV.HoTen = tenGV;
                    updGV.MaKhoa = maKhoa;
                    try
                    {
                        int result = QuanLyHocVuEntities.SaveChanges();
                        if (result > 0)
                        {
                            MessageBox.Show("Cập nhật GV thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RenderKhoa();
                            lsvGV.Items.Clear();

                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dữ liệu không hợp lệ hoặc bị trùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }
    }
}
