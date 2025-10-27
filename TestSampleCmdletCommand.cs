using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;   // dotnet add package System.Diagnostics.PerformanceCounter
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.InteropServices;
using System.Security;
using HtmlAgilityPack;
using System.Net.NetworkInformation;
using System.Management.Automation.Language;
//using Microsoft.Edge.SeleniumTools; // Obsolete
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Text.RegularExpressions;
using System.Text;
using System.CodeDom;


namespace moduleDll
{
    [Cmdlet(VerbsDiagnostic.Test, "SampleCmdlet")]
    [OutputType(typeof(FavoriteStuff))]
    public class TestSampleCmdletCommand : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public int FavoriteNumber { get; set; }

        [Parameter(
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        [ValidateSet("Cat", "Dog", "Horse")]
        public string FavoritePet { get; set; } = "Dog";

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            WriteObject(new FavoriteStuff
            {
                FavoriteNumber = FavoriteNumber,
                FavoritePet = FavoritePet
            });
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            WriteVerbose("End!");
        }
    }

    [Cmdlet(VerbsDiagnostic.Test, "SampleCmdlet2")]
    [OutputType(typeof(FavoriteStuff))]
    public class SampleCmdlet2 : PSCmdlet
    {
        // params
        [Parameter(
    Mandatory = true,
    Position = 0,
    ValueFromPipeline = true,
    ValueFromPipelineByPropertyName = true)]
        public int FavoriteNumber { get; set; }

