# ConcurrencyCheckTest

**測試 EF Core ConcurrencyCheck**
這個專案是為了測試 Entity Framework Core 中的 ConcurrencyCheck 功能而建立的。
ConcurrencyCheck 可以幫助我們確保在多個用戶同時修改同一個資料時，不會發生資料不一致的情況。


***步驟***

1.安裝相依套件：確保你已經安裝了相關的套件，包括 Entity Framework Core 和測試相關的套件。

2.建立資料庫：使用 EF Core 的 Code First 方法，建立一個包含 ConcurrencyCheck 欄位的資料庫表。

3.編寫測試：建立測試用例，測試在同時修改資料時，ConcurrencyCheck 是否能正確地檢測到並處理衝突。

4.執行測試：執行測試，確保 ConcurrencyCheck 的功能正常運作。

5.修正錯誤：如果測試失敗，根據錯誤訊息修正相應的程式碼，直到測試通過。