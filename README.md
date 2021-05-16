# C# 多執行緒程式設計範例程式碼

|專案名稱|專案說明|備註|
|-|-|-|
|MT001|練習情境 使用單一執行緒同步執行遞增與遞減||
|MT002|練習情境 使用多執行緒非同步執行遞增與遞減||
|MT003|練習情境 修正每次遞增與遞減要鎖定而變慢問題_使用Interlocked||
|MT004|練習情境 修正每次遞增與遞減要鎖定而變慢問題_使用區域變數且最後才同步||
|MT005|範例體驗 單一執行緒與多處理器關係驗證練習||
|MT006|練習情境 使用執行緒集區完成 MT004 需求||
|MT007|練習情境 使用 TPL 工作 Task 完成 MT004 需求||
|MT008|範例體驗 為何 Console.Write 輸出結果不會錯亂||
|MT009|練習情境 多執行緒執行遞增與遞減 - 使用 lock||
|MT010|範例體驗 多執行緒執行遞增與遞減 - 共用靜態、執行個體、區域變數測試比較||
|MT011|範例體驗 指定單核心與多核心，何者比較快||
|MT012|範例體驗 執行緒每次僅能夠執行時間配量TimeSlice||
|MT013|範例體驗 當WebClient完成後,會用何種執行緒為?||
|MT014|範例體驗 觀察執行緒集區對於IO執行緒使用情況||
|MT015|範例體驗 執行緒集區的使用與歸還||
|MT016|範例體驗 多個 HTTP 要求Request測試 for NET Core||
|MT017|範例體驗 多個 HTTP 要求Request測試 for NET Framework||
|MT018|範例體驗 量測處理程序的 CPU 使用效率||
|MT019|範例體驗 在 async 方法內沒有使用 await 效能分析||
|MT020|範例體驗 靜態欄位與多執行緒||
|MT021|範例體驗 ThreadStatic 建立執行緒區域儲存區||
|MT022|範例體驗 具名的資料插槽 (data slots)||
|MT023|範例體驗 匿名的資料插槽 (data slots)||
|MT024|範例體驗 ThreadLocal<T> 執行緒區域儲存區||
|MT025|練習情境 建立 Thread物件 - .NET 1.0 作法||
|MT026|練習情境 建立 Thread物件 - .NET 2.0 作法 (匿名函式)||
|MT027|練習情境 建立 Thread物件 - .NET 3.0 作法 (Lambda函式)||
|MT028|範例體驗 不同優先權的執行緒||
|MT029|練習情境 啟動 Thread，使用前請與背景設定值||
|MT030|範例體驗 封鎖等候執行緒完成 - Thread.Join||
|MT031|範例體驗 封鎖等候執行緒完成 - AutoResetEvent||
|MT032|範例體驗 封鎖等候執行緒完成 - WaitHandle||
|MT033|範例體驗 無封鎖等候執行緒完成 - 回報事件方式||
|MT034|範例體驗 停止執行緒執行 - 強制結束 Abort||
|MT035|範例體驗 停止執行緒執行 - 輪詢 Polling ||
|MT036|範例體驗 停止執行緒執行 - 取消來源權杖||
|MT037|範例體驗 執行緒發生例外異常||
|MT038|範例體驗 ASP.NET 同步內容 運作機制||
|MT039|範例體驗 WPF 同步內容 運作機制||
|MT040|範例體驗 Windows Forms 同步內容 運作機制||
|MT041|練習情境 ASP.NET MVC與同步內容的體驗||
|MT042|練習情境 靜態與區域變數的存取速度測試||
|MT043|範例體驗 Stack Overflow 造成的原因||
|MT044|範例體驗 Memory Overflow 造成的原因||
|MT045|Web API 同步與非同步呼叫||
|MT046|觀察要求超過執行緒集區建立的執行緒最小數目||
|MT047|使用BackgroundWorker||
|MT048|在 GUI 開發框架下使用BackgroundWorker||
|MT049|體驗多執行緒打死結||
|MT050|體驗多工作打死結||
|MT051|解除死結 方案1 – 使用相同順序來進行巢狀鎖定||
|MT052|解除死結 方案2 – 使用定時器做到逾時解除鎖定||
|MT053|巢狀lock||
|MT054|獨佔鎖定 Monitor||
|MT055|Monitor 鎖定物件的狀態有所變更 - 使用訊號通知||
|MT056|獨佔鎖定 Exclusive locking - lock 陳述式||
|MT057|Monitor PulseAll , Pulse 的差異||
|MT058|獨佔鎖定SpinLock||
|MT059|獨佔鎖定Mutex||
|MT060|用Mutex限制只有一個處理程序可以執行||
|MT061|用Mutex進行跨處理程序的執行緒同步||
|MT062|信號AutoResetEvent 為何僅有一個執行序可以正常執行，不是四個執行緒一起執行||
|MT063|信號AutoResetEvent 四個執行緒交替執行||
|MT064|信號ManualResetEvent||
|MT065|信號Barrier||
|MT066|信號Semaphore||
|MT067|信號CountdownEvent||
|MT068|信號SemaphoreSlim||
|MT069|非獨佔鎖定Interlocked||
|MT070|非獨佔鎖定ReaderWriterLockSlim||
|MT071|多執行緒與單一處理器核心||
|MT072|確認了解 ThreadPool 的內部資訊的狀態||
|MT073|修改 MT009 ，使用 SpinLock 來計算||
|MT074|Lock 與 SpinLock 的差異，觀察系統狀態值||
|MT075|同步處理的效能測試||
|MT076|體驗執行緒集區的參數調整意義||
|MT077|ThreadStatic與ThreadLocal 預設值||
|MT078|體驗執行緒集區的 Worker / IOCP Thread||
|MT079|了解執行緒集區的執行緒使用情況||
|MT080|了解執行緒集區的執行緒使用情況Console||
|MT081|當執行緒集區要服務的數量底大最大上限||
|MT082|當WebClient完成後,會用何種執行緒為 用 Task||
|MT083|當HttpClient完成後,會用何種執行緒為||
|MT084|各種非同步工作的建立與啟動方法||
|MT085|建立和起始有參數非同步工作||
|MT086|利用TAP工作建立大量並行工作練習||
|MT087|了解各種委派類型工作物件的狀態列舉值變化情形||
|MT088|了解各種承諾類型工作物件的狀態列舉值變化情形||
|MT089|檢查要求失敗的工作||
|MT090|各種不同取得工作執行結果的用法||
|MT091|等候非同步工作完成範例||
|MT092|Task.WaitAll，等待所有工作 Task 物件完成執行||
|MT093|Task.WaitAny，等候任一工作 Task 物件完成執行||
|MT094|Task.Run 與 Task.Factory.StartNew 差異(非同步委派)||
|MT095|Task.WhenAll，等候所有工作 Task 物件完成執行||
|MT096|Task.WhenAny，等候任一工作 Task 物件完成執行||
|MT097|同時呼叫多個WebAPI並取得計算結果練習-WhenAll||
|MT098|同時呼叫多個WebAPI並取得計算結果練習-WhenAny||
|MT099|使用 ContinueWith 方法，來接續不同工作狀態並執行相關處理||
|MT100|CancellationTokenSource 與 CancellationToken 的設計練習||
|MT101|使用 CancellationTokenSource 取消 HttpClient 網路存取練習||
|MT102|CreateLinkedTokenSource 來組合兩個 CancellationToken||
|MT103|Console用HttpClient從下載大檔案，並更新下載進度||
|MT104|WPF用HttpClient從下載大檔案，並更新下載進度||
|MT105|||
|MT106|||
|MT107|||
|MT108|||
|MT109|||
|MT110|||
|MT111|||
|MT112|||
|MT113|||
|MT114|||
|MT115|||
|MT116|||
|MT117|||
|MT118|||
|MT119|||
|UnderstandThreadPool|執行緒集區的應用展示||
||||
||||
||||
||||
||||
|-|-|-|
||||