        [Parameter(
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        [ValidateSet("Cat", "Dog", "Horse", "Cow", "Pig", "Chicken")]
        public string FavoritePet { get; set; } = "Chicken";
        protected override void BeginProcessing() { }
        protected override void ProcessRecord()
        {
            WriteObject(new FavoriteStuff
            {
                FavoriteNumber = FavoriteNumber,
                FavoritePet = FavoritePet
            });
        }
        protected override void EndProcessing() { }
    }

    [Cmdlet(VerbsCommon.Get, "MyStuff")]
    [OutputType(typeof(MyStuff))]
    public class GetMyStuffCmdlet : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public int MyNumber { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string MyUri { get; set; }
        [Parameter(
            Position = 2,
            ValueFromPipelineByPropertyName = true)]
        public bool MyBool { get; set; }
        [Parameter(
            Position = 3,
            ValueFromPipelineByPropertyName = true)]
        public decimal MyDecimal { get; set; }
        protected override void BeginProcessing()
        {
            string hostName = base.Host.Name;
            WriteVerbose("Starting on " + hostName);
        }
        protected override void ProcessRecord()
        {
            WriteVerbose("Process");
            var web = new HtmlAgilityPack.HtmlWeb();
            var doc = web.Load(MyUri);
            WriteObject(doc);
        }
        protected override void EndProcessing()
        {
            // base.Host.UI.RawUI;  // console size, color, cursor position
            WriteVerbose("Finished!");
        }
        protected override void StopProcessing()
        {
            WriteObject("To handle abnormal termination");
        }
    }

    [Cmdlet(VerbsCommon.Get, "SnowReportScrape")]
    //[OutputType(typeof(WebPage))]
    public class SnowReportScrape : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string Type { get; set; }
        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string Url { get; set; }

        protected override void BeginProcessing()
        {
            string hostName = base.Host.Name;
            WriteVerbose("Starting query on " + Url);
            //"https://wellspan.service-now.com/now/nav/ui/classic/params/target/%24pa_dashboard.do%3Fsysparm_dashboard%3D175bcd319359da50e10ab86efaba102b%26sysparm_tab%3D631c45b59359da50e10ab86efaba1043%26sysparm_cancelable%3Dtrue%26sysparm_editable%3Dfalse%26sysparm_active_panel%3Dfalse"
            //"https://wellspan.service-now.com/%24pa_dashboard.do%3Fsysparm_dashboard%3D175bcd319359da50e10ab86efaba102b%26sysparm_tab%3D631c45b59359da50e10ab86efaba1043%26sysparm_cancelable%3Dtrue%26sysparm_editable%3Dfalse%26sysparm_active_panel%3Dfalse"
            //-Type "incidents" -URl 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b&sysparm_tab=631c45b59359da50e10ab86efaba1043&sysparm_cancelable=true&sysparm_editable=false&sysparm_active_panel=false'
            //-Type "changeRequests" -Url 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b&sysparm_tab=e02f9ca897a76ed0e17277971153af4a&sysparm_cancelable=true&sysparm_editable=false&sysparm_active_panel=false'
            //-Type "catalogTasks" -Url 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b&sysparm_tab=a1ab05f19359da50e10ab86efaba1059&sysparm_cancelable=true&sysparm_editable=undefined&sysparm_active_panel=false'

        }
        protected override void ProcessRecord()
        {
            WriteVerbose("Processing Web Page");

            // Dictionary for column names
            Dictionary<int, string> columns = new Dictionary<int, string>();
            columns.Add(1, "incidentState");
            columns.Add(2, "incidentCount");
            columns.Add(3, "pctCount");

            // Dictionary<Key1Type, Dictionary<Key2Type, ValueType>>
            Dictionary<string, Dictionary<string, string>> data = new Dictionary<string, Dictionary<string, string>>();

            // Date Stamp - Format the DateTime object to "yyyy-MM-dd"
            DateTime now = DateTime.Now;
            string dateStamp = now.ToString("yyyy-MM-dd");

            //string userName = "dsmith14@wellspan.org";
            string password = "BigBadOne2#Sand";
            var secureString = new SecureString();
            foreach (char c in password)
            {
                secureString.AppendChar(c);
            }

            // Initialize the Chrome WebDriver
            // Make sure the ChromeDriver executable is in your PATH or specify its location
            //ChromeOptions options = new ChromeOptions();
            //options.AddArguments("headless");
            //IWebDriver driver = new ChromeDriver(options);
            //IWebDriver driver = new ChromeDriver();

            // Initialize the Edge WebDrive
            EdgeOptions options = new EdgeOptions();
            options.AddArguments("--headless=new");
            IWebDriver driver = new EdgeDriver(options);   //C:\Users\dsmith14admin\.nuget\packages\selenium.webdriver\4.35.0\runtimes\win\native\selenium-manager.exe --driver msedgedriver
            try
            {

                // Try to login in to ServiceNow
                string loginUrl = "https://wellspan.service-now.com/login_locate_sso.do";
                driver.Navigate().GoToUrl(loginUrl);    //OpenQA.Selenium.
                IWebElement usernameField = driver.FindElement(By.Id("sso_selector_id"));
                usernameField.SendKeys("dsmith14@wellspan.org");
                IWebElement loginButton = driver.FindElement(By.Name("not_important"));
                loginButton.Click();
                Thread.Sleep(10000);   //Wait 10 seconds for dynamic content to load

                // Navigate to the dynamic web page
                driver.Navigate().GoToUrl(Url);    //OpenQA.Selenium.
                Thread.Sleep(10000);   //Wait 15 seconds for dynamic content to load
                                       // Optional: Wait for dynamic content to load if necessary
                                       // For example, wait for a specific element to be present
                                       //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                                       //wait.Until(driver0 => driver.FindElement(By.Id("dynamicElementId")).Displayed);

                // Get the page source after dynamic content has loaded and stroe content in a file
                string pageSource = driver.PageSource;
                //File.WriteAllText("dynamic_page.html", pageSource);

                // Find the table where ID starts with 'display-grid-table' and get the rows
                IWebElement table = driver.FindElement(By.XPath("//table[starts-with(@id, 'display-grid-table')]"));

                Console.WriteLine("Extracted Table Data.  Beginning to parse data.");
                // Get all rows in the table
                IList<IWebElement> rows = table.FindElements(By.TagName("tr"));
                // Parse all the rows to get data
                bool flag = false;
                string assignmentGroup = "";
                int rowspan = 0;
                int counter = 0;
                string state = "";
                string count = "";
                string pct = "";
                StringBuilder output = new StringBuilder();
                output.AppendLine("assignmentGroup,state,count,percent,type,dateStamp");
                foreach (IWebElement row in rows)
                {
                    counter = 1;
                    // Get all cells in the current row
                    IList<IWebElement> cells = row.FindElements(By.XPath("./th|./td"));
                    foreach (IWebElement cell in cells)
                    {
                        string cellText = cell.Text;
                        var val = cell.GetAttribute("rowspan");
                        rowspan = Convert.ToInt32(val);
                        var tagName = cell.TagName;
                        if (cellText == "Assignment group")
                        {
                            flag = true;
                            continue;
                        }
                        if (flag)
                        {
                            if (tagName == "th" && rowspan > 0)
                            {
                                assignmentGroup = cell.Text;
                                counter = 1;
                            }
                            else if (tagName == "td")
                            {
                                // Adding data
                                // Create a string with commas and store in an array (assignmentGroup,State,Count,Percent)
                                if (!data.ContainsKey(assignmentGroup))
                                {
                                    data.Add(assignmentGroup, new Dictionary<string, string>());
                                }

                                if (counter == 1) { state = cell.Text; }
                                else if (counter == 2) { count = cell.Text; count = count.Replace(",", ""); }
                                else if (counter == 3)
                                {
                                    pct = (cell.Text).Replace("%", "");
                                    data[assignmentGroup].Add(state, count);
                                    output.AppendLine($"{assignmentGroup},{state},{count},{pct},{Type},{dateStamp}");
                                    //Console.WriteLine($"{assignmentGroup},{state},{count},{pct}");
                                    WriteObject(new SnowOutput
                                    {
                                        AssignmentGroup = assignmentGroup,
                                        State = state,
                                        Count = Convert.ToInt32(count),
                                        Percent = Convert.ToDecimal(pct),
                                        Type = Type,
                                        DateStamp = dateStamp

                                    });
                                }
                                else { Console.WriteLine("counter error for table data"); }
                                //data[assignmentGroup].Add(columns[counter], cellText);

                                counter++;
                            }
                        }
                        // Process the cell data as needed - When cellText = 'Assignment Group'  then begin to store data;  
                    }
                }
                //File.WriteAllText("Output-csv.txt", output.ToString());
                /* Additional Notes
                    Find the main Macroponent, which is the first shadow host
                    IWebElement firstShadowHost = driver.FindElement(By.CssSelector("macroponent-f51912f4c700201072b211d4d8c26010"));
                    ISearchContext firstShadowRoot = firstShadowHost.GetShadowRoot();

                    IWebElement myIframe0 = driver.FindElement(By.XPath("//iframe[starts-with(@id, 'gsft_main']"));
                    driver.SwitchTo().Frame(driver.FindElement(By.Name("gsft_main")));
                    driver.SwitchTo().Frame("gsft_main");
                    driver.FindElement(By.Name("gsft_main"));
                    driver.FindElement(By.Id("gsft_main"));

                    IWebElement myIframe = driver.FindElement(By.XPath("//iframe[@title='Main Content']"));
                    driver.SwitchTo().Frame(myIframe);
                    IWebElement myTable = driver.FindElement(By.XPath("//*[starts-with(@id, 'display-grid-table']"));
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Quit the driver to close the browser instance
                driver.Quit();
                driver.Dispose();
            }

        }
        protected override void EndProcessing()
        {
            // base.Host.UI.RawUI;  // console size, color, cursor position
            WriteVerbose("Finished!");
        }
        protected override void StopProcessing()
        {
            WriteObject("To handle abnormal termination");
        }
    }

    [Cmdlet(VerbsDiagnostic.Test, "CpuStress")]
    public class CpuStress : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public int Percent { get; set; }
        public List<Thread> threads = new List<Thread>();
        protected override void BeginProcessing()
        {
            WriteVerbose("Start CPU Stress Test!");
            if (Percent < 0 || Percent > 100)
                throw new ArgumentException("Percent is not 0 to 100 it is " + Percent);
            WriteObject("Number of processors is " + Environment.ProcessorCount);
        }
        protected override void ProcessRecord()
        {
            WriteVerbose("Stressing the CPU");
            WriteObject("CPU is being stressed");

            // Get the number of logical processors (cores/threads)
            int numProcessors = Environment.ProcessorCount;
            Thread[] threads = new Thread[numProcessors];
            CancellationTokenSource[] cts = new CancellationTokenSource[numProcessors];
            // Start a thread for each logical processor
            for (int i = 0; i < numProcessors; i++)
            {
                //threads[i] = new Thread(InternalFunctions.CpuLoad(Percent));
                cts[i] = new CancellationTokenSource();
                //token = cts.Token;
                threads[i] = new Thread(() => InternalFunctions.CpuLoad(Percent, cts[i].Token));
                //{
                //    IsBackground = true; // Allow the application to exit even if threads are running
                //};
                threads[i].Start();
                Console.WriteLine($"Started thread {i + 1} of {numProcessors}");
            }

            // Wait for user to press a key to close threads
            // Thread.Sleep(10000);
            Console.WriteLine("Stressing the {0} CPUs", numProcessors);
            Console.WriteLine("Press any key to stop the stress test");
            Console.ReadKey();
            for (int i = 0; i < numProcessors; i++)
            {
                cts[i].Cancel();
            }
        }
        protected override void EndProcessing()
        {
            WriteVerbose("Finished CPU Stress Test!");
        }
        protected override void StopProcessing()
        {
            WriteObject("To handle abnormal termination");
        }
    }

    [Cmdlet(VerbsDiagnostic.Test, "MemoryStress")]
    public class MemoryStress : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public int Percent { get; set; }
        public List<Thread> threads = new List<Thread>();
        protected override void BeginProcessing()
        {
            WriteVerbose("Start Memory Stress Test!");
            if (Percent < 0 || Percent > 100)
                throw new ArgumentException("Percent is not 0 to 100 it is " + Percent);
        }
        protected override void ProcessRecord()
        {
            WriteVerbose("Stressing the Memory");
            WriteObject("Memory is being stressed");

            UInt64 totalMemoryInMBytes = 0;
            UInt64 totalVirtualMemoryInMbytes = 0;
            UInt64 freeMemoryInBytes = 0;
            UInt64 freeVirtualMemoryInBytes = 0;
            UInt64 availableMemoryInMBytes = 0;
            UInt64 usedMemoryInMBytes = 0;
            UInt64 usedVirtualMemoryInBytes = 0;

            // Total Memory
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();
            foreach (ManagementObject result in results)
            {
                totalMemoryInMBytes = (UInt64)result["TotalVisibleMemorySize"] / 1024;
                totalVirtualMemoryInMbytes = (UInt64)result["TotalVirtualMemorySize"] / 1024;
                freeMemoryInBytes = (UInt64)result["FreePhysicalMemory"] / 1024;
                freeVirtualMemoryInBytes = (UInt64)result["FreeVirtualMemory"] / 1024;
            }
            // Memory Performance Counter
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");  // dotnet add package System.Diagnostics.PerformanceCounter

            // Get the current available memory
            availableMemoryInMBytes = (UInt64)ramCounter.NextValue();

            // Calculate the used memory
            usedMemoryInMBytes = totalMemoryInMBytes - availableMemoryInMBytes;
            usedVirtualMemoryInBytes = totalVirtualMemoryInMbytes - freeMemoryInBytes;
            UInt64 totalToUse = Convert.ToUInt64(totalMemoryInMBytes * ((float)Percent / 100));
            UInt64 totalToConsume = totalToUse - usedMemoryInMBytes;


            Console.WriteLine("totalMemoryInMBytes : {0}", totalMemoryInMBytes);
            Console.WriteLine("totalVirtualMemoryInMbytes : {0}", totalVirtualMemoryInMbytes);
            Console.WriteLine("freeMemoryInBytes : {0}", freeMemoryInBytes);
            Console.WriteLine("freeVirtualMemoryInBytes : {0}", freeVirtualMemoryInBytes);
            Console.WriteLine("availableMemoryInMBytes : {0}", availableMemoryInMBytes);
            Console.WriteLine("usedMemoryInMBytes : {0}", usedMemoryInMBytes);
            Console.WriteLine("usedVirtualMemoryInBytem : {0}", usedVirtualMemoryInBytes);
            Console.WriteLine("totalToUse : {0}", totalToUse);
            Console.WriteLine("totalToConsume : {0}", totalToConsume);
            // Test the available MB until reach the desired Percent
            // pctMB = Percent of totalMemoryInMBytes
            // pctAvailableMb < availableMemoryInMBytes  continue to write untile this condition is false
            // total = used + available
            //int blockSize = 1024 * 1024; //1mb
            Console.WriteLine("Memory before : " + (Process.GetCurrentProcess().PrivateMemorySize64 / 1024) / 1024); // get value in Megabytes
            unsafe
            {
                int blockSize = 1024 * 1024 * 10; //10mb
                Int32 blocks = Convert.ToInt32(totalToConsume / 10);
                byte*[] handles = new byte*[blocks];
                try
                {
                    for (int i = 0; i < blocks; i++)  // consume 1 GB memory
                    {
                        handles[i] = (byte*)Marshal.AllocHGlobal(blockSize);
                        //write to the memory
                        for (int off = 0; off < blockSize; off++)
                            *(handles[i] + off) = 1;
                    }
                }
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine($"Out of Memory Exception caught: {ex.Message}");
                }

                finally
                {
                    //create a thread to ensure the memory continues to be accessed
                    ManualResetEvent mreStop = new ManualResetEvent(false);
                    Thread memoryThrash = new Thread(
                        () =>
                            {
                                int ihandle = 0;
                                while (!mreStop.WaitOne(0, false))
                                {
                                    for (int off = 0; off < blockSize; off++)
                                        if (*(handles[ihandle++ % handles.Length] + off) != 1)
                                            throw new InvalidOperationException();
                                }
                            }
                        );
                    memoryThrash.IsBackground = true;
                    memoryThrash.Start();

                    Console.WriteLine("Memory after  : " + (Process.GetCurrentProcess().PrivateMemorySize64 / 1024) / 1024);
                    Console.WriteLine("Finished allocating " + totalToConsume + "MB memory....Press Enter to free up.");
                    Console.ReadLine();

                    mreStop.Set();
                    memoryThrash.Join();
                }

                // Free the memory used for the stress test
                try
                {
                    for (int i = 0; i < blocks; i++)
                    {
                        Marshal.FreeHGlobal(new IntPtr(handles[i]));
                    }
                }
                finally
                {
                    Console.WriteLine("Memory at the end : " + (Process.GetCurrentProcess().PrivateMemorySize64 / 1024) / 1024);
                    Console.WriteLine("All allocated memory freed. Press Enter to quit..");
                    Console.ReadLine();
                }
            }
            Console.WriteLine("Memory Now  : " + (Process.GetCurrentProcess().PrivateMemorySize64 / 1024) / 1024);
            /*
                Console.WriteLine("availableMemoryInMBytes : {0}", ramCounter.NextValue());
                Console.WriteLine("Memory after : " + (Process.GetCurrentProcess().PrivateMemorySize64 / 1024) / 1024); // get value in Megabytes
                Console.WriteLine("Press any key to release memory and exit.");
                Console.ReadKey();

            */
            Console.WriteLine("Stressing {0} percent memory of total memory {1}", Percent, totalMemoryInMBytes);
            //Console.WriteLine("Press any key to stop the stress test");
            //Console.ReadKey();
        }
        protected override void EndProcessing()
        {
            WriteVerbose("Finished Memory Stress Test!");
        }
        protected override void StopProcessing()
        {
            WriteObject("To handle abnormal termination");
        }
    }

    [Cmdlet(VerbsDiagnostic.Test, "FillDisk")]
    [OutputType(typeof(FillDisk))]
    public class FillDisk : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public int Percent { get; set; }
        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public char Drive { get; set; }
        protected override void BeginProcessing()
        {
            WriteVerbose("Start!");

            // Check the percent is 0 to 100
            if (Percent < 0 || Percent > 100)
                throw new ArgumentException("Percent is not 0 to 100 it is " + Percent);
            WriteVerbose("Percent is valid " + Percent);

            // Check the drive is a single charcter a-z A-Z
            if (Char.IsLetter(Drive) && Drive < 128)
            {
                if ((Drive >= 'A' && Drive <= 'Z') || (Drive >= 'a' && Drive <= 'z'))
                {
                    WriteVerbose("Drive letter is valid");
                }
                else
                {
                    throw new ArgumentException("Drive letter is not a-z or A-Z " + Drive);
                }
            }
            else
            {
                throw new ArgumentException("Drive letter must be a-z or A-Z " + Drive);
            }
        }
        protected override void ProcessRecord()
        {
            WriteVerbose("Process");
            // Create Disk Fill.  Use input parameter of folder location.
            // Create temp folder (and remove when finished)
            // Get disk free space and calculate disk space to fill to input disk percent
            string driveRoot = Drive + ":\\";
            string filePath = driveRoot + "\\largefile.bin"; // Change this to your desired path

            var root = Path.GetPathRoot(Path.GetFullPath(driveRoot)) ?? throw new InvalidOperationException($"Unable to resolve drive root for: {Drive}");
            var d = new DriveInfo(root);
            if (!d.IsReady)
                throw new InvalidOperationException($"Drive '{d.Name}' is not ready.");

            string driveName = d.Name;
            long totalSize = d.TotalSize;
            long totalFree = d.TotalFreeSpace;
            long availFree = d.AvailableFreeSpace;
            long used = totalSize - totalFree;
            double pctFree = totalSize > 0 ? (double)totalFree / totalSize * 100.0 : 0.0;
            double pctUsed = 100.0 - pctFree;
            Int64 totalToFill = Convert.ToInt64((double)totalSize * (double)Percent / 100);
            Int64 totalToWrite = totalToFill - used;

            Console.WriteLine($"Drive Stats {driveName}");
            Console.WriteLine($"  Total size      : {InternalFunctions.FormatBytes(totalSize)}");
            Console.WriteLine($"  Total free      : {InternalFunctions.FormatBytes(totalFree)} ({pctFree:0.0}% free)");
            Console.WriteLine($"  Available free  : {InternalFunctions.FormatBytes(availFree)} (current user/quotas)");
            Console.WriteLine($"  Used            : {InternalFunctions.FormatBytes(used)} ({pctUsed:0.0}% used)");
            Console.WriteLine($"  Total to Fill   : {InternalFunctions.FormatBytes(totalToFill)} ({totalToFill})");
            Console.WriteLine($"  Total to Write  : {InternalFunctions.FormatBytes(totalToWrite)} ({totalToWrite})");

            if (totalToWrite <= 0)
            {
                Console.WriteLine("Disk is already filled beyond the percent specified");
            }
            else
            {
                Console.WriteLine("Begin to fill disk, large file is " + filePath);
                long fileSizeInBytes = totalToWrite;
                int bufferSize = 4096; // Write in 4KB chunks

                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] buffer = new byte[bufferSize];
                            long bytesWritten = 0;

                            while (bytesWritten < fileSizeInBytes)
                            {
                                long bytesToWrite = Math.Min(bufferSize, fileSizeInBytes - bytesWritten);
                                bw.Write(buffer, 0, (int)bytesToWrite);
                                bytesWritten += bytesToWrite;
                            }
                        }
                    }
                    Console.WriteLine($"Successfully created file: {filePath} with size {fileSizeInBytes} bytes.");
                    // Read line and remove file when task completed
                    Console.WriteLine($"Disk space filled to {Percent} %. Press Enter to remove file and quit..");
                    Console.ReadLine();
                    File.Delete(filePath);
                    Console.WriteLine($"File '{filePath}' deleted successfully.");
                }
                catch (IOException ex)
                {
                    //Console.WriteLine($"Error writing to disk: {ex.Message}");
                    throw new IOException($"Error writing to disk: {ex.Message}");
                }
                catch (UnauthorizedAccessException ex)
                {
                    //Console.WriteLine($"Permission error: {ex.Message}");
                    throw new UnauthorizedAccessException($"Permission error: {ex.Message}");
                    //throw new InvalidOperationException($"Drive '{d.Name}' is not ready.");

                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    throw new Exception($"An unexpected error occurred: {ex.Message}");
                }
                finally
                {
                    // Remove the file filePath
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        Console.WriteLine($"File '{filePath}' deleted successfully.");
                    }
                    else
                    {
                        //Console.WriteLine($"File '{filePath}' does not exist.");
                    }
                }

            }

        }
        protected override void EndProcessing()
        {
            WriteVerbose("Finish!");
        }
        protected override void StopProcessing()
        {
            WriteObject("To handle abnormal termination");
        }
    }

    public class InternalFunctions
    {
        public static void CpuLoad(int pct, CancellationToken ct)
        {
            //int pct = 80;
            Parallel.For(0, 1, new Action<int>((int i) =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                while (!ct.IsCancellationRequested)
                {
                    if (watch.ElapsedMilliseconds > pct)
                    {
                        Thread.Sleep(100 - pct);
                        watch.Reset();
                        watch.Start();
                    }
                }
                Console.WriteLine("Finished CPU Stress");
            }));
        }

        public static string FormatBytes(long bytes)
        {
            string[] units = { "B", "KB", "MB", "GB", "TB" };
            double val = bytes;
            int idx = 0;
            while (val >= 1024 && idx < units.Length - 1)
            {
                val /= 1024;
                idx++;
            }
            return $"{val:0.##} {units[idx]}";
        }

    }
    public class MyStuff
    {
        public int MyNumber { get; set; }
        public string MyUri { get; set; }
        public bool MyBool { get; set; }
        public decimal MyDecimal { get; set; }
    }
    public class FavoriteStuff
    {
        public int FavoriteNumber { get; set; }
        public string FavoritePet { get; set; }
    }
    public class SnowOutput
    {
        public string AssignmentGroup { get; set; }
        public string State { get; set; }
        public int Count { get; set; }
        public decimal Percent { get; set; }
        public string Type { get; set; }
        public string DateStamp { get; set; }
    }
    public class WebPage
    {
        public string WebPageHtml { get; set; }
    }
}
