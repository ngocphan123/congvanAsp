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

namespace CongVan.Areas.Admin.Controllers
{
    public class UploadFileController : Controller
    {
        // GET: Admin/UploadFile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload(HttpPostedFileBase file)
        {
            string path = Server.MapPath("~/Areas/Admin/Files/" + file.FileName);
            string ext = System.IO.Path.GetExtension(file.FileName);
            if (String.Compare(ext.ToLower(), "pdf") == 0)
            {
                 @ViewBag.Path = "Bạn chỉ có thể tải lên file PDF";
            }
            else {
                file.SaveAs(path);
                 @ViewBag.Path = path;
                //@ViewBag.Path = "Test"+ExtractTextFromPdf(path);
                 Document pdfDoc = new Document(path);

                int pageNum = pdfDoc.Pages.Count;
               // Response.Write("Hello--" + pageNum);
                XImage[] image = new XImage[pageNum];
               // int x = 0;
                //int y = 0;
                for (int i = 1; i <= pageNum; i++)
                {
                    
                    using (FileStream imageStream = new FileStream(@"D:\hoctap\webasp\CongVan\CongVan\Areas\Admin\Files\Tmp\abc" + i + ".bmp", FileMode.Create))
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
                    Bitmap bt = (Bitmap)Bitmap.FromFile(@"D:\hoctap\webasp\CongVan\CongVan\Areas\Admin\Files\Tmp\abc" + i + ".bmp");
                    bt.SetResolution(1000, 1000);                  
                    Color c;
                    for (int m = 0; m < bt.Width; m++)
                        for (int j = 0; j < bt.Height; j++)
                        {
                            c = bt.GetPixel(m, j);
                            //c.R = 0; // màu đen
                            int grayScale = (int)((c.R * 0.3) + (c.G * 0.59) + (c.B * 0.11));
                            Color newColor = Color.FromArgb(c.A, grayScale, grayScale, grayScale);
                            bt.SetPixel(m, j, newColor); //
                        }                   
                    FileStream output_gray = new FileStream(@"D:\hoctap\webasp\CongVan\CongVan\Areas\Admin\Files\Tmp\output_grayT" + i + ".bmp", FileMode.Create);
                    bt.Save(output_gray, ImageFormat.Jpeg);
                    output_gray.Dispose();
                    output_gray.Close();
                    
                    //Nhi Phan (Đen thành đen , Trắng Thành Trắng)
                    for (int m = 0; m < bt.Width; m++)
                        for (int j = 0; j < bt.Height; j++)
                        {
                            c = bt.GetPixel(m, j);
                            //c.R = 0; // màu đen                            
                            if (c.R < 140) bt.SetPixel(m, j, Color.Black);
                            else bt.SetPixel(m, j, Color.White);
                        }
                    
                    FileStream output_nhi = new FileStream(@"D:\hoctap\webasp\CongVan\CongVan\Areas\Admin\Files\Tmp\output_nhiT" + i + ".bmp", FileMode.Create);
                    bt.Save(output_nhi, ImageFormat.Jpeg);
                    output_nhi.Dispose();
                    output_nhi.Close();                    
                    System.Drawing.Rectangle cloneRectCopy = new System.Drawing.Rectangle(0, 114, bt.Width, bt.Height - 114);
                    Bitmap cloneBitmapCopy = bt.Clone(cloneRectCopy, PixelFormat.Format32bppArgb);
                    bt = cloneBitmapCopy;
                    bt = Crop(bt);
                    FileStream output_drop = new FileStream(@"D:\hoctap\webasp\CongVan\CongVan\Areas\Admin\Files\Tmp\output_dropTest" + i + ".bmp", FileMode.Create);
                    bt.Save(output_drop, ImageFormat.Jpeg);
                    output_drop.Dispose();
                    output_drop.Close();   
                    // Cắt khoảng trắng

                    /*int x1, x2, y1, y2, k, test;
                     x1 = x2 = y1 = y2 = k = 0;
                     for (int j = 0; j < bt.Height; j++)
                     {
                         if (i == 1) c = bt.GetPixel(443, j);
                         else
                         c = bt.GetPixel(1845 , j) ;
                         //c.R = 0; // màu đen                            
                         if (c.R == 0)
                         {
                             if (k == 0) 
                             {
                                                             
                                 if(y==0)
                                 {
                                     y = j;
                                     y1 = j;
                                 }
                                 else
                                 {
                                     y1 = y;
                                 }
                                
                             }
                             else
                             {
                                 y2 = j;                              
                             }
                             k++;
                                
                         }
                     }
                     test = k;
                     k = 0;
                     for (int m = 0; m < bt.Width; m++)
                     {
                         c = bt.GetPixel(m, y1);
                         //c.R = 0; // màu đen                            
                         if (c.R == 0)
                         {
                             if (k == 0)
                             {                               
                                 if(x==0)
                                 {
                                     x = m;
                                     x1 = m;
                                 }
                                 else
                                 {
                                     x1 = x;
                                 }
                                
                             }                             
                             else
                             {
                                 x2 = m;                               
                             }
                             k++;
                         }
                     }
                     System.Drawing.Rectangle cloneRectSpace = new System.Drawing.Rectangle(x1, y1, x2-x1, y2-y1);
                     Bitmap cloneBitmapSpace = bt.Clone(cloneRectSpace, PixelFormat.Format32bppArgb);
                     FileStream output_Space = new FileStream(@"D:\hoctap\webasp\CongVan\CongVan\Areas\Admin\Files\Tmp\cv" + i + ".bmp", FileMode.Create);
                     cloneBitmapSpace.Save(output_Space, ImageFormat.Jpeg);
                     output_Space.Dispose();
                     output_Space.Close();
                */
                    //Cắt tiêu đề
                    int m1 = 0;
                    Color b = bt.GetPixel(1380, 0);
                    if (i == 1)
                    {
                        for (int j = 113; j < bt.Height; j++)
                        {
                            if (i == 1) b = bt.GetPixel(1380, j);
                            //c.R = 0; // màu đen                            
                            if (b.R == 0)
                            {
                                m1 = j;

                            }
                        }

                        System.Drawing.Rectangle cloneRectBlack = new System.Drawing.Rectangle(1380, m1, 1533 - 1380, m1 - 113);
                        Bitmap cloneBitmapBlack = bt.Clone(cloneRectBlack, PixelFormat.Format32bppArgb);
                        FileStream output_Black = new FileStream(@"D:\hoctap\webasp\CongVan\CongVan\Areas\Admin\Files\Tmp\Black" + i + ".bmp", FileMode.Create);
                        cloneBitmapBlack.Save(output_Black, ImageFormat.Jpeg);

                        output_Black.Dispose();
                        output_Black.Close();
                    }
                    
                    try
                    {
                      
                        //Response.Write("Hello--" + output.Name);
                        // Clone Image
                       // System.Drawing.Rectangle cloneRect = new System.Drawing.Rectangle(1640, 2955, 416, 64);
                        // 1640,2950 là tọa độ góc trái trên cùng của vùng
                        // 416,64 là width vs height
                        //Bitmap cloneBitmap = sourceImg.Clone(cloneRect, PixelFormat.Format32bppArgb);
                        // Xong clone
                        // Search SetResolution cho Bitmap

                        //

                       // FileStream output_1 = new FileStream("~/Areas/Admin/Files/Tmp/output" + i + ".bmp", FileMode.Create);

                       // cloneBitmap.Save(output_1, ImageFormat.Jpeg);
                        //Nhi Phan (Đen thành đen , Trắng Thành Trắng)
                        /*
                         private Bitmap Phanvung(UnmanagedImage bm, int n)
                        {

                        UnmanagedImage sourceImage = bm;

                                Color c;
                                for (int i = 0; i < sourceImage.Width; i++)
                                    for (int j = 0; j < sourceImage.Height; j++)
                                    {
                                        c = sourceImage.GetPixel(i, j);
                                        c.R == 0 // màu đen
                                        if (c.R > n) sourceImage.SetPixel(i, j, Color.White);
                                        else sourceImage.SetPixel(i, j, Color.Black);


                            }
                            return bm.ToManagedImage();
                        }

                        */
                        //
                        // Cắt khoảng trắng nhá , Lấy ra 4 điểm cạnh của công văn.Xong rồi Tính ra tọa độ góc trái trên cùng, width height của vùng đấy
                        // Cắt ra ảnh con mà không có khoảng trắng
                        // bắt đầu cắt từng vùng ra
                        // Dùng thư viện lấy ra text

                        //
                       // output_1.Dispose();
                        //output_1.Close();
                    }
                    catch { }

                }
            }
            
            return View();
        }

        public string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }
        public string ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
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
        public FileResult ShowDocument(string FilePath)
        {
            return File(Server.MapPath("~/Areas/Admin/Files") + FilePath, GetMimeType(FilePath));
        }
        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }  
    }
}