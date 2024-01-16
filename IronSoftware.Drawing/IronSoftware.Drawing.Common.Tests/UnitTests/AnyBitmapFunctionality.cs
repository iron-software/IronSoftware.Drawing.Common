using FluentAssertions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace IronSoftware.Drawing.Common.Tests.UnitTests
{
    public class AnyBitmapFunctionality : Compare
    {
        public AnyBitmapFunctionality(ITestOutputHelper output) : base(output)
        {
        }

        [FactWithAutomaticDisplayName]
        public void Create_AnyBitmap_by_Filename()
        {
            string imagePath = GetRelativeFilePath("Mona-Lisa-oil-wood-panel-Leonardo-da.webp");

            var bitmap = AnyBitmap.FromFile(imagePath);
            bitmap.SaveAs("result.bmp");
            Assert.Equal(671, bitmap.Width);
            Assert.Equal(1000, bitmap.Height);
            Assert.Equal(74684, bitmap.Length);
            AssertImageAreEqual(imagePath, "result.bmp");

            bitmap = new AnyBitmap(imagePath);
            bitmap.SaveAs("result.bmp");
            Assert.Equal(671, bitmap.Width);
            Assert.Equal(1000, bitmap.Height);
            Assert.Equal(74684, bitmap.Length);
            AssertImageAreEqual(imagePath, "result.bmp");

            bitmap.Dispose();
        }

        [FactWithAutomaticDisplayName]
        public void Create_AnyBitmap_by_Byte()
        {
            string imagePath = GetRelativeFilePath("Mona-Lisa-oil-wood-panel-Leonardo-da.webp");
            byte[] bytes = File.ReadAllBytes(imagePath);

            var bitmap = AnyBitmap.FromBytes(bytes);
            _ = bitmap.TrySaveAs("result.bmp");
            AssertImageAreEqual(imagePath, "result.bmp");

            bitmap = new AnyBitmap(bytes);
            _ = bitmap.TrySaveAs("result.bmp");
            AssertImageAreEqual(imagePath, "result.bmp");

            bitmap.Dispose();
        }

        [FactWithAutomaticDisplayName]
        public void Create_AnyBitmap_by_Stream()
        {
            string imagePath = GetRelativeFilePath("Mona-Lisa-oil-wood-panel-Leonardo-da.webp");
            byte[] bytes = File.ReadAllBytes(imagePath);
            using Stream ms = new MemoryStream(bytes);

            var bitmap = AnyBitmap.FromStream(ms);
            _ = bitmap.TrySaveAs("result.bmp");
            AssertImageAreEqual(imagePath, "result.bmp");

            ms.Position = 0;
            bitmap = new AnyBitmap(ms);
            bitmap.SaveAs("result.bmp");
            AssertImageAreEqual(imagePath, "result.bmp");

            bitmap.Dispose();
        }

        [FactWithAutomaticDisplayName]
        public void Create_AnyBitmap_by_MemoryStream()
        {
            string imagePath = GetRelativeFilePath("Mona-Lisa-oil-wood-panel-Leonardo-da.webp");
            byte[] bytes = File.ReadAllBytes(imagePath);
            using var ms = new MemoryStream(bytes);

            var bitmap = AnyBitmap.FromStream(ms);
            _ = bitmap.TrySaveAs("result.bmp");
            AssertImageAreEqual(imagePath, "result.bmp");

            bitmap = new AnyBitmap(ms);
            bitmap.SaveAs("result.bmp");
            AssertImageAreEqual(imagePath, "result.bmp");

            bitmap.Dispose();
        }

        [FactWithAutomaticDisplayName]
        public async void Create_AnyBitmap_by_Uri_Async()
        {
            var uri = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/e/ea/Van_Gogh_-_Starry_Night_-_Google_Art_Project.jpg/1200px-Van_Gogh_-_Starry_Night_-_Google_Art_Project.jpg");

            AnyBitmap bitmap = await AnyBitmap.FromUriAsync(uri);
            _ = bitmap.TrySaveAs("result.bmp");
            AssertImageExist("result.bmp", true);

            bitmap = new AnyBitmap(uri);
            _ = bitmap.TrySaveAs("result.bmp");
            AssertImageExist("result.bmp", true);

            bitmap.Dispose();
        }

        [FactWithAutomaticDisplayName]
        public void Create_SVG_AnyBitmap()
        {
            string imagePath = GetRelativeFilePath("Example_barcode.svg");
            using var bitmap = AnyBitmap.FromFile(imagePath);
            bitmap.SaveAs("result.bmp");
            AssertImageAreEqual(imagePath, "result.bmp");
        }

        [FactWithAutomaticDisplayName]
        public void Try_Save_Bitmap_with_Format()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using var anyBitmap = new AnyBitmap(imagePath);

            anyBitmap.SaveAs("result-png.png", AnyBitmap.ImageFormat.Png);
            Assert.True(File.Exists("result-png.png"));
            CleanResultFile("result-png.png");

            anyBitmap.SaveAs("result-png-loss.png", AnyBitmap.ImageFormat.Png, 50);
            Assert.True(File.Exists("result-png-loss.png"));
            CleanResultFile("result-png-loss.png");

            _ = anyBitmap.TrySaveAs("result-try-png.png", AnyBitmap.ImageFormat.Png);
            Assert.True(File.Exists("result-try-png.png"));
            CleanResultFile("result-try-png.png");

            _ = anyBitmap.TrySaveAs("result-try-png-loss.png", AnyBitmap.ImageFormat.Png, 50);
            Assert.True(File.Exists("result-try-png-loss.png"));
            CleanResultFile("result-try-png-loss.png");
        }

        [FactWithAutomaticDisplayName]
        public void Check_Bitmap_Colors()
        {
            string imagePath = GetRelativeFilePath("colors.png");
            using var anyBitmap = new AnyBitmap(imagePath);

            var colorInfo = anyBitmap.GetColorInfo();
            Assert.True(colorInfo.Keys.Count == 6);

            Assert.True(colorInfo.ContainsKey(new Color("#00A2E8")));
            Assert.True(colorInfo.ContainsKey(new Color("#A349A4")));
            Assert.True(colorInfo.ContainsKey(new Color("#FFF200")));
            Assert.True(colorInfo.ContainsKey(new Color("#ED1C24")));
            Assert.True(colorInfo.ContainsKey(new Color("#22B14C")));
            Assert.True(colorInfo.ContainsKey(new Color("#FFFFFF")));
        }

        [FactWithAutomaticDisplayName]
        public void Check_Color_Exists()
        {
            string imagePath = GetRelativeFilePath("colors.png");
            using var anyBitmap = new AnyBitmap(imagePath);

            Assert.True(anyBitmap.ContainsColor(new Color("#00A2E8")));
            Assert.False(anyBitmap.ContainsColor(new Color("#00AABB")));
        }

        // TODO: check for specific color ...

        [FactWithAutomaticDisplayName]
        public void Export_file()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using var anyBitmap = new AnyBitmap(imagePath);

            anyBitmap.ExportFile("result.png");
            Assert.True(File.Exists("result.png"));
            CleanResultFile("result.png");

            anyBitmap.ExportFile("result-png.png", AnyBitmap.ImageFormat.Png);
            Assert.True(File.Exists("result-png.png"));
            CleanResultFile("result-png.png");

            anyBitmap.ExportFile("result-png-loss.png", AnyBitmap.ImageFormat.Png, 50);
            Assert.True(File.Exists("result-png-loss.png"));
            CleanResultFile("result-png-loss.png");
        }

        [IgnoreOnUnixFact]
        public void CastBitmap_to_AnyBitmap()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using var bitmap = new System.Drawing.Bitmap(imagePath);
            using AnyBitmap anyBitmap = bitmap;

            bitmap.Save("expected.bmp");
            anyBitmap.SaveAs("result.bmp");

            AssertImageAreEqual("expected.bmp", "result.bmp", true);
        }

        [IgnoreOnUnixFact]
        public void CastBitmap_from_AnyBitmap()
        {
            using var anyBitmap = AnyBitmap.FromFile(GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg"));
            using System.Drawing.Bitmap bitmap = anyBitmap;

            anyBitmap.SaveAs("expected.bmp");
            bitmap.Save("result.bmp");

            AssertImageAreEqual("expected.bmp", "result.bmp", true);
        }

        [IgnoreOnUnixFact]
        public void CastImage_to_AnyBitmap()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using var bitmap = System.Drawing.Image.FromFile(imagePath);
            using AnyBitmap anyBitmap = bitmap;

            bitmap.Save("expected.bmp");
            anyBitmap.SaveAs("result.bmp");

            AssertImageAreEqual("expected.bmp", "result.bmp", true);
        }

        [IgnoreOnUnixFact]
        public void CastImage_from_AnyBitmap()
        {
            using var anyBitmap = AnyBitmap.FromFile(GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg"));
            using System.Drawing.Image bitmap = anyBitmap;

            anyBitmap.SaveAs("expected.bmp");
            bitmap.Save("result.bmp");

            AssertImageAreEqual("expected.bmp", "result.bmp", true);
        }

        [FactWithAutomaticDisplayName]
        public void AnyBitmap_should_get_Bytes()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using var anyBitmap = AnyBitmap.FromFile(imagePath);
            byte[] expected = File.ReadAllBytes(imagePath);

            byte[] result = anyBitmap.GetBytes();
            Assert.Equal(expected, result);

            byte[] resultExport = anyBitmap.ExportBytes();
            Assert.Equal(expected, resultExport);
        }

        [FactWithAutomaticDisplayName]
        public void AnyBitmap_should_set_Pixel()
        {
            string imagePath = GetRelativeFilePath("checkmark.jpg");

            using Image<Rgb24> formatRgb24 = Image.Load<Rgb24>(imagePath);
            using Image<Abgr32> formatAbgr32 = Image.Load<Abgr32>(imagePath);
            using Image<Argb32> formatArgb32 = Image.Load<Argb32>(imagePath);
            using Image<Bgr24> formatBgr24 = Image.Load<Bgr24>(imagePath);
            using Image<Bgra32> formatBgra32 = Image.Load<Bgra32>(imagePath);

            Image[] images = { formatRgb24, formatAbgr32, formatArgb32, formatBgr24, formatBgra32 };

            foreach (Image image in images)
            {
                using AnyBitmap bitmap = (AnyBitmap)image;

                // Get the current pixel color - should be white
                var pixelBefore = bitmap.GetPixel(0, 0);

                // Check current pixel color is not black
                Assert.NotEqual(pixelBefore, Color.Black);

                // Set the pixel color to black
                bitmap.SetPixel(0, 0, Color.Black);

                // Check the pixel color has changed
                Assert.Equal(bitmap.GetPixel(0, 0), Color.Black);
            }
        }

        [FactWithAutomaticDisplayName]
        public void AnyBitmap_should_get_Stream()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using var anyBitmap = AnyBitmap.FromFile(imagePath);
            var expected = new MemoryStream(File.ReadAllBytes(imagePath));

            MemoryStream result = anyBitmap.GetStream();
            AssertStreamAreEqual(expected, result);

            result = anyBitmap.ToStream();
            AssertStreamAreEqual(expected, result);

            Func<Stream> funcStream = anyBitmap.ToStreamFn();
            AssertStreamAreEqual(expected, funcStream);

            using var resultExport = new MemoryStream();
            anyBitmap.ExportStream(resultExport);
            AssertStreamAreEqual(expected, resultExport);
        }

        [FactWithAutomaticDisplayName]
        public void AnyBitmap_should_get_Hashcode()
        {
            string imagePath = GetRelativeFilePath("Mona-Lisa-oil-wood-panel-Leonardo-da.webp");
            using var anyBitmap = AnyBitmap.FromFile(imagePath);

            byte[] bytes = File.ReadAllBytes(imagePath);
            Assert.Equal(bytes, anyBitmap.GetBytes());

            int expected = anyBitmap.GetBytes().GetHashCode();
            int result = anyBitmap.GetHashCode();
            Assert.Equal(expected, result);
        }

        [FactWithAutomaticDisplayName]
        public void AnyBitmap_should_ToString()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using var anyBitmap = AnyBitmap.FromFile(imagePath);

            byte[] bytes = File.ReadAllBytes(imagePath);
            string expected = Convert.ToBase64String(bytes, 0, bytes.Length);

            string result = anyBitmap.ToString();
            Assert.Equal(expected, result);
        }

        [FactWithAutomaticDisplayName]
        public void Clone_AnyBitmap()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using var anyBitmap = AnyBitmap.FromFile(imagePath);
            using AnyBitmap clonedAnyBitmap = anyBitmap.Clone();

            anyBitmap.SaveAs("expected.png");
            clonedAnyBitmap.SaveAs("result.png");

            AssertImageAreEqual("expected.png", "result.png", true);

            using Image image = anyBitmap;
            image.Mutate(img => img.Crop(new Rectangle(0, 0, 100, 100)));
            using AnyBitmap clonedWithRect = anyBitmap.Clone(new Rectangle(0, 0, 100, 100));

            image.SaveAsPng("expected.png");
            clonedWithRect.SaveAs("result.png");

            AssertImageAreEqual("expected.png", "result.png", true);
        }

        [FactWithAutomaticDisplayName]
        public void CastSKBitmap_to_AnyBitmap()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using var skBitmap = SkiaSharp.SKBitmap.Decode(imagePath);
            using AnyBitmap anyBitmap = skBitmap;

            SaveSkiaBitmap(skBitmap, "expected.png");
            anyBitmap.SaveAs("result.png");

            AssertImageAreEqual("expected.png", "result.png", true);
        }

        [FactWithAutomaticDisplayName]
        public void CastSKBitmap_from_AnyBitmap()
        {
            var anyBitmap = AnyBitmap.FromFile(GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg"));
            SkiaSharp.SKBitmap skBitmap = anyBitmap;

            anyBitmap.SaveAs("expected.png");
            SaveSkiaBitmap(skBitmap, "result.png");

            AssertImageAreEqual("expected.png", "result.png", true);

            anyBitmap = AnyBitmap.FromFile(GetRelativeFilePath("Sample-Tiff-File-download-for-Testing.tiff"));
            skBitmap = anyBitmap;

            anyBitmap.SaveAs("expected.png");
            SaveSkiaBitmap(skBitmap, "result.png");

            AssertImageAreEqual("expected.png", "result.png", true);

            anyBitmap.Dispose();
            skBitmap.Dispose();
        }

        [FactWithAutomaticDisplayName]
        public void CastSKImage_to_AnyBitmap()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using var skImage = SkiaSharp.SKImage.FromBitmap(SkiaSharp.SKBitmap.Decode(imagePath));
            using AnyBitmap anyBitmap = skImage;

            SaveSkiaImage(skImage, "expected.png");
            anyBitmap.SaveAs("result.png");

            AssertImageAreEqual("expected.png", "result.png", true);
        }

        [FactWithAutomaticDisplayName]
        public void CastSKImage_from_AnyBitmap()
        {
            var anyBitmap = AnyBitmap.FromFile(GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg"));
            SkiaSharp.SKImage skImage = anyBitmap;

            anyBitmap.SaveAs("expected.png");
            SaveSkiaImage(skImage, "result.png");

            AssertImageAreEqual("expected.png", "result.png", true);

            anyBitmap = AnyBitmap.FromFile(GetRelativeFilePath("Sample-Tiff-File-download-for-Testing.tiff"));
            skImage = anyBitmap;

            anyBitmap.SaveAs("expected.png");
            SaveSkiaImage(skImage, "result.png");

            AssertImageAreEqual("expected.png", "result.png", true);

            anyBitmap.Dispose();
            skImage.Dispose();
        }

        [FactWithAutomaticDisplayName]
        public void CastSixLabors_to_AnyBitmap()
        {
            string imagePath = GetRelativeFilePath("mountainclimbers.jpg");
            using var imgSharp = Image.Load<Rgba32>(imagePath);
            using AnyBitmap anyBitmap = imgSharp;

            imgSharp.Save("expected.bmp");
            anyBitmap.SaveAs("result.bmp");

            AssertImageAreEqual("expected.bmp", "result.bmp", true);
        }

        [FactWithAutomaticDisplayName]
        public void CastSixLabors_from_AnyBitmap()
        {
            using var anyBitmap = AnyBitmap.FromFile(GetRelativeFilePath("mountainclimbers.jpg"));
            using Image imgSharp = anyBitmap;

            anyBitmap.SaveAs("expected.bmp");
            imgSharp.Save("result.bmp");

            AssertImageAreEqual("expected.bmp", "result.bmp", true);
        }

        [FactWithAutomaticDisplayName]
        public void Load_Tiff_Image()
        {
            var anyBitmap = AnyBitmap.FromFile(GetRelativeFilePath("IRON-274-39065.tif"));
            Assert.Equal(2, anyBitmap.FrameCount);

            var multiPage = AnyBitmap.FromFile(GetRelativeFilePath("animated_qr.gif"));
            Assert.Equal(4, multiPage.FrameCount);
            Assert.Equal(4, multiPage.GetAllFrames.Count());
            multiPage.GetAllFrames.First().SaveAs("first.png");
            multiPage.GetAllFrames.Last().SaveAs("last.png");
            AssertImageAreEqual(GetRelativeFilePath("first-animated-qr.png"), "first.png");
            AssertImageAreEqual(GetRelativeFilePath("last-animated-qr.png"), "last.png");

            byte[] bytes = File.ReadAllBytes(GetRelativeFilePath("IRON-274-39065.tif"));
            anyBitmap = AnyBitmap.FromBytes(bytes);
            Assert.Equal(2, anyBitmap.FrameCount);

            byte[] multiPageBytes = File.ReadAllBytes(GetRelativeFilePath("animated_qr.gif"));
            multiPage = AnyBitmap.FromBytes(multiPageBytes);
            Assert.Equal(4, multiPage.FrameCount);
            Assert.Equal(4, multiPage.GetAllFrames.Count());
            multiPage.GetAllFrames.First().SaveAs("first.png");
            multiPage.GetAllFrames.Last().SaveAs("last.png");
            AssertImageAreEqual(GetRelativeFilePath("first-animated-qr.png"), "first.png");
            AssertImageAreEqual(GetRelativeFilePath("last-animated-qr.png"), "last.png");

            anyBitmap.Dispose();
            multiPage.Dispose();
        }

        [FactWithAutomaticDisplayName]
        public void Try_UnLoad_Tiff_Image()
        {
            using var anyBitmap = AnyBitmap.FromFile(GetRelativeFilePath("multiframe.tiff"));
            Assert.Equal(2, anyBitmap.FrameCount);
        }

        [FactWithAutomaticDisplayName]
        public void Create_Multi_page_Tiff()
        {
            var bitmaps = new List<AnyBitmap>()
            {
                AnyBitmap.FromFile(GetRelativeFilePath("first-animated-qr.png")),
                AnyBitmap.FromFile(GetRelativeFilePath("last-animated-qr.png"))
            };

            using var anyBitmap = AnyBitmap.CreateMultiFrameTiff(bitmaps);
            Assert.Equal(2, anyBitmap.FrameCount);
            Assert.Equal(2, anyBitmap.GetAllFrames.Count());
            anyBitmap.GetAllFrames.ElementAt(0).SaveAs("first.png");
            anyBitmap.GetAllFrames.ElementAt(1).SaveAs("last.png");
            AssertImageAreEqual(GetRelativeFilePath("first-animated-qr.png"), "first.png");
            AssertImageAreEqual(GetRelativeFilePath("last-animated-qr.png"), "last.png");

            for (var i = bitmaps.Count - 1; i >= 0; i--)
            {
                bitmaps[i].Dispose();
            }
        }

        [FactWithAutomaticDisplayName]
        public void Create_Multi_page_Tiff_Paths()
        {
            var imagePaths = new List<string>()
            {
                GetRelativeFilePath("first-animated-qr.png"),
                GetRelativeFilePath("last-animated-qr.png")
            };

            using var anyBitmap = AnyBitmap.CreateMultiFrameTiff(imagePaths);
            Assert.Equal(2, anyBitmap.FrameCount);
            Assert.Equal(2, anyBitmap.GetAllFrames.Count());
            anyBitmap.GetAllFrames.ElementAt(0).SaveAs("first.png");
            anyBitmap.GetAllFrames.ElementAt(1).SaveAs("last.png");
            AssertImageAreEqual(GetRelativeFilePath("first-animated-qr.png"), "first.png");
            AssertImageAreEqual(GetRelativeFilePath("last-animated-qr.png"), "last.png");
        }

        [FactWithAutomaticDisplayName]
        public void Create_Multi_page_Gif()
        {
            var bitmaps = new List<AnyBitmap>()
            {
                AnyBitmap.FromFile(GetRelativeFilePath("first-animated-qr.png")),
                AnyBitmap.FromFile(GetRelativeFilePath("mountainclimbers.jpg"))
            };

            using var anyBitmap = AnyBitmap.CreateMultiFrameGif(bitmaps);
            Assert.Equal(2, anyBitmap.FrameCount);
            Assert.Equal(2, anyBitmap.GetAllFrames.Count());
            anyBitmap.GetAllFrames.ElementAt(0).SaveAs("first.png");
            using var first = Image.Load(GetRelativeFilePath("first-animated-qr.png"));
            first.Mutate(img => img.Resize(new ResizeOptions
            {
                Size = new Size(anyBitmap.GetAllFrames.ElementAt(0).Width, anyBitmap.GetAllFrames.ElementAt(0).Height),
                Mode = ResizeMode.BoxPad
            }));
            first.Save("first-expected.jpg");
            AssertImageAreEqual("first-expected.jpg", "first.png", true);

            anyBitmap.GetAllFrames.ElementAt(1).SaveAs("last.png");
            using var last = Image.Load(GetRelativeFilePath("mountainclimbers.jpg"));
            last.Mutate(img => img.Resize(new ResizeOptions
            {
                Size = new Size(anyBitmap.GetAllFrames.ElementAt(1).Width, anyBitmap.GetAllFrames.ElementAt(1).Height),
                Mode = ResizeMode.BoxPad
            }));
            last.Save("last-expected.jpg");
            AssertImageAreEqual("last-expected.jpg", "last.png", true);

            for (var i = bitmaps.Count - 1; i >= 0; i--)
            {
                bitmaps[i].Dispose();
            }
        }

        [FactWithAutomaticDisplayName]
        public void Create_Multi_page_Gif_paths()
        {
            var imagePaths = new List<string>()
            {
                GetRelativeFilePath("first-animated-qr.png"),
                GetRelativeFilePath("mountainclimbers.jpg")
            };

            using var anyBitmap = AnyBitmap.CreateMultiFrameGif(imagePaths);
            Assert.Equal(2, anyBitmap.FrameCount);
            Assert.Equal(2, anyBitmap.GetAllFrames.Count());
            anyBitmap.GetAllFrames.ElementAt(0).SaveAs("first.png");
            using var first = Image.Load(GetRelativeFilePath("first-animated-qr.png"));
            first.Mutate(img => img.Resize(new ResizeOptions
            {
                Size = new Size(anyBitmap.GetAllFrames.ElementAt(0).Width, anyBitmap.GetAllFrames.ElementAt(0).Height),
                Mode = ResizeMode.BoxPad
            }));
            first.Save("first-expected.jpg");
            AssertImageAreEqual("first-expected.jpg", "first.png", true);

            anyBitmap.GetAllFrames.ElementAt(1).SaveAs("last.png");
            using var last = Image.Load(GetRelativeFilePath("mountainclimbers.jpg"));
            last.Mutate(img => img.Resize(new ResizeOptions
            {
                Size = new Size(anyBitmap.GetAllFrames.ElementAt(1).Width, anyBitmap.GetAllFrames.ElementAt(1).Height),
                Mode = ResizeMode.BoxPad
            }));
            last.Save("last-expected.jpg");
            AssertImageAreEqual("last-expected.jpg", "last.png", true);
        }

        [FactWithAutomaticDisplayName]
        public void Should_Return_BitsPerPixel()
        {
            AnyBitmap bitmap = AnyBitmap.FromFile(GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg"));
            Assert.Equal(24, bitmap.BitsPerPixel);

            bitmap = Image.Load<Rgba32>(GetRelativeFilePath("mountainclimbers.jpg"));
            Assert.Equal(32, bitmap.BitsPerPixel);

            bitmap.Dispose();
        }

        [TheoryWithAutomaticDisplayName()]
        [InlineData("mountainclimbers.jpg", "image/jpeg", AnyBitmap.ImageFormat.Jpeg)]
        [InlineData("watermark.deployment.png", "image/png", AnyBitmap.ImageFormat.Png)]
        [InlineData("animated_qr.gif", "image/gif", AnyBitmap.ImageFormat.Gif)]
        [InlineData("Mona-Lisa-oil-wood-panel-Leonardo-da.webp", "image/webp", AnyBitmap.ImageFormat.Webp)]
        [InlineData("multiframe.tiff", "image/tiff", AnyBitmap.ImageFormat.Tiff)]
        public void Should_Return_MimeType(string fileName, string expectedMimeType, AnyBitmap.ImageFormat expectedImageFormat)
        {
            string imagePath = GetRelativeFilePath(fileName);
            using AnyBitmap bitmap = AnyBitmap.FromFile(imagePath);
            Assert.Equal(expectedMimeType, bitmap.MimeType);
            Assert.Equal(expectedImageFormat, bitmap.GetImageFormat());
        }

        [FactWithAutomaticDisplayName]
        public void Should_Return_Scan0()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using AnyBitmap bitmap = AnyBitmap.FromFile(imagePath);
            Assert.NotEqual(IntPtr.Zero, bitmap.Scan0);
        }

        [IgnoreOnUnixFact]
        public void Should_Return_Stride()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            using AnyBitmap anyBitmap = AnyBitmap.FromFile(imagePath);
            using System.Drawing.Bitmap bitmap = new(imagePath);
            BitmapData data = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            Assert.Equal(data.Stride, anyBitmap.Stride);
        }

        [TheoryWithAutomaticDisplayName]
        [InlineData("van-gogh-starry-night-vincent-van-gogh.jpg", 0, 0)]
        [InlineData("van-gogh-starry-night-vincent-van-gogh.jpg", 500, 0)]
        [InlineData("van-gogh-starry-night-vincent-van-gogh.jpg", 0, 300)]
        [InlineData("van-gogh-starry-night-vincent-van-gogh.jpg", 500, 100)]
        [InlineData("van-gogh-starry-night-vincent-van-gogh.jpg", 599, 150)]
        [InlineData("van-gogh-starry-night-vincent-van-gogh.jpg", 350, 450)]
        public void Should_Return_Pixel(string filename, int x, int y)
        {
            string imagePath = GetRelativeFilePath(filename);
            using var anyBitmap = AnyBitmap.FromFile(imagePath);
            using var bitmap = Image.Load<Rgb24>(imagePath);

            _ = anyBitmap.Width.Should().Be(bitmap.Width);
            _ = anyBitmap.Height.Should().Be(bitmap.Height);

            Color anyBitmapPixel = anyBitmap.GetPixel(x, y);
            Rgb24 bitmapPixel = bitmap[x, y];
            _ = anyBitmapPixel.R.Should().Be(bitmapPixel.R);
            _ = anyBitmapPixel.G.Should().Be(bitmapPixel.G);
            _ = anyBitmapPixel.B.Should().Be(bitmapPixel.B);
        }

        [TheoryWithAutomaticDisplayName]
        [InlineData("van-gogh-starry-night-vincent-van-gogh.jpg", 100, 100)]
        [InlineData("van-gogh-starry-night-vincent-van-gogh.jpg", 1200, 800)]
        [InlineData("mountainclimbers.jpg", 700, 600)]
        [InlineData("mountainclimbers.jpg", 50, 30)]
        [InlineData("support-team-member-1.webp", 10, 10)]
        public void Should_Resize_Image(string fileName, int width, int height)
        {
            string imagePath = GetRelativeFilePath(fileName);
            using AnyBitmap anyBitmap = AnyBitmap.FromFile(imagePath);
            using AnyBitmap resizeAnyBitmap = new(anyBitmap, width, height);
            _ = resizeAnyBitmap.Width.Should().Be(width);
            _ = resizeAnyBitmap.Height.Should().Be(height);
        }

        [FactWithAutomaticDisplayName]
        public void Should_RotateFlip()
        {
            string imagePath = GetRelativeFilePath("checkmark.jpg");

            // Check rotate
            AnyBitmap bitmap = AnyBitmap.FromFile(imagePath);
            bitmap = AnyBitmap.RotateFlip(bitmap, AnyBitmap.RotateMode.Rotate180, AnyBitmap.FlipMode.Horizontal);
            bitmap.SaveAs("result_rotate.bmp");
            Assert.Equal(52, bitmap.Width);
            Assert.Equal(52, bitmap.Height);
            AssertImageAreEqual(GetRelativeFilePath("checkmark90.jpg"), "result_rotate.bmp");

            // Check flip
            bitmap = AnyBitmap.FromFile(imagePath);
            bitmap = AnyBitmap.RotateFlip(bitmap, AnyBitmap.RotateMode.None, AnyBitmap.FlipMode.Horizontal);
            bitmap.SaveAs("result_flip.bmp");
            Assert.Equal(52, bitmap.Width);
            Assert.Equal(52, bitmap.Height);
            AssertImageAreEqual(GetRelativeFilePath("checkmarkFlip.jpg"), "result_flip.bmp");

            bitmap.Dispose();
        }

        [FactWithAutomaticDisplayName]
        public void Redact_ShouldRedactRegionWithColor()
        {
            // Arrange
            using var memoryStream = new MemoryStream();
            using var image = new Image<Rgba32>(Configuration.Default, 100, 100, Color.White);
            image.Save(memoryStream, new SixLabors.ImageSharp.Formats.Bmp.BmpEncoder()
            {
                BitsPerPixel = SixLabors.ImageSharp.Formats.Bmp.BmpBitsPerPixel.Pixel32,
                SupportTransparency = true
            });

            using AnyBitmap anyBitmap = new(memoryStream.ToArray());
            var rectangle = new Rectangle(10, 10, 50, 50);
            Color color = Color.Black;

            // Act
            using AnyBitmap result = AnyBitmap.Redact(anyBitmap, rectangle, color);

            // Assert
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixel = result.GetPixel(x, y);
                    if (rectangle.Contains(x, y))
                    {
                        Assert.Equal(color, pixel);
                    }
                    else
                    {
                        Assert.Equal(Color.White, pixel);
                    }
                }
            }
        }

        [FactWithAutomaticDisplayName]
        public void TestGetRGBBuffer()
        {
            string imagePath = GetRelativeFilePath("checkmark.jpg");
            using AnyBitmap bitmap = new(imagePath);
            var expectedSize = bitmap.Width * bitmap.Height * 3; // 3 bytes per pixel (RGB)

            byte[] buffer = bitmap.GetRGBBuffer();

            Assert.Equal(expectedSize, buffer.Length);

            // Verify the first pixel's RGB values
            var firstPixel = bitmap.GetPixel(0, 0);
            Assert.Equal(firstPixel.R, buffer[0]);
            Assert.Equal(firstPixel.G, buffer[1]);
            Assert.Equal(firstPixel.B, buffer[2]);
        }

        [FactWithAutomaticDisplayName]
        public void Test_LoadFromRGBBuffer()
        {
            // Arrange
            int width = 2;
            int height = 2;
            byte[] buffer =
            [
                255, 0, 0, // red
                0, 255, 0, // green
                0, 0, 255, // blue
                255, 255, 255, // white
            ];

            // Act
            using AnyBitmap result = AnyBitmap.LoadAnyBitmapFromRGBBuffer(buffer, width, height);

            // Assert
            byte[] resultData = result.GetRGBBuffer();
            Assert.Equal(buffer, resultData);
        }

        [FactWithAutomaticDisplayName]
        public void TestLoadAnyBitmapFromRGBBuffer()
        {
            string imagePath = GetRelativeFilePath("checkmark.jpg");

            using var bitmap = Image.Load<Rgb24>(imagePath);
            var width = bitmap.Width;
            var height = bitmap.Height;

            var buffer = GetRGBBuffer(imagePath);

            using AnyBitmap result = AnyBitmap.LoadAnyBitmapFromRGBBuffer(buffer, width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var expectedColor = bitmap[x, y];
                    var actualColor = result.GetPixel(x, y);

                    Assert.Equal(expectedColor.R, actualColor.R);
                    Assert.Equal(expectedColor.G, actualColor.G);
                    Assert.Equal(expectedColor.B, actualColor.B);
                }
            }
        }

