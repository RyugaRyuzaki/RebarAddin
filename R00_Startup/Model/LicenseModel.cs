using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace R00_Startup
{
    public class LicenseModel:BaseViewModel
    {
        private readonly string MainMail = "ryugaryuzaki200586@gmail.com";
        private readonly string SendMail = "vantantruong2004@gmail.com";
        private readonly string PassSendMail = "VANTANTRUONG2004";
        private readonly string DefaultLogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public string License { get; set; }
        public string TempFileName { get; set; }
        public bool IsTimeout { get; set; }
        public int DayTimeout { get; set; }

        private bool _CheckInternet;
        public bool CheckInternet { get => _CheckInternet; set { _CheckInternet = value; OnPropertyChanged(); } }

        public LicenseModel()
        {
            CheckInternet = CheckIntenetConnection();
            if (CheckInternet)
            {
                License = GetBios();
                string PathTemp = Path.Combine(DefaultLogFilePath, @"AppData\Local\Temp\");
                TempFileName = Path.Combine(PathTemp, License + ".dat");

                Match match = GetMatchFromSheet();
                if (match != Match.Empty)
                {
                    DayTimeout = GetDayTimeOut(match);
                   
                }
                else
                {
                    DayTimeout = 30;
                    if (!File.Exists(TempFileName))
                    {
                        using (var stream = new FileStream(TempFileName, FileMode.OpenOrCreate))
                        {
                            WriteFileToTemp(stream);
                            SendMailToAuthor();
                        }
                    }
                    else
                    {
                        using (var stream = new FileStream(TempFileName, FileMode.Open))
                        {
                            DayTimeout = GetDayFromReadFileToTemp(stream);
                        }
                    }
                }
            }
        }
        private string RunCMD(string cmd)
        {
            Process cmdProcess;
            cmdProcess = new Process();
            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.Arguments = "/c " + cmd;
            cmdProcess.StartInfo.RedirectStandardOutput = true;
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.StartInfo.CreateNoWindow = true;
            cmdProcess.Start();
            string output = cmdProcess.StandardOutput.ReadToEnd();
            cmdProcess.WaitForExit();
            if (String.IsNullOrEmpty(output))
                return "";
            return output;
        }
        private bool CheckIntenetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private int GetDayTimeOut(Match match)
        {
            string[] array = match.ToString().Split(new char[] { '|' });
            DateTime dateTime = DateTime.Now;
            int dayNow = dateTime.Day;
            int monthNow = dateTime.Month;
            int yearNow = dateTime.Year;
            string[] arrays = array[1].ToString().Split(new char[] { '/' });
            int dayThen = Int32.Parse(arrays[0]);
            int monthThen = Int32.Parse(arrays[1]);
            int yearThen = Int32.Parse(arrays[2]);
            DateTime Now = new DateTime(yearNow, monthNow, dayNow);
            DateTime Then = new DateTime(yearThen, monthThen, dayThen);
            TimeSpan dif = Then.Subtract(Now);
            return (int)Math.Ceiling(dif.TotalDays);
        }
        private Match GetMatchFromSheet()
        {
            HttpClient httpClient = new HttpClient();
            string requestUri2 = "https://docs.google.com/spreadsheets/d/1wYTNxtXCQOE92wYayAmdnsHPIHYeGAJi94I5lu68xbA/edit?usp=sharing";
            string request = httpClient.GetAsync(requestUri2).Result.Content.ReadAsStringAsync().Result.ToString();
            return Regex.Match(request, License + ".*?(?=OK)");
        }
        private void SendMailToAuthor()
        {
            DateTime dateTime = DateTime.Now;
            int dayNow = dateTime.Day;
            int monthNow = dateTime.Month;
            int yearNow = dateTime.Year;
            string combine = "|" + dayNow + "/" + monthNow + "/" + yearNow + "|OK";
            MailMessage mailMessage = new MailMessage(SendMail, MainMail, "License Revit Add-in", License + combine);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(SendMail, PassSendMail);
            smtpClient.Send(mailMessage);
        }
        private void WriteFileToTemp(Stream stream)
        {
            DateTime dateTime = DateTime.Now;
            int dayNow = dateTime.Day;
            var byteDayNow = BitConverter.GetBytes(dayNow);
            stream.Write(byteDayNow, 0, 4);
            int monthNow = dateTime.Month;
            var byteMonthNow = BitConverter.GetBytes(monthNow);
            stream.Write(byteMonthNow, 0, 4);
            int yearNow = dateTime.Year;
            var byteYearNow = BitConverter.GetBytes(yearNow);
            stream.Write(byteYearNow, 0, 4);
        }
        private int GetDayFromReadFileToTemp(Stream stream)
        {
            var byteDayPass = new byte[4];
            stream.Read(byteDayPass, 0, 4);
            int dayPass = BitConverter.ToInt32(byteDayPass, 0);
            var byteMonthPass = new byte[4];
            stream.Read(byteMonthPass, 0, 4);
            int monthPass = BitConverter.ToInt32(byteMonthPass, 0);
            var byteYearPass = new byte[4];
            stream.Read(byteYearPass, 0, 4);
            int yearPass = BitConverter.ToInt32(byteYearPass, 0);
            DateTime dateTime = DateTime.Now;
            int dayNow = dateTime.Day;
            int monthNow = dateTime.Month;
            int yearNow = dateTime.Year;
            DateTime Now = new DateTime(yearNow, monthNow, dayNow);
            DateTime Pass = new DateTime(yearPass, monthPass, dayPass);
            TimeSpan dif = Now.Subtract(Pass);
            return (int)Math.Ceiling(dif.TotalDays);
        }
        private string GetBios()
        {
            string output = RunCMD("wmic diskdrive get serialNumber"); // check số serial ổ cứng
            using (StreamWriter HDD = new StreamWriter("HDD.txt", true))
            {
                HDD.WriteLine(output);
                HDD.Close();
            }
            string[] lines = File.ReadAllLines("HDD.txt");
            File.Delete("HDD.txt");
            string str = Regex.Replace(lines[2], @"\s", ""); // lấy serial đầu tiên

            string outputs = RunCMD("wmic bios get serialnumber"); // check số serial bios
            using (StreamWriter BIOS = new StreamWriter("bios.txt", true))
            {
                BIOS.WriteLine(outputs);
                BIOS.Close();
            }
            string[] liness = File.ReadAllLines("bios.txt");
            File.Delete("bios.txt");
            string strs = Regex.Replace(liness[2], @"\s", ""); // lấy serial đầu tiên

            string bios = string.Concat(strs, str).Replace("ToBeFilledBy", "");
            return GetMD5(bios);
        }
        private string GetMD5(string bios)
        {
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes("Chuỗi cần mã hóa");
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
