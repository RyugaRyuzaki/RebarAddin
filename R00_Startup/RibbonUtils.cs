#region Namespaces
using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Windows.Media.Imaging;
using Autodesk.Revit.ApplicationServices;

#endregion

namespace R00_Startup
{
    public class RibbonUtils
    {
        private DSPConstraint dlqConstraint;
        private string imageFolder;
        private string dllFolder;

        public RibbonUtils(ControlledApplication a)
        {
            dlqConstraint = new DSPConstraint(a);
            imageFolder = dlqConstraint.ImageFolder;
            dllFolder = dlqConstraint.DllFolder;
        }
        public PushButtonData CreatePushButtonData(
            string name, string displayName,
            string dllName, string fullClassName,
            string image, string tooltip,
            string helperPath = null,
            string longDescription = null,
            string tooltipImage = null,
            string linkYoutube = null)
        {
            PushButtonData pushButtonData = new PushButtonData(name, displayName,
                Path.Combine(dllFolder, dllName), fullClassName);

            // image hiển thị khi tạo PushButton riêng lẻ
            pushButtonData.LargeImage = CreateBitmapImage(imageFolder, image);

            // image nhỏ hơn, hiển thị khi tạo PushButton cho ItemsStacked
            //pushButtonData.Image = CreateBitmapImage(imageFolder, image);

            pushButtonData.ToolTip = tooltip;

            if (!string.IsNullOrEmpty(tooltipImage))
            {
                Uri tooltipUri = new Uri(Path.Combine(imageFolder, tooltipImage),
                    UriKind.Absolute);
                pushButtonData.ToolTipImage = new BitmapImage(tooltipUri);
            }

            if (!string.IsNullOrEmpty(helperPath))
            {
                ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.ChmFile,
                    helperPath);
                pushButtonData.SetContextualHelp(contextHelp);
            }

            if (!string.IsNullOrEmpty(linkYoutube))
            {
                ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.Url,
                    linkYoutube);
                pushButtonData.SetContextualHelp(contextHelp);
            }

            if (longDescription != null)
            {
                pushButtonData.LongDescription = longDescription;
            }

            return pushButtonData;
        }


        /// <summary>
        /// Tạo data cho PulldownButton
        /// </summary>
        /// <param name="name">Tên duy nhất</param>
        /// <param name="displayName">Tên sẽ hiển thị trên thanh Ribbon</param>
        /// <param name="tooltip"></param>
        /// <param name="image"></param>
        /// <param name="helperPath"></param>
        /// <param name="linkYoutube"></param>
        /// <param name="toolTipImage"></param>
        /// <returns></returns>
        public PulldownButtonData CreatePulldownButtonData(
            string name,
            string displayName,
            string tooltip,
            string image,
            string helperPath = null,
            string linkYoutube = null,
            string toolTipImage = null)
        {

            PulldownButtonData pulldownButtonData = pulldownButtonData
                = new PulldownButtonData(name, displayName);

            pulldownButtonData.ToolTip = tooltip;

            if (!string.IsNullOrEmpty(helperPath))
            {
                ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.ChmFile,
                    helperPath);
                pulldownButtonData.SetContextualHelp(contextHelp);
            }
            if (!string.IsNullOrEmpty(linkYoutube))
            {
                ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.Url,
                    linkYoutube);
                pulldownButtonData.SetContextualHelp(contextHelp);
            }

            if (!string.IsNullOrEmpty(image))
            {
                pulldownButtonData.Image
                    = CreateBitmapImage(imageFolder, image);
            }

            if (!string.IsNullOrEmpty(toolTipImage))
            {
                pulldownButtonData.ToolTipImage
                    = CreateBitmapImage(imageFolder, toolTipImage);
            }

            return pulldownButtonData;
        }

        /// <summary>
        /// Add một PulldownButton vào một Panel.
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="name">Unique Name</param>
        /// <param name="displayName">Name will be display on Ribbon</param>
        /// <param name="tooltip"></param>
        /// <param name="largeImage"></param>
        /// <param name="toolTipImage"></param>
        /// <param name="helperPath"></param>
        /// <param name="linkYoutube"></param>
        /// <returns></returns>
        public PulldownButton CreatePulldownButton(
            RibbonPanel ribbonPanel,
            string name, string displayName,
            string tooltip,
            string largeImage,
            string toolTipImage = null,
            string helperPath = null,
            string linkYoutube = null)
        {

            PulldownButtonData pulldownButtonData
                = new PulldownButtonData(name, displayName);
            pulldownButtonData.ToolTip = tooltip;


            if (!string.IsNullOrEmpty(helperPath))
            {
                ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.ChmFile,
                    helperPath);
                pulldownButtonData.SetContextualHelp(contextHelp);
            }
            if (!string.IsNullOrEmpty(linkYoutube))
            {
                ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.Url,
                    linkYoutube);
                pulldownButtonData.SetContextualHelp(contextHelp);
            }

            if (!string.IsNullOrEmpty(largeImage))
            {
                pulldownButtonData.LargeImage
                    = CreateBitmapImage(imageFolder, largeImage);
            }

            if (!string.IsNullOrEmpty(toolTipImage))
            {
                pulldownButtonData.ToolTipImage
                    = CreateBitmapImage(imageFolder, toolTipImage);
            }

            return ribbonPanel.AddItem(pulldownButtonData) as PulldownButton;
        }


        /// <summary>
        /// Add một SplitButton vào một Panel
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="name">Unique Name</param>
        /// <param name="displayName">Name will be display on Ribbon</param>
        /// <param name="tooltip"></param>
        /// <param name="helperPath"></param>
        /// <param name="linkYoutube"></param>
        /// <returns></returns>
        public SplitButton CreateSplitButton(
            RibbonPanel ribbonPanel,
            string name,
            string displayName,
            string tooltip,
            string helperPath = null,
            string linkYoutube = null)
        {
            SplitButtonData splitButtonData = new SplitButtonData(name, displayName);
            splitButtonData.ToolTip = tooltip;

            if (!string.IsNullOrEmpty(helperPath))
            {
                ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.ChmFile,
                        helperPath);
                splitButtonData.SetContextualHelp(contextHelp);
            }
            if (!string.IsNullOrEmpty(linkYoutube))
            {
                ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.Url,
                    linkYoutube);
                splitButtonData.SetContextualHelp(contextHelp);
            }

            return ribbonPanel.AddItem(splitButtonData) as SplitButton;
        }

        /// <summary>
        /// Create BitmapImage
        /// </summary>
        /// <param name="imageFolders">eg. "C:\ProgramData\Autodesk\ApplicationPlugins\Q'Apps.bundle\Contents\Resources\Image"</param>
        /// <param name="image">eg. Match32x32.png</param>
        /// <returns></returns>
        public BitmapImage CreateBitmapImage(string imageFolder, string image)
        {
            string pathImage = Path.Combine(imageFolder, image);

            Uri iconUri = new Uri(pathImage, UriKind.Absolute);
            BitmapImage bitmapImage = new BitmapImage(iconUri);
            return new BitmapImage(iconUri);
        }
    }
}
