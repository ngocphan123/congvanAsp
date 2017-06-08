using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongVan.Areas.Admin.Comman;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using AForge.Imaging;
using RasterEdge.XImage.OCR;
using Tesseract;
using System.Web.Mvc.Html;
using CongVan.Areas.Admin.Models;

namespace CongVan.Areas.Admin.Controllers
{
    public class UploadFileController : Controller
    {
        NLP db = new NLP();
        String rootPath = "D:\\hoctap\\webasp\\CongVanT\\congvanAsp\\CongVan\\Areas\\Admin\\Files\\Tmp\\";
        // GET: Admin/UploadFile
        public ActionResult Index()
        {
            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);
            return View();
        }
        public static string GetText(Bitmap imgsource)
        {
            var ocrtext = string.Empty;
            var engine = new TesseractEngine("D:\\hoctap\\webasp\\CongVanT\\congvanAsp\\CongVan\\tessdata", "vie", EngineMode.Default);
            var img = PixConverter.ToPix(imgsource);
            var page = engine.Process(img);
            ocrtext = page.GetText();

            return ocrtext;
        }
        public void WriteText(Bitmap imgsource)
        {
            String outputFilePath = rootPath + "Output.txt";
            string text = GetText(imgsource);
            StreamWriter wr = new StreamWriter(outputFilePath);
            wr.Flush();
            wr.Write(text);
            wr.Close();
        }
        public Dictionary<string, int> CropImgae(BitmapData cloneBitmapCopyCSource, Bitmap btSource, UnmanagedImage sourceimageSource, int h0, String nameFile)
        {

            BitmapData cloneBitmapCopyC = cloneBitmapCopyCSource;
            Bitmap bt = btSource;
            UnmanagedImage sourceimage = sourceimageSource;

            Color c;
            //Tìm x trái nhất
            int k, xt1, yt1, xt2, yt2, space, h, d, heighd, xt1min, xt2max, dong, tmp1, tmp2, tmp, hd, kc, weight, heightpage;
            tmp1 = tmp2 = k = h = dong = xt1min = xt2max = heighd = d = space = xt1 = yt1 = xt2 = yt2 = tmp = kc = weight = heightpage = 0;
            if (nameFile.Contains("NoiNhanCV")) weight = sourceimage.Width;
            else weight = (int)(0.9 * sourceimage.Width / 2);
            heightpage = sourceimage.Height;
            for (hd = h0; hd <= h0 + 20; hd++)
            {
                for (k = 0; k < weight; k++)
                {
                    c = sourceimage.GetPixel(k, hd);
                    if (c.R == 0)
                    {
                        xt1 = k;
                        break;
                    }
                }
                if (tmp == 0 || tmp > xt1)
                {
                    tmp = xt1;
                    yt1 = hd;
                }

            }

            xt1 = tmp;
            tmp = 0;
            d = yt1;
            for (k = xt1; k < weight; k++)
            {
                for (h = d; h < (h0 + 59); h++)
                {
                    if (space > 2000)
                    {
                        break;
                    }

                    c = sourceimage.GetPixel(k, h);
                    if (c.R == 0)
                    {
                        xt2 = k;
                        yt2 = h;
                        if (tmp == 0 || tmp < yt2)
                        {
                            tmp = yt2;
                        }

                        space = 0;
                    }
                    else if (c.R == 255)
                    {
                        space += 1;
                    }
                }

            }
            yt2 = tmp;
            int height = 0;
            tmp = 0;
            if (nameFile.Contains("donviSoanCV")|| nameFile.Contains("TieuDeCV"))
            {

                if (nameFile.Contains("donviSoanCV"))
                {
                    heighd = height = yt2 + (int)(0.12 * yt2);
                    kc = yt2;
                }
                else if (nameFile.Contains("TieuDeCV"))
                {
                    heighd = height = (yt2 - yt1) + (int)(0.2 * (yt2 - yt1));
                    if (h0 > 250)
                        kc = yt2 - yt1 + (int)(0.5 * (yt2 - yt1));
                    else
                        kc = yt2 - yt1;
                    yt1 = yt1 - (int)(0.3 * (yt2 - yt1));
                    xt1 = 0;
                }

                dong = 1;
                do
                {

                    for (hd = height; hd <= (height + dong * ((int)(0.65 * yt2))); hd++)
                    {
                        h = 0;
                        d = 0;
                        for (k = 0; k < weight; k++)
                        {
                            c = sourceimage.GetPixel(k, hd);
                            if (c.R == 0)
                            {
                                if (h == 0)
                                {
                                    xt1min = k;
                                    if (tmp == 0 || tmp > xt1min)
                                    {
                                        tmp = xt1min;
                                    }
                                    h++;
                                }
                                else
                                {
                                    xt2max = k;
                                }

                                d++;
                            }
                        }
                    }
                    xt1min = tmp;
                    if (dong == 1)
                    {
                        tmp1 = xt1;
                        tmp2 = xt2;
                    }
                    if (d > 0)
                    {
                        if (tmp1 > xt1min) tmp1 = xt1min;
                        if (tmp2 < xt2max) tmp2 = xt2max;
                        dong++;
                    }
                } while (d > 0);

                if (dong > 1)
                {
                    if (tmp1 < xt1) xt1 = tmp1;
                    if (tmp2 > xt2) xt2 = tmp2;
                    height = height + dong * kc + (int)(0.2 * kc);
                }
            }
            else
            {
                heighd = height = (yt2 - yt1) + (int)(0.2 * (yt2 - yt1));
                yt1 = yt1 - (int)(0.5 * (yt2 - yt1));
            }

            if (nameFile.Contains("donviSoanCV"))
                cloneBitmapCopyC = bt.LockBits(new System.Drawing.Rectangle(xt1, 0, xt2 - xt1, height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bt.PixelFormat);
            else
                cloneBitmapCopyC = bt.LockBits(new System.Drawing.Rectangle(xt1, yt1, xt2 - xt1, height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bt.PixelFormat);

            UnmanagedImage titileImage = new UnmanagedImage(cloneBitmapCopyC);
            Bitmap imgTitile = titileImage.ToManagedImage();
            String filePath = rootPath + nameFile + ".bmp";
            if (!System.IO.File.Exists(filePath))
            {
                FileStream output_title = new FileStream((rootPath + nameFile + ".bmp"), FileMode.Create);
                imgTitile.Save(output_title, System.Drawing.Imaging.ImageFormat.Jpeg);
                output_title.Dispose();
                output_title.Close();
            }         
            bt.UnlockBits(cloneBitmapCopyC);
            Dictionary<string, int> dictionaryheight = new Dictionary<string, int>();
            dictionaryheight.Add("height", height);
            dictionaryheight.Add("heighd", heighd);
            return dictionaryheight;

        }
        public ActionResult Upload(HttpPostedFileBase file)
        {
            string path = Server.MapPath("~/Areas/Admin/Files/" + file.FileName);
            string ext = System.IO.Path.GetExtension(file.FileName);
            string[] nameFielCV = file.FileName.Split('.');
            if (String.Compare(ext.ToLower(), "pdf") == 0)
            {
                @ViewBag.Path = "Bạn chỉ có thể tải lên file PDF";
            }
            else
            {
                file.SaveAs(path);
                @ViewBag.Path = path;
                //@ViewBag.Path = "Test"+ExtractTextFromPdf(path);
                Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(path);

                int pageNum = pdfDoc.Pages.Count;
                // Response.Write("Hello--" + pageNum);
                XImage[] image = new XImage[pageNum];

                String NoiSoanCV, SoCV, TieuDeCV, NoiDungCV, NoiNhanCV, ChuKyCV, filePath;
                NoiSoanCV = SoCV = TieuDeCV = NoiDungCV = NoiNhanCV = ChuKyCV = filePath  = "";
                Color c;
                for (int i = 1; i <= pageNum; i++)
                {
                    Bitmap bt;
                    UnmanagedImage sourceimage;
                    BitmapData cloneBitmapCopyC;                   
                    filePath = rootPath + file.FileName + nameFielCV[0] + i + ".bmp";
                    if (!System.IO.File.Exists(filePath))
                    {
                        using (FileStream imageStream = new FileStream(rootPath + nameFielCV[0] + i + ".bmp", FileMode.Create))
                        {
                            // Create Resolution object
                            Resolution resolution = new Resolution(300);
                            // Create JPEG device with specified attributes (Width, Height, Resolution, Quality)
                            // where Quality [0-100], 100 is Maximum
                            JpegDevice jpegDevice = new JpegDevice(resolution, 100);

                            // Convert a particular page and save the image to stream
                            jpegDevice.Process(pdfDoc.Pages[i], imageStream);
                            // Close stream
                            imageStream.Close();
                        }
                        // Thao tác với bt
                        bt = (Bitmap)Bitmap.FromFile((rootPath + nameFielCV[0] + i + ".bmp"));
                        bt.SetResolution(192f, 192f);
                        cloneBitmapCopyC = bt.LockBits(new System.Drawing.Rectangle(0, 114, bt.Width, bt.Height - 114), System.Drawing.Imaging.ImageLockMode.ReadWrite, bt.PixelFormat);
                        sourceimage = new UnmanagedImage(cloneBitmapCopyC);

                        // chuyển thành ảnh cấp xám xong Nhi Phan (Đen thành đen , Trắng Thành Trắng)                        
                        for (int m = 0; m < sourceimage.Width; m++)
                            for (int j = 0; j < sourceimage.Height; j++)
                            {
                                c = sourceimage.GetPixel(m, j);
                                //c.R = 0; // màu đen
                                int grayScale = (int)((c.R * 0.3) + (c.G * 0.59) + (c.B * 0.11));
                                sourceimage.SetPixel(m, j, Color.FromArgb(c.A, grayScale, grayScale, grayScale));
                                if (c.R < 140) sourceimage.SetPixel(m, j, Color.Black);
                                else sourceimage.SetPixel(m, j, Color.White);
                            }
                        bt = sourceimage.ToManagedImage();
                        bt = Crop(bt);
                        filePath = rootPath + nameFielCV[0] + "output_grayCV" + i + ".bmp";
                        if (!System.IO.File.Exists(filePath))
                        {
                            FileStream output_gray = new FileStream((rootPath + nameFielCV[0] + "output_grayCV" + i + ".bmp"), FileMode.Create);
                            bt.Save(output_gray, System.Drawing.Imaging.ImageFormat.Jpeg);
                            output_gray.Dispose();
                            output_gray.Close();
                        }   
                    }
                    else
                    {
                        // Thao tác với bt
                        bt = (Bitmap)Bitmap.FromFile((rootPath + nameFielCV[0] + "output_grayCV" + i + ".bmp"));
                        cloneBitmapCopyC = bt.LockBits(new System.Drawing.Rectangle(0, 0, bt.Width, bt.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bt.PixelFormat);
                        sourceimage = new UnmanagedImage(cloneBitmapCopyC);
                    }
                                
                    sourceimage = UnmanagedImage.FromManagedImage(bt);

                    String nameFile = "";
                    if (i == 1)
                    {
                        int ht, hs;
                        ht = hs = 0;

                        //Cắt tiêu đề  
                        nameFile = nameFielCV[0] + "donviSoanCV";
                        Dictionary<string, int> dictionaryheightDV = CropImgae(cloneBitmapCopyC, bt, sourceimage, ht, nameFile);
                        ht = dictionaryheightDV["height"] + (int)1.3 * dictionaryheightDV["heighd"];
                        Bitmap dvCV = (Bitmap)Bitmap.FromFile((rootPath + nameFile + ".bmp"));
                        NoiSoanCV = GetText(dvCV);

                        //cắt số công văn
                        nameFile = nameFielCV[0] + "soCV";
                        Dictionary<string, int> dictionaryheightSoCV = CropImgae(cloneBitmapCopyC, bt, sourceimage, ht, nameFile);
                        ht = ht + dictionaryheightSoCV["height"];
                        Bitmap soCV = (Bitmap)Bitmap.FromFile((rootPath + nameFile + ".bmp"));
                        SoCV = GetText(soCV);

                        //cắt tiêu đề công văn
                        nameFile = nameFielCV[0] + "TieuDeCV";
                        Dictionary<string, int> dictionaryheightTieuDeCV = CropImgae(cloneBitmapCopyC, bt, sourceimage, ht, nameFile);
                        Bitmap TieuDeCVBit = (Bitmap)Bitmap.FromFile((rootPath + nameFile + ".bmp"));
                        ht = ht + dictionaryheightTieuDeCV["height"];
                        if (ht > 460)
                        {
                            ht = ht + (int)(2 * dictionaryheightTieuDeCV["heighd"]);
                        }
                        TieuDeCV = GetText(TieuDeCVBit);

                        //cắt nội dung
                        nameFile = nameFielCV[0] + "NoiDungCV";
                        BitmapData cloneBitmapNoiDungCV = bt.LockBits(new System.Drawing.Rectangle(0, ht, sourceimage.Width, sourceimage.Height - ht), System.Drawing.Imaging.ImageLockMode.ReadWrite, bt.PixelFormat);

                        UnmanagedImage titileImage = new UnmanagedImage(cloneBitmapNoiDungCV);
                        Bitmap imgTitile = titileImage.ToManagedImage();
                        filePath = rootPath + nameFile + ".bmp";
                        if (!System.IO.File.Exists(filePath))
                        {
                            FileStream output_title = new FileStream((rootPath + nameFile + ".bmp"), FileMode.Create);
                            imgTitile.Save(output_title, System.Drawing.Imaging.ImageFormat.Jpeg);
                            output_title.Dispose();
                            output_title.Close();
                        }                      
                        Bitmap NoiDungCVBit = (Bitmap)Bitmap.FromFile((rootPath + nameFile + ".bmp"));
                        NoiDungCV = GetText(NoiDungCVBit);
                    }
                    if (i == pageNum)
                    {
                        //cắt nơi nhận, chữ ký
                        bool kt = false;
                        for (int k = (int)(0.98 * bt.Height); k < bt.Height; k++)
                        {
                            c = sourceimage.GetPixel((int)(0.5 * bt.Width), k);
                            if (c.R == 0)
                            {
                                kt = true; break;
                            }
                        }
                        if (kt)
                        {
                            BitmapData cloneBitmapPageEnd = bt.LockBits(new System.Drawing.Rectangle(0, 0, bt.Width, bt.Height - 114), System.Drawing.Imaging.ImageLockMode.ReadWrite, bt.PixelFormat);
                            sourceimage = new UnmanagedImage(cloneBitmapPageEnd);
                            bt = sourceimage.ToManagedImage();
                            bt = Crop(bt);
                            nameFile = nameFielCV[0] + "PageEndCV";
                            filePath = rootPath + nameFile + ".bmp";
                            if (!System.IO.File.Exists(filePath))
                            {
                                FileStream PageEndCV = new FileStream((rootPath + nameFile + ".bmp"), FileMode.Create);
                                bt.Save(PageEndCV, System.Drawing.Imaging.ImageFormat.Jpeg);
                                PageEndCV.Dispose();
                                PageEndCV.Close();                    
                            }                                  
                            sourceimage = UnmanagedImage.FromManagedImage(bt);
                            //bt.UnlockBits(cloneBitmapPageEnd);
                        }

                        //cắt chữ ký
                        nameFile = nameFielCV[0] + "ChuKyCV";
                        BitmapData cloneBitmapChuKyCV = bt.LockBits(new System.Drawing.Rectangle((int)(0.5 * sourceimage.Width), (int)(0.9 * sourceimage.Height), (int)(0.4 * sourceimage.Width), sourceimage.Height - (int)(0.9 * sourceimage.Height)), System.Drawing.Imaging.ImageLockMode.ReadWrite, bt.PixelFormat);

                        UnmanagedImage ChuKyImage = new UnmanagedImage(cloneBitmapChuKyCV);
                        Bitmap ChuKyTitile = ChuKyImage.ToManagedImage();
                        ChuKyTitile = Crop(ChuKyTitile);
                        filePath = rootPath + nameFile + ".bmp";
                        if (!System.IO.File.Exists(filePath))
                        {
                            FileStream output_ChuKy = new FileStream((rootPath + nameFile + ".bmp"), FileMode.Create, FileAccess.ReadWrite);
                            ChuKyTitile.Save(output_ChuKy, System.Drawing.Imaging.ImageFormat.Jpeg);
                            output_ChuKy.Dispose();
                            output_ChuKy.Close();
                        }
                       
                        bt.UnlockBits(cloneBitmapChuKyCV);
                        Bitmap ChuKyCVBit = (Bitmap)Bitmap.FromFile((rootPath + nameFile + ".bmp"));
                        ChuKyCV = GetText(ChuKyCVBit);

                        // cắt nơi nhận                   

                        nameFile = nameFielCV[0] + "NoiNhanCV";
                        BitmapData cloneBitmapNoiNhanCV;                        
                        if (!kt)
                        {
                            cloneBitmapNoiNhanCV = bt.LockBits(new System.Drawing.Rectangle(0, (int)(0.8 * (sourceimage.Height)), (int)(0.5 * sourceimage.Width), sourceimage.Height - (int)(0.8 * sourceimage.Height) - 50), System.Drawing.Imaging.ImageLockMode.ReadWrite, bt.PixelFormat);
                        }
                        else
                            cloneBitmapNoiNhanCV = bt.LockBits(new System.Drawing.Rectangle(0, (int)(0.6 * (sourceimage.Height - 10)), (int)(0.5 * sourceimage.Width), sourceimage.Height - (int)(0.6 * sourceimage.Height) - 50), System.Drawing.Imaging.ImageLockMode.ReadWrite, bt.PixelFormat);

                        UnmanagedImage NoiNhanImage = new UnmanagedImage(cloneBitmapNoiNhanCV);
                        Bitmap NoiNhanTitile = NoiNhanImage.ToManagedImage();
                        NoiNhanTitile = Crop(NoiNhanTitile);
                         filePath = rootPath + nameFile + ".bmp";
                         if (!System.IO.File.Exists(filePath))
                         {
                             FileStream output_NoiNhan = new FileStream((rootPath + nameFile + ".bmp"), FileMode.Create);
                             NoiNhanTitile.Save(output_NoiNhan, System.Drawing.Imaging.ImageFormat.Jpeg);
                             output_NoiNhan.Dispose();
                             output_NoiNhan.Close();
                         }
                        
                        Bitmap NoiNhanCVBit = (Bitmap)Bitmap.FromFile((rootPath + nameFile + ".bmp"));
                        NoiNhanCV = GetText(NoiNhanCVBit);
                        bt.UnlockBits(cloneBitmapChuKyCV);
                    }
                }
                //Lưu thông tin vừa cắt vào cơ sở dữ liệu
                row data = new row();
                //NoiSoanCV, SoCV, TieuDeCV, NoiDungCV, NoiNhanCV, ChuKyCV;
                data.from_org = NoiSoanCV;
                data.number_dispatch = SoCV;
                data.title = TieuDeCV;
                data._abstract = NoiDungCV;
                data.to_org = NoiNhanCV;
                data.name_signer = ChuKyCV;
                data.attach_file = file.FileName;
                try
                {
                    db.rows.Add(data);
                    db.SaveChanges();

                    return RedirectToAction("List", "CongVan");
                }
                catch (Exception e)
                {
                    string x = e.InnerException.ToString();
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
        public static Bitmap Crop(Bitmap bmp)
        {
            int w = bmp.Width;
            int h = bmp.Height;

            Func<int, bool> allWhiteRow = row =>
            {
                for (int i = 0; i < w; ++i)
                    if (bmp.GetPixel(i, row).R != 255)
                        return false;
                return true;
            };

            Func<int, bool> allWhiteColumn = col =>
            {
                for (int i = 0; i < h; ++i)
                    if (bmp.GetPixel(col, i).R != 255)
                        return false;
                return true;
            };

            int topmost = 0;
            for (int row = 0; row < h; ++row)
            {
                if (allWhiteRow(row))
                    topmost = row;
                else break;
            }

            int bottommost = 0;
            for (int row = h - 1; row >= 0; --row)
            {
                if (allWhiteRow(row))
                    bottommost = row;
                else break;
            }

            int leftmost = 0, rightmost = 0;
            for (int col = 0; col < w; ++col)
            {
                if (allWhiteColumn(col))
                    leftmost = col;
                else
                    break;
            }

            for (int col = w - 1; col >= 0; --col)
            {
                if (allWhiteColumn(col))
                    rightmost = col;
                else
                    break;
            }

            if (rightmost == 0) rightmost = w; // As reached left
            if (bottommost == 0) bottommost = h; // As reached top.

            int croppedWidth = rightmost - leftmost;
            int croppedHeight = bottommost - topmost;

            if (croppedWidth == 0) // No border on left or right
            {
                leftmost = 0;
                croppedWidth = w;
            }

            if (croppedHeight == 0) // No border on top or bottom
            {
                topmost = 0;
                croppedHeight = h;
            }

            try
            {
                var target = new Bitmap(croppedWidth, croppedHeight);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(bmp,
                      new RectangleF(0, 0, croppedWidth, croppedHeight),
                      new RectangleF(leftmost, topmost, croppedWidth, croppedHeight),
                      GraphicsUnit.Pixel);
                }
                return target;
            }
            catch (Exception ex)
            {
                throw new Exception(
                  string.Format("Values are topmost={0} btm={1} left={2} right={3} croppedWidth={4} croppedHeight={5}", topmost, bottommost, leftmost, rightmost, croppedWidth, croppedHeight),
                  ex);
            }
        }

        public void TrimSpace(Document doc, int i)
        {
            // Render the page to image with 72 DPI
            PngDevice device = new PngDevice(new Resolution(72));

            using (MemoryStream imageStr = new MemoryStream())
            {
                device.Process(doc.Pages[i], imageStr);
                Bitmap bmp = (Bitmap)Bitmap.FromStream(imageStr);

                System.Drawing.Imaging.BitmapData imageBitmapData = null;

                // Determine white areas
                try
                {
                    imageBitmapData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                                            System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                    Aspose.Pdf.Rectangle prevCropBox = doc.Pages[i].CropBox;

                    int toHeight = bmp.Height;
                    int toWidth = bmp.Width;

                    int? leftNonWhite = null;
                    int? rightNonWhite = null;
                    int? topNonWhite = null;
                    int? bottomNonWhite = null;

                    for (int y = 0; y < toHeight; y++)
                    {
                        byte[] imageRowBytes = new byte[imageBitmapData.Stride];

                        // Copy the row data to byte array
                        if (IntPtr.Size == 4)
                            System.Runtime.InteropServices.Marshal.Copy(new IntPtr(imageBitmapData.Scan0.ToInt32() + y * imageBitmapData.Stride), imageRowBytes, 0, imageBitmapData.Stride);
                        else
                            System.Runtime.InteropServices.Marshal.Copy(new IntPtr(imageBitmapData.Scan0.ToInt64() + y * imageBitmapData.Stride), imageRowBytes, 0, imageBitmapData.Stride);


                        int? leftNonWhite_row = null;
                        int? rightNonWhite_row = null;

                        for (int x = 0; x < toWidth; x++)
                        {
                            if (imageRowBytes[x * 4] != 255
                                || imageRowBytes[x * 4 + 1] != 255
                                || imageRowBytes[x * 4 + 2] != 255)
                            {
                                if (leftNonWhite_row == null)
                                    leftNonWhite_row = x;

                                rightNonWhite_row = x;
                            }
                        }

                        if (leftNonWhite_row != null || rightNonWhite_row != null)
                        {
                            if (topNonWhite == null)
                                topNonWhite = y;

                            bottomNonWhite = y;
                        }

                        if (leftNonWhite_row != null
                            && (leftNonWhite == null || leftNonWhite > leftNonWhite_row))
                        {
                            leftNonWhite = leftNonWhite_row;
                        }
                        if (rightNonWhite_row != null
                            && (rightNonWhite == null || rightNonWhite < rightNonWhite_row))
                        {
                            rightNonWhite = rightNonWhite_row;
                        }
                    }

                    leftNonWhite = leftNonWhite ?? 0;
                    rightNonWhite = rightNonWhite ?? toWidth;
                    topNonWhite = topNonWhite ?? 0;
                    bottomNonWhite = bottomNonWhite ?? toHeight;

                    // Set crop box with correction to previous crop box
                    doc.Pages[i].CropBox =
                        new Aspose.Pdf.Rectangle(
                            leftNonWhite.Value + prevCropBox.LLX,
                            (toHeight + prevCropBox.LLY) - bottomNonWhite.Value,
                            rightNonWhite.Value + doc.Pages[i].CropBox.LLX,
                            (toHeight + prevCropBox.LLY) - topNonWhite.Value
                            );
                }
                finally
                {
                    if (imageBitmapData != null)
                        bmp.UnlockBits(imageBitmapData);
                }
            }

            // Save the document
            doc.Save(@"D:\hoctap\webasp\CongVan\CongVan\Areas\Admin\Files\Tmp\spacePdf" + i + ".pdf");
        }

        public FileResult DisplayPDF()
        {
            return File("~/Areas/Admin/Files/6_3Vol68No1.pdf", "application/pdf");
        }

        public FileResult PDFDownload()
        {

            string filepath = Server.MapPath("~/Areas/Admin/Files/6_3Vol68No1.pdf");
            byte[] pdfByte = Helper.GetBytesFromFile(filepath);
            return File(pdfByte, "application/pdf", "demoform1");
        }

        public FileResult PDFDisplay()
        {
            string filepath = Server.MapPath("~/Areas/Admin/Files/6_3Vol68No1.pdf");
            byte[] pdfByte = Helper.GetBytesFromFile(filepath);
            return File(pdfByte, "application/pdf");
        }

        public PartialViewResult PDFPartialView()
        {
            return PartialView();
        }

        public ActionResult ConvertPDFtoText()
        {
            string filepath = Server.MapPath("~/Areas/Admin/Files/12426_01. Cong van so 113_BKHCN_TTra.pdf");
            @ViewBag.Path = ExtractTextFromPdf(filepath);
            return View();
        }
        public static string ExtractTextFromPdf(string path)
        {
            ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string thePage = PdfTextExtractor.GetTextFromPage(reader, i, its);
                    string[] theLines = thePage.Split('\n');
                    foreach (var theLine in theLines)
                    {
                        text.AppendLine(theLine);
                    }
                }
                return text.ToString();
            }
        }
    }
}