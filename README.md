## 首先要確認的是要引用的 dll 是 managed 或是 unmanaged。

- managed

  > 指的是給CLR托管 有記憶體垃圾回收C#,VB,C++/CLI 屬於這類。

- unmanaged

  > 非托管的代碼則是要自己得要處理記憶體問題，
  > 傳統的windows api或者COM介面或者是其他人寫的native C++都是這種形式的。
  > (稱native只是為了和C++/CLI有所區隔，可以想成是又快又猛又易翻車的C++)

依你要的需求，大概是使用C#來呼叫unmanaged code相去不遠了。

這種用C#和原生C++摻在一塊做牛丸的混合編程，

其中最直接相關庫的就是 `System.Runtime.InteropServices`

這東西搞起來非常的麻煩，不僅要知道native c++的api形式，

還要熟悉C++與C#的用法規則，不然很容易出錯也不好debug。



## 在此我先弄一個範例：

https://github.com/lightyen/WpfMessageBox

其中建立了一個WPF專案，設計一個按鈕，

在點選了按鈕之後會去呼叫user32.dll中的MessageBox來顯示一個對話框。



如果你有一些工具軟體去查看user32.dll，(例如DLL Export Viewer)

你會發現user32.dll存在兩個與MessageBox有關的Function，

一個是A結尾一個是W結尾，A指的是ASCII，W指的是寬字元。

不過我們已經把CharSet設成Auto了，.NET平台會自動地去選擇合適的。

(比較重要的點是在C#中，string全部都是寬字元的，

要和unmanaged code互通時要多留意)



## 好了，現在我們可以來實作牛丸了

首先新增一個*.cs 然宣告一個static class來當我們的介面參考，

然後再宣告一個static extern方法來描述MessageBox這個函式，

把函式名稱、參數形式、返回值依照API文件規範依樣畫葫蘆。

IntPtr在C++中代表的就是指標，而IntPtr.Zero則代表NULL，

HWND是一個視窗的handle，也是一個整數。

(其實指標在記憶體中也不過是一個整數罷了。)



(user32.dll不需要填絕對路徑，因為windows已經內定註冊了，

第三方的library應該還是要填詳細的路徑。)



LPCTSTR -> LP-C-T-STR ->一個tchar的唯讀字串，在這裡用一個string代表。

使用了這個方法後，平台會隱隱地偷偷地把string的內容先放到unmanaged空間，

然後再做參數傳給MessageBox。

假若今天要傳的是一個物件(void*)，

則我們必須先從unmanaged要一塊記憶體空間，然後填上相應資料，

在透過IntPtr寫入(或讀出)，最後用完後還要記得回收記憶體，

整個過程真的蝦雞巴麻煩的。

參考：System.Runtime.InteropServices.Marshal

AllocHGlobal,FreeHGlobal,SizeOf


## 最後設計的部分

許多c++的函式都用一個整數代表功能的體現，

在C#中我們可以來為這些功能(flag)寫成一個enum，加些註解，

或者把一些資料包裝成一個class，再加上一些額外的功能，

使得在調用這些unmanaged code能更加舒服一些。

## 更多的範例
https://github.com/lightyen/COMInterop

## 更多的關鍵字
pinvoke, Interop, DllImport, Marshal,

IntPtr, UnmanagedType, StructLayout, MarshalAs