#if !NETFRAMEWORK
        [FactWithAutomaticDisplayName]
        public void CastMaui_to_AnyBitmap()
        {
            string imagePath = GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg");
            byte[] bytes = File.ReadAllBytes(imagePath);
            using var image = (Microsoft.Maui.Graphics.Platform.PlatformImage)Microsoft.Maui.Graphics.Platform.PlatformImage.FromStream(new MemoryStream(bytes));
            using AnyBitmap anyBitmap = image;

            SaveMauiImages(image, "expected.bmp");
            anyBitmap.SaveAs("result.bmp");

            AssertImageAreEqual("expected.bmp", "result.bmp", true);
        }

        [FactWithAutomaticDisplayName]
        public void CastMaui_from_AnyBitmap()
        {
            using AnyBitmap anyBitmap = AnyBitmap.FromFile(GetRelativeFilePath("van-gogh-starry-night-vincent-van-gogh.jpg"));
            using Microsoft.Maui.Graphics.Platform.PlatformImage image = anyBitmap;

            anyBitmap.SaveAs("expected.bmp");
            SaveMauiImages(image, "result.bmp");

            AssertImageAreEqual("expected.bmp", "result.bmp", true);
        }
#endif

        [FactWithAutomaticDisplayName]
        public void Should_Read_Tiff_With_Zero_Width_or_Height()
        {
            string imagePath = GetRelativeFilePath("partial_valid.tif");
            using AnyBitmap anyBitmap = AnyBitmap.FromFile(imagePath);
            anyBitmap.FrameCount.Should().BeGreaterThan(0);
        }

        [FactWithAutomaticDisplayName]
        public void Create_New_Image_Instance()
        {
            string blankBitmapPath = "blank_bitmap.bmp";
            using var bitmap = new AnyBitmap(8, 8);
            bitmap.SaveAs(blankBitmapPath);

            using AnyBitmap blankBitmap = AnyBitmap.FromFile(blankBitmapPath);

            blankBitmap.Width.Should().Be(8);
            blankBitmap.Height.Should().Be(8);
        }

        [FactWithAutomaticDisplayName]
        public void Create_New_Image_With_Background_Instance()
        {
            string blankBitmapPath = "blank_bitmap.bmp";
            using AnyBitmap bitmap = new AnyBitmap(8, 8, Color.DarkRed);
            bitmap.SaveAs(blankBitmapPath);

            using AnyBitmap blankBitmap = AnyBitmap.FromFile(blankBitmapPath);

            blankBitmap.Width.Should().Be(8);
            blankBitmap.Height.Should().Be(8);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    blankBitmap.GetPixel(i, j).Should().Be(Color.DarkRed);
                }
            }
        }

        [FactWithAutomaticDisplayName]
        public void ExtractAlphaData_With32bppImage_ReturnsAlphaChannel()
        {
            // Arrange
            string imagePath = GetRelativeFilePath("32_bit_transparent.png");
            using AnyBitmap bitmap = new(imagePath);

            // Act
            var result = bitmap.ExtractAlphaData();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result[0]);
            Assert.Equal(5, result[49282]);
            Assert.Equal(108, result[49292]);
            Assert.Equal(211, result[49300]);
            Assert.Equal(0, result[47999]);
        }

        [FactWithAutomaticDisplayName]
        public void ExtractAlphaData_WithUnsupportedBppImage_ThrowsException()
        {
            // Arrange
            string imagePath = GetRelativeFilePath("24_bit.png");
            using AnyBitmap bitmap = new(imagePath);

            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() => bitmap.ExtractAlphaData());
            Assert.Equal($"Extracting alpha data is not supported for {bitmap.BitsPerPixel} bpp images.", exception.Message);
        }
    }
}
